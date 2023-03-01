
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Domain.IService;
using Domain.Models;

namespace EC.Service.Service
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;
        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string GenerateToken(Users user)
        {
           
            var claims = new List<Claim>()
                {
                //new Claim(ClaimTypes.Name,user.Name),
                //new Claim(ClaimTypes.NameIdentifier,user.Password),
               
                new Claim("Id",Convert.ToString(user.Id)),
                //new Claim(ClaimTypes.Role,user.Role),
                };
          
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["tokenValidation:key"]));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var tokendisc = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = cred,
                Issuer = _configuration["tokenValidation:issuer"],
            };
            var tokenhandler = new JwtSecurityTokenHandler();
            var token = tokenhandler.CreateToken(tokendisc);

            return tokenhandler.WriteToken(token);
        }
    }
}
