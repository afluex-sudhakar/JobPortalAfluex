using System.Collections.Generic;
namespace Data.DTOs
{
    public class CourseDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string NameH { get; set; }
        public List<Course> lst { get; set; }
        public Course course { get; set; }
    }

    public class UpdateCourseDTO : CourseDTO
    {
    }

    public class CourseResponseDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class CoursesResponseDTO
    {
        public List<CourseResponseDTO> Courses { get; set; }
    }
}
