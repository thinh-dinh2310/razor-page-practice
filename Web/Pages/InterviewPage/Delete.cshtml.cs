using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObject;

namespace Web.Pages.InterviewPage
{
    public class DeleteModel : PageModel
    {
        private readonly BusinessObject.eRecruitment_PRN221Context _context;

        public DeleteModel(BusinessObject.eRecruitment_PRN221Context context)
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
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Interview = await _context.Interviews.FindAsync(id);

            if (Interview != null)
            {
                _context.Interviews.Remove(Interview);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
