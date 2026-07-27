using Microsoft.AspNetCore.Mvc;

namespace SocialCircle.MVC.Controllers
{
    public class DirectMessagesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
