using System;
using System.Collections.Generic;

namespace CappyPop_Full_HTML.Models.Tables
{
    public partial class SizeBobatea
    {
        public int Id { get; set; }
        public int? BobaTeaId { get; set; }
        public int? SizeId { get; set; }

        public virtual Bobatea? BobaTea { get; set; }
        public virtual Size? Size { get; set; }
    }
}
