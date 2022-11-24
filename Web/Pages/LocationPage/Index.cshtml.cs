using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObject;
using DataAccess.Repository.Interface;
using BusinessObject.DTO;

namespace Web.Pages.LocationPage
{
    public class IndexModel : PageModel
    {
        private readonly ILocationRepository _locationRepository ;

        public IndexModel(ILocationRepository locationRepository)
        {
            this._locationRepository = locationRepository;
        }

        [BindProperty]
        public PaginationResult<Location> Location { get; set; }
        [BindProperty]
        public string currentSearch { get; set; } = "";

        public async Task OnGetAsync(int limit, int offset, string keywords)
        {
            if (offset == 0)
            {
                offset = 0;
            }
            if (limit == 0)
            {
                limit = 10;
            }
            if (keywords != null)
            {
                currentSearch = keywords;
            }
            else
            {
                keywords = currentSearch;
            }
            Location = await _locationRepository.GetAllLocation(limit, offset, keywords);
        }
    }
}
