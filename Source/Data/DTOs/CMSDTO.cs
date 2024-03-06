using System.Collections.Generic;
using Utility.Enums;
namespace Data.DTOs
{
    public class CMSDTO 
    {
        public Language language { get; set; }
        public string pageName { get; set; }
        public string Description { get; set; }
        public string HDescription { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string MobileNo { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public string Result { get; set; }
        public int Id { get; set; }
        public StaticContent StaticContent { get; set; }
        public List<StaticContent> lst { get; set; }
    }
    public class CMSResponseDTO
    {
        public string Text { get; set; }
    }
    public class ContactListDTO
    {
        public List<Feedback> lst { get; set; }
    }
    public class UpdateCMSDTO :CMSDTO
    {

    }
}
