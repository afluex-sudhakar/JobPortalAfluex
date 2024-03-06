using Data.DTOs;
using System.Collections.Generic;

namespace Data.Interfaces.Repositories
{
    public interface ICourseRepository : IRepositoryBase<Course>
    {
        bool Add(CourseDTO req);

        bool Update(UpdateCourseDTO req);

        bool Delete(int id, int userId);

        new List<Course> GetAll();
        List<Course> GetDetailById(int Id);


        List<CourseResponseDTO> GetCoursesMobile(LanguageFilterDTO req);

        Course GetCourseById(int Id);
    }
}
