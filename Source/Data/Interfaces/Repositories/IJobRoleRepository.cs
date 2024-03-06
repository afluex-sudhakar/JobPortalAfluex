using Data.DTOs;
using System.Collections.Generic;

namespace Data.Interfaces.Repositories
{
    public interface IJobRoleRepository : IRepositoryBase<JobRole>
    {
        bool Add(JobRoleDTO req);

        bool Update(UpdateJobRoleDTO req);

        bool Delete(int id, int userId);

        new List<JobRole> GetAll();

        List<JobRole> GetDetailById(int Id);

        JobRole GetJobRoleById(int Id);

        bool BlockJobRole(int id, int userId);

        bool UnBlockJobRole(int id, int userId);
    }
}
