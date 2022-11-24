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
    public class IndexModel : PageModel
    {
        private readonly BusinessObject.eRecruitment_PRN221Context _context;

        public IndexModel(BusinessObject.eRecruitment_PRN221Context context)
        {
            _context = context;
        }

        public IList<Interview> Interview { get;set; }

        public async Task OnGetAsync()
        {
            Interview = await _context.Interviews
                .Include(i => i.ApplicantPost).ThenInclude(m => m.Post)
                .Include(i => i.Interviewer).ToListAsync();
        }
    }
}
