using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASHOP.DAL.DTOs.Requests
{
    public class BrandRequest
    {
        public string Name { get; set; }
        public IFormFile MainImage { get; set; }
    }
}
