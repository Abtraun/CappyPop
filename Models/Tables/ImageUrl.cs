using System;
using System.Collections.Generic;

namespace CappyPop_Full_HTML.Models.Tables
{
    public partial class ImageUrl
    {
        public int ImageId { get; set; }
        public int? BobaTeaId { get; set; }
        public string Url { get; set; } = null!;
        public bool? IsPrimary { get; set; }

        public virtual Bobatea? BobaTea { get; set; }
    }
}
