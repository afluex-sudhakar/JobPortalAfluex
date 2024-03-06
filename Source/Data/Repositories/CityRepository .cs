using Data.DTOs;
using Data.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using Utility;
using Utility.Enums;

namespace Data.Repositories
{
    public class CityRepository : RepositoryBase<City>, ICityRepository
    {
        public bool Add(CityDTO req)
        {
            try
            {
                DateTime createdAt = new Constants().IST_DATE_TIME;
                var tt = new City();
                tt.CreatedAt = createdAt;
                tt.IsDeleted = false;
                tt.Name = req.Name;
                tt.NameH = req.NameH;
                Add(tt);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Update(UpdateCityDTO req)
        {
            try
            {
                DateTime createdAt = new Constants().IST_DATE_TIME;
                var d = GetById(req.Id);
                if (d != null)
                {
                    d.ModifiedAt = createdAt;
                    d.Name = req.Name;
                    d.NameH = req.NameH;
                    Update(d);
                    return true;
                }
                else
                    return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Delete(int id, int userId)
        {
            try
            {
                var d = GetById(id);
                if (d != null)
                {
                    d.IsDeleted = true;
                    d.ModifiedAt = new Constants().IST_DATE_TIME;
                    Update(d);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public List<City> GetAll()
        {
            try
            {
                return GetAll(x => x.IsDeleted == false).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public List<City> GetDetailById(int Id)
        {
            try
            {
                return GetAll(x => x.Id == Id && x.IsDeleted == false).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public List<CityResponseDTO> GetCitiesMobile(CityRequestDTO req)
        {
            try
            {
                var data = GetAll(x => x.IsDeleted == false && x.Name.Contains(req.SearchText) && x.StateId==34).Select(x => new CityResponseDTO
                {
                    Id = x.Id,
                    Name = req.Language == Language.English ? x.Name : x.NameH
                }).ToList();
                return data;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        //public List<StateDTO> GetAllStates(Lanugage lang)
        //{
        //    return GetAll(x => x.IsDeleted == false).Select(x => new StateDTO { Name = lang == Lanugage.English ? x.State : x.StateH }).Distinct().ToList();
        //}

        //public List<StateDTO> GetAllCities(Lanugage lang)
        //{
        //    return GetAll(x => x.IsDeleted == false).Select(x => new StateDTO { Name = lang == Lanugage.English ? x.Name : x.NameH }).Distinct().ToList();
        //}

        public List<CityResponseDTO> GetLocations(CityRequestDTO req)
        {
            try
            {
                var data = db.PincodeMasters.Where(x => x.IsDeleted == false && x.CityId == 370 && x.Name.Contains(req.SearchText)).Select(x => new CityResponseDTO
                {
                    Id = x.Id,
                    Name = req.Language == Language.English ? x.Name : x.NameH
                }).ToList();
                return data;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
