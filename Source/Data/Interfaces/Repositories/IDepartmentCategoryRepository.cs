using Data.DTOs;
using System.Collections.Generic;

namespace Data.Interfaces.Repositories
{
    public interface IDepartmentCategoryRepository : IRepositoryBase<DepartmentCategory>
    {
        bool Add(DepartmentCategoryDTO req);

        bool Delete(int id, int userId);

        new List<DepartmentCategory> GetAll();

        List<DepartmentCategory> GetDetailById(int Id);

        DepartmentCategory GetByIdCustom(int Id);
        bool Update(DepartmentCategoryDTO model);
        List<DepartmentWiseCategoryDTO> GetCategoryById(int[] DepartmentId, LanguageFilterDTO lang);
    }
}
