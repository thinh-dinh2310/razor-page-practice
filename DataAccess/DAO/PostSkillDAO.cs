using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class PostSkillDAO
    {
        private static PostSkillDAO instance = null;
        private static readonly object instanceLock = new object();
        private PostSkillDAO() { }

        public static PostSkillDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new PostSkillDAO();
                    }
                }
                return instance;
            }
        }

        public async void CreatePost(PostSkill pk)
        {
            try
            {
                PostSkill post = new PostSkill()
                {
                   PostId=pk.PostId,
                   SkillsId=pk.SkillsId

                };
                var context = new eRecruitment_PRN221Context();
                await context.PostSkills.AddAsync(post);
                if (context.SaveChanges() > 0)
                {
                    Console.WriteLine(DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "Created post skill successfully");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "Error at CreatePost skill: " + ex.Message);
            }
        }


    }
}
