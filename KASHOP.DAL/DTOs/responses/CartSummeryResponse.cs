using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASHOP.DAL.DTOs.responses
{
    public class CartSummeryResponse
    {
        public ICollection<CartResponse> Items { get; set; } = new List<CartResponse>();
        public decimal CartTotal => Items.Sum(i => i.TotalPrice);
    }
}
