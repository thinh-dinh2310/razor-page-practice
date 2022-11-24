using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObject;
using Microsoft.AspNetCore.Authorization;
using DataAccess.Repository.Interface;
using DataAccess.Repository;
using BusinessObject.DTO;

namespace Web.Pages.PostPage
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly BusinessObject.eRecruitment_PRN221Context _context;
        private readonly IPostRepository postRepository = new PostRepository();

        public IndexModel(BusinessObject.eRecruitment_PRN221Context context)
        {
            _context = context;
        }
        [BindProperty]
        public PaginationResult<Post> Post { get; set; }
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
            ViewData["CurrentPage"] = offset + 1;
            Post = await postRepository.GetAllPosts(offset, limit, null, null, null, null, keywords);
        }

        public async Task<IActionResult> OnPostDeleteAsync(Guid postId)
        {
            try
            {
                Console.WriteLine(postId);
                postRepository.DeletePostById(postId);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return Page();
            }

            return RedirectToPage("./Index");
        }
    }
}
