using EvolveDb;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;
using MySqlConnector;
using RestWithASP_NET8Udemy.Business;
using RestWithASP_NET8Udemy.Business.Implementations;
using RestWithASP_NET8Udemy.Configurations;
using RestWithASP_NET8Udemy.Hypermedia.Enricher;
using RestWithASP_NET8Udemy.Hypermedia.Filters;
using RestWithASP_NET8Udemy.Model.Context;
using RestWithASP_NET8Udemy.Repository;
using RestWithASP_NET8Udemy.Repository.Generic;
using RestWithASP_NET8Udemy.Services;
using RestWithASP_NET8Udemy.Services.Implementations;
using Serilog;
using System.Text;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var appName = "REST API's From 0 to Azure with ASP.NET Core 5 and Docker";
        var appVersion = "v1";
        var appDescription = $"API RESTful developed in course '{appName}'";

        builder.Services.AddRouting(options => options.LowercaseUrls = true);

        var tokenConfigurations = new TokenConfiguration();

        new ConfigureFromConfigurationOptions<TokenConfiguration>(builder.Configuration.GetSection("TokenConfigurations")).Configure(tokenConfigurations);

        builder.Services.AddSingleton(tokenConfigurations);

        builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = tokenConfigurations.Issuer,
                ValidAudience = tokenConfigurations.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenConfigurations.Secret))
            };
        });

        builder.Services.AddAuthorization(auth =>
        {
            auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
            .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
            .RequireAuthenticatedUser().Build());
        });

        builder.Services.AddCors(options => options.AddDefaultPolicy(builder =>
        {
            builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
        }
        ));

        builder.Services.AddControllers();
        
        var connection = builder.Configuration["MySQLConnection:MySQLConnectionString"];
        builder.Services.AddDbContext<MySQLContext>(options => options.UseMySql(connection, new MySqlServerVersion(new Version(8, 2))));

        if (builder.Environment.IsDevelopment())
        {
            MigrateDatabase(connection);
        }

        builder.Services.AddMvc(options =>
        {
            options.RespectBrowserAcceptHeader = true;
            options.FormatterMappings.SetMediaTypeMappingForFormat("xml", MediaTypeHeaderValue.Parse("applications/xml"));
            options.FormatterMappings.SetMediaTypeMappingForFormat("json", MediaTypeHeaderValue.Parse("applications/json"));
        })
        .AddXmlSerializerFormatters();

        var filterOptions = new HyperMediaFilterOptions();
        filterOptions.ContentResponseEnricherList.Add(new PersonEnricher());
        filterOptions.ContentResponseEnricherList.Add(new BookEnricher());
        
        builder.Services.AddSingleton(filterOptions);

        // Versioning API
        builder.Services.AddApiVersioning();

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc(appVersion,
                new OpenApiInfo
                {
                    Title = appName,
                    Version = appVersion,
                    Description = appDescription,
                    Contact = new OpenApiContact
                    {
                        Name = "Marlise Borba",
                        Url = new Uri("https://github.com/marliseborba")
                    }
                });

        });

        // Dependency Injection
        builder.Services.AddScoped<IPersonBusiness, PersonBusinessImplementation>();
        builder.Services.AddScoped<IBookBusiness, BookBusinessImplementation>();
        builder.Services.AddScoped<ILoginBusiness, LoginBusinessImplementation>();

        builder.Services.AddTransient<ITokenService, TokenService>();

        builder.Services.AddScoped<IUserRepository, UserRepository>();
        builder.Services.AddScoped<IPersonRepository, PersonRepository>();

        builder.Services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
     
        var app = builder.Build();

        app.UseHttpsRedirection();

        app.UseCors();

        app.UseSwagger();
        app.UseSwaggerUI(c => 
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", $"{appName} - {appVersion}");
        });

        var option = new RewriteOptions();
        option.AddRedirect("^$", "swagger");
        app.UseRewriter(option);

        app.UseAuthorization();

        app.MapControllers();
        app.MapControllerRoute("DefaultApi", "{controller=values}/v{version=apiVersion}/{id?}");

        app.Run();

        void MigrateDatabase(string connection)
        {
            try
            {
                var evolveConnection = new MySqlConnection(connection);
                var evolve = new Evolve(evolveConnection, Log.Information)
                {
                    Locations = new List<string> { "db/migrations", "db/dataset" },
                    IsEraseDisabled = true,
                };
                evolve.Migrate();

            }
            catch (Exception ex)
            {
                Log.Error("Database migraton failed", ex);
                throw;
            }
        }
    }
}