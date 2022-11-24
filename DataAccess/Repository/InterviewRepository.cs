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
    public class InterviewRepository : IInterviewRepository
    {
        public void CreateInterview(Interview createInterview)
            => InterviewDAO.Instance.CreateInterview(createInterview);
        public void DeleteInterviewById(Interview deleteInterview) => InterviewDAO.Instance.DeleteInterview(deleteInterview);
        public Task<IEnumerable<Interview>> GetAllInterview() => InterviewDAO.Instance.GetAllInterview();
        public Task<IEnumerable<Interview>> GetAllInterviewByPostId(Guid postId) => InterviewDAO.Instance.GetAllInterviewByPostId(postId);
        public Task<Interview> GetInterviewById(Guid interviewerId, int round, Guid postId, Guid applicantId) => InterviewDAO.Instance.GetInterviewById(interviewerId, round, postId, applicantId);
        public void UpdateInterviewById(Interview updateInterview)
            => InterviewDAO.Instance.UpdateInterviewById(updateInterview);
    }
}
