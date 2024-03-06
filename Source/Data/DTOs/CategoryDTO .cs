using System.Collections.Generic;
namespace Data.DTOs
{
    public class CategoryDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string NameH { get; set; }
        public List<Category> lst { get; set; }
        public Category category { get; set; }
    }

    public class UpdateCategoryDTO : CategoryDTO
    { 
    }

}
