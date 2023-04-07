using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SessionWorkshop.Models;

namespace SessionWorkshop.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    [Route("")]
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    [Route("process")]
    public IActionResult Process(string name)
    {
        if (name != null)
        {
            HttpContext.Session.SetString("UserName", name);
            HttpContext.Session.SetInt32("StartNum", 42);
            return View("Dashboard");
        }
        return RedirectToAction("Index");
    }

    [HttpPost]
    [Route("logout")]
    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Index");
    }
    public IActionResult Privacy()
    {
        return View();
    }

    [HttpPost]
    [Route("math")]
    public IActionResult Math(string plusone, string minusone, string timestwo, string plusrandom)
    {
        int? NextNum = HttpContext.Session.GetInt32("StartNum");
        if (plusone == "+ 1"){
            NextNum += 1;
        }
        if (minusone == "- 1"){
            NextNum -= 1;
        }
        if (timestwo == "x 2"){
            NextNum *= 2;
        }
        if (plusrandom == "+ Random"){
            Random rand = new Random();
            NextNum += rand.Next(1,11);
        }
        HttpContext.Session.SetInt32("StartNum", NextNum.GetValueOrDefault());
        return View("Dashboard");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
