using System;
using System.Collections.Generic;

namespace CappyPop_Full_HTML.Models.Tables
{
    public partial class IceBobatea
    {
        public int Id { get; set; }
        public int? BobaTeaId { get; set; }
        public int? IceId { get; set; }

        public virtual Bobatea? BobaTea { get; set; }
        public virtual Ice? Ice { get; set; }
    }
}
