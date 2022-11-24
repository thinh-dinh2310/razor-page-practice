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

namespace Web.Pages.LocationPage
{
    public class EditModel : PageModel
    {
        private readonly ILocationRepository _locationRepository;

        public EditModel(ILocationRepository locationRepository)
        {
            _locationRepository = locationRepository;
        }

        [BindProperty]
        public Location Location { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Location = _locationRepository.GetLocationById(id);

            if (Location == null)
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
            await _locationRepository.UpdateLocation(Location);
            return RedirectToPage("./Index");
        }
    }
}
