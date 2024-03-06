using Data.DTOs;
using Data.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using Utility;
using Utility.Enums;

namespace Data.Repositories
{
  public class CMSRepository: RepositoryBase<StaticContent>, ICMSRepository
    {
       public  bool Add(CMSDTO req)
        {
            try
            {
                DateTime createdAt = new Constants().IST_DATE_TIME;
                var tt = new StaticContent();
                tt.CreatedAt = createdAt;
                tt.IsDeleted = false;
                tt.PageName = req.pageName;
                tt.Description = req.Description;
                tt.DescriptionH = req.HDescription;
                Add(tt);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool Update(CMSDTO req)
        {
            try
            {
                DateTime createdAt = new Constants().IST_DATE_TIME;
                var d = GetById(req.Id);
                if (d != null)
                {
                    d.ModifiedAt = createdAt;
                    d.PageName = req.pageName;
                    d.Description = req.Description;
                    d.DescriptionH = req.HDescription;
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
        public StaticContent GetPageNameById(int Id)
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
        public List<StaticContent> GetAll()
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
        public StaticContent GetPageDetailsByPageName(string pageName)
        {
            try
            {
                return GetAll(x => x.PageName == pageName && x.IsDeleted == false).FirstOrDefault();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
