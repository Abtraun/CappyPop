using System;
using System.Collections.Generic;

namespace CappyPop_Full_HTML.Models.Tables
{
    public partial class Topping
    {
        public Topping()
        {
            ToppingBobateas = new HashSet<ToppingBobatea>();
        }

        public int ToppingId { get; set; }
        public string Name { get; set; } = null!;
        public decimal Price { get; set; }

        public virtual ICollection<ToppingBobatea> ToppingBobateas { get; set; }
    }
}
