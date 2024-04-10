using Microsoft.AspNetCore.Mvc;

namespace www1.Controllers;

public class LoginController : Controller
{
    // GET
    public IActionResult Index()
    {
        var s = HttpContext.Session;
        Console.WriteLine($"SID = {s.Id}");
        return View("../home/Login");
    }
}