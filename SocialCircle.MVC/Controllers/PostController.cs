using Microsoft.AspNetCore.Mvc;

namespace SocialCircle.MVC.Controllers
{
    public class PostController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
