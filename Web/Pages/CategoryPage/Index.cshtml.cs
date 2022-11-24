using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObject;
using DataAccess.Repository.Interface;
using BusinessObject.DTO;

namespace Web.Pages.CategoryPage
{
    public class IndexModel : PageModel
    {
        private readonly ICategoryRepository _categoryRepository;
        public IndexModel(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        [BindProperty]
        public PaginationResult<Category> Category { get; set; }
        [BindProperty]
        public string currentSearch { get; set; } = "";

        public async Task OnGetAsync(int limit, int offset, string keywords)
        {
            if (offset == 0)
            {
                offset = 0;
            }
            if (limit == 0)
            {
                limit = 10;
            }
            if (keywords != null)
            {
                currentSearch = keywords;
            }
            else
            {
                keywords = currentSearch;
            }
            Category = await _categoryRepository.GetAllCategory(limit, offset, keywords);
        }

    }
}
