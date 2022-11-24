using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BusinessObject;
using DataAccess.Repository.Interface;

namespace Web.Pages.SkillPage
{
    public class EditModel : PageModel
    {
        private readonly ISkillRepository _skillRepository;

        public EditModel(ISkillRepository skillRepository)
        {
            this._skillRepository = skillRepository;
        }

        [BindProperty]
        public Skill Skill { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Skill = _skillRepository.GetSkillById(id);

            if (Skill == null)
            {
                return NotFound();
            }
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _skillRepository.UpdateSkill(Skill);
            return RedirectToPage("./Index");
        }
    }
}
