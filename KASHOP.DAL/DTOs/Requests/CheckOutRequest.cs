using KASHOP.DAL.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace KASHOP.DAL.DTOs.Requests
{
    public class CheckOutRequest
    {
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public PaymnetMethodEnum PaymentMethod { get; set; }
    }
}
