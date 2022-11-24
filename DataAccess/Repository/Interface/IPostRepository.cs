using BusinessObject;
using BusinessObject.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository.Interface
{
    public interface IPostRepository
    {
        Task<PaginationResult<Post>> GetAllPosts(int offset, int limit, string locations, string category, string skills, string levels, string keywords);
        Task<Post> GetPostByIdAsync(Guid PostId);
        void UpdatePostById(Post post);
        void DeletePostById(Guid PostId);
        void CreatePost(PostForCreationDto post);
    }
}
