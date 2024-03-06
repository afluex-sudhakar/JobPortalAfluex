using System;
using System.Collections.Generic;
using System.Web;

namespace Data.DTOs
{
    public class UserDTO : UserLogDTO
    {
        public string PassingYear { get; set; }
        public string Designation { get; set; }
        public string University { get; set; }
        public string SkillName { get; set; }
        public int SkillId { get; set; }
        public string Resume { get; set; }
        public string Result { get; set; }
        public int UserDetailId { get; set; }
        public string Body { get; set; }
        public string Response { get; set; }
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string TemporaryPassword { get; set; }
        public string ConfirmPassword { get; set; }
        public string OldPassword { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string Mobile2 { get; set; }
        public string MobileNo { get; set; }
        public string Gender { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string FatherName { get; set; }
        public string MotherName { get; set; }
        public string SpouseName { get; set; }
        public List<City> lstCity { get; set; }
        public string HusbandName { get; set; }
        public string Subject { get; set; }
        public string EmailMessage { get; set; }
        public string Name { get; set; }
        public DateTime? DOB { get; set; }
        public int Age { get; set; }
        public int PinCode { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string Photo { get; set; }
        public string CompanyName { get; set; }
        public string ExDesignation { get; set; }
        public string ExCompany { get; set; }
        public string ContactPersonName { get; set; }
        public string OfficialEmailId { get; set; }
        public int NoOfEmployees { get; set; }
        public string About { get; set; }
        public int CityId { get; set; }
        public string Logo { get; set; }
        public int RoleId { get; set; }
        public Nullable<int> CourseId { get; set; }
        public Nullable<int> JobRoleId { get; set; }
        public string YearFrom { get; set; }
        public string YearTo { get; set; }
        public string MarksObt { get; set; }
        public string TotalMarks { get; set; }
        public bool IsDeleted { get; set; }
        public string CompanyType { get; set; }
        public User User { get; set; }
        public UserDetail UserDetail { get; set; }
        public List<UserDocument> lstUserDocument { get; set; }
        public List<EducationDetail> Lsteducation { get; set; }
        public List<UserSkill> LstSkills { get; set; }
        public List<UserEducation> ListEducation { get; set; }
        public List<UserExperience> ListExperience { get; set; }
        public List<UpdateUserDTO> UserListEducation { get; set; }
        public decimal ExperienceMax { get; set; }
        public List<Course> CourseList { get; set; }
        public HttpPostedFileBase postedFile { get; set; }
        public HttpPostedFileBase postedPan { get; set; }
        public HttpPostedFileBase postedAadhar { get; set; }
        public HttpPostedFileBase postedCompanyCertificate { get; set; }
        public List<UserListForEmailDTO> lstUsers { get; set; }
        public int? LocationId { get; set; }
    }
    public class UpdateUserDTO : UserDTO
    {
        public string skills { get; set; }
        public decimal ProfilePercent { get; set; }
        public string PANNo { get; set; }
        public string AadharNo { get; set; }
        public string PAN { get; set; }
        public string Aadhar { get; set; }
        public string CompanyCertificate { get; set; }
        public JobReposneDTOWeb JobResponse { get; set; }
        //public int Id { get; set; }
    }
    public class UserRegistrationResponseDTO
    {
        public int UserId { get; set; }
    }
    public class UserRegistrationDTO : UserLogDTO
    {
        public string MobileNo { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FCMId { get; set; }
    }
    public class RegisterDeviceDTO : UserLogDTO
    {
        public string FCMId { get; set; }
        public string MobileNo { get; set; }
        public int UserId { get; set; }
    }
    public class UPdateProfileDTO : UserLogDTO
    {
        public string About { get; set; }
        public int Age { get; set; }
        public int? CityId { get; set; }
        public DateTime? DOB { get; set; }
        public string Email { get; set; }
        public string Extension { get; set; }
        public string FatherName { get; set; }
        public string FirstName { get; set; }
        public string Gender { get; set; }
        public string HusbandName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string Mobile { get; set; }
        public string Mobile2 { get; set; }
        public string MotherName { get; set; }
        public string Photo { get; set; }
        public int? PinCode { get; set; }
        public string SpouseName { get; set; }
        public int UserId { get; set; }
    }
    public class UploadDocumentDTO : UserLogDTO
    {
        public string Extension { get; set; }
        public string File { get; set; }
        public string Name { get; set; }
        public int UserId { get; set; }
        public DocumentType DocumentType { get; set; }
    }
    public class UserLoginDTO : UserLogDTO
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }
        public string Response { get; set; }
    }
    public class UserLoginMobileDTO : UserLogDTO
    {
        public string MobileNo { get; set; }
        public string Password { get; set; }
        public string FCMId { get; set; }
    }

    public class UserLoginResponseDTO
    {
        public decimal ProfileFillPercent { get; set; }
        public int Id { get; set; }
        public string FirstName { get; set; }
        //public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string About { get; set; }
        public string Address { get; set; }
        public int Age { get; set; }
        public int? CityId { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public DateTime? DOB { get; set; }
        public string Email { get; set; }
        public string FatherName { get; set; }
        public string Gender { get; set; }
        public string HusbandName { get; set; }
        public string MobileNo { get; set; }
        public string Mobile2 { get; set; }
        public string MotherName { get; set; }
        public string Photo { get; set; }
        public string Resume { get; set; }
        public int? PinCode { get; set; }
        public string SpouseName { get; set; }
        public string CompanyName { get; set; }
        public string Designation { get; set; }
        public List<UserSkillDTO> Skills { get; set; }
        public List<EducationRespDTO> Educations { get; set; }
        public List<ExperienceRespDTO> Experiences { get; set; }
        //public int? RoleId { get; set; }
    }
    public class UserLoginResponseWEBDTO
    {
        public int Id { get; set; }
        public int? RoleId { get; set; }
    }
    public class UserProfileDTO : UserLoginResponseDTO
    {
        public string Skills { get; set; }
        public string Education { get; set; }
    }
    public class GenerateOTPDTO : UserLogDTO
    {
        public string MobileNo { get; set; }
    }
    public class ValidateOTPDTO : GenerateOTPDTO
    {
        public string OTP { get; set; }
    }
    public class UserProfileResponseDTO : UserLoginResponseDTO
    {
    }
    public class UserRequestDTO : UserLogDTO
    {
        public int UserId { get; set; }
    }
    public interface IUserLogDTO
    {
        string Address { get; set; }
        string DeviceId { get; set; }
        string DeviceOtherInfo { get; set; }
        string Lat { get; set; }
        string Long { get; set; }
        string DeviceType { get; set; }
        string IP { get; set; }
        string Domain { get; set; }
        string UserAgent { get; set; }
        string OS { get; set; }
    }
    public class UserLogDTO : IUserLogDTO
    {
        public string Address { get; set; }
        public string DeviceId { get; set; }
        public string DeviceOtherInfo { get; set; }
        public string Lat { get; set; }
        public string Long { get; set; }
        public string DeviceType { get; set; }
        public string IP { get; set; }
        public string Domain { get; set; }
        public string UserAgent { get; set; }
        public string OS { get; set; }
    }
    public class UserDetailResponseDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MobileNo { get; set; }
        public string Email { get; set; }
        public string Photo { get; set; }

    }

    public class UserSkillDTO
    {
        public int? Id { get; set; }
        public string Name { get; set; }
    }
    public class ChangePasswordDTO
    {
        public int Id { get; set; }
        public string MobileNo { get; set; }
        public string ConfirmPassword { get; set; }
        public string OldPassword { get; set; }
        public string Password { get; set; }
        public string UserAgent { get; set; }
        public string DeviceType { get; set; }
    }
    public class ForgetPasswordDTO : UserLogDTO
    {
        public string MobileNo { get; set; }
    }

    public class ResetPasswordDTO : ForgetPasswordDTO
    {
        public string TempPassword { get; set; }
        public string Password { get; set; }
    }

    public class ChangePasswordMobileDTO : UserLogDTO
    {
        public int Id { get; set; }
        public string OldPassword { get; set; }
        public string Password { get; set; }
    }

    public class UPdateProfilePersonalDetailDTO : UserLogDTO
    {
        public int Id { get; set; }
        //public int Age { get; set; }
        public int? CityId { get; set; }
        public DateTime? DOB { get; set; }
        public string Email { get; set; }
        public string FatherName { get; set; }
        public string FirstName { get; set; }
        public string Gender { get; set; }
        //public string HusbandName { get; set; }
        public string LastName { get; set; }
        //public string MiddleName { get; set; } 
        public string Mobile2 { get; set; }
        public string MotherName { get; set; }
        public int? PinCode { get; set; }
        public string PermanentAddress { get; set; }
    }

    public class UPdateProfileCompanyDetailDTO : UserLogDTO
    {
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public string Designation { get; set; }
    }

    public class UPdateProfileAboutDTO : UserLogDTO
    {
        public int Id { get; set; }
        public string About { get; set; }
    }

    public class UpdateProfileSkillsDTO : UserLogDTO
    {
        public int Id { get; set; }
        public int[] SkillIds { get; set; }
    }

    public class UpdateProfileEducationDTO : UserLogDTO
    {
        public int Id { get; set; }
        public List<EducationDTO> Educations { get; set; }

    }

    public class EducationDTO
    {
        public int CourseId { get; set; }
        public string College { get; set; }
        public int YearOfPassing { get; set; }
    }

    public class EducationRespDTO
    {
        public int? CourseId { get; set; }
        public string Course { get; set; }
        public string College { get; set; }
        public string YearOfPassing { get; set; }
    }
    public class UpdateProfileExperienceDTO : UserLogDTO
    {
        public int Id { get; set; }
        public List<ExperienceDTO> Experiences { get; set; }

    }

    public class ExperienceDTO
    {
        public string Company { get; set; }
        public string Designation { get; set; }
        public int YearFrom { get; set; }
        public int YearTo { get; set; }
        public bool IsCurrent { get; set; }
    }

    public class ExperienceRespDTO
    {
        public string Company { get; set; }
        public string Designation { get; set; }
        public string YearFrom { get; set; }
        public string YearTo { get; set; }
        public bool IsCurrent { get; set; }
    }
    public class UserProfileReqDTO : UserLogDTO
    {
        public int Id { get; set; }
    }

    public class UserLogoutDTO : UserLogDTO
    {
        public int Id { get; set; }
    }
    public class DTO : UserLogDTO
    {
        public int Id { get; set; }
    }
    public class UserListDTO
    {
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public List<User> Users { get; set; }
    }
   
    public class UserListForEmailDTO
    {
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public int? UserId { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Mobile { get; set; }
    }

}
