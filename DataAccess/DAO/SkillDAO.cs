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
    public class SkillDAO
    {
        private static SkillDAO instance = null;
        private static readonly object instanceLock = new object();
        private SkillDAO() { }

        public static SkillDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new SkillDAO();
                    }
                }
                return instance;
            }
        }

        public Skill GetSkillByID(Guid id)
        {
            Skill skill = null;
            try
            {
                var context = new eRecruitment_PRN221Context();
                skill = context.Skills.FirstOrDefault(item => item.SkillsId == id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "Error at GetSkillById: " + ex.Message);
            }
            return skill;
        }
        public async void CreateSkill(string skillName)
        {
            try
            {

                Skill skill = new Skill()
                {
                    SkillsId = Guid.NewGuid(),
                    SkillName = skillName
                };
                var context = new eRecruitment_PRN221Context();
                await context.Skills.AddAsync(skill);
                if (context.SaveChanges() > 0)
                {
                    Console.WriteLine(DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "Created skill successfully");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "Error at CreateSkill: " + ex.Message);
            }
        }
        public async void DeleteSkillById(Guid id)
        {
            try
            {
                var context = new eRecruitment_PRN221Context();
                Skill skill = context.Skills.FirstOrDefault(u => u.SkillsId.Equals(id));
                context.Skills.Remove(skill);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "Error at DeleteSkill: " + ex.Message);
            }
        }

        public async Task<PaginationResult<Skill>> GetAllSkills(int limit = 0, int offset = 0, string keywords = "")
        {
            PaginationResult<Skill> response = new PaginationResult<Skill>();
            List<Skill> list = new List<Skill>();
            try
            {
                var context = new eRecruitment_PRN221Context();
                list = await context.Skills.Where(k => k.SkillName.ToLower().Contains(keywords))
                        .Skip(offset * limit)
                        .Take(limit)
                        .ToListAsync();
                response.limit = limit;
                response.offset = offset;
                response.totalInPage = list.Count();
                response.totalItems = context.Skills.Where(k => k.SkillName.ToLower().Contains(keywords)).Count();
                response.data = list;
            }
            catch (Exception ex)
            {
                Console.WriteLine(DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "Error at GetAllSkill: " + ex.Message);
            }
            return response;
        }

        public async Task<IEnumerable<Skill>> GetAllSkillsPost(Guid PostId)
        {
            List<Skill> list = new List<Skill>();
            try
            {
                List<Skill> tmp = new List<Skill>();
                var context = new eRecruitment_PRN221Context();
                List<PostSkill> ps = await context.PostSkills.Where(P => P.PostId == PostId).ToListAsync();
                foreach(PostSkill t in ps)
                {
                    tmp.Add(context.Skills.FirstOrDefault(p=>p.SkillsId==t.SkillsId));
                }
                list = tmp.ToList();

            }
            catch (Exception ex)
            {
                Console.WriteLine(DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "Error at GetAllSkillPost: " + ex.Message);
            }
            
            return list;
        }

        public async Task<Skill> UpdateSkill(Skill updateSkill)
        {
            Skill tmp = new Skill();
            try
            {
                var context = new eRecruitment_PRN221Context();
                tmp = await context.Skills.AsNoTracking().FirstOrDefaultAsync(u => u.SkillsId == updateSkill.SkillsId);
                context.Update(updateSkill);
                await context.SaveChangesAsync();
                return updateSkill;
            }
            catch (Exception ex)
            {
                Console.WriteLine(DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "Error at UpdateUser: " + ex.Message);
                return tmp;
            }
        }
    }
}
