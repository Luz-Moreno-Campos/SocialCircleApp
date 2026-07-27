using Microsoft.AspNetCore.Mvc;
using SocialCircle.BLL;
using SocialCircle.Models;

namespace SocialCircle.MVC.Controllers
{
    public class PostController : Controller
    {
        private readonly PostService _postService;
        private readonly CommentService _commentService;

        public PostController(PostService postService, CommentService commentService)
        {
            _postService = postService;
            _commentService = commentService;
        }

        public async Task<IActionResult> Index()
        {
            var posts = await _postService.GetAllPostsAsync();
            return View(posts);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Post post)
        {
            if (ModelState.IsValid)
            {
                await _postService.CreatePostAsync(post);
                return RedirectToAction(nameof(Index));
            }
            return View(post);
        }

        public async Task<IActionResult> Details(int id)
        {
            var post = await _postService.GetPostByIdAsync(id);
            if (post == null)
            {
                return NotFound();
            }
            return View(post);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddComment(int postId, int userId, string text)
        {
            if (!string.IsNullOrWhiteSpace(text))
            {
                var comment = new Comment
                {
                    PostId = postId,
                    UserId = userId,
                    CommentText = text
                };
                await _commentService.AddCommentAsync(comment);
            }
            return RedirectToAction(nameof(Details), new { id = postId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await _postService.DeletePostAsync(id);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteComment(int id, int postId)
        {
            await _commentService.DeleteCommentAsync(id);
            return RedirectToAction(nameof(Details), new { id = postId });
        }
    }
}