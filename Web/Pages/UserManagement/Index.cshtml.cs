using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObject;
using DataAccess.Repository;
using Microsoft.AspNetCore.Authorization;
using DataAccess.Repository.Interface;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using BusinessObject.DTO;

namespace Web.Pages.UserManagement
{
    public class IndexModel : PageModel
    {
        private readonly IUserRepository UserRepository;
        private readonly IHttpContextAccessor httpContextAccessor;
        public IndexModel(IUserRepository userRepository, IHttpContextAccessor httpContextAccessor)
        {
            this.UserRepository = userRepository;
            this.httpContextAccessor = httpContextAccessor;
        }

        [BindProperty]
        public PaginationResult<User> User { get; set; }
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
            User = await UserRepository.GetAllUsers(offset, limit, keywords);
        }

        public async Task<IActionResult> OnPostDeleteAsync(Guid userId)
        {
            try
            {
                Console.WriteLine(userId);
                UserRepository.DeleteUserById(userId);
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
