using RestWithASP_NET8Udemy.Data.VO;
using RestWithASP_NET8Udemy.Model;
using RestWithASP_NET8Udemy.Model.Context;
using System.Security.Cryptography;
using System.Text;

namespace RestWithASP_NET8Udemy.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly MySQLContext _context;

        public UserRepository(MySQLContext context)
        {
            _context = context;
        }

        public User ValidadeCredentials(UserVO user)
        {
            var pass = ComputeHash(user.Password, SHA256.Create());
            return _context.Users.FirstOrDefault(u => (u.UserName == user.Username) && u.Password == pass);
        }

        private object ComputeHash(string input, HashAlgorithm hashAlgorithm)
        {
            Byte[] inputBytes = Encoding.UTF8.GetBytes(input);
            Byte[] hashedBytes = hashAlgorithm.ComputeHash(inputBytes);
            
            var builder = new StringBuilder();

            foreach ( var item in hashedBytes )
            {
                builder.Append(item.ToString("x2"));
            }
            return builder.ToString();
        }
    }
}
