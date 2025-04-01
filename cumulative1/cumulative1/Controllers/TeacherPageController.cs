using Microsoft.AspNetCore.Mvc;

namespace cumulative1.Controllers
{
    public class TeacherPageController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
