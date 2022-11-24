using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObject;
using DataAccess.Repository.Interface;
using DataAccess.Repository;
using Microsoft.AspNetCore.Authorization;
using BusinessObject.DTO;

namespace Web.Pages
{
    [Authorize]
    public class HomeModel : PageModel
    {
        private readonly BusinessObject.eRecruitment_PRN221Context _context;
        private readonly IPostRepository postRepository = new PostRepository();
      
        public HomeModel(BusinessObject.eRecruitment_PRN221Context context)
        {
            _context = context;
        }

        public IEnumerable<Post> Post { get;set; }
        [BindProperty]
        public PaginationResult<Post> postList { get; set; }
        public async Task<IActionResult> OnGetAsync()
        {
            ViewData["CurrentPage"] = 1;
            ViewData["Category"] = await _context.Categories.ToListAsync();
            ViewData["Skills"] = await _context.Skills.ToListAsync();
            ViewData["Locations"] = await _context.Locations.ToListAsync();
            ViewData["Levels"] = await _context.Levels.ToListAsync();
            int limit = 10;
            int offset = 0;
            postList = await postRepository.GetAllPosts(offset, limit, null, null, null, null, null);
            return Page();
        }

        public async Task<IActionResult> OnGetSearchAsync(int limit, int offset, string keywords, string category, string locations, string skills, string levels)
        {
            ViewData["CurrentPage"] = offset + 1;
            postList = await postRepository.GetAllPosts(offset, limit, locations, category, skills, levels, keywords);
            return Partial("~/Pages/_JobHomePartial.cshtml", this);
        }
    }
}
