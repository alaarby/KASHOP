using KASHOP.DAL.DTOs.Requests;
using KASHOP.DAL.DTOs.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASHOP.BLL.Interfaces
{
    public interface ICategoryService
    {
        int Create(CategoryRequest request);
        IEnumerable<CategoryResponse> GetAll();
        CategoryResponse? GetById(int id);
        int UpdateCategory(int id, CategoryRequest request);
        int DeleteCategory(int id);
        public bool Toggle(int id);

    }
}

