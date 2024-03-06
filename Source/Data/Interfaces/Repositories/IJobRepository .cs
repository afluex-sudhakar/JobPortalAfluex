using Data.DTOs;
using System.Collections.Generic;

namespace Data.Interfaces.Repositories
{
    public interface IJobRepository : IRepositoryBase<Job>
    {
        bool Add(JobDTO req);

        bool Update(UpdateJobDTO req);

        bool Delete(int id, int userId);

        new List<Job> GetAll();

        JobReposneDTO SearchJob(JobSearchFilterDTO req);

        JobReposneDTOWeb SearchJobWeb(JobSearchFilterDTOWeb req);

        JobFilteredDTO GetCustomById(JOBDetailDTO req);

        int ApplyJob(JobApplyDTO req);

        JobFilteredDTOWeb GetCustomByIdWeb(JOBDetailDTOWeb req);
        
        List<Job> GetAvailableJobs();

        JobReposneDTO GetRecentJobs(RecentJobRequestDTO model);

        JobReposneDTOWeb GetRecentJobsWeb(RecentJobRequestDTO model);

    }
}
