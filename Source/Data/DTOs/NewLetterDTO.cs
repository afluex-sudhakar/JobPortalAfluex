using System;
using System.Collections.Generic;
using System.Web;

namespace Data.DTOs
{
    public class NewsLetterDTO : UserLogDTO
    {
        public int Id { get; set; }
       public string Email { get; set; }
    }
  
}
