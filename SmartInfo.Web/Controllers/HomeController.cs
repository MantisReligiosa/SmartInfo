using Microsoft.AspNetCore.Mvc;
using SmartInfo.Web.ViewModels;

namespace SmartInfo.Web.Controllers;

[ApiController]
[Route("")]
public class Home : Controller
{
    // GET
    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {
        var model = new HomeModel{
            User="USR"
            };
        return View("Home", model);
    }
}