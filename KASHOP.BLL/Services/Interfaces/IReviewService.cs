using KASHOP.DAL.DTOs.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASHOP.BLL.Services.Interfaces
{
    public interface IReviewService
    {
        Task<bool> AddReviewAsync(ReviewRequest request, string userId);
    }
}
