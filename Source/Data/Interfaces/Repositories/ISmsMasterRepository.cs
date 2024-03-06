using Data.DTOs;
using System.Collections.Generic;

namespace Data.Interfaces.Repositories
{
    public interface ISmslMasterRepository : IRepositoryBase<SMSMaster>
    {
        bool Add(SmsMasterDTO req);
        
        new List<SMSMaster> GetAll();
      
    
    }
}
