using System.Collections.Generic;
namespace Data.DTOs
{
    public class SmsMasterDTO
    {
        public int Id { get; set; }
        public string To { get; set; }
        public string From { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string Email { get; set; }
        public bool IsTemplate { get; set; }
        public string Mobile { get; set; }
        public bool IsDeleted { get; set; }
        public string RecipientMobile { get; set; }
        public int UserId { get;set;}
        public string UserName { get; set; }
        public string EmailMessage { get; set; }
        public string SMS { get; set; }
        public List<SMSMaster> OldSmsList { get; set; }
    }
    
    //public class EmailMaster
    //{
       
    //}
}
