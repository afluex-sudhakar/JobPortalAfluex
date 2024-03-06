using System.Collections.Generic;
namespace Data.DTOs
{
    public class CityDTO
    {
        public int Id { get; set; }
        public int StateId { get; set; }
        public string Name { get; set; }
        public string NameH { get; set; }
        public List<City> lst { get; set; }
    }

    public class UpdateCityDTO : CityDTO
    {
    }


    //public class StateDTO
    //{
    //    public string Name { get; set; }
    //}


    public class CityStateDTO
    {
        public string State { get; set; }
        public string City { get; set; }
        public int? CityId { get; set; }
        public int? StateId { get; set; }
    }
    public class CityRequestDTO : LanguageFilterDTO
    {
        public string SearchText { get; set; }
    }
    public class CityResponseDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class CitiesResponseDTO
    {
        public List<CityResponseDTO> Cities { get; set; }
    }
}
