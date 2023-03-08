using Microsoft.AspNetCore.Mvc;
using SmartInfo.Web.ViewModels;

namespace SmartInfo.Web.Controllers;

[ApiController]
[Route("")]
public class Home : Controller
{
    // GET
    public IActionResult Index()
    {
        var model = new HomeModel{
            User="USR"
            };
        return View("Home", model);
    }
}