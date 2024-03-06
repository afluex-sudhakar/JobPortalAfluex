using Data.DTOs;
using Data.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using Utility;
using Utility.Enums;

namespace Data.Repositories
{
    public class JobRepository : RepositoryBase<Job>, IJobRepository
    {
        public bool Add(JobDTO req)
        {
            try
            {
                DateTime createdAt = new Constants().IST_DATE_TIME;
                var tt = new Job();
                tt.CreatedAt = createdAt;
                tt.IsDeleted = false;
                tt.Title = req.Title;
                tt.TitleH = req.TitleH;
                Add(tt);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Update(UpdateJobDTO req)
        {
            try
            {
                DateTime createdAt = new Constants().IST_DATE_TIME;
                var d = GetById(req.Id);
                if (d != null)
                {
                    d.ModifiedAt = createdAt;
                    d.Title = req.Title;
                    d.TitleH = req.TitleH;
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

        public List<Job> GetAll()
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
        public JobReposneDTOWeb SearchJobWeb(JobSearchFilterDTOWeb model)
        {
            JobReposneDTOWeb obj = new JobReposneDTOWeb();
            UserLog ul = new UserLog();
            DateTime createdAt = new Constants().IST_DATE_TIME;
            try
            {
                var mdata = from x in db.Jobs
                            where x.IsDeleted == false && x.IsPublishd == true
                            select x;
                //Sorting
                if (!(string.IsNullOrEmpty(model.SortBy)))
                {
                    //mdata = mdata.OrderBy(x => sortColumn + " " + sortColumnDir);
                    if (Search.Date.Equals(model.SortBy))
                    {
                        mdata = mdata.OrderByDescending(x => x.PostedDate);
                    }
                    else
                    {
                        mdata = mdata.OrderBy(x => x.Title);
                    }
                }
                //Search
                if (!string.IsNullOrEmpty(model.SearchTerm))
                {
                    mdata = mdata.Where(m => m.Title.Contains(model.SearchTerm) || m.Description.Contains(model.SearchTerm) || m.ShortDescription.Contains(model.SearchTerm));
                }
                if (model.EmployerId != 0)
                {
                    mdata = mdata.Where(m => m.UserId == model.EmployerId);
                }
                if (model.CategoryId != null)
                {
                    mdata = mdata.Where(m => model.CategoryId.Contains(m.CategoryId.Value));
                }
                if (model.SkillId != null)
                {
                    mdata = mdata.Where(m => m.JobSkills.Where(x => x.IsDeleted == false && model.SkillId.Contains(x.SkillId.Value)).Any());
                }
                if (model.DepartmentId != null)
                {
                    mdata = mdata.Where(m => model.DepartmentId.Contains(m.DepartmentId.Value));
                }
                if (model.CityId != null)
                {
                    mdata = mdata.Where(m => m.JobLocations.Where(x => x.IsDeleted == false && model.CityId.Contains(x.CityId.Value)).Any());
                }
                if (model.City != null)
                {
                    mdata = mdata.Where(m => m.JobLocations.Where(x => x.IsDeleted == false && x.PincodeMaster.Name.Contains(model.City)).Any());
                }
                if (model.LocationId != 0)
                {
                    mdata = mdata.Where(m => m.JobLocations.Where(x => x.IsDeleted == false && model.LocationId== x.PincodeMasterId).Any());
                }
                if (model.CityId != null && model.City != null)
                {
                    mdata = mdata.Where(m => m.JobLocations.Where(x => x.IsDeleted == false && model.CityId.Contains(x.CityId.Value)).Any() || m.JobLocations.Where(x => x.IsDeleted == false && model.City.Contains(x.City.Name)).Any());
                }
                if (model.CourseId != null)
                {
                    mdata = mdata.Where(m => m.JobQualifications.Where(x => x.IsDeleted == false && model.CourseId.Contains(x.CourseId.Value)).Any());
                }
                ////if (!string.IsNullOrEmpty(model.City))
                ////{
                ////    mdata = mdata.Where(m => m.JobLocations.Where(x => x.IsDeleted == false && model.City.Contains(x.City.Name)).Any());
                //}
                if (model.SalaryMax != 0)
                {
                    mdata = mdata.Where(m => (m.SalaryMin >= model.SalaryMin && m.SalaryMin <= model.SalaryMax) || (m.SalaryMax >= model.SalaryMin && m.SalaryMax <= model.SalaryMax));
                }
                //total number of rows count
                obj.TotalRecords = mdata.Count();
                var skipRecords = model.Page * Constants.PAGE_SIZE;
                //Paging
                var data = mdata.Select(x => new JobFilteredDTOWeb
                {
                    Category = model.Language == Language.English ? x.Category.Name : x.Category.NameH,
                    CategoryId = x.CategoryId,
                    CityId = x.JobLocations.Select(i => i.City.Id).FirstOrDefault(),
                    City = model.Language == Language.English ? x.JobLocations.Where(xx => xx.PincodeMasterId != null).Select(xx => xx.PincodeMaster != null ? xx.PincodeMaster.Name : "").FirstOrDefault() + ", "  + x.JobLocations.Select(i => i.City.Name).FirstOrDefault()  +", "+ x.JobLocations.Select(xx => xx.PincodeMaster != null ? xx.PincodeMaster.PinCode : 0).FirstOrDefault() : x.JobLocations.Where(xx => xx.PincodeMasterId != null).Select(xx => xx.PincodeMaster != null ? xx.PincodeMaster.NameH : "").FirstOrDefault() + ", " + x.JobLocations.Select(i => i.City.NameH).FirstOrDefault()+", "+ x.JobLocations.Select(xx => xx.PincodeMaster != null ? xx.PincodeMaster.PinCode : 0).FirstOrDefault(),
                    //Skill = x.JobSkills.Select(xx=> new { xx = new string[]{ xx.Skill.Name }}).ToArray(),
                    Description = model.Language == Language.English ? x.Description : x.DescriptionH,
                    PostedDate = x.PostedDate,
                    ExperienceMax = x.ExperienceMax,
                    CompanyName = x.User.UserDetails.Select(i => i.CompanyName).FirstOrDefault() == null ? "" : x.User.UserDetails.Select(i => i.CompanyName).FirstOrDefault(),
                    ExperienceMin = x.ExperienceMin,
                    Id = x.Id,
                    Image = x.Image,
                    IsMonthly = x.IsMonthly,
                    JobRole = model.Language == Language.English ? x.JobRole.Name : x.JobRole.NameH,
                    JobRoleId = x.JobRoleId,
                    JobType = model.Language == Language.English ? x.JobType.Name : x.JobType.NameH,
                    JobTypeId = x.JobTypeId,
                    LastDate = x.LastDate,
                    SalaryMax = x.SalaryMax,
                    SalaryMin = x.SalaryMin,
                    Department= model.Language == Language.English ? x.Department.Name : x.Department.NameH,
                    ShortDescription = model.Language == Language.English ? x.ShortDescription : x.ShortDescriptionH,
                    Title = model.Language == Language.English ? x.Title : x.TitleH
                }).OrderByDescending(x => x.Id).Skip(skipRecords).Take(Constants.PAGE_SIZE).ToList();
                obj.ListJob = data;
                ul.Remark = Constants.LOG_JOB_SEARCH_SUCCESS + createdAt.ToShortDateString();
            }
            catch (Exception ex)
            {
                ul.Remark = Constants.LOG_JOB_SEARCH_FAILED + createdAt.ToShortDateString();
                ul.Error = ex.Message;
                obj.TotalRecords = 0;
                obj.ListJob = null;
            }
            try
            {
                using (CareerMitraContainer db = new Data.CareerMitraContainer())
                {
                    ul.Data = new Security().Serialize<JobSearchFilterDTOWeb>(model);
                    ul.CreatedAt = createdAt;
                    ul.OS = model.OS;
                    ul.IsDeleted = false;
                    ul.DeviceId = model.DeviceId == null ? "" : model.DeviceId;
                    ul.Lat = model.Lat;
                    ul.Lng = model.Long;
                    ul.Address = model.Address;
                    ul.DeviceOtherInfo = model.DeviceOtherInfo;
                    ul.UserAgent = model.UserAgent;
                    ul.Domain = model.Domain;
                    ul.DeviceType = model.DeviceType;
                    ul.IP = model.IP;
                    db.UserLogs.Add(ul);
                    db.SaveChanges();
                }
            }
            catch (Exception e)
            {
            }
            return obj;
        }

        public JobReposneDTO SearchJob(JobSearchFilterDTO model)
        {
            JobReposneDTO obj = new JobReposneDTO();
            UserLog ul = new UserLog();
            DateTime createdAt = new Constants().IST_DATE_TIME;
            try
            {
                var mdata = from x in db.Jobs
                            where x.IsDeleted == false && x.IsPublishd == true
                            select x;

                //Search    
                if (!string.IsNullOrEmpty(model.SearchTerm))
                {
                    mdata = mdata.Where(m => m.Title.Contains(model.SearchTerm) || m.Description.Contains(model.SearchTerm) || m.ShortDescription.Contains(model.SearchTerm));
                }
                if (model.CategoryId != null && model.CategoryId.Length>0)
                {
                    mdata = mdata.Where(m => model.CategoryId.Contains(m.CategoryId.Value));
                }
                if (model.SkillId != null && model.SkillId.Length > 0)
                {
                    mdata = mdata.Where(m => m.JobSkills.Where(x => x.IsDeleted == false && model.SkillId.Contains(x.SkillId.Value)).Any());
                }
                if (model.CityId != null && model.CityId.Length > 0)
                {
                    mdata = mdata.Where(m => m.JobLocations.Where(x => x.IsDeleted == false && model.CityId.Contains(x.CityId.Value)).Any());
                }
                if (model.CourseId != null && model.CourseId.Length > 0)
                {
                    mdata = mdata.Where(m => m.JobQualifications.Where(x => x.IsDeleted == false && model.CourseId.Contains(x.CourseId.Value)).Any());
                }
                if (!string.IsNullOrEmpty(model.City))
                {
                    mdata = mdata.Where(m => m.JobLocations.Where(x => x.IsDeleted == false && model.City.Contains(x.City.Name)).Any());
                }
                if (model.SalaryMin != 0 && model.SalaryMax != 0)
                {
                    mdata = mdata.Where(m => m.SalaryMin >= model.SalaryMin && m.SalaryMax >= model.SalaryMax);
                }

                //total number of rows count     
                obj.TotalRecords = mdata.Count();

                var skipRecords = model.Page * Constants.PAGE_SIZE;
                //Paging     
                var data = mdata.Select(x => new JobFilteredDTO
                {
                    Category = model.Language == Language.English ? x.Category.Name : x.Category.NameH,
                    CategoryId = x.CategoryId,
                    CityId = x.JobLocations.Select(i => i.City.Id).FirstOrDefault(),
                    City = model.Language == Language.English ? x.JobLocations.Select(i => i.City.Name).FirstOrDefault() : x.JobLocations.Select(i => i.City.NameH).FirstOrDefault(),
                    Description = model.Language == Language.English ? x.Description : x.DescriptionH,
                    
                    PostedDate = x.PostedDate,
                    ExperienceMax = x.ExperienceMax,
                    CompanyName = x.User.UserDetails.Select(i => i.CompanyName).FirstOrDefault(),
                    ExperienceMin = x.ExperienceMin,
                    Id = x.Id,
                    Image = x.Image,
                    IsMonthly = x.IsMonthly,
                    JobRole = model.Language == Language.English ? x.JobRole.Name : x.JobRole.NameH,
                    JobRoleId = x.JobRoleId,
                    JobType = model.Language == Language.English ? x.JobType.Name : x.JobType.NameH,
                    JobTypeId = x.JobTypeId,
                    LastDate = x.LastDate,
                    SalaryMax = x.SalaryMax,
                    SalaryMin = x.SalaryMin,
                    ShortDescription = model.Language == Language.English ? x.ShortDescription : x.ShortDescriptionH,
                    Title = model.Language == Language.English ? x.Title : x.TitleH,
                   // Department = model.Language.Equals(Language.English) ? x.Department.Name : x.Department.NameH,
                    //Skills=x.JobSkills
                }).OrderByDescending(x => x.Id).Skip(skipRecords).Take(Constants.PAGE_SIZE).ToList();

                //Sorting    
                //if (!(string.IsNullOrEmpty(model.SortBy)))
                //{
                //    //mdata = mdata.OrderBy(x => sortColumn + " " + sortColumnDir);

                //    if (Search.Date.Equals(model.SortBy))
                //    {
                //        mdata = mdata.OrderByDescending(x => x.PostedDate);
                //    }
                //}
                //else
                //{
                //    //relevance or skill wise
                //    if (model.Id != null && model.Id > 0)
                //    {
                //        var skills = db.Users.Where(x => x.Id == model.Id && x.IsDeleted == false && x.IsVerified == true).Select(x => x.UserSkills.Where(xx => xx.IsDeleted == false).Select(p => p.SkillId).ToList()).FirstOrDefault();

                //        //check this mona
                //        // item => preferences.IndexOf(item)
                //        mdata = mdata.Where(x => x.JobSkills.Where(xx => xx.IsDeleted == false && skills.Contains(xx.SkillId)).Any()).OrderBy(xx => xx.Title).Union(mdata.Where(x => x.JobSkills.Where(xx => xx.IsDeleted == false && !skills.Contains(xx.SkillId)).Any()).OrderBy(xx => xx.Title));
                //    }
                //    else
                //    {
                //        mdata = mdata.OrderByDescending(x => x.Id);
                //    }
                //}


                obj.ListJob = data;

                ul.Remark = Constants.LOG_JOB_SEARCH_SUCCESS + createdAt.ToShortDateString();
            }
            catch (Exception ex)
            {
                ul.Remark = Constants.LOG_JOB_SEARCH_FAILED + createdAt.ToShortDateString();
                ul.Error = ex.Message;
                obj.TotalRecords = 0;
                obj.ListJob = null;
            }
            try
            {
                using (CareerMitraContainer db = new Data.CareerMitraContainer())
                {
                    ul.Data = new Security().Serialize<JobSearchFilterDTO>(model);
                    ul.CreatedAt = createdAt;
                    ul.OS = model.OS;
                    ul.IsDeleted = false;
                    ul.DeviceId = model.DeviceId;
                    ul.Lat = model.Lat;
                    ul.Lng = model.Long;
                    ul.Address = model.Address;
                    ul.DeviceOtherInfo = model.DeviceOtherInfo;
                    ul.UserAgent = model.UserAgent;
                    ul.Domain = model.Domain;
                    ul.DeviceType = model.DeviceType;
                    ul.IP = model.IP;
                    db.UserLogs.Add(ul);
                    db.SaveChanges();
                }
            }
            catch (Exception e)
            {

            }
            return obj;
        }
        public JobFilteredDTO GetCustomById(JOBDetailDTO req)
        {
            JobFilteredDTO data = new JobFilteredDTO();
            UserLog ul = new UserLog();
            DateTime createdAt = new Constants().IST_DATE_TIME;
            try
            {
                data = GetAll(x => x.IsDeleted == false && x.Id == req.Id).Select(x => new JobFilteredDTO
                {
                    Category = req.Language == Language.English ? x.Category.Name : x.Category.NameH,
                    CategoryId = x.CategoryId,
                    City = req.Language == Language.English ? x.JobLocations.Select(i => i.City.Name).FirstOrDefault() : x.JobLocations.Select(i => i.City.NameH).FirstOrDefault(),
                    Qualification = req.Language == Language.English ? x.JobQualifications.Select(i => i.Course.Name).FirstOrDefault() : x.JobQualifications.Select(i => i.Course.NameH).FirstOrDefault(),
                    Skill = req.Language == Language.English ? x.JobSkills.Select(i => i.Skill.Name).ToArray() : x.JobSkills.Select(i => i.Skill.NameH).ToArray(),
                    Description = req.Language == Language.English ? x.Description : x.DescriptionH,
                    ExperienceMax = x.ExperienceMax,
                    ExperienceMin = x.ExperienceMin,
                    Time= req.Language == Language.English? Common.TimeLeft(Convert.ToDateTime(x.PostedDate)): Common.TimeLeftHindi(Convert.ToDateTime(x.PostedDate)),
                    PostedDate = x.PostedDate,
                    Id = x.Id,
                    Image = x.Image,
                    CompanyName = x.User != null ? x.User.UserDetails.Select(xx => xx.CompanyName).FirstOrDefault() : "",
                    NoOfVacancies = x.NoOfVacancies,
                    IsMonthly = x.IsMonthly,
                    JobRole = req.Language.Equals(Language.English) ? x.JobRole.Name : x.JobRole.NameH,
                    JobRoleId = x.JobRoleId,
                    JobType = req.Language.Equals(Language.English) ? x.JobType.Name : x.JobType.NameH,
                    JobTypeId = x.JobTypeId,
                    LastDate = x.LastDate,
                    SalaryMax = x.SalaryMax,
                    SalaryMin = x.SalaryMin,
                    ShortDescription = req.Language.Equals(Language.English) ? x.ShortDescription : x.ShortDescriptionH,
                    Title = req.Language.Equals(Language.English) ? x.Title : x.TitleH,
                    Department = req.Language.Equals(Language.English) ? x.Department.Name : x.Department.NameH,
                    Email = x.User.UserDetails.Select(xx => xx.Email).FirstOrDefault(),
                    Contact = x.User.UserDetails.Select(xx => xx.Mobile).FirstOrDefault(),
                    Address = x.User.UserDetails.Select(xx => xx.About).FirstOrDefault(),
                    About = x.User.UserDetails.Select(xx => xx.About).FirstOrDefault(),
                    Location= req.Language == Language.English? x.JobLocations.Where(xx=>xx.PincodeMasterId !=null).Select(xx=>xx.PincodeMaster !=null? xx.PincodeMaster.Name:"").FirstOrDefault() + ", " + x.JobLocations.Select(xx => xx.PincodeMaster != null ? xx.PincodeMaster.PinCode : 0).FirstOrDefault() : x.JobLocations.Where(xx => xx.PincodeMasterId != null).Select(xx => xx.PincodeMaster != null ? xx.PincodeMaster.NameH : "").FirstOrDefault() + ", " + x.JobLocations.Select(xx => xx.PincodeMaster != null ? xx.PincodeMaster.PinCode : 0).FirstOrDefault()
                }).FirstOrDefault();

                ul.Remark = Constants.LOG_JOB_DETAIL_SUCCESS + createdAt.ToShortDateString();
            }
            catch (Exception ex)
            {
                ul.Remark = Constants.LOG_JOB_DETAIL_FAILED + createdAt.ToShortDateString();
                ul.Error = ex.Message;
            }
            try
            {
                using (CareerMitraContainer db = new Data.CareerMitraContainer())
                {
                    ul.Data = new Security().Serialize<JOBDetailDTO>(req);
                    ul.CreatedAt = createdAt;
                    ul.OS = req.OS;
                    ul.IsDeleted = false;
                    ul.DeviceId = req.DeviceId;
                    ul.Lat = req.Lat;
                    ul.Lng = req.Long;
                    ul.Address = req.Address;
                    ul.DeviceOtherInfo = req.DeviceOtherInfo;
                    ul.UserAgent = req.UserAgent;
                    ul.Domain = req.Domain;
                    ul.DeviceType = req.DeviceType;
                    ul.IP = req.IP;
                    db.UserLogs.Add(ul);
                    db.SaveChanges();
                }
            }
            catch (Exception e)
            {

            }
            return data;
        }

        public int ApplyJob(JobApplyDTO model)
        {
            UserLog ul = new UserLog();
            DateTime createdAt = new Constants().IST_DATE_TIME;
            int res = 0;
            try
            {
                var d = GetAll(x => x.IsDeleted == false && x.Id == model.JobId).FirstOrDefault();
                if (d != null)
                {
                    if (d.UserJobs.Where(xx => xx.IsDeleted == false && xx.UserId == model.Id).Any())
                    {
                        ul.Remark = Constants.LOG_APPLY_JOB_ALREADY_APPLIED + createdAt.ToShortDateString();
                        res = -1;//already applied
                    }
                    else
                    {
                        UserJob u = new UserJob();
                        u.CreatedAt = createdAt;
                        u.IsDeleted = false;
                        //u.JobId = model.JobId;
                        u.UserId = model.Id;
                        u.Status = PlacementStatus.Applied;
                        d.UserJobs.Add(u);
                        Update(d);
                        string TempId = "";
                        //send mail & message to employer
                        if (d.User != null)
                        {
                            if (d.User.UserDetails != null)
                            {
                                if (!string.IsNullOrEmpty(d.User.UserDetails.Select(x => x.Mobile).FirstOrDefault()))
                                    BLSMS.SendSMS(d.User.UserDetails.Select(x => x.Mobile).FirstOrDefault(), "", TempId);
                                if (!string.IsNullOrEmpty(d.User.UserDetails.Select(x => x.Email).FirstOrDefault()))
                                    BLSMS.SendSMS(d.User.UserDetails.Select(x => x.Mobile).FirstOrDefault(), "", TempId);
                            }
                        }
                        ul.Remark = Constants.LOG_APPLY_JOB_SUCCESSFUL + createdAt.ToShortDateString();
                        res = 1;
                    }
                }
                else
                {
                    ul.Remark = Constants.LOG_APPLY_JOB_ALREADY_APPLIED + createdAt.ToShortDateString();
                    res = -1;//already applied
                }
            }
            catch (Exception ex)
            {
                ul.Remark = Constants.LOG_APPLY_JOB_FAILED + createdAt.ToShortDateString();
                ul.Error = ex.Message;
                res = 0;
            }
            try
            {
                using (CareerMitraContainer db = new Data.CareerMitraContainer())
                {
                    ul.Data = new Security().Serialize<JobApplyDTO>(model);
                    ul.CreatedAt = createdAt;
                    ul.OS = model.OS;
                    ul.IsDeleted = false;
                    ul.DeviceId = model.DeviceId;
                    ul.Lat = model.Lat;
                    ul.Lng = model.Long;
                    ul.Address = model.Address;
                    ul.DeviceOtherInfo = model.DeviceOtherInfo;
                    ul.UserAgent = model.UserAgent;
                    ul.Domain = model.Domain;
                    ul.DeviceType = model.DeviceType;
                    ul.IP = model.IP;
                    db.UserLogs.Add(ul);
                    db.SaveChanges();
                }
            }
            catch (Exception e)
            {

            }
            return res;
        }
        public JobFilteredDTOWeb GetCustomByIdWeb(JOBDetailDTOWeb req)
        {
            JobFilteredDTOWeb data = new JobFilteredDTOWeb();
            UserLog ul = new UserLog();
            DateTime createdAt = new Constants().IST_DATE_TIME;
            try
            {
                data = GetAll(x => x.IsDeleted == false && x.Id == req.Id).Select(x => new JobFilteredDTOWeb
                {
                    Category = req.Language == Language.English ? x.Category.Name : x.Category.NameH,
                    CategoryId = x.CategoryId,
                    State = req.Language == Language.English ? x.JobLocations.Select(i => i.PincodeMaster.City.State.Name).FirstOrDefault() : x.JobLocations.Select(i => i.PincodeMaster.City.State.NameH).FirstOrDefault(),
                    Location = req.Language == Language.English ? x.JobLocations.Select(i => i.PincodeMaster.City.Name).FirstOrDefault() : x.JobLocations.Select(i => i.PincodeMaster.City.NameH).FirstOrDefault(),
                    City = req.Language == Language.English ? x.JobLocations.Select(i => i.PincodeMaster.Name).FirstOrDefault() : x.JobLocations.Select(i => i.PincodeMaster.NameH).FirstOrDefault(),
                    PinCode = x.JobLocations.Select(i => i.PincodeMaster.PinCode).FirstOrDefault(),
                    Qualification = req.Language == Language.English ? x.JobQualifications.Select(i => i.Course.Name).FirstOrDefault() : x.JobQualifications.Select(i => i.Course.NameH).FirstOrDefault(),
                    Skill = req.Language == Language.English ? x.JobSkills.Where(xx => xx.IsDeleted == false).Select(i => i.Skill.Name).ToArray() : x.JobSkills.Select(i => i.Skill.NameH).ToArray(),
                    Description = req.Language == Language.English ? x.Description : x.DescriptionH,
                    SkillId = x.JobSkills.Where(xx => xx.IsDeleted == false).Select(i=>i.Skill.Id).ToArray(),
                    Email=x.User.UserDetails.Select(xx=>xx.Email).FirstOrDefault(),
                    Contact = x.User.UserDetails.Select(xx => xx.Mobile).FirstOrDefault(),
                    Address = x.User.UserDetails.Select(xx => xx.About).FirstOrDefault(),
                    About = x.User.UserDetails.Select(xx => xx.About).FirstOrDefault(),
                    PostedBy= x.User != null ? x.User.UserDetails.Select(xx => xx.FirstName).FirstOrDefault()+" "+x.User.UserDetails.Select(xx => xx.LastName).FirstOrDefault():"",
                    ExperienceMax = x.ExperienceMax,
                    ExperienceMin = x.ExperienceMin,
                    PostedDate = x.PostedDate,
                    CompanyName = x.User != null ? x.User.UserDetails.Select(xx => xx.CompanyName).FirstOrDefault() : "",
                    NoOfVacancies = x.NoOfVacancies,
                    Id = x.Id,
                    Image = x.Image!=null?Constants.BASE_URL + "FileUpload/Other/" + x.Image : "",
                    IsMonthly = x.IsMonthly,
                    JobRole = req.Language.Equals(Language.English) ? x.JobRole.Name : x.JobRole.NameH,
                    JobRoleId = x.JobRoleId,
                    JobType = req.Language.Equals(Language.English) ? x.JobType.Name : x.JobType.NameH,
                    JobTypeId = x.JobTypeId,
                    LastDate = x.LastDate,
                    SalaryMax = x.SalaryMax,
                    SalaryMin = x.SalaryMin,
                    Department= req.Language.Equals(Language.English) ? x.Department.Name : x.Department.NameH,
                    ShortDescription = req.Language.Equals(Language.English) ? x.ShortDescription : x.ShortDescriptionH,
                    Title = req.Language.Equals(Language.English) ? x.Title : x.TitleH
                }).FirstOrDefault();

                ul.Remark = Constants.LOG_JOB_DETAIL_SUCCESS + createdAt.ToShortDateString();
            }
            catch (Exception ex)
            {
                ul.Remark = Constants.LOG_JOB_DETAIL_FAILED + createdAt.ToShortDateString();
                ul.Error = ex.Message;
            }
            try
            {
                using (CareerMitraContainer db = new Data.CareerMitraContainer())
                {
                    ul.Data = new Security().Serialize<JOBDetailDTOWeb>(req);
                    ul.CreatedAt = createdAt;
                    ul.OS = req.OS;
                    ul.IsDeleted = false;
                    ul.DeviceId = req.DeviceId;
                    ul.Lat = req.Lat;
                    ul.Lng = req.Long;
                    ul.Address = req.Address;
                    ul.DeviceOtherInfo = req.DeviceOtherInfo;
                    ul.UserAgent = req.UserAgent;
                    ul.Domain = req.Domain;
                    ul.DeviceType = req.DeviceType;
                    ul.IP = req.IP;
                    db.UserLogs.Add(ul);
                    db.SaveChanges();
                }
            }
            catch (Exception e)
            {

            }
            return data;
        }
        public List<Job> GetAvailableJobs()
        {
            try
            {
                return GetAll(x => x.IsDeleted == false && x.IsPublishd == true).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public JobReposneDTO GetRecentJobs(RecentJobRequestDTO model)
        {
            JobReposneDTO obj = new JobReposneDTO();
            UserLog ul = new UserLog();
            DateTime createdAt = new Constants().IST_DATE_TIME;
            try
            {
                var mdata = (from x in db.Jobs
                             where x.IsDeleted == false && x.IsPublishd == true
                             select x).Take(10);

                obj.TotalRecords = mdata.Count();

                var data = mdata.Select(x => new JobFilteredDTO
                {
                    Category = model.Language == Language.English ? x.Category.Name : x.Category.NameH,
                    CategoryId = x.CategoryId,
                    CityId = x.JobLocations.Select(i => i.City.Id).FirstOrDefault(),
                    City = model.Language == Language.English ? x.JobLocations.Select(i => i.City.Name).FirstOrDefault() : x.JobLocations.Select(i => i.City.NameH).FirstOrDefault(),
                    Description = model.Language == Language.English ? x.Description : x.DescriptionH,
                    PostedDate = x.PostedDate,
                    ExperienceMax = x.ExperienceMax,
                    CompanyName = x.User.UserDetails.Select(i => i.CompanyName).FirstOrDefault(),
                    ExperienceMin = x.ExperienceMin,
                    Id = x.Id,
                    Image = x.Image,
                    IsMonthly = x.IsMonthly,
                    JobRole = model.Language == Language.English ? x.JobRole.Name : x.JobRole.NameH,
                    JobRoleId = x.JobRoleId,
                    JobType = model.Language == Language.English ? x.JobType.Name : x.JobType.NameH,
                    JobTypeId = x.JobTypeId,
                    LastDate = x.LastDate,
                    SalaryMax = x.SalaryMax,
                    SalaryMin = x.SalaryMin,
                    ShortDescription = model.Language == Language.English ? x.ShortDescription : x.ShortDescriptionH,
                    Title = model.Language == Language.English ? x.Title : x.TitleH,
                }).OrderByDescending(x => x.PostedDate).Take(10).ToList();
                obj.ListJob = data;
                ul.Remark = Constants.LOG_JOB_SEARCH_SUCCESS + createdAt.ToShortDateString();
            }
            catch (Exception ex)
            {
                ul.Remark = Constants.LOG_JOB_SEARCH_FAILED + createdAt.ToShortDateString();
                ul.Error = ex.Message;
                obj.TotalRecords = 0;
                obj.ListJob = null;
            }
            try
            {
                using (CareerMitraContainer db = new Data.CareerMitraContainer())
                {
                    ul.Data = new Security().Serialize<RecentJobRequestDTO>(model);
                    ul.CreatedAt = createdAt;
                    ul.OS = model.OS;
                    ul.IsDeleted = false;
                    ul.DeviceId = model.DeviceId;
                    ul.Lat = model.Lat;
                    ul.Lng = model.Long;
                    ul.Address = model.Address;
                    ul.DeviceOtherInfo = model.DeviceOtherInfo;
                    ul.UserAgent = model.UserAgent;
                    ul.Domain = model.Domain;
                    ul.DeviceType = model.DeviceType;
                    ul.IP = model.IP;
                    db.UserLogs.Add(ul);
                    db.SaveChanges();
                }
            }
            catch (Exception e)
            {

            }
            return obj;
        }

        public JobReposneDTOWeb GetRecentJobsWeb(RecentJobRequestDTO model)
        {
            JobReposneDTOWeb obj = new JobReposneDTOWeb();
            UserLog ul = new UserLog();
            DateTime createdAt = new Constants().IST_DATE_TIME;
            try
            {
                var mdata = (from x in db.Jobs
                             where x.IsDeleted == false && x.IsPublishd == true
                             select x).Take(10);

                obj.TotalRecords = mdata.Count();

                var data = mdata.Select(x => new JobFilteredDTOWeb
                {
                    Category = model.Language == Language.English ? x.Category.Name : x.Category.NameH,
                    CategoryId = x.CategoryId,
                    CityId = x.JobLocations.Select(i => i.City.Id).FirstOrDefault(),
                    City = model.Language == Language.English ? x.JobLocations.Select(i => i.City.Name).FirstOrDefault() : x.JobLocations.Select(i => i.City.NameH).FirstOrDefault(),
                    Description = model.Language == Language.English ? x.Description : x.DescriptionH,
                    PostedDate = x.PostedDate,
                    ExperienceMax = x.ExperienceMax,
                    CompanyName = x.User.UserDetails.Select(i => i.CompanyName).FirstOrDefault(),
                    ExperienceMin = x.ExperienceMin,
                    Id = x.Id,
                    Image = x.Image,
                    IsMonthly = x.IsMonthly,
                    JobRole = model.Language == Language.English ? x.JobRole.Name : x.JobRole.NameH,
                    JobRoleId = x.JobRoleId,
                    JobType = model.Language == Language.English ? x.JobType.Name : x.JobType.NameH,
                    JobTypeId = x.JobTypeId,
                    LastDate = x.LastDate,
                    SalaryMax = x.SalaryMax,
                    SalaryMin = x.SalaryMin,
                    ShortDescription = model.Language == Language.English ? x.ShortDescription : x.ShortDescriptionH,
                    Title = model.Language == Language.English ? x.Title : x.TitleH,
                }).OrderByDescending(x => x.PostedDate).Take(10).ToList();
                obj.ListJob = data;
                ul.Remark = Constants.LOG_JOB_SEARCH_SUCCESS + createdAt.ToShortDateString();
            }
            catch (Exception ex)
            {
                ul.Remark = Constants.LOG_JOB_SEARCH_FAILED + createdAt.ToShortDateString();
                ul.Error = ex.Message;
                obj.TotalRecords = 0;
                obj.ListJob = null;
            }
            try
            {
                using (CareerMitraContainer db = new Data.CareerMitraContainer())
                {
                    ul.Data = new Security().Serialize<RecentJobRequestDTO>(model);
                    ul.CreatedAt = createdAt;
                    ul.OS = model.OS;
                    ul.IsDeleted = false;
                    ul.DeviceId = model.DeviceId;
                    ul.Lat = model.Lat;
                    ul.Lng = model.Long;
                    ul.Address = model.Address;
                    ul.DeviceOtherInfo = model.DeviceOtherInfo;
                    ul.UserAgent = model.UserAgent;
                    ul.Domain = model.Domain;
                    ul.DeviceType = model.DeviceType;
                    ul.IP = model.IP;
                    db.UserLogs.Add(ul);
                    db.SaveChanges();
                }
            }
            catch (Exception e)
            {

            }
            return obj;
        }
        //public JobReposneDTO RecentJobs(JobSearchFilterDTO model)
        //{
        //    try
        //    {

        //    }
        //    catch(Exception ex)
        //    {

        //    }
        //}
    }
}
