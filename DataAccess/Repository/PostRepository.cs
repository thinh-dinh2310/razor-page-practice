using BusinessObject;
using BusinessObject.DTO;
using DataAccess.DAO;
using DataAccess.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class PostRepository : IPostRepository
    {
        public Task<PaginationResult<Post>> GetAllPosts(int offset, int limit, string locations, string category, string skills, string levels, string keywords)
            => PostsDAO.Instance.GetAllPosts(offset, limit, locations, category, skills, levels, keywords);
        public Task<Post> GetPostByIdAsync(Guid PostId) => PostsDAO.Instance.GetPostByIdAsync(PostId);
        public void UpdatePostById(Post post)
            => PostsDAO.Instance.UpdatePostById(post);
        public void DeletePostById(Guid PostId) => PostsDAO.Instance.DeletePostById(PostId);
        public void CreatePost(PostForCreationDto post)
            => PostsDAO.Instance.CreatePost(post);
    }
}
