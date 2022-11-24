using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BusinessObject;
using Microsoft.EntityFrameworkCore;

namespace Web.Pages.InterviewPage
{
    public class CreateModel : PageModel
    {
        private readonly BusinessObject.eRecruitment_PRN221Context _context;

        public CreateModel(BusinessObject.eRecruitment_PRN221Context context)
        {
            _context = context;
        }
        ApplicantPost ApplicantPost { get; set; }
        public IActionResult OnGet(Guid id)
        {
           

            ViewData["ApplicantId"] = new SelectList(_context.ApplicantPosts, "ApplicantId", "ApplicantId");
        ViewData["InterviewerId"] = new SelectList(_context.Users, "Id", "Email");
            ViewData["PostId"] = new SelectList(_context.Posts, "PostId", "Title");
            return Page();
        }

        [BindProperty]
        public Interview Interview { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync(Guid id)
        {
            ApplicantPost = _context.ApplicantPosts.FirstOrDefault(m => m.ApplicantId == id);
            if (!ModelState.IsValid)
            {
                return Page();
            }
            Interview.ApplicantId = id;
            Interview.PostId = ApplicantPost.PostId;
            _context.Interviews.Add(Interview);
         
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
