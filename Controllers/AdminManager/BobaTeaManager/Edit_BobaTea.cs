using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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

    [HttpGet("editbobatea")]
    public IActionResult EditBobaTea()
    {
        return View("~/Views/Admin/BobateaManager/EditBobaTea.cshtml");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View("Error!");
    }
}
