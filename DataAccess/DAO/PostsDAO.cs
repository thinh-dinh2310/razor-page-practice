using BusinessObject;
using BusinessObject.DTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class PostsDAO
    {
        private static PostsDAO instance = null;
        private static readonly object instanceLock = new object();
        private PostsDAO() { }

        public static PostsDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new PostsDAO();
                    }
                }
                return instance;
            }
        }

        public async Task<PaginationResult<Post>> GetAllPosts(int offset = 0, int limit = 10,
            string locations = "",
            string category = "",
            string skills = "",
            string levels = "",
            string keywords = "")
        {
            PaginationResult<Post> response = new PaginationResult<Post>();
            List<Post> list = new List<Post>();
            try
            {
                var context = new eRecruitment_PRN221Context();
                List<Guid> locationIds = String.IsNullOrEmpty(locations)
                    ? context.Locations.Select(e => new Guid(e.LocationId.ToString())).ToList()
                    : locations.Split(new char[] { '+' }).ToList().Select(l => Guid.Parse(l)).ToList();

                List<Guid> categoriesIds = String.IsNullOrEmpty(category)
                    ? context.Categories.Select(e => new Guid(e.CategoryId.ToString())).ToList()
                    : category.Split(new char[] { '+' }).ToList().Select(c => Guid.Parse(c)).ToList();

                List<Guid> skillsIds = String.IsNullOrEmpty(skills)
                    ? context.Skills.Select(e => new Guid(e.SkillsId.ToString())).ToList()
                    : skills.Split(new char[] { '+' }).ToList().Select(s => Guid.Parse(s)).ToList();

                List<Guid> levelsId = String.IsNullOrEmpty(levels)
                    ? context.Levels.Select(e => new Guid(e.LevelId.ToString())).ToList()
                    : levels.Split(new char[] { '+' }).ToList().Select(lv => Guid.Parse(lv)).ToList();
                keywords = String.IsNullOrEmpty(keywords) ? "" : keywords.Trim();

                IEnumerable<Post> listPost = await context.Posts
                    .Include(p => p.Category)
                    .Include(p => p.Level)
                    .Include(p => p.LocationPosts)
                        .ThenInclude(e => e.Location)
                    .Include(p => p.PostSkills)
                        .ThenInclude(e => e.Skills)
                        .Where(c => categoriesIds.Contains(c.CategoryId))
                        .Where(lv => levelsId.Contains(lv.LevelId))
                        .Where(l => l.LocationPosts.Any(x => locationIds.Contains(x.LocationId)))
                        .Where(s => s.PostSkills.Any(x => skillsIds.Contains(x.SkillsId)))
                        .Where(k => k.Title.ToLower().Contains(keywords))
                        .OrderByDescending(o => o.CreatedAt)
                        .Skip(offset * limit)
                        .Take(limit).ToListAsync();   

                response.limit = limit;
                response.offset = offset;
                response.totalInPage = list.Count();
                response.totalItems = context.Posts.Include(p => p.Category)
                    .Include(p => p.Level)
                    .Include(p => p.LocationPosts)
                        .ThenInclude(e => e.Location)
                    .Include(p => p.PostSkills)
                        .ThenInclude(e => e.Skills)
                        .Where(c => categoriesIds.Contains(c.CategoryId))
                        .Where(lv => levelsId.Contains(lv.LevelId))
                        .Where(l => l.LocationPosts.Any(x => locationIds.Contains(x.LocationId)))
                        .Where(s => s.PostSkills.Any(x => skillsIds.Contains(x.SkillsId)))
                        .Where(k => k.Title.ToLower().Contains(keywords)).Count();
                response.data = listPost;
            }
            catch (Exception ex)
            {
                Console.WriteLine(DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + " Error at GetAllPosts: " + ex.Message);
            }
            return response;
        }

        public async Task<Post> GetPostByIdAsync(Guid postId)
        {
            Post tmp = new Post();
            try
            {
                var context = new eRecruitment_PRN221Context();
                tmp = await context.Posts
                    .Include(p => p.Level)
                    .Include(p => p.Category)
                    .Include(p => p.LocationPosts)
                        .ThenInclude(e => e.Location)
                    .Include(p => p.PostSkills)
                        .ThenInclude(e => e.Skills)
                    .FirstOrDefaultAsync(m => m.PostId == postId);
            }
            catch (Exception ex)
            {
                Console.WriteLine(DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "Error at GetPostByIdAsync: " + ex.Message);
            }
            return tmp;
        }

        public async void UpdatePostById(Post post)
        {
            try
            {
                var context = new eRecruitment_PRN221Context();
                context.Posts.Update(post);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "Error at UpdatePostById: " + ex.Message);
            }
        }

        public async void DeletePostById(Guid PostId)
        {
            try
            {
                var context = new eRecruitment_PRN221Context();
                Post Post = context.Posts
                    .Include(p => p.PostSkills)
                    .Include(p => p.LocationPosts)
                    .FirstOrDefault(u => u.PostId.Equals(PostId));
                foreach(var item in Post.LocationPosts)
                {
                    context.LocationPosts.Remove(item);
                    await context.SaveChangesAsync();
                }

                foreach (var item in Post.PostSkills)
                {
                    context.PostSkills.Remove(item);
                    await context.SaveChangesAsync();
                }
                context.Posts.Remove(Post);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "Error at DeletePostById: " + ex.InnerException.Message);
            }
        }

        public void CreatePost(PostForCreationDto post)
        {
            try
            {
                var context = new eRecruitment_PRN221Context();
                Guid postId = Guid.NewGuid();
                Post Post = new Post()
                {
                    PostId = postId,
                    Title = post.Title.Trim(),
                    Description = post.Description.Trim(),
                    Status = 1,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                    CategoryId = post.CategoryId,
                    LevelId = post.LevelId
                };
                List<PostSkill> postSkills = new List<PostSkill>();
                List<LocationPost> postLocations = new List<LocationPost>();

                foreach (Guid skillId in post.PostSkillsIds)
                {
                    var skill = context.Skills.FirstOrDefault(skill => skill.SkillsId == skillId);
                    if (skill == null)
                    {
                        continue;
                    }
                    PostSkill postSkill = new PostSkill()
                    {
                        SkillsId = skillId,
                        PostId = postId,
                    };
                    postSkills.Add(postSkill);
                }
                Post.PostSkills = postSkills;

                foreach (Guid locationId in post.PostLocationsIds)
                {
                    var location = context.Locations.FirstOrDefault(location => location.LocationId == locationId);
                    if (location == null)
                    {
                        continue;
                    }
                    LocationPost LocationPost = new LocationPost()
                    {
                        LocationId = locationId,
                        PostId = postId,
                    };
                    postLocations.Add(LocationPost);
                }
                Post.PostSkills = postSkills;
                Post.LocationPosts = postLocations;
                context.Posts.Add(Post);
                context.Entry(Post).State = EntityState.Added;

                if (context.SaveChanges() > 0)
                {
                    Console.WriteLine(DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "Created Post successfully");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "Error at CreatePost: " + ex.Message);
            }
        }

    }
}
