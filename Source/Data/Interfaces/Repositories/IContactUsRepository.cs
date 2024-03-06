using Data.DTOs;
using System.Collections.Generic;

namespace Data.Interfaces.Repositories
{
    public interface IContactUsRepository : IRepositoryBase<Category>
    {
        bool Add(CategoryDTO req);

        bool Update(UpdateCategoryDTO req);

        bool Delete(int id, int userId);

        new List<Category> GetAll();

        List<Category> GetDetailById(int Id);
    }
}
