using Data.DTOs;
using Data.Interfaces.Repositories;
using System.Collections.Generic;

namespace Data.Interfaces.Repositories
{
   
    public interface IAdminDashboardRepository : IRepositoryBase<Job>
    {
        AdminDashboardDTO GetDashboardDataForAdmin();
        List<AdminDashboardDTO> GetJobPostList();
        List<AdminDashboardDTO> GetJobAppliedList();
        BarChartDataDTO GetBarchartData();
        BarChartDataUserRegistrationsDTO GetBarchartDataUserRegistration();
    }
}
