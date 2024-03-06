using Data.DTOs;
using System.Collections.Generic;

namespace Data.Interfaces.Repositories
{
    public interface ICityRepository : IRepositoryBase<City>
    {
        bool Add(CityDTO req);

        bool Update(UpdateCityDTO req);

        bool Delete(int id, int userId);

        new List<City> GetAll();

        List<City> GetDetailById(int Id);

        List<CityResponseDTO> GetCitiesMobile(CityRequestDTO req);
        //List<StateDTO> GetAllStates(Lanugage lang);
        //List<StateDTO> GetAllCities(Lanugage lang);

        List<CityResponseDTO> GetLocations(CityRequestDTO req);
    }
}
