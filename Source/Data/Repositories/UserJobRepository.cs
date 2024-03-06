using Data.DTOs;
using Data.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using Utility;
using Utility.Enums;

namespace Data.Repositories
{
    public class UserJobRepository : RepositoryBase<UserJob>, IUserJobRepository
    {
        public List<UserJob> GetPlacementDetails(UserJobsDTO model)
        {
            try
            {
                if(model.JobRoleId !=0)
                {
                    return GetAll(x => x.IsDeleted == false && x.Status == "Placed" && x.User.RoleId == 3 && x.Job.JobRoleId==model.JobRoleId).ToList();
                }
                else
                {
                    return GetAll(x => x.IsDeleted == false && x.Status == "Placed" && x.User.RoleId == 3).ToList();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public List<UserJob> LocationWisePlacement(UserJobsDTO model)
        {
            try
            {
                if (model.CityId != 0)
                {
                    return GetAll(x => x.IsDeleted == false && x.Status == "Placed" && x.Job.JobLocations.Select(y=>y.CityId==model.CityId).FirstOrDefault()).ToList();
                }
                else
                {
                    return GetAll(x => x.IsDeleted == false && x.Status == "Placed" && x.User.RoleId == 3).ToList();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public List<UserJob> GetAll()
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
        public List<UserJob> GetEmployerWisePlacement(int employerId)
        {
            try
            {
                return GetAll(x => x.IsDeleted == false && x.UserId == employerId).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public List<AppliedJobDTO> AppliedJobs(AppliedJobFilterDTO model)
        {
            try
            {
                if (model.Page == 0) model.Page = 1;
                model.Page = model.Page - 1;
                return GetAll(x => x.IsDeleted == false && x.UserId == model.Id).Select(x => new AppliedJobDTO
                {
                    Id = x.Id,
                    //Company = x.Job,
                    Description = model.Language.Equals(Language.English) ? x.Job.Description : x.Job.DescriptionH,
                    ShortDescription = model.Language.Equals(Language.English) ? x.Job.ShortDescription : x.Job.ShortDescriptionH,
                    Title = model.Language.Equals(Language.English) ? x.Job.Title : x.Job.TitleH,
                    Date = x.CreatedAt
                }).OrderByDescending(x => x.Id).Skip(model.Page * Constants.PAGE_SIZE).Take(Constants.PAGE_SIZE).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public List<AppliedJobDTOWeb> AppliedJobsWeb(AppliedJobFilterDTOWeb model)
        {
            try
            {
                if (model.Page == 0) model.Page = 1;
                model.Page = model.Page - 1;
                return GetAll(x => x.IsDeleted == false && x.UserId == model.Id).Select(x => new AppliedJobDTOWeb
                {
                    Id = x.Id,
                    //Company = x.Job,
                    JobId=x.JobId,
                    JobRole = model.Language.Equals(Language.English) ? x.Job.JobRole.Name : x.Job.JobRole.NameH,
                    JobType = model.Language.Equals(Language.English) ? x.Job.JobType.Name : x.Job.JobType.NameH,
                    Description = model.Language.Equals(Language.English) ? x.Job.Description : x.Job.DescriptionH,
                    ShortDescription = model.Language.Equals(Language.English) ? x.Job.ShortDescription : x.Job.ShortDescriptionH,
                    Title = model.Language.Equals(Language.English) ? x.Job.Title : x.Job.TitleH,
                    Date = x.CreatedAt
                }).OrderByDescending(x => x.Id).Skip(model.Page * Constants.PAGE_SIZE).Take(Constants.PAGE_SIZE).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public List<AppliedCandidate> GetApplyCandidate(EmpJobPostDTO model)
        {
            DateTime createdAt = new Constants().IST_DATE_TIME;
            UserLog ul = new UserLog();
            try
            {
                var mdata = from x in db.UserJobs
                            where x.IsDeleted == false && (x.Status == PlacementStatus.Applied || x.Status == PlacementStatus.Placed) && x.Job.UserId==model.UserId 
                            select x;
                if(model.JobRoleId !=null)
                {
                    mdata = mdata.Where(m => m.Job.JobRoleId == model.JobRoleId);
                }
                if (model.JobTypeId != null)
                {
                    mdata = mdata.Where(m => m.Job.JobTypeId == model.JobTypeId);
                }
                if (!string.IsNullOrEmpty(model.Title))
                {
                    mdata = mdata.Where(m => m.Job.Title.Contains(model.Title));
                }
                //if (model.FromDate != null)
                //{
                //    mdata = mdata.Where(m => m.PostedDate == model.FromDate);
                //}
                var data = mdata.Select(x => new AppliedCandidate
                {
                    Name = x.User.UserDetails.Select(y=>y.FirstName).FirstOrDefault(),
                    userid = x.User.UserDetails.Select(y => y.UserId).FirstOrDefault().ToString(),
                    Mobile = x.User.UserDetails.Select(y => y.Mobile).FirstOrDefault(),
                    Email = x.User.UserDetails.Select(y => y.Email).FirstOrDefault(),
                    Address = x.User.UserDetails.Select(y => y.Address).FirstOrDefault(),
                    Category = model.Language == Language.English ? x.Job.Category.Name : x.Job.Category.NameH,
                    CategoryId = x.Job.CategoryId,
                    CityId = x.Job.JobLocations.Select(i => i.City.Id).FirstOrDefault(),
                    City = model.Language == Language.English ? x.Job.JobLocations.Select(i => i.City.Name).FirstOrDefault() : x.Job.JobLocations.Select(i => i.City.NameH).FirstOrDefault(),
                    Description = model.Language == Language.English ? x.Job.Description : x.Job.DescriptionH,
                    PostedDate = x.Job.PostedDate,
                    ExperienceMax = x.Job.ExperienceMax,
                    userSkill = x.User.UserSkills.Select(y => new UserSkillDTO
                    {
                        Name = y.Skill.Name,
                        Id = y.SkillId
                    }).ToList(),
                    ExperienceMin = x.Job.ExperienceMin,
                    Id = x.Job.Id,
                    Image = x.Job.Image,
                    JobRole = model.Language == Language.English ? x.Job.JobRole.Name : x.Job.JobRole.NameH,
                    JobRoleId = x.Job.JobRoleId,
                    JobTypeId = x.Job.JobTypeId,
                    LastDate = x.Job.LastDate,
                    SalaryMax = x.Job.SalaryMax,
                    SalaryMin = x.Job.SalaryMin,
                    ShortDescription = model.Language == Language.English ? x.Job.ShortDescription : x.Job.ShortDescriptionH,
                    Title = model.Language == Language.English ? x.Job.Title : x.Job.TitleH
                }).ToList();
                return data;
            }
            catch (Exception ex)
            {
                ul.Remark = Constants.LOG_APPLY_JOB_FAILED + createdAt.ToShortDateString();
                ul.Error = ex.Message;
                return null;
            }
            try
            {
                using (CareerMitraContainer db = new Data.CareerMitraContainer())
                {
                    ul.Data = new Security().Serialize<EmpJobPostDTO>(model);
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
            
        }
        public List<AppliedCandidate> GetShortListedCandidate(EmpJobPostDTO model)
        {
            DateTime createdAt = new Constants().IST_DATE_TIME;
            UserLog ul = new UserLog();
            try
            {
                var mdata = from x in db.UserJobs
                            where x.IsDeleted == false && ( x.Status == PlacementStatus.Placed) && x.Job.UserId == model.UserId
                            select x;
                if (model.JobRoleId != 0)
                {
                    mdata = mdata.Where(m => m.Job.JobRoleId == model.JobRoleId);
                }
                if (model.JobTypeId != 0)
                {
                    mdata = mdata.Where(m => m.Job.JobTypeId == model.JobTypeId);
                }
                if (!string.IsNullOrEmpty(model.Title))
                {
                    mdata = mdata.Where(m => m.Job.Title == model.Title);
                }
                //if (model.FromDate != null)
                //{
                //    mdata = mdata.Where(m => m.PostedDate == model.FromDate);
                //}
                var data = mdata.Select(x => new AppliedCandidate
                {
                    Name = x.User.UserDetails.Select(y => y.FirstName).FirstOrDefault(),
                    Mobile = x.User.UserDetails.Select(y => y.Mobile).FirstOrDefault(),
                    Email = x.User.UserDetails.Select(y => y.Email).FirstOrDefault(),
                    Address = x.User.UserDetails.Select(y => y.Address).FirstOrDefault(),
                    Category = model.Language == Language.English ? x.Job.Category.Name : x.Job.Category.NameH,
                    CategoryId = x.Job.CategoryId,
                    CityId = x.Job.JobLocations.Select(i => i.City.Id).FirstOrDefault(),
                    City = model.Language == Language.English ? x.Job.JobLocations.Select(i => i.City.Name).FirstOrDefault() : x.Job.JobLocations.Select(i => i.City.NameH).FirstOrDefault(),
                    Description = model.Language == Language.English ? x.Job.Description : x.Job.DescriptionH,
                    PostedDate = x.Job.PostedDate,
                    ExperienceMax = x.Job.ExperienceMax,
                    userSkill = x.User.UserSkills.Select(y => new UserSkillDTO
                    {
                        Name = y.Skill.Name,
                        Id = y.SkillId
                    }).ToList(),
                    ExperienceMin = x.Job.ExperienceMin,
                    Id = x.Job.Id,
                    Image = x.Job.Image,
                    JobRole = model.Language == Language.English ? x.Job.JobRole.Name : x.Job.JobRole.NameH,
                    JobRoleId = x.Job.JobRoleId,
                    JobTypeId = x.Job.JobTypeId,
                    LastDate = x.Job.LastDate,
                    SalaryMax = x.Job.SalaryMax,
                    SalaryMin = x.Job.SalaryMin,
                    ShortDescription = model.Language == Language.English ? x.Job.ShortDescription : x.Job.ShortDescriptionH,
                    Title = model.Language == Language.English ? x.Job.Title : x.Job.TitleH
                }).ToList();
                return data;
            }
            catch (Exception ex)
            {
                ul.Remark = Constants.LOG_APPLY_JOB_FAILED + createdAt.ToShortDateString();
                ul.Error = ex.Message;
                return null;
            }
            try
            {
                using (CareerMitraContainer db = new Data.CareerMitraContainer())
                {
                    ul.Data = new Security().Serialize<EmpJobPostDTO>(model);
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

        }


        public ChatUserJobDetailsDTO GetJobDetailsByJobId(int JobId,int userid)
        {
            try
            {
                ChatUserJobDetailsDTO obj = new ChatUserJobDetailsDTO();
                obj = db.Jobs.Where(x => x.IsDeleted == false && x.Id == JobId).Select(x => new ChatUserJobDetailsDTO
                {
                    ExperienceMax = x.ExperienceMax,
                    ExperienceMin = x.ExperienceMin,
                    Title = x.Title,
                    ShortDescription = x.ShortDescription
                }).FirstOrDefault();

                var user = db.UserDetails.Where(x => x.User.IsDeleted == false && x.UserId == userid).Select(x => x.FirstName).FirstOrDefault();
                var companyname = db.UserDetails.Where(x => x.User.IsDeleted == false && x.UserId == userid).Select(x => x.CompanyName).FirstOrDefault();
                var cityid = db.JobLocations.Where(x => x.Job.IsDeleted == false && x.JobId == JobId).Select(x => x.CityId).FirstOrDefault();
                var joblocation = db.Cities.Where(x => x.IsDeleted == false && x.Id == cityid).Select(x => x.Name).FirstOrDefault();
                obj.UserName = user;
                obj.JobLocation= joblocation;
                return obj;
            }
            catch (Exception ex)
            {
                return null;
            } 
        }

        public ChatUserJobDetailsDTO GetJobDetailsByJobId(int chatId, Language lang)
        {
            try
            {
                ChatUserJobDetailsDTO obj = new ChatUserJobDetailsDTO();
                var jobId = db.Chats.Where(x => x.IsDeleted == false && x.Id == chatId).Select(x=>x.JobId).FirstOrDefault();
                var userId = db.Chats.Where(x => x.IsDeleted == false && x.Id == chatId).Select(x=>x.Employer).FirstOrDefault();
                obj = db.Jobs.Where(x => x.IsDeleted == false && x.Id == jobId).Select(x => new ChatUserJobDetailsDTO
                {
                    ExperienceMax = x.ExperienceMax,
                    ExperienceMin = x.ExperienceMin,
                    Title = lang == Language.English? x.Title:x.TitleH,
                    ShortDescription = lang == Language.English ? x.ShortDescription : x.ShortDescriptionH
                }).FirstOrDefault();

                var user = db.UserDetails.Where(x => x.User.IsDeleted == false && x.UserId == userId).Select(x => x.FirstName).FirstOrDefault();
                var companyname = db.UserDetails.Where(x => x.User.IsDeleted == false && x.UserId == userId).Select(x => x.CompanyName).FirstOrDefault();
                var cityid = db.JobLocations.Where(x => x.Job.IsDeleted== false && x.JobId== jobId).Select(x => x.CityId).FirstOrDefault();
                var joblocation = db.Cities.Where(x => x.IsDeleted == false && x.Id == cityid).Select(x => lang == Language.English ? x.Name : x.NameH).FirstOrDefault();
                obj.UserName = user;
                obj.CompanyName = companyname;
                obj.JobLocation = joblocation;
                return obj;
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        public bool GetAppliedStatusByUser(int UserId, int JobId)
        {
            bool status = false;
            try
            {
               var data = db.UserJobs.Where(x=>x.IsDeleted==false && x.UserId==UserId && x.JobId==JobId).FirstOrDefault();
                if(data!=null)
                {
                     status = true;
                }
                else
                {
                    status = false;
                }
                return status;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
