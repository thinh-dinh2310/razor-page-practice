using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObject;

namespace Web.Pages.ApplicantPosts
{
    public class IndexModel : PageModel
    {
        private readonly BusinessObject.eRecruitment_PRN221Context _context;

        public IndexModel(BusinessObject.eRecruitment_PRN221Context context)
        {
            _context = context;
        }

        public IList<ApplicantPost> ApplicantPost { get;set; }

        public async Task OnGetAsync(Guid id)
        {
            ApplicantPost = await _context.ApplicantPosts
                .Include(a => a.Applicant)
                .Include(a => a.Post).Where(p=> p.PostId == id).ToListAsync();
        }
    }
}
