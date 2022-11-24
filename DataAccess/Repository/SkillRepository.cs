using BusinessObject;
using BusinessObject.DTO;
using DataAccess.DAO;
using DataAccess.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class SkillRepository : ISkillRepository

    {
        public Skill GetSkillById(Guid id)
        {
            return SkillDAO.Instance.GetSkillByID(id);
        }
        public Task<PaginationResult<Skill>> GetAllSkill(int limit, int offset, string keywords) => SkillDAO.Instance.GetAllSkills(limit, offset, keywords);

        public Task<IEnumerable<Skill>> GetAllSkillPost(Guid postId) => SkillDAO.Instance.GetAllSkillsPost(postId);

        public void DeleteSkillById(Guid id)
        {
            SkillDAO.Instance.DeleteSkillById(id);
        }
        
        public void CreateSkill(Skill skill)
        {
            SkillDAO.Instance.CreateSkill(skill.SkillName);
        }
        public Task<Skill> UpdateSkill(Skill Skill) => SkillDAO.Instance.UpdateSkill(Skill);
    }
}
