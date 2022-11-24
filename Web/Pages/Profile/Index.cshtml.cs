using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObject;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using System.IO;
using DataAccess.Repository.Interface;
using DataAccess.Repository;
using Web.Model;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Web.Pages.Profile
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly BusinessObject.eRecruitment_PRN221Context _context;
        private readonly IHttpContextAccessor httpContextAccessor;
        private IUserRepository userRepository = new UserRepository();
        private IUserSkillRepository userSkillRepository = new UserSkillRepository();
        public IndexModel(BusinessObject.eRecruitment_PRN221Context context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            this.httpContextAccessor = httpContextAccessor;
        }
        [BindProperty]
        public Guid[] SelectedSkills { get; set; }
        [BindProperty]
        public string Image { get; set; }
        [BindProperty]
        public User User { get; set; }

        private User GetCurrentUser()
        {
            User tmp = new User();
            try
            {
                var identity = httpContextAccessor.HttpContext.User.Identity as ClaimsIdentity;
                if (identity != null)
                {
                    var userClaims = identity.Claims;
                    string stringId = userClaims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
                    Guid id = new Guid(stringId);
                    tmp = userRepository.GetUserById(id);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return tmp;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            User = GetCurrentUser();

            if (User.Avatar != null)
            {
                Image = Convert.ToBase64String(User.Avatar);
            }

            if (User == null)
            {
                return NotFound();
            }
            var skillList = _context.Skills.ToList();
            ViewData["Skills"] = new SelectList(skillList, "SkillsId", "SkillName");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(IFormFile File, IFormFile FileResume)
        {
            try
            {
                if (File != null)
                {
                    using (var ms = new MemoryStream())
                    {
                        await File.CopyToAsync(ms);
                        var fileBytes = ms.ToArray();
                        User.Avatar = fileBytes;
                    }
                }

                if (FileResume != null)
                {
                    using (var ms = new MemoryStream())
                    {
                        await FileResume.CopyToAsync(ms);
                        var fileBytes = ms.ToArray();
                        User.Resume = fileBytes;
                    }
                }

                User = userRepository.UpdateUser(User).Result;
                return RedirectToPage("./Index");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return Page();
            }

        }

        public IActionResult OnPostDeleteSkillAsync(Guid id)
        {
            try
            {
                User = GetCurrentUser();
                var us = new UserSkill();
                us.SkillsId = id;
                us.UsersId = User.Id;
                userSkillRepository.DeleteUserSkill(us);
                User = GetCurrentUser();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return Page();
            }

            return RedirectToPage("./Index");
        }

        public IActionResult OnPostAddSkillAsync()
        {
            try
            {
                User = GetCurrentUser();
                foreach (var item in SelectedSkills)
                {
                    var us = new UserSkill();
                    us.SkillsId = item;
                    us.UsersId = User.Id;
                    userSkillRepository.AddUserSkill(us);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return Page();
            }

            return RedirectToPage("./Index");
        }

        public async Task<IActionResult> OnGetResumeAsync()
        {
            User = GetCurrentUser();
            if (User.Resume != null)
            {
                return File(User.Resume, "application/pdf");
            }
            else
            {
                return Page();
            }
        }
    }
}
