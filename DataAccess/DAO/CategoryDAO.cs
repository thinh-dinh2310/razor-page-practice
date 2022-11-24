using BusinessObject;
using BusinessObject.DTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class CategoryDAO
    {
        private static CategoryDAO instance = null;
        private static readonly object instanceLock = new object();
        private CategoryDAO() { }

        public static CategoryDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new CategoryDAO();
                    }
                }
                return instance;
            }
        }

        public Category GetCategoryByID(Guid id)
        {
            Category Category = null;
            try
            {
                var context = new eRecruitment_PRN221Context();
                Category = context.Categories.FirstOrDefault(item => item.CategoryId == id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "Error at GetCategoryById: " + ex.Message);
            }
            return Category;
        }

        public async void CreateCategory(string CategoryName)
        {
            try
            {

                Category Category = new Category()
                {
                    CategoryId = Guid.NewGuid(),
                    CategoryName = CategoryName
                };
                var context = new eRecruitment_PRN221Context();
                await context.Categories.AddAsync(Category);
                if (context.SaveChanges() > 0)
                {
                    Console.WriteLine(DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "Created Category successfully");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "Error at CreateCategory: " + ex.Message);
            }
        }

        public async Task<PaginationResult<Category>> GetAllCategories(int limit = 0, int offset = 0, string keywords = "")
        {
            PaginationResult<Category> response = new PaginationResult<Category>();
            List<Category> list = new List<Category>();
            try
            {
                var context = new eRecruitment_PRN221Context();
                list = await context.Categories.Where(k => k.CategoryName.ToLower().Contains(keywords))
                        .Skip(offset * limit)
                        .Take(limit)
                        .ToListAsync();
                response.limit = limit;
                response.offset = offset;
                response.totalInPage = list.Count();
                response.totalItems = context.Categories.Where(k => k.CategoryName.ToLower().Contains(keywords)).Count();
                response.data = list;
            }
            catch (Exception ex)
            {
                Console.WriteLine(DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "Error at GetAllCategory: " + ex.Message);
            }
            return response;
        }

        public async Task<Category> UpdateCategory(Category updateCategory)
        {
            Category tmp = new Category();
            try
            {
                var context = new eRecruitment_PRN221Context();
                tmp = await context.Categories.AsNoTracking().FirstOrDefaultAsync(u => u.CategoryId == updateCategory.CategoryId);
                context.Update(updateCategory);
                await context.SaveChangesAsync();
                return updateCategory;
            }
            catch (Exception ex)
            {
                Console.WriteLine(DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "Error at UpdateUser: " + ex.Message);
                return tmp;
            }
        }
    }
}
