using Data.DTOs;
using System.Collections.Generic;

namespace Data.Interfaces.Repositories
{
    public interface IJobTypeRepository : IRepositoryBase<JobType>
    {
        bool Add(JobTypeDTO req);

        bool Update(UpdateJobTypeDTO req);

        bool Delete(int id, int userId);

        new List<JobType> GetAll();

        List<JobType> GetDetailById(int Id);
    }
}
