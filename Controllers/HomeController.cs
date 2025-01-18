using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using CappyPop_Full_HTML.Models;
using CappyPop_Full_HTML.Models.Tables;
using Microsoft.EntityFrameworkCore.Metadata.Internal;


namespace CappyPop_Full_HTML.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index(int page = 1, int pageSize = 8) // Set default pageSize to 8
    {
        using (var db = new bobateashopContext())
        {
            var totalProducts = (from p in db.Bobateas
                                 select p).Count();

            var products = (from p in db.Bobateas
                            join img in db.ImageUrls.Where(img => (bool)img.IsPrimary) on p.BobaTeaId equals img.BobaTeaId

                            select new Models.HomeViewModel.BobaTea
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
            return View("~/Views/Home/Index.cshtml", bobateaViewModel);
        }
    }

    public IActionResult Privacy()
    {
        return View();
    }

    public IActionResult Boba()
    {
        return View();
    }
    public IActionResult UserMyCart()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
