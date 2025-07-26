using Azure.Core;
using Azure;
using KASHOP.BLL.Interfaces;
using KASHOP.DAL.DTOs.Requests;
using KASHOP.DAL.DTOs.Responses;
using KASHOP.DAL.Entities;
using KASHOP.DAL.Enums;
using KASHOP.DAL.Repositories.Interfaces;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KASHOP.BLL.Services.Classes;

namespace KASHOP.BLL.Services
{
    public class CategoryService : GenericService<CategoryRequest, CategoryResponse, Category>, ICategoryService
    {
        public CategoryService(ICategoryRepository repository) : base(repository) { }
        
        
    }
}
