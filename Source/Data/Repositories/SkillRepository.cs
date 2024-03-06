using Data.DTOs;
using Data.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using Utility;
using Utility.Enums;

namespace Data.Repositories
{
    public class SkillRepository : RepositoryBase<Skill>, ISkillRepository
    {
        public bool Add(SkillDTO req)
        {
            try
            {
                DateTime createdAt = new Constants().IST_DATE_TIME;
                var tt = new Skill();
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

        public bool Update(UpdateSkillDTO req)
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

        public List<Skill> GetAll()
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
        public List<SkillDTO> GetSkill(Language lang)
        {
            try
            {
                var mdata = from x in db.Skills
                            where x.IsDeleted == false
                            select x;
                var data = mdata.Select(x => new SkillDTO
                {
                    Id = x.Id,
                    Name = lang == Language.English ? x.Name : x.NameH
                }).ToList();
                return data;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public List<Skill> GetDetailById(int Id)
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

        public List<SkillResponseDTO> GetSkillsMobile(LanguageFilterDTO req)
        {
            try
            {
                var data = GetAll(x => x.IsDeleted == false).Select(x => new SkillResponseDTO
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
        public Skill GetSkillById(int Id)
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
    }
}
