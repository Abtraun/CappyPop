using System;
using System.Collections.Generic;

namespace CappyPop_Full_HTML.Models.Tables
{
    public partial class Adminseller
    {
        public Adminseller()
        {
            Blogs = new HashSet<Blog>();
            Bobateas = new HashSet<Bobatea>();
            Orders = new HashSet<Order>();
        }

        public int AdminSellerId { get; set; }
        public string Name { get; set; } = null!;
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public int? AccountId { get; set; }

        public virtual Account? Account { get; set; }
        public virtual ICollection<Blog> Blogs { get; set; }
        public virtual ICollection<Bobatea> Bobateas { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
