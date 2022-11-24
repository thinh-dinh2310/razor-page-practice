using BusinessObject;
using BusinessObject.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository.Interface
{
    public interface ISkillRepository
    {
        public Skill GetSkillById(Guid id);
        void CreateSkill(Skill skill);
        void DeleteSkillById(Guid id);
        Task<PaginationResult<Skill>> GetAllSkill(int limit, int offset, string keywords);
        Task<IEnumerable<Skill>> GetAllSkillPost(Guid postId);
        Task<Skill> UpdateSkill(Skill Skill);
    }
}
