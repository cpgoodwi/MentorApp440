using MentorApp440.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace MentorApp440.Controllers;

// [Route("api/[controller]")]
public class APIController : Controller
{
    // takes goal from form and adds it to goal database
    // [Route("Home/Dashboard")]
    [HttpPost]
    public IActionResult PostGoal(int memId, string goalStr)
    {
        SqlConnection.InsertGoal(memId, goalStr);
        return RedirectToAction("toDashboard","Home");
    }

    [HttpGet]
    public JsonResult GetGoals(int memId)
    {
        var goalList = SqlConnection.GetGoalsFromMemberId(memId);
        return new JsonResult(goalList);
    }

    [HttpGet]
    public JsonResult GetTasks(int memId)
    {
        var taskList = SqlConnection.GetTasksFromMemberId(memId);
        return new JsonResult(taskList);
    }
}