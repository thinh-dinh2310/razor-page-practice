using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BusinessObject;
using DataAccess.Repository;
using DataAccess.Repository.Interface;

namespace Web.Pages.UserManagement
{
    public class CreateModel : PageModel
    {
        private readonly BusinessObject.eRecruitment_PRN221Context _context;
        private readonly IUserRepository UserRepository;

        public CreateModel(BusinessObject.eRecruitment_PRN221Context context, IUserRepository userRepository)
        {
            _context = context;
            this.UserRepository = userRepository;
        }

        public IActionResult OnGet()
        {
        ViewData["RoleId"] = new SelectList(_context.Roles, "RoleId", "RoleName");
            return Page();
        }

        [BindProperty]
        public User User { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            UserRepository.RegisterUser(User);

            return RedirectToPage("./Index");
        }
    }
}
