using Microsoft.AspNetCore.Mvc;

namespace PlanIt.Controllers
{
  public class HomeController : Controller
  {
    [HttpGet("/")]
    public ActionResult Index()
    {
      return View();
    }
  }
}