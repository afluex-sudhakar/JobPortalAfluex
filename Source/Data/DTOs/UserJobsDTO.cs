using System.Collections.Generic;
namespace Data.DTOs
{
    public class UserJobsDTO
    {
        public int Id { get; set; }
        public int CityId { get; set; }
        public int JobId { get; set; }
        public int UserId { get; set; }
        public int JobRoleId { get; set; }
        public int TotalJobs { get; set; }
        public List<UserJob> lst { get; set; }
        public List<User> lstUser { get; set; }
        public List<Job> lstJob { get; set; }
        public List<UserDetail> lstUserDetail { get; set; }
    }

    public class UpdateUserJobsDTO : UserJobsDTO
    { 
    }

}
