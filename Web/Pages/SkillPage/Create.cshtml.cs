using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BusinessObject;
using DataAccess.Repository.Interface;

namespace Web.Pages.SkillPage
{
    public class CreateModel : PageModel
    {
        private readonly ISkillRepository _skillRepository;

        public CreateModel(ISkillRepository skillRepository)
        {
            _skillRepository = skillRepository;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Skill Skill { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            Skill.SkillsId = new Guid();
            _skillRepository.CreateSkill(Skill);

            return RedirectToPage("./Index");
        }
    }
}
