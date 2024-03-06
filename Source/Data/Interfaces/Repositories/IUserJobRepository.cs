using Data.DTOs;
using System.Collections.Generic;
using Utility.Enums;

namespace Data.Interfaces.Repositories
{
    public interface IUserJobRepository : IRepositoryBase<UserJob>
    {
        List<UserJob> GetPlacementDetails(UserJobsDTO model);

        new List<UserJob> GetAll();

        List<UserJob> GetEmployerWisePlacement(int employerId);

        List<AppliedJobDTO> AppliedJobs(AppliedJobFilterDTO model);

        List<AppliedCandidate> GetApplyCandidate(EmpJobPostDTO model);

        List<AppliedCandidate> GetShortListedCandidate(EmpJobPostDTO model);

        List<UserJob> LocationWisePlacement(UserJobsDTO model);

        List<AppliedJobDTOWeb> AppliedJobsWeb(AppliedJobFilterDTOWeb model);

        bool GetAppliedStatusByUser(int UserId, int JobId);
        ChatUserJobDetailsDTO GetJobDetailsByJobId(int JobId, int userid);

        ChatUserJobDetailsDTO GetJobDetailsByJobId(int chatId, Language lang);
    }
}
