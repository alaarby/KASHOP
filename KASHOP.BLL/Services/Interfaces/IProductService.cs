using KASHOP.DAL.DTOs.Requests;
using KASHOP.DAL.DTOs.responses;
using KASHOP.DAL.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASHOP.BLL.Services.Interfaces
{
    public interface IProductService : IGenericService<ProductRequest, ProductResponse, Product>
    {
        Task<int> CreateProduct(ProductRequest request);
        Task<List<ProductResponse>> GetAllProductsAsync(HttpRequest request, bool onliyActive = false, int pageNumber = 1, int pageSize = 1);
    }
}
