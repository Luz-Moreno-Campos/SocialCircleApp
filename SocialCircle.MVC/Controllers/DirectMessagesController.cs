using Microsoft.AspNetCore.Mvc;
using SocialCircle.MVC.Models;

namespace SocialCircle.MVC.Controllers
{
    public class DirectMessagesController : Controller
    {
        private static List<DirectMessage> messages = new List<DirectMessage>()
        {
            new DirectMessage
            {
                MessageId = 1,
                SenderId = 1,
                ReceiverId = 2,
                MessageText = "Hello!",
                SentAt = DateTime.Now
            }
        };

        public IActionResult Index()
        {
            return View(messages);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(DirectMessage message)
        {
            message.MessageId = messages.Count + 1;
            message.SentAt = DateTime.Now;

            messages.Add(message);

            return RedirectToAction("Index");
        }
    }
}