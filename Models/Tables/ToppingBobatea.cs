using System;
using System.Collections.Generic;

namespace CappyPop_Full_HTML.Models.Tables
{
    public partial class ToppingBobatea
    {
        public int Id { get; set; }
        public int? BobaTeaId { get; set; }
        public int? ToppingId { get; set; }

        public virtual Bobatea? BobaTea { get; set; }
        public virtual Topping? Topping { get; set; }
    }
}
