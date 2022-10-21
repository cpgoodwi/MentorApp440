using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MentorApp440.Models;
using MentorApp440.Session;

namespace MentorApp440.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    /* LogIn(username)
     * takes username as only log in parameter, because that's not the focus of our project
     * returns views based on their user type (references database)
     */
    [HttpPost]
    public IActionResult LogIn(string username)
    {
        // TODO: add username to session variables
        if (!string.IsNullOrWhiteSpace(username))
        {
            HttpContext.Session.Set(SessionVariables.SessionKeyUsername, username);
            HttpContext.Session.Set(SessionVariables.SessionKeySessionId, Guid.NewGuid().ToString()); // not sure if this is 100% necessary, but including it anyway

            ViewData["Username"] = HttpContext.Session.Get<string>(SessionVariables.SessionKeyUsername);
        }
        
        if (username.Equals("admin")) // if username in admin return admin view
            return View("Admin");
        else // if username in employee return standard user view
            return View("Dashboard");
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}