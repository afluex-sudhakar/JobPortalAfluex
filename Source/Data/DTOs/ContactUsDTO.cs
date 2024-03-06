using System.Collections.Generic;
namespace Data.DTOs
{
    public class ContactUsDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string NameH { get; set; }
        public List<Category> lst { get; set; }
    }

    public class UpdateContactUsDTO : ContactUsDTO
    {
        public int Id { get; set; }
    }

}
