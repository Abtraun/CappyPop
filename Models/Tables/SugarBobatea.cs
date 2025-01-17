using System;
using System.Collections.Generic;

namespace CappyPop_Full_HTML.Models.Tables
{
    public partial class SugarBobatea
    {
        public int Id { get; set; }
        public int? BobaTeaId { get; set; }
        public int? SugarId { get; set; }

        public virtual Bobatea? BobaTea { get; set; }
        public virtual Sugar? Sugar { get; set; }
    }
}
