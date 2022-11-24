using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository.Interface
{
    public interface IInterviewRepository
    {
        Task<IEnumerable<Interview>> GetAllInterview();
        Task<Interview> GetInterviewById(Guid interviewerId, int round, Guid postId, Guid applicantId);
        Task<IEnumerable<Interview>> GetAllInterviewByPostId(Guid postId);
        void CreateInterview(Interview createInterview);
        void UpdateInterviewById(Interview updateInterview);
        void DeleteInterviewById(Interview deleteInterview);
    }
}
