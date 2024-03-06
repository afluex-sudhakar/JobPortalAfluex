using Data.DTOs;
using System.Collections.Generic;

namespace Data.Interfaces.Repositories
{
    public interface IEmpJobPostRespository : IRepositoryBase<Job>
    {
        bool Add(EmpJobPostDTO req);
     
        bool Update(UpdateJobPostDTO req);

        bool Delete(int id, int userId);

        new List<Job> GetAll();
        Job GetDetailById(int Id);
        List<Job> GetJobDetailById(int Id);
        List<Job> GetUserList(int Id);
        List<Job> GetShortListedCandidate(int Id);
        List<Job> GetAvailableJobs();
        bool PublishJob(int id, int v);
        List<EmpJobPostDTO> GetJobPostList(int Id);
        EmpDashboardDTO GetDashboardDataForEmployer(int Id);
     
        bool ShortList(int id);
    }
}
