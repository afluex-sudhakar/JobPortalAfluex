using System;
using System.Collections.Generic;
using System.Web;
using Utility.Enums;
namespace Data.DTOs
{
    public class EnrollmentProgramDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string TitleH { get; set; }
        public string Description { get; set; }
        public string DescriptionH { get; set; }
        public int UserId { get; set; }
        public System.DateTime DateStart { get; set; }
        public System.DateTime DateEnd { get; set; }
        public System.DateTime LastDate { get; set; }
        public System.DateTime PublishDate { get; set; }
        public string Duration { get; set; }
        public bool IsDeleted { get; set; }
        public System.DateTime CreatedAt { get; set; }
        public System.DateTime ModifiedAt { get; set; }
        public System.DateTime DeletedAt { get; set; }
        public List<EnrollmentProgram> lst { get; set; }
        public EnrollmentProgram enProgram { get; set; }
        public HttpPostedFileBase postedImage { get; set; }
        public string Image { get; set; }
    }

    public class UpdateEnrollmentProgramDTO : EnrollmentProgramDTO
    {
      
    }
    public class EnrollmentProgramRequestDTO : UserLogDTO
    {
        public Language Language { get; set; }
    }
    public class EnrollmentProgramDTOWeb
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? PublishDate { get; set; }
        public DateTime? LastDate { get; set; }
        public string Image { get; set; }
        public bool AppliedUserStatus { get; set; }
    }
    public class EnrollmentProgramDTOMobile
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? PublishDate { get;  set; }
        public DateTime? LastDate { get; set; }
        public string Image { get; set; }
    }
    public class EnrollmentProgramResponseDTO
    {
        public List<EnrollmentProgramDTOMobile> lstProgram { get; set; }
    }
    public class EnrollmentApplyDTO : UserLogDTO
    {
        public int Id { get; set; }
        public int EnrollmentProgramId { get; set; }
        public Language Language { get; set; }
    }
    public class EnrollmentProgramResponseDTOWeb
    {
        public List<EnrollmentProgramDTOWeb> lstProgram { get; set; }
       
    }
}
