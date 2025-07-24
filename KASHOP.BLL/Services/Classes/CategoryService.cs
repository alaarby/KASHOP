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

namespace KASHOP.BLL.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public int Create(CategoryRequest request)
        {
            var category = request.Adapt<Category>();

            return _categoryRepository.Add(category);
        }

        public int DeleteCategory(int id)
        {
            var category = _categoryRepository.GetById(id);
            if (category == null)
            {
                return 0;
            }
            return _categoryRepository.Remove(category);
        }

        public IEnumerable<CategoryResponse> GetAll()
        {
            var categories = _categoryRepository.GetAll();

            return categories.Adapt<IEnumerable<CategoryResponse>>();
        }

        public CategoryResponse? GetById(int id)
        {
            var category = _categoryRepository.GetById(id);

            if (category == null)
            {
                return null;
            }
            return category.Adapt<CategoryResponse>();
        }

        public int UpdateCategory(int id, CategoryRequest request)
        {
            var category = _categoryRepository.GetById(id);
            if (category == null) return 0;

            category.Name = request.Name;

            return _categoryRepository.Update(category);

        }

        public bool Toggle(int id)
        {
            var category = _categoryRepository.GetById(id);
            if (category == null) return false;

            category.Status = category.Status == Status.Active ? Status.Inactive : Status.Active;

            _categoryRepository.Update(category);
            return true;
        }
    }
}
