using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTOs
{
    public class AdminDashboardDTO:Job
    {
     

        public int TotalJobSeekers { get; set; }
        public int TotalEmployeer { get; set; }
        public int TotalJobPosted { get; set; }
        public int TotalJobApplied { get; set; }
        public int? TotalVacancies { get; set; }
        public int TotalPlacement { get; set; }
        public int NoofApplicants { get; set; }
        public string Designation { get; set; }
        public string AppliedDate { get; set; }
        public List<AdminDashboardDTO> jobList { get; set; }
        public List<AdminDashboardDTO> RecentJobApplied { get; set; }
        public string BarCahrtData { get; set; }
    }

    public class BarChartDataDTO
    {
        public string[] Months { get; set; }
        public int?[] Vacancies { get; set; }
        public int?[] HiredJobSeekers { get; set; }
    }
    public class BarChartDataUserRegistrationsDTO
    {
        public string[] Months { get; set; }
        public int?[] WebUser { get; set; }
        public int?[] MobileUser { get; set; }
    }

}
