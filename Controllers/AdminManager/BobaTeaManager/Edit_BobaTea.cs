using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using CappyPop_Full_HTML.Models.ManagerAdmin;
using CappyPop_Full_HTML.Models.Tables;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
namespace CappyPop_Full_HTML.Controllers.AdminManager.BobaTeaManager;

[Route("admin")]
public class Edit_BobaTea : Controller
{
    private readonly ILogger<Edit_BobaTea> _logger;

    public Edit_BobaTea(ILogger<Edit_BobaTea> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpGet("editbobatea/{id}")]
    public IActionResult Editbobatea(int id)
    {
        using (var db = new bobateashopContext())
        {
            // Fetch the boba and related data
            var boba = db.Bobateas
                .Include(v => v.IceBobateas)
                    .ThenInclude(av => av.Ice)
                .Include(v => v.SugarBobateas)
                    .ThenInclude(mv => mv.Sugar)
                .Include(v => v.ToppingBobateas)
                    .ThenInclude(cv => cv.Topping)
                .Include(v => v.SizeBobateas)
                    .ThenInclude(v => v.Size)
                .FirstOrDefault(v => v.BobaTeaId == id);

            if (boba == null)
            {
                return NotFound(); // Return 404 if no boba is found
            }
            var images = db.ImageUrls
                        .Where(img => img.BobaTeaId == boba.BobaTeaId)
                        .ToList();
            // Map boba data to the view model
            var viewModel = new BobaViewModel
            {
                SelectedBoba = new Boba
                {
                    BobaId = boba.BobaTeaId,
                    BobaName = boba.Name,
                    BobaDescription = boba.Description,
                    Price = boba.Price,
                    BobaQuantity = boba.Quantity ?? 0,

                },
                AllIces = db.Ices
                    .Select(a => new CappyPop_Full_HTML.Models.ManagerAdmin.Ice { Id = a.IceId, Name = a.IceLevel })
                    .ToList(),
                SelectedIceIds = boba.IceBobateas
                    .Select(av => av.IceId)
                    .Where(id => id.HasValue)   // Filter out null values
                    .Select(id => id.Value)    // Cast to non-nullable int
                    .ToList(),
                AllSugars = db.Sugars
                    .Select(m => new CappyPop_Full_HTML.Models.ManagerAdmin.Sugar { Id = m.SugarId, Name = m.SugarLevel })
                    .ToList(),
                SelectedSugarIds = boba.SugarBobateas
                    .Select(mv => mv.SugarId)
                    .Where(id => id.HasValue)   // Filter out null values
                    .Select(id => id.Value)    // Cast to non-nullable int
                    .ToList(),
                AllTopping = db.Toppings
                    .Select(c => new CappyPop_Full_HTML.Models.ManagerAdmin.Topping { Id = c.ToppingId, Name = c.Name })
                    .ToList(),
                SelectedToppingIds = boba.ToppingBobateas
                    .Select(cv => cv.ToppingId)
                    .Where(id => id.HasValue)   // Filter out null values
                    .Select(id => id.Value)    // Cast to non-nullable int
                    .ToList(),
                AllSizes = db.Sizes
                    .Select(s => new CappyPop_Full_HTML.Models.ManagerAdmin.Size { Id = s.SizeId, Name = s.SizeName })
                    .ToList(),
                SelectedSizeIds = boba.SizeBobateas
                    .Select(cv => cv.SizeId)
                    .Where(id => id.HasValue)   // Filter out null values
                    .Select(id => id.Value)    // Cast to non-nullable int
                    .ToList(),
                AllImages = images.Select(img => new ImageUrl { ImageId = img.ImageId, Url = img.Url }).ToList(),
                SelectedImageId = images.Any() ? images.First().ImageId : 0 // Use the primary image if available
            };

            return View("~/Views/Admin/BobateaManager/EditBobaTea.cshtml", viewModel);
        }
    }

    [HttpPost("editbobatea/{id}")]
    public IActionResult Editbobatea(EditBobaModel model, int id)
    {
        if (ModelState.IsValid)
        {
            using (var db = new bobateashopContext())
            {
                // Fetch the boba and related data
                var boba = db.Bobateas
                    .Include(v => v.ImageUrls)
                    .Include(v => v.IceBobateas)
                    .Include(v => v.SizeBobateas)
                    .Include(v => v.SugarBobateas)
                    .Include(v => v.ToppingBobateas)
                    .FirstOrDefault(v => v.BobaTeaId == id);

                if (boba == null)
                {
                    return NotFound(); // Return 404 if boba not found
                }

                // Update the boba details
                boba.Name = model.SelectedBoba.BobaName;
                boba.Price = model.SelectedBoba.Price;
                boba.Quantity = model.SelectedBoba.BobaQuantity;
                boba.Description = model.SelectedBoba.BobaDescription;

                // Update icebobas
                boba.IceBobateas.Clear();
                foreach (var iceId in model.SelectedIceIds)
                {
                    var iceBobatea = new IceBobatea
                    {
                        BobaTeaId = boba.BobaTeaId,
                        IceId = iceId
                    };
                    boba.IceBobateas.Add(iceBobatea);
                }

                // Update Categoriesbobas
                boba.SugarBobateas.Clear();
                foreach (var sugarId in model.SelectedSugarIds)
                {
                    var sugarbobatea = new SugarBobatea
                    {
                        BobaTeaId = boba.BobaTeaId,
                        SugarId = sugarId
                    };
                    boba.SugarBobateas.Add(sugarbobatea);
                }

                // Update Moodbobas
                boba.SizeBobateas.Clear();
                foreach (var sizeId in model.SelectedSizeIds)
                {
                    var sizebobatea = new SizeBobatea
                    {
                        BobaTeaId = boba.BobaTeaId,
                        SizeId = sizeId
                    };
                    boba.SizeBobateas.Add(sizebobatea);
                }

                // Add the image URL (if provided)
                 if (!string.IsNullOrEmpty(model.SelectedImageUrls))
                {
                    var imageUrls = model.SelectedImageUrls.Split(','); // Assuming the URLs are separated by commas
                    bool isFirstImage = true;

                    foreach (var url in imageUrls)
                    {
                        var imageUrl = new ImageUrl
                        {
                            BobaTeaId = boba.BobaTeaId, // Link to the saved Product
                            Url = url,
                            IsPrimary = isFirstImage
                        };

                        db.ImageUrls.Add(imageUrl);

                        // Only the first image should be marked as primary
                        isFirstImage = false;
                    }
                }

                db.SaveChanges(); // Save changes to the database
            }

            return RedirectToAction("Editbobatea", new { id = id });
        }
        else
        {
            // If validation fails, log errors and return the view with the validation message
            foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
            {
                Console.WriteLine(error.ErrorMessage);
            }

            return View("~/Views/Admin/bobateamanager/editbobatea.cshtml"); // Return the model with validation errors if the form is not valid
        }
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View("Error!");
    }
}
