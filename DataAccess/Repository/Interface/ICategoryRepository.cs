using BusinessObject;
using BusinessObject.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository.Interface
{
    public interface ICategoryRepository
    {
        public Category GetCategoryById(Guid id);
        void CreateCategory(Category Category);
        Task<PaginationResult<Category>> GetAllCategory(int limit, int offset, string keywords);
        Task<Category> UpdateCategory(Category Category);
    }
}
