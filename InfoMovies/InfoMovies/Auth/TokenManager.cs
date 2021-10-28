﻿using InfoMovies.Auth.Model;
using InfoMovies.Data.Dtos.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace InfoMovies.Auth
{
    public interface ITokenManager
    {
        Task<string> CreateAccessTokenAsync(InfoMoviesUser user);
    }

    public class TokenManager : ITokenManager
    {
        private readonly SymmetricSecurityKey _authSigningKey;
        private readonly UserManager<InfoMoviesUser> _userManager;
        public TokenManager(IConfiguration configuration, UserManager<InfoMoviesUser> userManager)
        {
            _authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]));
            _userManager = userManager;
        }

        public async Task<string> CreateAccessTokenAsync(InfoMoviesUser user)
        {
            var userRoles = await _userManager.GetRolesAsync(user);
            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(CustomClaims.UserId, user.Id.ToString())
            };
            authClaims.AddRange(userRoles.Select(userRole => new Claim(ClaimTypes.Role, userRole)));

            var accessSecurityToken = new JwtSecurityToken(
                expires: DateTime.UtcNow.AddHours(1),
                claims: authClaims,
                signingCredentials: new SigningCredentials(_authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return new JwtSecurityTokenHandler().WriteToken(accessSecurityToken);
        }
    }
}