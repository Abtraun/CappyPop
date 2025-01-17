using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CappyPop_Full_HTML.Models.ManagerAdmin
{
    public class AdminDashboardViewModel
    {
        public List<ListBobaTeaViewModel> BobaTeas { get; set; }
        public int TotalOrders { get; set; }
        public int TotalRevenue { get; set; }
    }

    public partial class ListBobaTeaViewModel
    {
        public int? BobaId;
        public string BobaName;
        public string Description;
        public int? BobatQuantity;
        public int Price;
        public string PrimaryImage { get; set; }
    }
    public class Image
    {
        public int ImageId { get; set; }
        public int? BobaTeaId { get; set; }
        public string ImageUrl { get; set; }
        public bool? IsPrimary { get; set; }
        public ListBobaTeaViewModel Bobatea { get; set; } // Quan hệ ngược
    }

}
