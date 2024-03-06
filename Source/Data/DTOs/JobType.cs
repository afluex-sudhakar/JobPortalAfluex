using System.Collections.Generic;
namespace Data.DTOs
{
    public class JobTypeDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string NameH { get; set; }
        public List<JobType> lst { get; set; }
    }

    public class UpdateJobTypeDTO : JobTypeDTO
    { 
    }

}
