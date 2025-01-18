using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CappyPop_Full_HTML.Models.HomeViewModel
{
    public class OrderForm
    {
        [JsonPropertyName("totalPayment")]
        public decimal TotalPayment { get; set; }

        public List<OrderDetailForm> Cart { get; set; }
    }
}