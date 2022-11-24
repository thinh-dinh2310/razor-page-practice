using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BusinessObject;
using DataAccess.Repository.Interface;

namespace Web.Pages.LocationPage
{
    public class CreateModel : PageModel
    {
        private readonly ILocationRepository _locationRepository;

        public CreateModel(ILocationRepository locationRepository)
        {
            this._locationRepository = locationRepository;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Location Location { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            Location.LocationId = new Guid();
            _locationRepository.CreateLocation(Location);

            return RedirectToPage("./Index");
        }
    }
}
