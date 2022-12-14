using System.Diagnostics;
using MentorApp440.Helpers;
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
        HttpContext.Session.Set(SessionVariables._Username, "");
        HttpContext.Session.Set(SessionVariables.SessionKeySessionId, "");
        
        HttpContext.Session.Set(SessionVariables._OrgId, "");

        return View("Index");
    }

    /* LogIn(username)
     * takes username as only log in parameter, because that's not the focus of our project
     * returns views based on their user type (references database)
     */
    [Route("Home/Dashboard")]
    [HttpPost]
    public IActionResult LogIn(int orgSelect, string username)
    {
        if (!string.IsNullOrWhiteSpace(username))
        {
            if (SqlConnection.ValidUser(orgSelect, username))
            {
                HttpContext.Session.Set(SessionVariables._Username, username);
                HttpContext.Session.Set(SessionVariables._UserId, SqlConnection.UserMemId(orgSelect, username));
                
                HttpContext.Session.Set(SessionVariables.SessionKeySessionId,
                    Guid.NewGuid().ToString()); // not sure if this is 100% necessary, but including it anyway

                ViewData["UserId"] = HttpContext.Session.Get<string>(SessionVariables._Username);
                ViewData["ViewUser"] = ViewData["UserId"];

                HttpContext.Session.Set(SessionVariables._Viewing, username);
            
                // HttpContext.Session.Set(SessionVariables._OrgId, int.Parse(orgId));
                HttpContext.Session.Set(SessionVariables._OrgId, orgSelect);

                // ViewData["UserObj"] = HttpContext.Session.Set<User>(SessionVariables._CurrUser, new User(username));
            }
            else
            {
                // TODO: fix the URL
                return View("Index");
            }
        }

        return View(SqlConnection.LoginAdmin(orgSelect, username) ? // if username in admin return admin view
            "Admin" :
            // if username in employee return standard user view
            "User");
    }

    /* ToDashboard()
     * should only be available if user is logged in
     * returns a view of user's own dashboard as if they logged in again
     */
    [Route("Home/Dashboard")]
    public IActionResult ToDashboard()
    {
        ViewData["UserId"] = HttpContext.Session.Get<string>(SessionVariables._Username);
        ViewData["ViewUser"] = ViewData["UserId"];
        
        HttpContext.Session.Set(SessionVariables._Viewing, HttpContext.Session.Get<string>(SessionVariables._Username));

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
        ViewData["UserId"] = HttpContext.Session.Get<string>(SessionVariables._Username);
        ViewData["ViewUser"] = username;
        
        // HttpContext.Session.Set(SessionVariables._Viewing, new User(1, username));
        HttpContext.Session.Set(SessionVariables._Viewing, username);
        
        return View("User");
    }
    
    // links to goal for Peer or mentor to give feedback and person to rate the feedback
    // [Route("Goal")]
    // [HttpGet]
    // public IActionResult ExpandComment(int memId, int goalId)
    // {
    //     return View("GoalTask");
    // }

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