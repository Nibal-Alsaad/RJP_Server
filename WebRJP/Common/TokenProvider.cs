using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace WebRJP.Common
{
    public class TokenProvider
    {
        IConfiguration _configuration;
        public TokenProvider(IConfiguration configuration)
        {
            _configuration = configuration;
        }   
        public string createToken()
        {
            var sekretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Tokens:Key"]));
            var signinCredentials = new SigningCredentials(sekretKey, SecurityAlgorithms.HmacSha256);
            var tokeOptions = new JwtSecurityToken(
                issuer: _configuration["Tokens:Issuer"],
                audience: _configuration["Tokens:Audience"],
                claims: new List<Claim>(),
            //    expires: DateTime.Now.AddMinutes(5),
                signingCredentials: signinCredentials);
            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
            return tokenString;
        }
    }
}
