using Data.DTOs;
using Data.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using Utility;
using Utility.Enums;

namespace Data.Repositories
{
    public class AudittrailRepository : RepositoryBase<UserLog>, IAudittrailRepository
    {

        //public List<UserLog> GetById(int currentPage)
        //{

        //        try
        //    {
        //        int maxRows = 10;
        //        return GetAll(x => x.IsDeleted == false).OrderBy(x => x.CustomerID)
        //                    .Skip((currentPage - 1) * maxRows)
        //                    .Take(maxRows).ToList();
        //        }
        //    catch (Exception ex)
        //    {
        //        return null;
        //    }
        //}
        public List<UserLog> GetAll()
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
   
    }
}
