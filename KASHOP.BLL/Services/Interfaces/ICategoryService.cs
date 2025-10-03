using KASHOP.BLL.Services.Interfaces;
using KASHOP.DAL.DTOs.Requests;
using KASHOP.DAL.DTOs.Responses;
using KASHOP.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASHOP.BLL.Interfaces
{
    public interface ICategoryService : IGenericService<CategoryRequest, CategoryResponse, Category>
    {
    }
}

