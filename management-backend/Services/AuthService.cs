using AccountManager.API.Data;
using AccountManager.API.DTOs;
using AccountManager.API.Models;
using AccountManager.API.Helpers;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace AccountManager.API.Services
{
    public class AuthService
    {
        private readonly ApplicationDbContext _context;
        private readonly JwtHelper _jwtHelper;

        public AuthService(ApplicationDbContext context, JwtHelper jwtHelper)
        {
            _context = context;
            _jwtHelper = jwtHelper;
        }

        public string Register(RegisterDto dto)
        {
            if (_context.Users.Any(x => x.Email == dto.Email))
                return "User already exists";

            var user = new User
            {
                Name = dto.Name,
                Email = dto.Email,
                PasswordHash = HashPassword(dto.Password)
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            return "User registered successfully";
        }

        public string Login(LoginDto dto)
        {
            var user = _context.Users.FirstOrDefault(x => x.Email == dto.Email);

            if (user == null || user.PasswordHash != HashPassword(dto.Password))
                return null;

            return _jwtHelper.GenerateToken(user.Id, user.Email);
        }

        private string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(password);
            var hash = sha256.ComputeHash(bytes);

            return Convert.ToBase64String(hash);
        }
    }
}