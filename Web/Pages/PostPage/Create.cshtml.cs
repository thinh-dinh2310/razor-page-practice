using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BusinessObject;
using DataAccess.Repository.Interface;
using DataAccess.Repository;
using BusinessObject.DTO;

namespace Web.Pages.PostPage
{
    public class CreateModel : PageModel
    {
        private readonly BusinessObject.eRecruitment_PRN221Context _context;
        private readonly IPostRepository postRepository = new PostRepository();
        private readonly ISkillRepository skillRepository = new SkillRepository();
        private readonly IPostSkillRepository postskillRepository = new PostSkillRepository();


        public CreateModel(BusinessObject.eRecruitment_PRN221Context context)
        {
            _context = context;
        }
        public List<string> skillnames = new List<string>();
        public List<string> Skills;
        [BindProperty]
        public PostForCreationDto Post {get; set;}
    public IActionResult OnGet()
        {
            ViewData["Category"] = new SelectList(_context.Categories.ToList(), "CategoryId", "CategoryName");
            ViewData["Levels"] = new SelectList(_context.Levels.ToList(), "LevelId", "LevelName");
            ViewData["Locations"] = new SelectList(_context.Locations.ToList(), "LocationId", "LocationName"); ;
            ViewData["Skills"] = new SelectList(_context.Skills.ToList(), "SkillsId", "SkillName");
            return Page();
        }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public IActionResult OnPost()
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (Post.PostSkillsIds == null)
                    {
                        Post.PostSkillsIds = new Guid[] { };
                    }
                    if (Post.PostLocationsIds == null)
                    {
                        Post.PostLocationsIds = new Guid[] { };
                    }
                    postRepository.CreatePost(Post);
                    return RedirectToPage("/PostPage/Index");
                }
                else
                {
                    ViewData["Category"] = new SelectList(_context.Categories.ToList(), "CategoryId", "CategoryName");
                    ViewData["Levels"] = new SelectList(_context.Levels.ToList(), "LevelId", "LevelName");
                    ViewData["Locations"] = new SelectList(_context.Locations.ToList(), "LocationId", "LocationName"); ;
                    ViewData["Skills"] = new SelectList(_context.Skills.ToList(), "SkillsId", "SkillName");
                    if (Post.PostSkillsIds == null)
                    {
                        Post.PostSkillsIds = new Guid[] { };
                    }
                    if (Post.PostLocationsIds == null)
                    {
                        Post.PostLocationsIds = new Guid[] { };
                    }
                    throw new Exception("Not all fields validated!");
                }
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex.Message);
                return Page();
            }
            
        }

    }
}
