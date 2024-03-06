using System;
using System.Collections.Generic;
using Utility.Enums;

namespace Data.DTOs
{
    public class JobDTO
    {
        public int Id { get; set; }
        public string Skill { get; set; }
        public string City { get; set; }
        public string Title { get; set; }
        public string TitleH { get; set; }
        public string ShortDescription { get; set; }
        public string ShortDescriptionH { get; set; }
        public string Description { get; set; }
        public string DescriptionH { get; set; }
        public string SalaryMin { get; set; }
        public string SalaryMax { get; set; }
        public bool IsMonthly { get; set; }
        public string ExperienceMin { get; set; }
        public string ExperienceMax { get; set; }
        public bool IsPublishd { get; set; }
        public bool IsVerified { get; set; }
        public string Image { get; set; }
        public bool IsDeleted { get; set; }
        public List<Job> lst { get; set; }
        public List<SkillDTO> lstSkill { get; set; }
        public List<City> lstCity { get; set; }
        public List<User> lstEmployer { get; set; }
        public List<UserDetail> lstEmployerDetail { get; set; }
        public List<Category> lstCategory { get; set; }
        public List<Course> lstCourse { get; set; }
        public int LanguageId { get; set; }
        public string EmployerName { get; set; }
        public int CategoryId { get; set; }
        public int CourseId { get; set; }
        public int EmployerId { get; set; }
        public int CityId { get; set; }
        public int SkillId { get; set; }
        public string SearchTerm { get; set; }
        public int PageSize { get; set; }
    }

    public class UpdateJobDTO : JobDTO
    {
    }
    public class JobReposneDTO
    {
        public int TotalRecords { get; set; }
        public List<JobFilteredDTO> ListJob { get; set; }
    }

    public class JobFilteredDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public string CompanyName { get; set; }
        public int? NoOfVacancies { get; set; }
        public Nullable<decimal> SalaryMin { get; set; }
        public Nullable<decimal> SalaryMax { get; set; }
        public bool IsMonthly { get; set; }
        public string ExperienceMin { get; set; }
        public string Time { get; set; }
        public string ExperienceMax { get; set; }
        public string[] Skill { get; set; }
        public string Course { get; set; }
        //[DataMember(IsRequired = false, EmitDefaultValue = false)]
        public System.DateTime? PostedDate { get; set; }
        public Nullable<System.DateTime> LastDate { get; set; }
        public string Image { get; set; }
        public Nullable<int> CityId { get; set; }
        public string City { get; set; }
        public Nullable<int> CategoryId { get; set; }
        public string Category { get; set; }
        public Nullable<int> JobRoleId { get; set; }
        public string JobRole { get; set; }
        public Nullable<int> JobTypeId { get; set; }
        public string JobType { get; set; }
        public string Qualification { get; set; }
        public string Department { get; set; }
        public string About { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Contact { get; set; }
        public string Location { get; set; }
        //public JobReposneDTO Job{get; set;} 
    }
    public class UserJobDTO : JobDTO
    {
        public int UserId { get; set; }
    }

