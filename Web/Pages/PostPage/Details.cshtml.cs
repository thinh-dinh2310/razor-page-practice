using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObject;
using DataAccess.Repository.Interface;
using DataAccess.Repository;
using System.Web;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Web.Pages.PostPage
{
    public class DetailsModel : PageModel
    {
        private readonly BusinessObject.eRecruitment_PRN221Context _context;
        private readonly IHttpContextAccessor httpContextAccessor;
        private IUserRepository userRepository = new UserRepository();
        private readonly IPostRepository postRepository = new PostRepository();
        private readonly ISkillRepository skillRepository = new SkillRepository();

        public DetailsModel(BusinessObject.eRecruitment_PRN221Context context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            this.httpContextAccessor = httpContextAccessor;
        }
        [BindProperty]
        public Post Post { get; set; }
        [BindProperty]
        public User User { get; set; }

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
        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            Post = await postRepository.GetPostByIdAsync(id);
            string decode = HttpUtility.HtmlDecode(Post.Description);
            Post.Description = decode;

            User = GetCurrentUser();
            var tmp = await _context.ApplicantPosts.FirstOrDefaultAsync(p => p.ApplicantId == User.Id && p.PostId == Post.PostId);
            if(tmp != null)
            {
                ViewData["Show"] = "NotShow";
            }
            else
            {
                ViewData["Show"] = "Show";
            }

            if (!User.Role.RoleName.Equals("USER"))
            {
                ViewData["Show"] = "NotShow";
            }
            if (Post == null)
            {
                return NotFound();
            }

            return Page();
        }
        
        
    }
}
