using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using CappyPop_Full_HTML.Models.ManagerAdmin;

namespace CappyPop_Full_HTML.Models.HomeViewModel
{
    public class ViewBoBaTea
    {
        public List<BobaTea> BobaTeas { get; set; }
        public BobaTea SelectedBobaTea { get; set; }
        public List<Ice> AllIces { get; set; }
        public List<Sugar> AllSugars { get; set; }
        public List<Topping> AllToppings { get; set; }
        public List<Size> AllSizes { get; set; }
        public List<string> SelectedIceLevels { get; set; }
        public List<string> SelectedSugarLevels { get; set; }
        public List<string> SelectedToppingNames { get; set; }
        public List<string> SelectedSizeNames { get; set; }
        public List<int> SelectedIceIds { get; set; } // Thêm thuộc tính này
        public List<int> SelectedSugarIds { get; set; } // Thêm thuộc tính này
        public List<int> SelectedToppingIds { get; set; } // Thêm thuộc tính này
        public List<string> SelectedSizeIds { get; set; } // Added property for selected eras
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalProducts { get; set; }
        public int TotalPages => (int)Math.Ceiling((double)TotalProducts / PageSize);
    }

    public class BobaTea
    {
        public int BobaId { get; set; }
        public string BobaName { get; set; }
        public string BobaImage { get; set; }
        public string BobaDescription { get; set; }
        public decimal Price { get; set; }
        public string Tracklist { get; set; }
        public string Status { get; set; } // For preorder or other statuses
        public int Years { get; set; }
        public string IceLevels { get; set; }
        public string SugarLevels { get; set; }
        public string SizeNames { get; set; }
        public string ToppingNames { get; set; }
        public int? ProductQuantity { get; set; }
        public List<string> ImageUrls { get; set; }
        public string? PrimaryImageUrl { get; set; }
    }
}