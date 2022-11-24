using DataAccess.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Threading;
using BusinessObject;
using DataAccess.Repository.Interface;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System;

namespace Web.Pages.Auth
{
    public class LoginModel : PageModel
    {
        private readonly IUserRepository UserRepository;
        private readonly IHttpContextAccessor httpContextAccessor;
        public LoginModel(IUserRepository userRepository, IHttpContextAccessor httpContextAccessor)
        {
            this.UserRepository = userRepository;
            this.httpContextAccessor = httpContextAccessor;
        }

        [BindProperty]
        public Credential Credential { get; set; }

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
                    tmp = UserRepository.GetUserById(id);
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            return tmp;
        }
        public IActionResult OnGet()
        {
            if (GetCurrentUser() != null)
            {
                return RedirectToPage("/Index");
            }
            return Page();
        }


        public IActionResult OnPost()
        {
            User user = UserRepository.GetUserByEmailAndPassword(Credential.Email, Credential.Password);
             if (user != null)
            {
                var claims = new List<Claim>();

                claims.Add(new Claim(ClaimTypes.Email, user.Email.ToString()));
                claims.Add(new Claim(ClaimTypes.Role, user.Role.RoleName.ToString()));
                claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())); 
                claims.Add(new Claim(ClaimTypes.Name, user.FirstName + " " + user.LastName));
                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);
                var pros = new AuthenticationProperties();

                Thread.CurrentPrincipal = principal;
                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, pros).Wait();

                var identity2 = httpContextAccessor.HttpContext.User.Identity as ClaimsIdentity;
                if (identity2 != null)
                {
                    var userClaims = identity2.Claims;
                    var Email = userClaims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
                    var id = userClaims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
                }
                return RedirectToPage("/Index");
            }
            return Page();
        }
        
    }
    public class Credential
    {
        [Display(Name = "Email")]
        [Required(ErrorMessage = "Email must not be empty")]
        public string Email { get; set; }
        [Display(Name = "Password")]
        [Required(ErrorMessage = "Password must not be empty")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }

    public class CurrentUser
    {
     
        public string Email { get; set; }
             public string Id { get; set; }
    }

}
