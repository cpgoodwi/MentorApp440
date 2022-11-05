using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MentorApp440.Models;
using MentorApp440.Session;
using Microsoft.AspNetCore.Components.Forms;

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

    /* LogOut()
     * should only be accessible if the user is logged in
     * returns to log in page and clears session data
     */
    [Route("Index")]
    public IActionResult LogOut()
    {
        HttpContext.Session.Set(SessionVariables.SessionKeyUserId, "");
        HttpContext.Session.Set(SessionVariables.SessionKeySessionId, "");

        return View("Index");
    }

    /* LogIn(username)
     * takes username as only log in parameter, because that's not the focus of our project
     * returns views based on their user type (references database)
     */
    [Route("Home/Dashboard")]
    [HttpPost]
    public IActionResult LogIn(string username)
    {
        // TODO: add username to session variables
        if (!string.IsNullOrWhiteSpace(username))
        {
            HttpContext.Session.Set(SessionVariables.SessionKeyUserId, username);
            HttpContext.Session.Set(SessionVariables.SessionKeySessionId, Guid.NewGuid().ToString()); // not sure if this is 100% necessary, but including it anyway

            ViewData["UserId"] = HttpContext.Session.Get<string>(SessionVariables.SessionKeyUserId);
            ViewData["ViewUser"] = ViewData["UserId"];

            // ViewData["UserObj"] = HttpContext.Session.Set<User>(SessionVariables._CurrUser, new User(username));
        }
        
        if (username.Equals("admin")) // if username in admin return admin view
            return View("Admin");
        else // if username in employee return standard user view
            return View("User");
    }

    /* ToDashboard()
     * should only be available if user is logged in
     * returns a view of user's own dashboard as if they logged in again
     */
    [Route("Home/Dashboard")]
    public IActionResult ToDashboard()
    {
        ViewData["UserId"] = HttpContext.Session.Get<string>(SessionVariables.SessionKeyUserId);
        ViewData["ViewUser"] = ViewData["UserId"];

        return View("User");
    }

    /* SelectUser(username)
     * takes username as parameter
     * returns a view of a user's profile
     */
    [Route("User")]
    [HttpGet]
    public IActionResult SelectUser(string username)
    {
        ViewData["UserId"] = HttpContext.Session.Get<string>(SessionVariables.SessionKeyUserId);
        ViewData["ViewUser"] = username;
        return View("User");
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