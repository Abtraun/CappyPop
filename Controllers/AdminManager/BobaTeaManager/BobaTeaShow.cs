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
public class BobaTeaShow : Controller
{
    private readonly ILogger<BobaTeaShow> _logger;

    public BobaTeaShow(ILogger<BobaTeaShow> logger)
    {
        _logger = logger;
    }

    [HttpGet("BobaShow")]
    public IActionResult BobaShow()
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


            return View("~/Views/Admin/BobateaManager/BobaShow.cshtml", listBobaViewModel);
        }
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View("Error!");
    }
}
