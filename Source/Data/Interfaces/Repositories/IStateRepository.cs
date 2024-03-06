using Data.DTOs;
using System.Collections.Generic;

namespace Data.Interfaces.Repositories
{
    public interface IStateRepository : IRepositoryBase<State>
    {
        bool Add(StateDTO req);

        bool Update(UpdateStateDTO req);

        bool Delete(int id, int userId);

        new List<State> GetAll();

        List<State> GetDetailById(int Id);
    }
}