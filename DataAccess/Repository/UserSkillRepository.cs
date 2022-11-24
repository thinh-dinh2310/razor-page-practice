using BusinessObject;
using DataAccess.DAO;
using DataAccess.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class UserSkillRepository : IUserSkillRepository
    {
        public void AddUserSkill(UserSkill us) => UserSkillDao.Instance.AddUserSkill(us);
        public void DeleteUserSkill(UserSkill us) => UserSkillDao.Instance.DeleteUserSkill(us);
    }
}
