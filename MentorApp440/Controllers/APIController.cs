using MentorApp440.Helpers;
using MentorApp440.Session;
using Microsoft.AspNetCore.Mvc;

namespace MentorApp440.Controllers;

// [Route("api/[controller]")]
public class APIController : Controller
{
    // takes goal from form and adds it to goal database
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

    [HttpGet]
    public JsonResult GetPeers(int memId)
    {
        var PeerLists = SqlConnection.GetPeersFromMemberId(memId, HttpContext.Session.Get<int>(SessionVariables._OrgId));
        return new JsonResult(PeerLists);
    }
}