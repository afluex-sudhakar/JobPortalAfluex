using Data.DTOs;
using System.Collections.Generic;

namespace Data.Interfaces.Repositories
{
    public interface IEmailMasterRepository : IRepositoryBase<EmailMaster>
    {
        bool Add(EmailMasterDTO req);
        
        new List<EmailMaster> GetAll();
       
    
    }
}
