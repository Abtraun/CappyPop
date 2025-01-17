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

namespace CappyPop_Full_HTML.Controllers.AdminManager;


public class AdminController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public AdminController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }
    [HttpGet("admin_dashboard")]
    public IActionResult Adnin_Dashboard()
    {
        using (var db = new bobateashopContext())
        {
            var bobatea = db.Bobateas
            .Include(v => v.ImageUrls)
                .ToList();

            var listBobaViewModel = bobatea.Select(v => new ListBobaTeaViewModel
            {
                BobaId = v.BobaTeaId,
                BobaName = v.Name,
                Price = (int)v.Price,
                Description = v.Description,
                PrimaryImage = v.ImageUrls.FirstOrDefault(i => i.IsPrimary == true)?.Url // Lấy hình ảnh chính
            }).ToList();

            var orders = db.Orders
            .Include(o => o.OrderDetails)
                .ThenInclude(od => od.BobaTea) // Include Product (BobaTea) in OrderDetails
            .ToList();

            // Calculate total orders
            var totalOrders = orders.Count;

            // Calculate total revenue
            var totalRevenue = orders
                .SelectMany(o => o.OrderDetails) // Flatten OrderDetails
                .Sum(od => od.Quantity * (od.BobaTea?.Price ?? 0)); // Calculate revenue

            // Prepare data to pass to the view
            var dashboardViewModel = new AdminDashboardViewModel
            {
                BobaTeas = listBobaViewModel,
                TotalOrders = totalOrders,
                TotalRevenue = (int)totalRevenue
            };

            return View("~/Views/Admin/Admin_DashBoard.cshtml", dashboardViewModel);
        }
    }
    
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View("Error!");
    }
}
