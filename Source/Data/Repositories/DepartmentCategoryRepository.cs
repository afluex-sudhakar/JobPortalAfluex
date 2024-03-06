using Data.DTOs;
using Data.Interfaces.Repositories;
using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Utility;
using Utility.Enums;

namespace Data.Repositories
{
    public class DepartmentCategoryRepository : RepositoryBase<DepartmentCategory>, IDepartmentCategoryRepository
    {
        CommonRepository c = new CommonRepository();
        public bool Add(DepartmentCategoryDTO req)
        {
            DateTime createdAt = new Constants().IST_DATE_TIME;
            try
            {
                var data = GetAll(x => x.DepartmentId == req.DepartmentId && x.CategoryId == req.CategoryId && x.IsDeleted == false).FirstOrDefault();
                if (data != null)
                {
                    c.AddUserLog(req, createdAt, Constants.HTTPSTATUS_ALREADY_EXIST, "", new Security().Serialize<DepartmentCategoryDTO>(req), req.AddedBy);
                    return false;
                }
                else
                {
                    var tt = new DepartmentCategory();
                    tt.IsDeleted = false;
                    tt.DepartmentId = req.DepartmentId;
                    tt.CategoryId = req.CategoryId;
                    Add(tt);
                    c.AddUserLog(req, createdAt, Constants.HTTPSTATUS_SUCCESS, "", new Security().Serialize<DepartmentCategoryDTO>(req), req.AddedBy);
                    return true;
                }
            }
            catch (Exception ex)
            {
                c.AddUserLog(req, createdAt, Constants.HTTPSTATUS_FAILED, ex.Message, new Security().Serialize<DepartmentCategoryDTO>(req), req.AddedBy);
                return false;
            }
        }

        public bool Update(DepartmentCategoryDTO req)
        {
            try
            {
                var d = GetById(req.Id);
                if (d != null)
                {
                    d.IsDeleted = false;
                    d.DepartmentId = req.DepartmentId;
                    d.CategoryId = req.CategoryId;
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

        public List<DepartmentCategory> GetAll()
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
        public List<DepartmentCategory> GetDetailById(int Id)
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
        public DepartmentCategory GetByIdCustom(int Id)
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
        public List<DepartmentWiseCategoryDTO> GetCategoryById(int[] DepartmentId, LanguageFilterDTO lang)
        {
            try
            {
                return GetAll(x => x.IsDeleted == false && DepartmentId.Contains(x.DepartmentId.Value)).Select(x => new DepartmentWiseCategoryDTO
                {
                    Id = x.CategoryId,
                    Name = lang.Language==Language.English ? x.Category.Name: x.Category.NameH
                }).Distinct().ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
