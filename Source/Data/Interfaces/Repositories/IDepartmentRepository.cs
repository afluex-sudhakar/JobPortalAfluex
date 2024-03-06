using Data.DTOs;
using System.Collections.Generic;

namespace Data.Interfaces.Repositories
{
    public interface IDepartmentRepository : IRepositoryBase<Department>
    {
        bool Add(UpdateDepartmentDTO req);

        bool Update(UpdateDepartmentDTO req);

        bool Delete(int id, int userId);

        new List<Department> GetAll();

        List<Department> GetDetailById(int Id);

        Department GetByIdCustom(int Id);

        bool AddOrUpdateMapping(DepartmentCategoryDTO req);
    }
}
