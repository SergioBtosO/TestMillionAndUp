using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MillionAndUp.Core.Aplication.Interfaces;
using MillionAndUp.Core.Aplication.Wrappers;
using MillionAndUp.Core.Application.DTOs.Users;
using MillionAndUp.Core.Application.Enums;
using MillionAndUp.Core.Application.Exceptions;
using MillionAndUp.Core.Application.Interfaces;
using MillionAndUp.Core.Domain.Settings;
using MillionAndUp.Infraestructure.Identity.Helpers;
using MillionAndUp.Infraestructure.Identity.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MillionAndUp.Infraestructure.Identity.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly JWTSettings _jwtSettings;
        private readonly IDateTimeService _dateTimeService;
        public AccountService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IOptions<JWTSettings> jwtSettings, IDateTimeService dateTimeService, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _jwtSettings = jwtSettings.Value;
            _dateTimeService = dateTimeService;
            _signInManager = signInManager;
        }

        public async Task<Response<AuthenticationResponse>> AuthenticateAsync(AuthenticationRequest request, string ipAddress)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                throw new ApiException($"Email {request.Email} not registered.");
            }

            var result = await _signInManager.PasswordSignInAsync(user.UserName, request.Password, false, lockoutOnFailure: false);
            if (!result.Succeeded)
            {
                throw new ApiException($"Credentials no valid.");
            }

            JwtSecurityToken jwtSecurity = await GenerateJWToken(user);

            AuthenticationResponse response = new AuthenticationResponse();
            response.Id = user.Id;
            response.JWToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurity);
            response.Email = user.Email;
            response.UserName = user.UserName;
            response.Name = user.Name;
            response.LastName = user.LastName;

            var rolesList = await _userManager.GetRolesAsync(user).ConfigureAwait(false);
            response.Roles = rolesList.ToList();
            response.IsVerified = user.EmailConfirmed;

            var refreshToken = GenerateRefreshToken(ipAddress);
            response.RefreshToken = refreshToken.Token;

            return new Response<AuthenticationResponse>(response, $"User {user.UserName} authenticated.");

        }

        public async Task<Response<string>> RegisterAsync(RegisterRequest request, string origin)
        {
            var userWithEqualUserName = await _userManager.FindByNameAsync(request.UserName);
            if(userWithEqualUserName != null)
            {
                throw new ApiException($"UserName {request.UserName} exists. ");
            }            
            var userwithEqualEmail = await _userManager.FindByEmailAsync(request.Email);
            if (userwithEqualEmail != null)
            {
                throw new ApiException($"Email {request.Email} exists.");
            }
            var user = new ApplicationUser
            {
                Email = request.Email,
                Name = request.Name,
                LastName = request.LastName,
                UserName = request.UserName
            };
            var result = await _userManager.CreateAsync(user, request.Password);
            if (!result.Succeeded)
            {
                throw new ApiException($"{result.Errors}.");
            }

            await _userManager.AddToRoleAsync(user, Roles.Basic.ToString());
            return new Response<string>(user.Id, message: $"User registered: {request.UserName}");

        }

        private async Task<JwtSecurityToken> GenerateJWToken(ApplicationUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);
            var rolesClaims = new List<Claim>();

            for (int i = 0; i < roles.Count; i++)
            {
                rolesClaims.Add(new Claim("roles", roles[i]));
            }

            string ipAddress = IpHelper.GetIpAddress();

            var claims = new[]
            {
                new Claim (JwtRegisteredClaimNames.Sub,user.UserName),
                new Claim (JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                new Claim (JwtRegisteredClaimNames.Email,user.Email),
                new Claim ("uid",user.Id),
                new Claim ("ip",ipAddress)
            }
            .Union(userClaims)
            .Union(rolesClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer:_jwtSettings.Issuer,
                audience:_jwtSettings.Audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(_jwtSettings.DurationInMinutes),
                signingCredentials:signingCredentials
                );

            return jwtSecurityToken;
        }
        private RefreshToken GenerateRefreshToken(string ipAddress)
        {
            return new RefreshToken
            {
                Token = RandomTokenString(),
                Expires = DateTime.Now.AddDays(7),
                Created = DateTime.Now,
                CreatedByIp = ipAddress
            };
        }

        private string RandomTokenString()
        {
            using var rngCryptoServiceProvider = new RNGCryptoServiceProvider();
            var randomBytes = new byte[40];
            rngCryptoServiceProvider.GetBytes(randomBytes);
            return BitConverter.ToString(randomBytes).Replace("-", "");
        }
    }
}
