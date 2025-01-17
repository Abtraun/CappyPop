using System;
using System.Collections.Generic;

namespace CappyPop_Full_HTML.Models.Tables
{
    public partial class Bobatea
    {
        public Bobatea()
        {
            IceBobateas = new HashSet<IceBobatea>();
            ImageUrls = new HashSet<ImageUrl>();
            OrderDetails = new HashSet<OrderDetail>();
            SizeBobateas = new HashSet<SizeBobatea>();
            SugarBobateas = new HashSet<SugarBobatea>();
            ToppingBobateas = new HashSet<ToppingBobatea>();
        }

        public int BobaTeaId { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int? Quantity { get; set; }
        public int? AdminSeller { get; set; }

        public virtual Adminseller? AdminSellerNavigation { get; set; }
        public virtual ICollection<IceBobatea> IceBobateas { get; set; }
        public virtual ICollection<ImageUrl> ImageUrls { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
        public virtual ICollection<SizeBobatea> SizeBobateas { get; set; }
        public virtual ICollection<SugarBobatea> SugarBobateas { get; set; }
        public virtual ICollection<ToppingBobatea> ToppingBobateas { get; set; }
    }
}
