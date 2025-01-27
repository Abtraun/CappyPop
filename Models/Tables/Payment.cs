﻿using System;
using System.Collections.Generic;

namespace CappyPop_Full_HTML.Models.Tables
{
    public partial class Payment
    {
        public Payment()
        {
            Orders = new HashSet<Order>();
        }

        public int PaymentId { get; set; }
        public string Method { get; set; } = null!;

        public virtual ICollection<Order> Orders { get; set; }
    }
}
