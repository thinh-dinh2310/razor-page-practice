using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class UserSkillDao
    {
        private static UserSkillDao instance = null;
        private static readonly object instanceLock = new object();
        private UserSkillDao() { }

        public static UserSkillDao Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new UserSkillDao();
                    }
                }
                return instance;
            }
        }

        public void AddUserSkill(UserSkill us)
        {
            try
            {
                var context = new eRecruitment_PRN221Context();
                Console.WriteLine(us.UsersId);
                var tmp = context.UserSkills.FirstOrDefault(s => s.SkillsId == us.SkillsId && s.UsersId == us.UsersId);
                Console.WriteLine("Added Skills2");
                if (tmp == null)
                {
                    Console.WriteLine("Added Skills");
                    context.UserSkills.Add(us);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "Error at AddUserSkill: " + ex.InnerException.Message);
            }
        }

        public void DeleteUserSkill(UserSkill us)
        {
            try
            {
                var context = new eRecruitment_PRN221Context();
                var tmp = context.UserSkills.FirstOrDefault(s => s.SkillsId == us.SkillsId && s.UsersId == s.UsersId);
                if (tmp != null)
                {
                    context.UserSkills.Remove(tmp);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "Error at DeleteUserSkill: " + ex.Message);
            }
        }
    }
}
