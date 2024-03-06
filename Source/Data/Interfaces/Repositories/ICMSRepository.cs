using Data.DTOs;
using System.Collections.Generic;

namespace Data.Interfaces.Repositories
{
    public interface ICMSRepository
    {
        bool Add(CMSDTO req);
        bool Update(CMSDTO req);
        bool Delete(int id, int userId);
        StaticContent GetPageNameById(int Id);
        new List<StaticContent> GetAll();
        StaticContent GetPageDetailsByPageName(string pageName);
        //List<City> GetDetailById(int Id);
    }
}
