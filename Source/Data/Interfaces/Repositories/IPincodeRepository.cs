using System.Collections.Generic;
using Data.DTOs;
using Utility.Enums;

namespace Data.Interfaces.Repositories
{
    public interface IPincodeRepository : IRepositoryBase<PincodeMaster>
    {
        bool Add(PinCodeDTO req);
        bool Update(PinCodeDTO req);
        CityStateDTO GetCityState(Language lang, int pincode);
        PincodeMaster GetPinCodeById(int Id);

        bool Delete(int id, int userId);
    }
}