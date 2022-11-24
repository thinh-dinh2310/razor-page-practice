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
    public class CategoryRepository : ICategoryRepository
    {
        public Category GetCategoryById(Guid id)
        {
            return CategoryDAO.Instance.GetCategoryByID(id);
        }
        public Task<PaginationResult<Category>> GetAllCategory(int limit, int offset, string keywords) => CategoryDAO.Instance.GetAllCategories(limit, offset, keywords);

        public void CreateCategory(Category Category)
        {
            CategoryDAO.Instance.CreateCategory(Category.CategoryName);
        }

        public Task<Category> UpdateCategory(Category Category) => CategoryDAO.Instance.UpdateCategory(Category);
    }

}
