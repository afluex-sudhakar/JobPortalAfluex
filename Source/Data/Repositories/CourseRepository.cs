using Data.DTOs;
using Data.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using Utility;

namespace Data.Repositories
{
    public class CourseRepository : RepositoryBase<Course>, ICourseRepository
    {
        public bool Add(CourseDTO req)
        {
            try
            {
                DateTime createdAt = new Constants().IST_DATE_TIME;
                var tt = new Course();
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

        public bool Update(UpdateCourseDTO req)
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

        public List<Course> GetAll()
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
        public List<Course> GetDetailById(int Id)
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

        public List<CourseResponseDTO> GetCoursesMobile(LanguageFilterDTO req)
        {
            try
            {
                var data = GetAll(x => x.IsDeleted == false).Select(x => new CourseResponseDTO
                {
                    Id = x.Id,
                    Name = req.Language == Utility.Enums.Language.English ? x.Name : x.NameH
                }).ToList();
                return data;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public Course GetCourseById(int Id)
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
