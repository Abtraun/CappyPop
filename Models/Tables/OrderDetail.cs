using System;
using System.Collections.Generic;

namespace CappyPop_Full_HTML.Models.Tables
{
    public partial class OrderDetail
    {
        public int OrderDetailId { get; set; }
        public int? OrderId { get; set; }
        public int? BobaTeaId { get; set; }
        public int Quantity { get; set; }

        public virtual Bobatea? BobaTea { get; set; }
        public virtual Order? Order { get; set; }
    }
}
