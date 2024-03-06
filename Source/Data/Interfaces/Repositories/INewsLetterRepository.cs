using Data.DTOs;
using System.Collections.Generic;

namespace Data.Interfaces.Repositories
{
    public interface INewsLetterRepository : IRepositoryBase<NewsLetter>
    {
        bool Add(NewsLetterDTO req);
        
    }
}
