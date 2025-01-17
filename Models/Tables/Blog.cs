using System;
using System.Collections.Generic;

namespace CappyPop_Full_HTML.Models.Tables
{
    public partial class Blog
    {
        public int BlogId { get; set; }
        public string Title { get; set; } = null!;
        public string Content { get; set; } = null!;
        public int? AuthorId { get; set; }
        public DateTime PublishedDate { get; set; }

        public virtual Adminseller? Author { get; set; }
    }
}
