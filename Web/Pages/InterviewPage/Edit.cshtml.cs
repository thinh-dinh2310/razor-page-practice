using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BusinessObject;

namespace Web.Pages.InterviewPage
{
    public class EditModel : PageModel
    {
        private readonly BusinessObject.eRecruitment_PRN221Context _context;

        public EditModel(BusinessObject.eRecruitment_PRN221Context context)
        {
            _context = context;
        }

        [BindProperty]
        public Interview Interview { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Interview = await _context.Interviews
                .Include(i => i.ApplicantPost)
                .Include(i => i.Interviewer).FirstOrDefaultAsync(m => m.InterviewerId == id);

            if (Interview == null)
            {
                return NotFound();
            }
           ViewData["ApplicantId"] = new SelectList(_context.ApplicantPosts, "ApplicantId", "ApplicantId");
           ViewData["InterviewerId"] = new SelectList(_context.Users, "Id", "Email");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Interview).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InterviewExists(Interview.InterviewerId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool InterviewExists(Guid id)
        {
            return _context.Interviews.Any(e => e.InterviewerId == id);
        }
    }
}
