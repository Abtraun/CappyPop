using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using CappyPop_Full_HTML.Models.ManagerAdmin;
using CappyPop_Full_HTML.Models.Tables;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
namespace CappyPop.Controllers.AdminManager.BobaTeaManager;

[Route("admin")]
public class Add_BobaTea : Controller
{
    private readonly ILogger<Add_BobaTea> _logger;

    public Add_BobaTea(ILogger<Add_BobaTea> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpGet("addbobatea")]
    public IActionResult AddBobatea()
    {
        using (var db = new bobateashopContext())
        {
            var allIces = db.Ices
                .Select(m => new CappyPop_Full_HTML.Models.ManagerAdmin.Ice
                {
                    Id = m.IceId,
                    Name = m.IceLevel
                })
                .ToList();

            var allSugars = db.Sugars
                .Select(m => new CappyPop_Full_HTML.Models.ManagerAdmin.Sugar
                {
                    Id = m.SugarId,
                    Name = m.SugarLevel
                })
                .ToList();

            var allTopings = db.Toppings
                           .Select(m => new CappyPop_Full_HTML.Models.ManagerAdmin.Topping
                           {
                               Id = m.ToppingId,
                               Name = m.Name
                           })
                           .ToList();

            var allSizes = db.Sizes
                           .Select(m => new CappyPop_Full_HTML.Models.ManagerAdmin.Size
                           {
                               Id = m.SizeId,
                               Name = m.SizeName
                           })
                           .ToList();


            // Create a new instance of VinylViewModel to pass to the view
            var viewModel = new BobaViewModel
            {
                AllIces = allIces,
                AllSizes = allSizes,
                AllTopping = allTopings,
                AllSugars = allSugars,
                SelectedBoba = new CappyPop_Full_HTML.Models.ManagerAdmin.Boba(), // Initialize an empty product for the form
                SelectedIceIds = new List<int>(),
                SelectedSizeIds = new List<int>(),
                SelectedToppingIds = new List<int>(),
                SelectedSugarIds = new List<int>(),
            };

            return View("~/Views/Admin/BobateaManager/AddBoBaTea.cshtml", viewModel);
        }
    }

    [HttpPost("addbobatea")]
    public IActionResult AddBobatea(AddBobaModel model)
    {
        if (ModelState.IsValid)
        {
            using (var db = new bobateashopContext())
            {
                // Create a new Product entry
                var newBobaTea = new CappyPop_Full_HTML.Models.Tables.Bobatea
                {
                    Name = model.SelectedBoba.BobaName,
                    Description = model.SelectedBoba.BobaDescription,
                    Price = model.SelectedBoba.Price,
                    Quantity = model.SelectedBoba.BobaQuantity,
                };

                db.Bobateas.Add(newBobaTea);
                db.SaveChanges(); // Save Product first to generate ProductId

                // Add the associated artists
                foreach (var iceId in model.SelectedIceIds)
                {
                    var icebobateas = new IceBobatea
                    {
                        BobaTea = newBobaTea,
                        IceId = iceId
                    };
                    newBobaTea.IceBobateas.Add(icebobateas);
                }
                foreach (var sugarId in model.SelectedSugarIds)
                {
                    var sugarbobateas = new SugarBobatea
                    {
                        BobaTea = newBobaTea,
                        SugarId = sugarId
                    };
                    newBobaTea.SugarBobateas.Add(sugarbobateas);
                }
                foreach (var sizeId in model.SelectedSizeIds)
                {
                    var sizebobateas = new SizeBobatea
                    {
                        BobaTea = newBobaTea,
                        SizeId = sizeId
                    };
                    newBobaTea.SizeBobateas.Add(sizebobateas);
                }
                foreach (var toppingId in model.SelectedToppingIds)
                {
                    var toppingbobateas = new ToppingBobatea
                    {
                        BobaTea = newBobaTea,
                        ToppingId = toppingId
                    };
                    newBobaTea.ToppingBobateas.Add(toppingbobateas);
                }

                // Add the associated images
                if (!string.IsNullOrEmpty(model.SelectedImageUrls))
                {
                    var imageUrls = model.SelectedImageUrls.Split(','); // Assuming the URLs are separated by commas
                    bool isFirstImage = true;

                    foreach (var url in imageUrls)
                    {
                        var imageUrl = new ImageUrl
                        {
                            BobaTeaId = newBobaTea.BobaTeaId, // Link to the saved Product
                            Url = url,
                            IsPrimary = isFirstImage
                        };

                        db.ImageUrls.Add(imageUrl);

                        // Only the first image should be marked as primary
                        isFirstImage = false;
                    }
                }

                db.SaveChanges(); // Save all changes to the database
            }

            return Redirect("/admin/Bobashow");
        }
        else
        {
            // If the form is invalid, log the errors and return the view with validation messages
            foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
            {
                Console.WriteLine(error.ErrorMessage);
            }

            return View("~/Views/Admin/bobateamanager/addbobatea.cshtml", model);
        }
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View("Error!");
    }
}
