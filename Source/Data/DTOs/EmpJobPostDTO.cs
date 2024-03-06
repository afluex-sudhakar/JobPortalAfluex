using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using Utility.Enums;

namespace Data.DTOs
{
    public class EmpJobPostDTO : UserLogDTO
    {
        public int Id { get; set; }
        public List<Job> lst { get; set; }
        //public List<Job> lstCourse { get; set; }
        public string Title { get; set; }
        public int EmployerId { get; set; }
        public string Message { get; set; }
        public string HindiTitle { get; set; }
        public int? NoOfVacanices { get; set; }
        public Nullable<decimal> SalaryMin { get; set; }
        public Nullable<decimal> SalaryMax { get; set; }
        public string ExperienceMin { get; set; }
        public string ExperienceMax { get; set; }
        public System.DateTime PostedDate { get; set; }
        public Nullable<System.DateTime> LastDate { get; set; }
        public string ShortDescription { get; set; }
        public string HindiShortDescription { get; set; }
        public string Description { get; set; }
        public int? JobTypeId { get; set; }
        public int? JobRoleId { get; set; }
        public int? CategoryId { get; set; }
        public string JobRole { get; set; }
        public string Jobtype { get; set; }
        public string Category { get; set; }
        public string Qualification { get; set; }
        public int? CourseId { get; set; }
        public string StateId { get; set; }
        public int? CityId { get; set; }
        public int? LocationId { get; set; }
        public string City { get; set; }
        public int UserId { get; set; }
        public string Image { get; set; }
        public string Location { get; set; }
        public HttpPostedFileBase postedImage { get; set; }
        public System.DateTime FromDate { get; set; }
        public System.DateTime ToDate { get; set; }
        public string HindiDescription { get; set; }
        public List<JobLocation> lstJobLocation { get; set; }
        public List<UserJob> lstUserJob { get; set; }
        public User user { get; set; }
        public List<User> lstUser { get; set; }
        public UserDetail userdetail { get; set; }
        public Job job { get; set; }
        public JobQualification jobQualification { get; set; }
        public int?[] skills { get; set; }
        public int?[] Courses { get; set; }
        public List<JobskillDTO> jobSkill { get; set; }
        public List<JobsCourseDTO> JobCourses { get; set; }
        public List<UserSkillDTO> userSkill { get; set; }
        public JobLocation jobLocation { get; set; }
        public Language Language { get; set; }
        public List<AppliedCandidate> lstCandidate { get; set; }
        public int? DepartmentId { get; set; }
        public bool IsVerified { get; set; }
        public string UserName { get; set; }
    }
    public class JobskillDTO
    {
        public int? SkillId { get; set; }
        public string Skill { get; set; }
    }
    public class JobsCourseDTO
    {
        public int? CourseId { get; set; }
        public string Course { get; set; }
    }
    public class UpdateJobPostDTO : EmpJobPostDTO
    { 
    }
    
    public class JobList
    {
        public int Id { get; set; }
        public List<EmpJobPostDTO> jobList { get; set; }
    }
    public class AppliedCandidate :EmpJobPostDTO
    {
        public string Name { get; set; }
        public string userid { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string Location { get; set; }
       
    }
    public class EmpDashboardDTO 
    {
      public int TotalJobPosted { get; set; }
      public int TotalAppliedCandidates { get; set; }
      public int TotalShortListedCandidates { get; set; }
      public int Messages { get; set; }
    }


    public class ChatUserJobDetailsDTO
    {
        public string ExperienceMax { get;  set; }
        public string ExperienceMin { get;  set; }
        public string ShortDescription { get;  set; }
        public string JobLocation { get; set; }
        public string CompanyName { get; set; }
        public string Title { get;  set; }
        public string UserName { get;  set; }
    }
}
