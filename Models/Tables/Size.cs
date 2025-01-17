using System;
using System.Collections.Generic;

namespace CappyPop_Full_HTML.Models.Tables
{
    public partial class Size
    {
        public Size()
        {
            SizeBobateas = new HashSet<SizeBobatea>();
        }

        public int SizeId { get; set; }
        public string SizeName { get; set; } = null!;

        public virtual ICollection<SizeBobatea> SizeBobateas { get; set; }
    }
}
