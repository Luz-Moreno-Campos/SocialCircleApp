using SocialCircle.DAL;
using SocialCircle.Models;

namespace SocialCircle.BLL
{
    public class CommentService
    {
        private readonly CommentRepository _commentRepository;

        public CommentService(CommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        public async Task AddCommentAsync(Comment comment)
        {
            comment.CommentTimeStamp = DateTime.Now;
            await _commentRepository.AddCommentAsync(comment);
        }

        public Task DeleteCommentAsync(int id) => _commentRepository.DeleteCommentAsync(id);
    }
}