using KASHOP.DAL.DTOs.Requests;
using KASHOP.DAL.DTOs.responses;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASHOP.BLL.Services.Interfaces
{
    public interface IAuthenticationService
    {
        Task<UserResponse> LoginAsync(LoginRequest loginRequest);
        Task<UserResponse> RegisterAsync(RegisterRequest registerRequest, HttpRequest httpRequest);
        Task<string> ConfirmEmail(string token, string UserId);
        Task<bool> ForgotPassword(ForgotPasswordRequest request);
        Task<bool> ResetPssword(ResetPasswordRequest request);
    }
}
