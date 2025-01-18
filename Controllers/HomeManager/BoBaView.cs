using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using CappyPop_Full_HTML.Models.HomeViewModel;
using CappyPop_Full_HTML.Models.Tables;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CappyPop.Controllers.HomeManager;

[Route("home")]
public class BoBaView : Controller
{
    private readonly ILogger<BoBaView> _logger;

    public BoBaView(ILogger<BoBaView> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }
    [HttpGet("Boba")]
    public IActionResult BobaView(int page = 1, int pageSize = 8) // Set default pageSize to 8
    {
        using (var db = new bobateashopContext())
        {
            var totalProducts = (from p in db.Bobateas
                                 select p).Count();

            var products = (from p in db.Bobateas
                            join img in db.ImageUrls.Where(img => (bool)img.IsPrimary) on p.BobaTeaId equals img.BobaTeaId

                            select new CappyPop_Full_HTML.Models.HomeViewModel.BobaTea
                            {
                                BobaId = p.BobaTeaId,
                                BobaName = p.Name,
                                BobaImage = img.Url,
                                BobaDescription = p.Description,
                                Price = p.Price,

                            })
                            .Skip((page - 1) * pageSize)
                            .Take(pageSize)
                            .ToList();

            var bobateaViewModel = new CappyPop_Full_HTML.Models.HomeViewModel.ViewBoBaTea

            {
                BobaTeas = products,
                CurrentPage = page,
                PageSize = pageSize,
                TotalProducts = totalProducts
            };

            ViewBag.Message = TempData["Message"];
            return View("~/Views/Home/Boba.cshtml", bobateaViewModel);
        }
    }

    [HttpGet("BobaTeaDetail/{id}")]
    public IActionResult BobaTeaDetails(int id)
    {
        using (var db = new bobateashopContext())
        {
            var bobatea = db.Bobateas
                .Include(v => v.IceBobateas)
                    .ThenInclude(av => av.Ice)
                .Include(v => v.SugarBobateas)
                    .ThenInclude(mv => mv.Sugar)
                .Include(v => v.ToppingBobateas)
                    .ThenInclude(cv => cv.Topping)
                .Include(v => v.SizeBobateas)
                    .ThenInclude(cv => cv.Size)
                .FirstOrDefault(v => v.BobaTeaId == id);

            if (bobatea == null)
            {
                return NotFound(); // Return 404 if no vinyl is found
            }

            var Bobateas = db.Bobateas
                .Where(p => p.BobaTeaId != id) // Exclude the selected product
                .Select(p => new CappyPop_Full_HTML.Models.HomeViewModel.BobaTea // Map each product to the ViewModel
                {
                    BobaId = p.BobaTeaId,
                    BobaName = p.Name,
                    Price = p.Price,
                    PrimaryImageUrl = db.ImageUrls
                        .Where(i => i.BobaTeaId == p.BobaTeaId && i.IsPrimary == true)
                        .Select(i => i.Url)
                        .FirstOrDefault() // Lấy URL của ảnh chính
                }).ToList();

            var imageUrls = db.ImageUrls
                .Where(i => i.BobaTeaId == bobatea.BobaTeaId)
                .ToList(); // Lấy danh sách đối tượng ImageUrl

            var primaryImageUrl = imageUrls
                .FirstOrDefault(i => i.IsPrimary == true)?.Url; // Lấy URL ảnh chính

            var allImageUrls = imageUrls
                .Select(i => i.Url)
                .ToList();

            var toppingDetails = bobatea.ToppingBobateas
             .Select(cv => new
             {
                 Name = cv.Topping.Name,
                 Price = (int)cv.Topping.Price
             }).ToList();

            var viewModel = new ViewBoBaTea
            {
                SelectedBobaTea = new CappyPop_Full_HTML.Models.HomeViewModel.BobaTea
                {
                    BobaId = bobatea.BobaTeaId,
                    BobaName = bobatea.Name,
                    BobaDescription = bobatea.Description,
                    Price = (int)bobatea.Price,
                    ProductQuantity = bobatea.Quantity ?? 0,
                    PrimaryImageUrl = primaryImageUrl,
                    ImageUrls = allImageUrls
                },
                SelectedIceLevels = bobatea.IceBobateas
                    .Select(av => av.Ice.IceLevel)
                    .ToList(),
                SelectedSizeNames = bobatea.SizeBobateas
                    .Select(mv => mv.Size.SizeName)
                    .ToList(),
                SelectedToppingNames = toppingDetails
                    .Select(t => t.Name)
                    .ToList(),
                SelectedToppingPrices = toppingDetails
                    .ToDictionary(t => t.Name, t => t.Price),
                SelectedSugarLevels = bobatea.SugarBobateas
                    .Select(cv => cv.Sugar.SugarLevel)
                    .ToList(),
                BobaTeas = Bobateas,
            };

            return View("~/Views/Home/BobateaDetail.cshtml", viewModel);
        }
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View("Error!");
    }
}
