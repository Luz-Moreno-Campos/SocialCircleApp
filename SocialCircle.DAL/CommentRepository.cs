using Microsoft.EntityFrameworkCore;
using SocialCircle.Models;

namespace SocialCircle.DAL
{
    public class CommentRepository
    {
        private readonly SocialCircleContext _context;

        public CommentRepository(SocialCircleContext context)
        {
            _context = context;
        }

        public async Task AddCommentAsync(Comment comment)
        {
            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCommentAsync(int id)
        {
            var comment = await _context.Comments.FindAsync(id);
            if (comment != null)
            {
                _context.Comments.Remove(comment);
                await _context.SaveChangesAsync();
            }
        }
    }
}