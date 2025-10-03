using KASHOP.BLL.Services.Interfaces;
using KASHOP.DAL.DTOs.Requests;
using KASHOP.DAL.DTOs.responses;
using KASHOP.DAL.Entities;
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
using Microsoft.AspNetCore.Identity.UI.Services;
using  KASHOP.DAL.DTOs.Requests;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Http;

namespace KASHOP.BLL.Services.Classes
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly IEmailSender _emailSender;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AuthenticationService(UserManager<ApplicationUser> userManager,
            IConfiguration configuration,
            IEmailSender emailSender,
            SignInManager<ApplicationUser> signInManager
            )
        {
            _userManager = userManager;
            _configuration = configuration;
            _emailSender = emailSender;
            _signInManager = signInManager;
        }
        public async Task<UserResponse> LoginAsync(LoginRequest loginRequest)
        {
             var user = await _userManager.FindByEmailAsync(loginRequest.Email);
            if (user == null) 
            {
                throw new Exception("Invalid Email or Password");
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginRequest.Password, true);

            if (result.Succeeded) 
            {
                return new UserResponse()
                {
                    Token = await CreateTokenAsync(user)
                };
            }
            else if (result.IsLockedOut)
            {
                throw new Exception("your account is locked");
            }
            else if (result.IsNotAllowed)
            {
                throw new Exception("Please confirm your Email!");
            }
            else
            {
                throw new Exception("Invalid Email or Password");
            }           
        }
        public async Task<UserResponse> RegisterAsync(RegisterRequest registerRequest, HttpRequest httpRequest)
        {
            var user = new ApplicationUser()
            {
                FullName = registerRequest.FullName,
                Email = registerRequest.Email,
                UserName = registerRequest.UserName,
                PhoneNumber = registerRequest.PhoneNumber
            };

            var result = await _userManager.CreateAsync(user, registerRequest.Password);
            if (result.Succeeded) 
            {
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var escapeToken = Uri.EscapeDataString(token);
                var emailUrl = $"{httpRequest.Scheme}://{httpRequest.Host}/api/identity/Account/ConfirmEmail?token={escapeToken}&userId={user.Id}";

                await _userManager.AddToRoleAsync(user, "Customer");

                await _emailSender.SendEmailAsync(user.Email, "wellcome", $"<h1>Hello {user.UserName}</h1>" +
                    $"<a href='{emailUrl}' >confirm email</a>");
                return new UserResponse()
                {
                    Token = registerRequest.Email
                };
            }
            else
            {
                throw new Exception($"{result.Errors}");
            }
        }
        public async Task<string> ConfirmEmail(string token, string UserId)
        {
            var user = await _userManager.FindByIdAsync(UserId);
            if(user is null)
            {
                throw new Exception("user not found");
            }
            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
            {
                return "email confirmed succesfully";
            }
            return "email confirmation failed";
        }
        private async Task<string> CreateTokenAsync(ApplicationUser user)
        {
            var Claims = new List<Claim>()
            {
                new Claim("Email", user.Email),
                new Claim("Name", user.UserName),
                new Claim("Id", user.Id)
            };

            var Roles = await _userManager.GetRolesAsync(user);
            foreach (var role in Roles) 
            {
                Claims.Add(new Claim("Role", role));
            }

            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("jwtOptions")["SecretKey"]));

            var credentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                    claims: Claims,
                    expires: DateTime.Now.AddDays(15),
                    signingCredentials: credentials
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        public async Task<bool> ForgotPassword(ForgotPasswordRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user is null) throw new Exception("user not found");

            var random = new Random();
            var code = random.Next(1000, 9999).ToString();
            user.CodeResetPassword = code;
            user.PasswordResetCodeExpierty = DateTime.UtcNow.AddMinutes(15);

            await _userManager.UpdateAsync(user);

            await _emailSender.SendEmailAsync(request.Email, "reset password", $"<p>code is {code}</p>");

            return true;
        }
        public async Task<bool> ResetPssword(ResetPasswordRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user is null) throw new Exception("user not found");

            if (user.CodeResetPassword != request.Code) return false;

            if (user.PasswordResetCodeExpierty < DateTime.UtcNow) return false;

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            var result = await _userManager.ResetPasswordAsync(user, token, request.NewPassword);

            if (result.Succeeded)
            {
                await _emailSender.SendEmailAsync(request.Email, "Reset Password", "<h1>your password reset successfully</h1>");          
            }
            return true;
        }
    }
}
