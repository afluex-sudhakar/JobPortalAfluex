using System.Collections.Generic;
namespace Data.DTOs
{
    public class DepartmentCategoryDTO : UserLogDTO
    {
        public int Id { get; set; }
        public int? DepartmentId { get; set; }
        public int? CategoryId { get; set; }
        public int[] Categories { get; set; }
        public List<DepartmentCategory> lst { get; set; }
        public DepartmentCategory dcMapping { get; set; }
        public int? AddedBy { get; set; }
        public List<DepartmentWiseCategoryDTO> DepartmentCategories { get; set; }

    }
    public class DepartmentWiseCategoryDTO
    {
        public int? Id { get; set; }
        public string Name { get; set; }
    }
}
