using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASHOP.DAL.Entities
{
    public class Brand : BaseModel
    {
        public string Name { get; set; }
        public string MainImage { get; set; }
        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
