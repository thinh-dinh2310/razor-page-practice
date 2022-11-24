using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class ApplicantPostDAO
    {
        private static ApplicantPostDAO instance = null;
        private static readonly object instanceLock = new object();
        private ApplicantPostDAO() { }

        public static ApplicantPostDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new ApplicantPostDAO();
                    }
                }
                return instance;
            }
        }

        public async void CreateApplicantPost(ApplicantPost apost)
        {
            try
            {
                apost.Count = 1;
                Console.WriteLine(apost.PostId);
                var context = new eRecruitment_PRN221Context();
                await context.ApplicantPosts.AddAsync(apost);
                if (context.SaveChanges() > 0)
                {
                    Console.WriteLine(DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "Created applicant post successfully");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "Error at Create applicantPost: " + ex.InnerException.Message);
            }
           
        }
    }
}
