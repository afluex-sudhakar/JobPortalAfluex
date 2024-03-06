using System.Collections.Generic;
namespace Data.DTOs
{
    public class AudittrailDTO
    {
        public int Id { get; set; }
        public string Remark { get; set; }
        public System.DateTime Date { get; set; }
        public List<UserLog> lst { get; set; }
        public UserLog UserLog { get; set; }
      
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int? PageCount { get; set; }
        public int CurrentPageIndex { get; set; }
    }

}
