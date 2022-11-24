using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObject;
using DataAccess.Repository;
using DataAccess.Repository.Interface;

namespace Web.Pages.UserManagement
{
    public class DeleteModel : PageModel
    {
        private readonly IUserRepository UserRepository;

        public DeleteModel( IUserRepository userRepository)
        {
            this.UserRepository = userRepository;
        }

        [BindProperty]
        public User User { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            User = UserRepository.GetUserById(id);

            if (User == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            User = UserRepository.GetUserById(id);

            if (User != null)
            {
               UserRepository.DeleteUserById(id);
            }

            return RedirectToPage("./Index");
        }
    }
}
