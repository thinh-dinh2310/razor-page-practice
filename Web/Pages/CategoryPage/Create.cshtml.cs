using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BusinessObject;
using DataAccess.Repository.Interface;

namespace Web.Pages.CategoryPage
{
    public class CreateModel : PageModel
    {
        private readonly ICategoryRepository _categoryRepository;

        public CreateModel(ICategoryRepository categoryRepository)
        {
            this._categoryRepository = categoryRepository;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Category Category { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            Category.CategoryId = new Guid();
            _categoryRepository.CreateCategory(Category);

            return RedirectToPage("./Index");
        }
    }
}
