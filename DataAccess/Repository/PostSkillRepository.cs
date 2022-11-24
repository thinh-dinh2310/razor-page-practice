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
    public class PostSkillRepository : IPostSkillRepository
    {
       
        public void CreatePostskill(PostSkill pk) => PostSkillDAO.Instance.CreatePost(pk);

    }
}
