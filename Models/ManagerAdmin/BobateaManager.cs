using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using CappyPop_Full_HTML.Models.Tables;

namespace CappyPop_Full_HTML.Models.ManagerAdmin
{
    public partial class BobaViewModel
    {
        [Required]
        public Boba SelectedBoba { get; set; }
        public List<Size> AllSizes { get; set; }
        [Required]
        public List<int> SelectedSizeIds { get; set; }
        public List<Sugar> AllSugars { get; set; } // All Sugars for dropdown

        [Required]
        public List<int> SelectedSugarIds { get; set; } // IDs of associated Sugars

        public List<Ice> AllIces { get; set; }

        [Required]
        public List<int> SelectedIceIds { get; set; }

        public List<Topping> AllTopping { get; set; } // All Sugars for dropdown

        [Required]
        public List<int> SelectedToppingIds { get; set; }

        [Required]
        public int SelectedBrandId { get; set; }

        public List<ImageUrl> AllImages { get; set; }
        public int SelectedImageId { get; set; }
        public List<string> UploadedImageUrls { get; set; }
        public string ImageUrlsString { get; set; }
        public string? SelectedImageUrls { get; set; }
    }
    public class AdminDashboardViewModel
    {
        public List<ListBobaTeaViewModel> BobaTeas { get; set; }
        public int TotalOrders { get; set; }
        public int TotalRevenue { get; set; }
    }
    public partial class AddBobaModel
    {
        [Required]
        public Boba SelectedBoba { get; set; }

        [Required]
        public List<int> SelectedIceIds { get; set; } // IDs of associated artists

        [Required]
        public List<int> SelectedSugarIds { get; set; }

        [Required]
        public List<int> SelectedToppingIds { get; set; }

        [Required]
        public List<int> SelectedSizeIds { get; set; }
        public string? SelectedImageUrls { get; set; }
    }
    public partial class EditBobaModel
    {
        [Required]
        public Boba SelectedBoba { get; set; }

        [Required]
        public List<int> SelectedIceIds { get; set; } // IDs of associated artists

        [Required]
        public List<int> SelectedSugarIds { get; set; }

        [Required]
        public List<int> SelectedToppingIds { get; set; }

        [Required]
        public List<int> SelectedSizeIds { get; set; }
        public string? SelectedImageUrls { get; set; }
    }
    public partial class ListBobaTeaViewModel
    {
        public int? BobaId;
        public string BobaName;
        public string BobaDescription;
        public int? BobatQuantity;
        public int Price;
        public string PrimaryImage { get; set; }
    }
    public class Image
    {
        public int Id { get; set; }
        public int? BobaTeaId { get; set; }
        public string ImageUrl { get; set; }
        public bool? IsPrimary { get; set; }
        public ListBobaTeaViewModel Bobatea { get; set; } // Quan hệ ngược
    }
    public class Boba
    {
        public int BobaId { get; set; }
        public string? BobaName { get; set; }
        public string BobaDescription { get; set; }
        public int BobaQuantity { get; set; }
        public decimal Price { get; set; }
    }
    public class Sugar
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class Ice
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class Topping
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class Size
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}

