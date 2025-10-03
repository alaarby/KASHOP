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
    public interface ICheckOutService
    {
        Task<CheckOutResponse> ProcessPaymentAsync(CheckOutRequest request, string UserId, HttpRequest httpRequest);
        Task<bool> HandlePaymentSuccessAsync(int OrderId); 
    }
}
