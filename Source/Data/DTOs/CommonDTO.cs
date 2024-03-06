using System.Collections.Generic;
using Utility.Enums;

namespace Data.DTOs
{

    public class IdFilterDTO
    {
        public int Id { get; set; }
        public Language Language { get; set; }
    }
    public class LanguageFilterDTO
    {
        public Language Language { get; set; }
    }

    public class UserIdFilterDTO
    {
        public int UserId { get; set; }
        public Language Language { get; set; }
    }


    public class PinCodeRequestDTO : LanguageFilterDTO
    {
        public int PinCode { get; set; }
    }

    public class PinCodeDTO
    {
        public int Id { get; set; }
        public int? PinCode { get; set; }
        public string Name { get; set; }
        public string NameH { get; set; }
        public int CityId { get; set; }
        public List<PincodeMaster> lst { get; set; }
        public PincodeMaster pinCodeMaster { get; set; }
    }
}
