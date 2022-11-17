using MentorApp440.Helpers;
using MentorApp440.Session;
using Microsoft.AspNetCore.Mvc;

namespace MentorApp440.Controllers;

public class FeedbackController : Controller
{
    
    // links to goal for Peer or mentor to give feedback and person to rate the feedback
    [HttpGet]
    public IActionResult Goal(int memId, int goalId)
    {
        ViewData["MemId"] = memId;
        ViewData["GoalId"] = goalId;
        return View();
    }
    
    [HttpGet]
    public IActionResult Task(int memId, int taskId)
    {
        ViewData["MemId"] = memId;
        ViewData["TaskId"] = taskId;
        return View();
    }
    
    [HttpPost]
    public IActionResult GoalComment(int memId, int goalId, string comm)
    {
        var fromId = HttpContext.Session.Get<int>(SessionVariables._UserId);

        SqlConnection.GoalComment(fromId, memId, goalId, comm);

        var tempMemId = memId;
        var tempGoalId = goalId;

        // ViewData["MemId"] = memId;
        // ViewData["TaskId"] = goalId;
        return RedirectToAction("Goal", "Feedback", new {memId = tempMemId, goalId = tempGoalId});
    }

    [HttpPost]
    public IActionResult Task(int memId, int taskId, string comm)
    {
        var fromId = HttpContext.Session.Get<int>(SessionVariables._UserId);
        
        SqlConnection.TaskComment(fromId, memId, taskId, comm);
        
        var tempMemId = memId;
        var tempTaskId = taskId;
        
        return RedirectToAction("Task", "Feedback", new {memId = tempMemId, taskId = tempTaskId});
    }
}