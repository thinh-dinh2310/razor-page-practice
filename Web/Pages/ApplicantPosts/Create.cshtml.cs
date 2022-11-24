using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BusinessObject;
using static System.Net.Mime.MediaTypeNames;
using DataAccess.Repository.Interface;
using DataAccess.Repository;
using System.IO;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using static System.Net.WebRequestMethods;
using Microsoft.AspNetCore.Hosting;
using Web.Model;
using BusinessObject.Common;

namespace Web.Pages.ApplicantPosts
{
    public class CreateModel : PageModel
    {
        private readonly BusinessObject.eRecruitment_PRN221Context _context;
        private readonly IApplicantPostRepository applicantPostRepository = new ApplicantPostRepository();
        private readonly IHttpContextAccessor httpContextAccessor;
        private IUserRepository userRepository = new UserRepository();
        private readonly IPostRepository postRepository = new PostRepository();
        [BindProperty]
        public Post Post { get; set; }
        [BindProperty]
        public User User { get; set; }

        [BindProperty]
        public ApplicantPost ApplicantPost { get; set; }
        public CreateModel(BusinessObject.eRecruitment_PRN221Context context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            this.httpContextAccessor = httpContextAccessor;
        }

        private User GetCurrentUser()
        {
            User tmp = null;
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

        public async Task<IActionResult> OnGet(Guid id)
        {
            Post = await postRepository.GetPostByIdAsync(id);
            User = GetCurrentUser();
            if (User == null)
            {
                return RedirectToPage("/Login");
            }

            return Page();
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

        public async Task<IActionResult> OnPostAsync(string message, string id)
        {
            bool check = false;
            if (String.IsNullOrEmpty(message))
            {
                check = true;
                ViewData["Message"] = "Message can't be empty";
            }

            User = GetCurrentUser();
            Console.WriteLine(id);
            Console.WriteLine("HEAHA");
            if (User.Resume == null)
            {
                check = true;
                ViewData["ResumeMessage"] = "You need to upload Resume to apply";
            }

            if(check == true)
            {
                return Page();
            }

            ApplicantPost apost = new ApplicantPost();
            apost.ApplicantId = User.Id;
            apost.PostId = Guid.Parse(id);
            apost.Resume = User.Resume;
            apost.Message = message;
            apost.Status = CommonEnums.APOST_STATUS.Pending;
            applicantPostRepository.CreateApplicantPost(apost);
            return RedirectToPage("/Home");
        }
    }
}
