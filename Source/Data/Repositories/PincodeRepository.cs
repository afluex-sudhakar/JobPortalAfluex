using Data.DTOs;
using Data.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using Utility;
using Utility.Enums;

namespace Data.Repositories
{
    public class PincodeRepository : RepositoryBase<PincodeMaster>, IPincodeRepository
    {
        public bool Add(PinCodeDTO req)
        {
            try
            {
                DateTime createdAt = new Constants().IST_DATE_TIME;
                var tt = new PincodeMaster();
                tt.CreatedAt = createdAt;
                tt.CityId = req.CityId;
                tt.IsDeleted = false;
                tt.PinCode = req.PinCode;
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

        public bool Update(PinCodeDTO req)
        {
            try
            {
                DateTime createdAt = new Constants().IST_DATE_TIME;
                var d = GetById(req.Id);
                if (d != null)
                {
                    d.ModifiedAt = createdAt;
                    d.PinCode = req.PinCode;
                    d.CityId = req.CityId;
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

        public CityStateDTO GetCityState(Language lang, int pincode)
        {
            return GetAll(x => x.IsDeleted == false && x.PinCode == pincode).Select(x => new CityStateDTO
            {
                City = x.City != null ? lang == Language.English ? x.City.Name : x.City.NameH : "",
                State = x.City != null && x.City.State != null ? lang == Language.English ? x.City.State.Name : x.City.State.NameH : "",
                CityId = x.CityId,
                StateId = x.City.StateId
            }).FirstOrDefault();
        }

        public PincodeMaster GetPinCodeById(int Id)
        {
            try
            {
                return GetAll(x => x.Id == Id && x.IsDeleted == false).FirstOrDefault();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public List<PincodeMaster> GetAll()
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
    }
}