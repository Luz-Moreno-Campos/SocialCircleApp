using Microsoft.AspNetCore.Mvc;

namespace SocialCircle.MVC.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
