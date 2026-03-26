using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace AccountManager.API.Helpers
{
    public class JwtHelper
    {
        private readonly string _key;

        public JwtHelper(string key)
        {
            if (string.IsNullOrWhiteSpace(key) || Encoding.UTF8.GetByteCount(key) < 32)
            {
                throw new ArgumentException("JWT key must be at least 32 bytes (256 bits) for HS256.", nameof(key));
            }

            _key = key;
        }

        public string GenerateToken(int userId, string email)
        {
            var claims = new[]
            {
                new Claim("id", userId.ToString()),
                new Claim(ClaimTypes.Email, email)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddHours(2),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}