using System;
using System.Collections.Generic;

namespace CappyPop_Full_HTML.Models.Tables
{
    public partial class Sugar
    {
        public Sugar()
        {
            SugarBobateas = new HashSet<SugarBobatea>();
        }

        public int SugarId { get; set; }
        public string SugarLevel { get; set; } = null!;

        public virtual ICollection<SugarBobatea> SugarBobateas { get; set; }
    }
}
