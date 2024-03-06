using System.Collections.Generic;
namespace Data.DTOs
{
    public class JobRoleDTO 
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string NameH { get; set; }
        public List<JobRole> lst { get; set; }
        public JobRole jobRole { get; set; }

    }

    public class UpdateJobRoleDTO : JobRoleDTO
    { 
    }

}
