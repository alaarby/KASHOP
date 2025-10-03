using KASHOP.DAL.DTOs.Requests;
using KASHOP.DAL.DTOs.Responses;
using KASHOP.DAL.Entities;
using KASHOP.DAL.Repositories.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASHOP.BLL.Services.Interfaces
{
    public interface IBrandService : IGenericService<BrandRequest, BrandResponse, Brand>
    {
        Task<int> CreateFile(BrandRequest request);
    }
}
