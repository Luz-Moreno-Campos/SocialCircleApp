using Microsoft.EntityFrameworkCore;
using SocialCircle.Models;

namespace SocialCircle.DAL
{
    public class PostRepository
    {
        private readonly SocialCircleContext _context;

        public PostRepository(SocialCircleContext context)
        {
            _context = context;
        }

        public async Task<List<Post>> GetAllPostsAsync()
        {
            return await _context.Posts
                .Include(p => p.User)
                .Include(p => p.Comments)
                    .ThenInclude(c => c.User)
                .OrderByDescending(p => p.User)
                .ToListAsync();
        }

        public async Task<Post?> GetPostByIdAsync(long id)
        {
            return await _context.Posts
                .Include(p => p.User)
                .Include(p => p.Comments)
                    .ThenInclude(c => c.User)
                .FirstOrDefaultAsync(p => p.PostId == id);
        }

        public async Task CreatePostAsync(Post post)
        {
            _context.Posts.Add(post);
            await _context.SaveChangesAsync();
        }

        public async Task DeletePostAsync(int id)
        {
            var post = await _context.Posts.FindAsync(id);
            if (post != null)
            {
                _context.Posts.Remove(post);
                await _context.SaveChangesAsync();
            }
        }

        //Added by Luz
        public List<Post> GetPostsByUser(long userId)
        {
            return _context.Posts
                .Where(p => p.UserId == userId)
                .Include(p => p.Comments)  
                .OrderByDescending(p => p.PostTimeStamp)
                .ToList();
        }


    }
}