using BusinessObject;
using DataAccess.Repository;
using DataAccess.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;

using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;
using System.Text.RegularExpressions;
using System.ComponentModel.DataAnnotations;
using Web.Common;
using System.Text;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System.Linq;

namespace Web.Pages.Auth
{
    public class RegisterModel : PageModel
    {
        private readonly IUserRepository userRepository;
        private readonly eRecruitment_PRN221Context _context;
        private readonly IHttpContextAccessor httpContextAccessor;

        public RegisterModel(IUserRepository userRepository , IHttpContextAccessor httpContextAccessor)
        {
            this.userRepository = userRepository;
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
                return null;
            }
            return tmp;
        }
        public IActionResult OnGet()
        {
           if(GetCurrentUser() != null)
            {
                return RedirectToPage("/Index");
            }
            return Page();
        }

        [BindProperty]
        public RegisterDTO RegisterUser { get; set; }


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          
            var res = new ServerResponse<string>();
            FluentValidation.Results.ValidationResult result = new RegisterDTOValidator().Validate(RegisterUser);
            if (!result.IsValid)
            {
                foreach (var failure in result.Errors)
                {
                    ModelState.AddModelError("RegisterUser." + failure.PropertyName.ToString(), Helper.StringFormat(failure.ErrorMessage, failure.FormattedMessagePlaceholderValues));
                }
            }

            User userEmail = userRepository.GetUserByEmail(RegisterUser.Email);
            if (userEmail != null)
            {
                ModelState.AddModelError("RegisterUser.Email", "Email is Duplicated");
            }
            if (!ModelState.IsValid)
            {
                return Page();
            }

            User user = new User()
            {
                Id = Guid.NewGuid(),
                RoleId = Guid.Parse("09F7343C-67F9-474F-8186-1C9CDC78E1FF"),
                Email = RegisterUser.Email,
                FirstName = RegisterUser.FirstName,
                LastName = RegisterUser.LastName,
                Password = RegisterUser.Password,
                DisplayName = null,
                Phone = RegisterUser.Phone,
                Address = RegisterUser.Address,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                DateOfBirth = null,
                IsDeleted = false
                
            };

            userRepository.RegisterUser(user);
            

            return RedirectToPage("Login");
        }
    }
    public class RegisterDTO
    {
        [Display(Name = "Email")]
        public string Email { get; set; } 

        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        [Display(Name = "FirstName")]
        public string FirstName { get; set; }

        [Display(Name = "LastName")]
        public string LastName { get; set; }

        [Display(Name = "Phone")]
        public string Phone { get; set; }

        [Display(Name = "Address")]
        public string Address { get; set; }


    }

    public class RegisterDTOValidator : AbstractValidator<RegisterDTO>
    {
        public RegisterDTOValidator()
        {
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.Password).NotEmpty().Length(5,50);
            RuleFor(x => x.ConfirmPassword).NotEmpty().Equal(x => x.Password);
            RuleFor(x => x.FirstName).NotEmpty().Length(1,50);
            RuleFor(x => x.LastName).NotEmpty().Length(1,50);
            RuleFor(x => x.Phone).NotEmpty().NotNull().Matches(new Regex(@"^(03|05|07|08|09|01[2|6|8|9])+([0-9]{8})\b"));
            RuleFor(x => x.Address).NotEmpty().Length(1,150);
        }
    }

}
