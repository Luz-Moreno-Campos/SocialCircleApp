using SocialCircle.DAL;
using SocialCircle.Models;

namespace SocialCircle.BLL
{
    public class PostService
    {
        private readonly PostRepository _postRepository;

        public PostService(PostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        public Task<List<Post>> GetAllPostsAsync() => _postRepository.GetAllPostsAsync();

        public Task<Post?> GetPostByIdAsync(long id) => _postRepository.GetPostByIdAsync(id);

        public async Task CreatePostAsync(Post post)
        {
            post.PostTimeStamp = DateTime.Now;
            await _postRepository.CreatePostAsync(post);
        }

        public Task DeletePostAsync(int id) => _postRepository.DeletePostAsync(id);

        //Added by Luz

        public List<Post> GetPostsByUser(long userId)
        {
            return _postRepository.GetPostsByUser(userId);
        }

    }
}