using System.Collections.Generic;
namespace Data.DTOs
{
    public class DepartmentDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string NameH { get; set; }
        public List<Department> lst { get; set; }
        public Department Department { get; set; }
    }

    public class UpdateDepartmentDTO : CategoryDTO
    { 
    }

}
