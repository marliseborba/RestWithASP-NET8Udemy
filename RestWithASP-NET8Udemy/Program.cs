using EvolveDb;
using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;
using MySqlConnector;
using RestWithASP_NET8Udemy.Business;
using RestWithASP_NET8Udemy.Business.Implementations;
using RestWithASP_NET8Udemy.Hypermedia.Enricher;
using RestWithASP_NET8Udemy.Hypermedia.Filters;
using RestWithASP_NET8Udemy.Model.Context;
using RestWithASP_NET8Udemy.Repository;
using RestWithASP_NET8Udemy.Repository.Generic;
using Serilog;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

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

        // Dependency Injection
        builder.Services.AddScoped<IPersonBusiness, PersonBusinessImplementation>();
        builder.Services.AddScoped<IBookBusiness, BookBusinessImplementation>();

        builder.Services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

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