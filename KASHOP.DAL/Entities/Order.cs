using KASHOP.DAL.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASHOP.DAL.Entities
{
    public class Order
    {
        //order
        public int Id { get; set; }
        public OrderStatus Status { get; set; } = OrderStatus.Pending;
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public DateTime ShippedDate { get; set; } 
        public decimal TotalAmount { get; set; }

        //payment
        public PaymnetMethodEnum PaymnetMethod { get; set; } 
        public string? PaymentId { get; set; }

        //carrier
        public string? CarrierName { get; set; }
        public string? TrackingNumber { get; set; }

        //user
        public ApplicationUser User { get; set; }
        public string? UserId { get; set; }
        public List<OrderItem> Items { get; set; } = new List<OrderItem>();
    }
}
