using Data.DTOs;
using Data.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Web.Script.Serialization;
using Utility;
using Utility.Enums; 

namespace Data.Repositories
{

    public class AdminDashboardRespository : RepositoryBase<Job>, IAdminDashboardRepository
    {

        public AdminDashboardDTO GetDashboardDataForAdmin()
        {
            var TotalJobPosted = GetAll(x => x.IsDeleted == false && x.IsPublishd == true).Count();
            var TotalJobSeekers = db.Users.Where(x => x.IsDeleted == false && x.IsVerified == true && x.IsBlocked == false && x.RoleId == 3).Count();
            var TotalEmployeer = db.Users.Where(x => x.IsDeleted == false && x.IsVerified == true && x.IsBlocked == false && x.RoleId == 2).Count();
            var TotalVacancies = GetAll(x => x.IsDeleted == false && x.IsPublishd == true).Sum(x => x.NoOfVacancies);
            var TotalPlacements = db.UserJobs.Where(x => x.IsDeleted == false).Count();
            return new AdminDashboardDTO
            {
                TotalJobPosted = TotalJobPosted,
                TotalJobSeekers = TotalJobSeekers,
                TotalEmployeer = TotalEmployeer,
                TotalJobApplied = 0,
                TotalVacancies = TotalVacancies,
                TotalPlacement = TotalPlacements
            };
        }
        public List<AdminDashboardDTO> GetJobPostList()
        {
            return GetAll(x => x.IsDeleted == false).Select(x => new AdminDashboardDTO
            {

                Title = x.Title,
                JobRoleId = x.JobRoleId,
                ShortDescription = x.ShortDescription,
                Description = x.Description,
                SalaryMax = x.SalaryMax,
                SalaryMin = x.SalaryMin,
                ExperienceMax = x.ExperienceMax,
                ExperienceMin = x.ExperienceMin,
                LastDate = x.LastDate,
                PostedDate = x.PostedDate,
            }).Take(5).OrderByDescending(x => x.PostedDate).ToList();
        }
        public List<AdminDashboardDTO> GetJobAppliedList()
        {
            return GetAll(x => x.IsDeleted == false).Select(x => new AdminDashboardDTO
            {
                Title = x.Title,
                JobRoleId = x.JobRoleId,
                Designation = x.JobRole.Name,
                NoOfVacancies = x.NoOfVacancies,
                NoofApplicants = x.UserJobs.Where(xx => xx.IsDeleted == false).Select(xx => xx.UserId).Count(),
                LastDate = x.LastDate,
                PostedDate = x.PostedDate,
                AppliedDate = Convert.ToString(x.UserJobs.Select(xx=>xx.CreatedAt)),
            }).Take(5).OrderByDescending((xx => xx.CreatedAt)).ToList();
        }

        public BarChartDataDTO GetBarchartData()
        {
            try
            {
                BarChartDataDTO obj = new BarChartDataDTO();
                DateTime dt = new Constants().IST_DATE_TIME;
                obj.Months = Constants.MONTHS_ENGLISH.Split(',');
                var vacancies = db.Jobs.Where(x => x.IsDeleted == false && x.IsPublishd == true && EntityFunctions.TruncateTime(x.PostedDate) <= EntityFunctions.TruncateTime(dt)).Select(x => new { x.PostedDate, x.NoOfVacancies }).OrderBy(x => x.PostedDate).ToList();

                var jobseekerhired = db.Jobs.Where(x => x.IsDeleted == false && x.IsPublishd == true && EntityFunctions.TruncateTime(x.PostedDate) <= EntityFunctions.TruncateTime(dt)).Select(x => new { x.PostedDate, Hired = x.UserJobs.Where(xx => xx.IsDeleted == false && xx.Status == PlacementStatus.Placed).Count() }).OrderBy(x => x.PostedDate).ToList();

                int?[] vacency = new int?[obj.Months.Length];
                int?[] hired = new int?[obj.Months.Length];
                for (int i = 0; i < obj.Months.Length; i++)
                {
                    vacency[i] = vacancies.Where(x => obj.Months[x.PostedDate.Month-1] == obj.Months[i] && x.PostedDate.Year==dt.Year).Sum(x => x.NoOfVacancies);
                }

                for (int i = 0; i < obj.Months.Length; i++)
                {
                    hired[i] = jobseekerhired.Where(x => obj.Months[x.PostedDate.Month-1] == obj.Months[i] && x.PostedDate.Year == dt.Year).Sum(x => x.Hired);
                }
                obj.HiredJobSeekers = hired;
                obj.Vacancies = vacency; 
                return obj;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public BarChartDataUserRegistrationsDTO GetBarchartDataUserRegistration()
        {
            try
            {
                BarChartDataUserRegistrationsDTO obj = new BarChartDataUserRegistrationsDTO();
                DateTime dt = new Constants().IST_DATE_TIME;
                obj.Months = Constants.MONTHS_ENGLISH.Split(',');
                var MobileUser = db.Users.Where(x => x.IsDeleted == false && x.Mode ==DeviceType.Mobile && x.RoleId == 3 && EntityFunctions.TruncateTime(x.CreatedAt) <= EntityFunctions.TruncateTime(dt)).Select(x => new { x.CreatedAt, x.Id}).OrderBy(x => x.CreatedAt).ToList();

                var WebUser = db.Users.Where(x => x.IsDeleted == false && x.Mode == DeviceType.Web && x.RoleId == 3 && EntityFunctions.TruncateTime(x.CreatedAt) <= EntityFunctions.TruncateTime(dt)).Select(x => new { x.CreatedAt }).OrderBy(x => x.CreatedAt).ToList();

                int?[] Mobile = new int?[obj.Months.Length];
                int?[] Web = new int?[obj.Months.Length];
                for (int i = 0; i < obj.Months.Length; i++)
                {
                    Mobile[i] = MobileUser.Where(x => obj.Months[x.CreatedAt.Month - 1] == obj.Months[i] && x.CreatedAt.Year == dt.Year).Count();
                }

                for (int i = 0; i < obj.Months.Length; i++)
                {
                    Web[i] = WebUser.Where(x => obj.Months[x.CreatedAt.Month - 1] == obj.Months[i] && x.CreatedAt.Year == dt.Year).Count();
                }
                obj.MobileUser = Mobile;
                obj.WebUser = Web;
                return obj;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}