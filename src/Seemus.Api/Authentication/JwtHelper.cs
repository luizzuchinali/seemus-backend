using Microsoft.IdentityModel.Tokens;
using Seemus.Domain.Entities;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Seemus.Api.Authentication
{
    public static class JwtHelper
    {
        public static string GenerateToken(User user, string configKey, string issuer, string audience,
            DateTime expiration, IList<Claim> moreClaims = null)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sid, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
            };

            foreach (var role in user.UserRoles)
                claims.Add(new Claim(ClaimTypes.Role, role.Role.Name));

            if (moreClaims != null)
                claims.AddRange(moreClaims);


            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: expiration,
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
