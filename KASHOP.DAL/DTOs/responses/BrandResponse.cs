using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace KASHOP.DAL.DTOs.Responses
{
    public class BrandResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [JsonIgnore]
        public string MainImage { get; set; }
        public string MainImageUrl => $"https://localhost:7230/images/{MainImage}";
    }
}
