using Data.DTOs;
using System.Collections.Generic;

namespace Data.Interfaces.Repositories
{
    public interface IAudittrailRepository : IRepositoryBase<UserLog>
    {
        /*new List<UserLog> GetById(int currentPage)*/
        new List<UserLog> GetAll();
     
    }
}
