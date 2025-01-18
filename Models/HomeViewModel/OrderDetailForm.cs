using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CappyPop_Full_HTML.Models.HomeViewModel
{
     public class OrderDetailForm
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
    }
    public class PaymentViewModel{
        public string CustomerName{get;set;}
        public string Phone{get;set;}
        public string Address{get;set;}
    }
}