    public class JobApplyDTO : UserLogDTO
    {
        public int Id { get; set; }
        public int JobId { get; set; }
    }
    public class AppliedJobDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ShortDescription { get; set; }
        public string Company { get; set; }
        public DateTime Date { get; set; }
    }

    public class AppliedJobFilterDTO : UserLogDTO
    {
        public int Page { get; set; }
        public int Id { get; set; }
        public Language Language { get; set; }
    }

    public class AppliedJobResponseDTO
    {
        public List<AppliedJobDTO> ListJob { get; set; }
    }

    public class JobSearchFilterDTO : UserLogDTO
    {
        public int? Id { get; set; }
        public Language Language { get; set; }
        public int Page { get; set; }
        public int[] CategoryId { get; set; }
        public int[] SkillId { get; set; }
        public int[] CityId { get; set; }
        public int[] CourseId { get; set; }
        public string City { get; set; }
        public Decimal SalaryMin { get; set; }
        public Decimal SalaryMax { get; set; }
        public string SearchTerm { get; set; }
        public string SortBy { get; set; }
    }

    public class JOBDetailDTO : UserLogDTO
    {
        public int Id { get; set; }
        public Language Language { get; set; }
    }
    public class JobDTOWeb : UserLogDTO
    {
        public int Id { get; set; }
        public string Skill { get; set; }
        public string City { get; set; }
        public string Title { get; set; }
        public string TitleH { get; set; }
        public string ShortDescription { get; set; }
        public string ShortDescriptionH { get; set; }
        public string Description { get; set; }
        public string DescriptionH { get; set; }
        public string SalaryMin { get; set; }
        public string SalaryMax { get; set; }
        public bool IsMonthly { get; set; }
        public string ExperienceMin { get; set; }
        public string ExperienceMax { get; set; }
        public bool IsPublishd { get; set; }
        public bool IsVerified { get; set; }
        public string Image { get; set; }
        public bool IsDeleted { get; set; }
        public List<Job> lst { get; set; }
        public List<SkillDTO> lstSkill { get; set; }
        public List<City> lstCity { get; set; }
        public List<PincodeMaster> lstLocation { get; set; }
        public List<User> lstEmployer { get; set; }
        public List<UserDetail> lstEmployerDetail { get; set; }
        public List<Category> lstCategory { get; set; }
        public List<Department> lstDepartment { get; set; }
        public List<DepartmentWiseCategoryDTO> lstDepCategories { get; set; }
        public List<Course> lstCourse { get; set; }
        public int LanguageId { get; set; }
        public string EmployerName { get; set; }
        public int CategoryId { get; set; }
        public int[] DepartmentId { get; set; }
        public int Department_Id { get; set; }
        public int CourseId { get; set; }
        public int EmployerId { get; set; }
        public int CityId { get; set; }
        public int SkillId { get; set; }
        public string SearchTerm { get; set; }
        public int PageSize { get; set; }
        public string[] Skills { get; set; }
        public JobReposneDTOWeb JobResponse { get; set; }
        public JobFilteredDTOWeb job { get; set; }
        public RecentJobsDTO RecentJobs { get; set; }
        public bool AppliedUserStatus { get; set; }
    }
    public class JOBDetailDTOWeb : UserLogDTO
    {
        public int Id { get; set; }
        public Language Language { get; set; }
    }
    public class RecentJobsDTO : UserLogDTO
    {
        public int Id { get; set; }
        public Language Language { get; set; }
        public string Title { get; set; }
    }
    public class JobSearchFilterDTOWeb : UserLogDTO
    {
        public int? Id { get; set; }
        public Language Language { get; set; }
        public int Page { get; set; }
        public int[] CategoryId { get; set; }
        public int[] DepartmentId { get; set; }
        public int[] SkillId { get; set; }
        public int[] CityId { get; set; }
        public int[] CourseId { get; set; }
        public int EmployerId { get; set; }
        public int LocationId { get; set; }
        public string City { get; set; }
        public Decimal SalaryMin { get; set; }
        public Decimal SalaryMax { get; set; }
        public string SearchTerm { get; set; }
        public string SortBy { get; set; }
    }
    public class JobReposneDTOWeb
    {
        public int TotalRecords { get; set; }
        public List<JobFilteredDTOWeb> ListJob { get; set; }
    }
    public class JobFilteredDTOWeb
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public string CompanyName { get; set; }
        public string About { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Contact { get; set; }
        public int? NoOfVacancies { get; set; }
        public Nullable<decimal> SalaryMin { get; set; }
        public Nullable<decimal> SalaryMax { get; set; }
        public bool IsMonthly { get; set; }
        public string ExperienceMin { get; set; }
        public string Time { get; set; }
        public string ExperienceMax { get; set; }
        public string[] Skill { get; set; }
        public int[] SkillId { get; set; }
        public string Course { get; set; }
        //[DataMember(IsRequired = false, EmitDefaultValue = false)]
        public System.DateTime? PostedDate { get; set; }
        public Nullable<System.DateTime> LastDate { get; set; }
        public string Image { get; set; }
        public Nullable<int> CityId { get; set; }
        public string City { get; set; }
        public Nullable<int> CategoryId { get; set; }
        public string Category { get; set; }
        public Nullable<int> JobRoleId { get; set; }
        public string JobRole { get; set; }
        public Nullable<int> JobTypeId { get; set; }
        public string JobType { get; set; }
        public string Qualification { get; set; }
        public string Department { get; set; }
        public Nullable<int> DepartmentId { get; set; }

        public string PostedBy { get; set; }
        public int? PinCode { get;  set; }
        public string Location { get;  set; }
        public string State { get;  set; }
        //public JobReposneDTO Job{get; set;} 
    }
    public class RecentJobRequestDTO : UserLogDTO
    {
        public Language Language { get; set; }
    }
    public class AppliedJobDTOWeb
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ShortDescription { get; set; }
        public string Company { get; set; }
        public DateTime Date { get; set; }
        public string JobType { get; set; }
        public string JobRole { get; set; }
        public int? JobId { get; set; }
        public string Time { get; set; }
    }

    public class AppliedJobFilterDTOWeb : UserLogDTO
    {
        public int Page { get; set; }
        public int Id { get; set; }
        public Language Language { get; set; }
    }

    public class AppliedJobResponseDTOWeb
    {
        public List<AppliedJobDTOWeb> ListJob { get; set; }
    }
}
