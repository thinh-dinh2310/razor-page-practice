using BusinessObject;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class InterviewDAO
    {
        private static InterviewDAO instance = null;
        private static readonly object instanceLock = new object();

        private InterviewDAO() { }

        public static InterviewDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new InterviewDAO();
                    }
                }
                return instance;
            }
        }

        public async Task<IEnumerable<Interview>> GetAllInterview()
        {
            List<Interview> list = new List<Interview>();
            try
            {
                var context = new eRecruitment_PRN221Context();
                list = await context.Interviews
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "Error at GetAllInterviews: " + ex.Message);
            }
            return list;
        }

        public async Task<Interview> GetInterviewById(Guid interviewerId, int round, Guid postId, Guid applicantId)
        {
            Interview tmp = new Interview();
            try
            {
                var context = new eRecruitment_PRN221Context();
                tmp = await context.Interviews
                    .FirstOrDefaultAsync(p => p.InterviewerId == interviewerId && p.Round == round && p.PostId == postId && p.ApplicantId == applicantId);
            }
            catch (Exception ex)
            {
                Console.WriteLine(DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "Error at GetInterviewById: " + ex.Message);
            }
            return tmp;
        }

        public async Task<IEnumerable<Interview>> GetAllInterviewByPostId(Guid postId)
        {
            List<Interview> list = new List<Interview>();
            try
            {
                var context = new eRecruitment_PRN221Context();
                list = await context.Interviews
                    .Include(p => p.Interviewer)
                    .Include(p => p.ApplicantPost)
                    .Where(p => p.ApplicantPost.PostId == postId)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "Error at GetAllInterviewByPostId: " + ex.Message);
            }
            return list;
        }

        public async void CreateInterview(Interview createInterview)
        {
            try
            {
                var context = new eRecruitment_PRN221Context();
                await context.Interviews.AddAsync(createInterview);
                if (context.SaveChanges() > 0)
                {
                    Console.WriteLine(DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "Created user successfully");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "Error at CreateInterview: " + ex.Message);
            }
        }

        public async void UpdateInterviewById(Interview updateInterview)
        {
            try
            {
                var context = new eRecruitment_PRN221Context();
                context.Interviews.Update(updateInterview);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "Error at UpdateInterview: " + ex.Message);
            }
        }

        public async void DeleteInterview(Interview deleteInterview)
        {
            try
            {
                var context = new eRecruitment_PRN221Context();
                context.Interviews.Remove(deleteInterview);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "Error at DeleteInterviewById: " + ex.Message);
            }
        }
    }
}
