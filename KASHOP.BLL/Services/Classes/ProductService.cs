using KASHOP.BLL.Services.Interfaces;
using KASHOP.DAL.DTOs.Requests;
using KASHOP.DAL.DTOs.responses;
using KASHOP.DAL.Entities;
using KASHOP.DAL.Repositories.Interfaces;
using Mapster;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASHOP.BLL.Services.Classes
{
    public class ProductService : GenericService<ProductRequest, ProductResponse, Product>, IProductService
    {
        private readonly IProductRepository _repository;
        private readonly IFileService _fileService;

        public ProductService(IProductRepository repository, IFileService fileService) : base(repository)
        {
            _repository = repository;
            _fileService = fileService;
        }

        public async Task<int> CreateProduct(ProductRequest request)
        {
            var entity = request.Adapt<Product>();
            entity.CreatedAt = DateTime.UtcNow;

            if(request.MainImage != null)
            {
                var imagePath = await _fileService.UploadAsync(request.MainImage);
                entity.MainImage = imagePath;
            }
            if(request.SubImages != null)
            {
                var subImagesPaths = await _fileService.UploadManyAsync(request.SubImages);
                entity.SubImages = subImagesPaths.Select(i => new ProductImage {  ImageName = i}).ToList();
            }

            return _repository.Add(entity);
        }
        public async Task<List<ProductResponse>> GetAllProductsAsync(HttpRequest request,bool onliyActive = false, int pageNumber = 1, int pageSize = 1)
        {
            var products = _repository.GetAllProductsWithImage();

            if (onliyActive)
            {
                products = products.Where(p => p.Status == DAL.Enums.Status.Active).ToList();
            }

            var pagedProducts = products.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            return pagedProducts.Select(p => new ProductResponse
            {
                Id = p.Id,
                Name = p.Name,
                Quantity = p.Quantity,
                MainImageUrl = $"{request.Scheme}://{request.Host}/Images/{p.MainImage}",
                SubImagesUrls = p.SubImages.Select(i => $"{request.Scheme}://{request.Host}/Images/{i.ImageName}").ToList(),
                Reviews = p.Reviews.Select(r => new ReviewResponse
                {
                    Id = r.Id,
                    Rate = r.Rate,
                    Comment = r.Comment,
                    FullName = r.User.FullName
                }).ToList(),
            }).ToList();    
        }
    }
}
