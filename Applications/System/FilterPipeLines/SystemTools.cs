using System;
using System.Collections;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;
using RESTAPIRNSQLServer.DTOs.SystemDTOs;

namespace RESTAPIRNSQLServer.Applications.System.FilterPipeLines
{
    public class SystemTools
    {
        public static List<Claim> ClaimIdentityFromToken(FilterContext context)
        {
            var decodeToken = context.HttpContext.User.Identity as ClaimsIdentity;
            return decodeToken.Claims.ToList();
        }
        public static UserWithToken GenerateToken(UserWithToken infoUser, JWTSettings settings)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(settings.SecretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]{
                    new Claim(ClaimTypes.Email, infoUser.Email),
                    new Claim("role", infoUser.Role.Value.ToString()),
                    new Claim("code", infoUser.Code)
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature
                )
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            infoUser.Token = tokenHandler.WriteToken(token);

            return infoUser;
        }
    }
}