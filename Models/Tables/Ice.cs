using System;
using System.Collections.Generic;

namespace CappyPop_Full_HTML.Models.Tables
{
    public partial class Ice
    {
        public Ice()
        {
            IceBobateas = new HashSet<IceBobatea>();
        }

        public int IceId { get; set; }
        public string IceLevel { get; set; } = null!;

        public virtual ICollection<IceBobatea> IceBobateas { get; set; }
    }
}
