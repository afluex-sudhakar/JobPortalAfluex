using Data.DTOs;
using Data.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using Utility;
using Utility.Enums;
namespace Data.Repositories
{
    public class EmpJobPostRespository : RepositoryBase<Job>, IEmpJobPostRespository
    {
        public bool Add(EmpJobPostDTO req)
        {
            var domainName = Util.GetDomainName();
            var IPAddress = Util.GetIPAddress();
            var OSVersion = Util.GetOSVersion();
            DateTime createdAt = new Constants().IST_DATE_TIME;
            UserLog ul = new UserLog();
            try
            {
                var tt = new Job();
                tt.CreatedAt = createdAt;
                tt.UserId = req.UserId;
                tt.IsDeleted = false;
                tt.Title = req.Title;
                tt.TitleH = req.HindiTitle;
                tt.JobTypeId = Convert.ToInt32(req.JobTypeId);
                tt.JobRoleId = Convert.ToInt32(req.JobRoleId);
                tt.CategoryId = Convert.ToInt32(req.CategoryId);
                tt.DepartmentId = Convert.ToInt32(req.DepartmentId);
                tt.NoOfVacancies = req.NoOfVacanices;
                tt.SalaryMin = req.SalaryMin;
                tt.SalaryMax = req.SalaryMax;
                tt.ExperienceMin = req.ExperienceMin;
                tt.ExperienceMax = req.ExperienceMax;
                tt.PostedDate = req.PostedDate;
                tt.LastDate = req.LastDate;
                tt.Image = req.Image;
                tt.ShortDescription = req.ShortDescription;
                tt.ShortDescriptionH = req.HindiShortDescription;
                tt.Description = req.Description;
                tt.DescriptionH = req.HindiDescription;
                tt.IsPublishd = req.IsVerified;
                JobLocation JobL = new JobLocation();
                JobL.CreatedAt = createdAt;
                JobL.CityId = Convert.ToInt32(req.CityId);
                if(req.LocationId!=null && req.LocationId>0)
                {
                    JobL.PincodeMasterId = req.LocationId;
                }
                tt.JobLocations.Add(JobL);

                if (req.skills != null)
                {
                    foreach (var sk in req.skills)
                    {
                        JobSkill skill = new JobSkill();
                        skill.IsDeleted = false;
                        skill.SkillId = sk;
                        skill.CreatedAt = createdAt;
                        tt.JobSkills.Add(skill);
                    }
                }
                if (req.Courses != null)
                {
                    foreach (var sk in req.Courses)
                    {
                        JobQualification jq = new JobQualification();
                        jq.IsDeleted = false;
                        jq.CourseId = sk;
                        jq.CreatedAt = createdAt;
                        tt.JobQualifications.Add(jq);
                    }
                }

                Add(tt);
                using (CareerMitraContainer db = new Data.CareerMitraContainer())
                {
                    //User Log
                    ul.Remark = Constants.LOG_JOB_POST_SUCCESS + createdAt.ToShortDateString();
                    ul.Data = "";// new Security().Serialize<EmpJobPostDTO>(req);
                    ul.CreatedAt = createdAt;
                    ul.OS = OSVersion;
                    ul.IsDeleted = false;
                    ul.DeviceId = req.DeviceId == null ? "" : req.DeviceId;
                    ul.Lat = req.Lat;
                    ul.Lng = req.Long;
                    ul.Address = req.Address;
                    ul.DeviceOtherInfo = req.DeviceOtherInfo;
                    ul.UserAgent = req.UserAgent;
                    ul.Domain = domainName;
                    ul.DeviceType = req.DeviceType;
                    ul.IP = IPAddress;
                    db.UserLogs.Add(ul);
                    db.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {
                using (CareerMitraContainer db = new Data.CareerMitraContainer())
                {
                    ul.Remark = Constants.HTTPSTATUS_FAILED + createdAt.ToShortDateString();
                    ul.Error = ex.Message;
                    ul.Data = "";// new Security().Serialize<EmpJobPostDTO>(req);
                    ul.CreatedAt = createdAt;
                    ul.OS = OSVersion;
                    ul.IsDeleted = false;
                    ul.DeviceId = req.DeviceId == null ? "" : req.DeviceId;
                    ul.Lat = req.Lat;
                    ul.Lng = req.Long;
                    ul.Address = req.Address;
                    ul.DeviceOtherInfo = req.DeviceOtherInfo;
                    ul.UserAgent = req.UserAgent;
                    ul.Domain = domainName;
                    ul.DeviceType = req.DeviceType;
                    ul.IP = IPAddress;
                    db.UserLogs.Add(ul);
                    db.SaveChanges();
                }
                return false;
            }
        }

        public bool Update(UpdateJobPostDTO req)
        {
            try
            {
                DateTime createdAt = new Constants().IST_DATE_TIME;
                var d = GetById(req.Id);
                if (d != null)
                {
                    d.ModifiedAt = createdAt;
                    JobLocation JobL = new JobLocation();
                    JobQualification JobQ = new JobQualification();
                    JobSkill JobS = new JobSkill();
                    d.ModifiedAt = createdAt;
                    d.IsDeleted = false;
                    d.Title = req.Title;
                    d.TitleH = req.HindiTitle;
                    JobQ.CourseId = Convert.ToInt32(req.CourseId);
                    JobQ.ModifiedAt = createdAt;
                    JobL.ModifiedAt = createdAt;
                    JobL.CityId = Convert.ToInt32(req.CityId);
                    JobL.JobId = Convert.ToInt32(req.JobTypeId);
                    d.JobTypeId = Convert.ToInt32(req.JobTypeId);
                    d.JobRoleId = Convert.ToInt32(req.JobRoleId);
                    d.CategoryId = Convert.ToInt32(req.CategoryId);
                    d.SalaryMin = req.SalaryMin;
                    d.SalaryMax = req.SalaryMax;
                    d.ExperienceMin = req.ExperienceMin;
                    d.ExperienceMax = req.ExperienceMax;
                    d.PostedDate = req.PostedDate;
                    d.LastDate = Convert.ToDateTime(req.LastDate);
                    d.Image = req.Image;
                    d.ShortDescription = req.ShortDescription;
                    d.ShortDescriptionH = req.HindiShortDescription;
                    d.Description = req.Description;
                    d.DescriptionH = req.HindiDescription;
                    if (req.CityId != null)
                    {
                        var jl = d.JobLocations.Where(x => x.IsDeleted == false).ToList();
                        foreach (var item in jl)
                        {
                            item.CityId = req.CityId;
                            item.ModifiedAt = createdAt;
                        }

                    }
                    if (req.skills != null)
                    {
                        var js = d.JobSkills.Where(x => x.IsDeleted == false).ToList();
                        foreach (var item in js)
                        {
                            item.IsDeleted = true;
                            item.ModifiedAt = createdAt;
                        }

                        foreach (var sk in req.skills)
                        {
                            JobSkill jobskill = new JobSkill();
                            jobskill.IsDeleted = false;
                            jobskill.SkillId = sk;
                            jobskill.CreatedAt = createdAt;
                            d.JobSkills.Add(jobskill);
                        }
                    }
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
        public List<Job> GetJobDetailById(int Id)
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
        public Job GetDetailById(int Id)
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
        public List<Job> GetUserList(int Id)
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
        public List<Job> GetShortListedCandidate(int Id)
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
        public bool PublishJob(int Id, int UserId)
        {
            try
            {
                DateTime createdAt = new Constants().IST_DATE_TIME;
                var d = GetById(Id);
                if (d != null)
                {
                    Job ud = new Job();
                    d.IsPublishd = true;
                    d.ModifiedAt = createdAt;
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

        public List<EmpJobPostDTO> GetJobPostList(int Id)
        {
            return GetAll(x => x.IsDeleted == false && x.UserId == Id).Select(x => new EmpJobPostDTO
            {
                Id = x.Id,
                Title = x.Title,
                JobRoleId = x.JobRoleId,
                JobRole = x.JobRole.Name,
                CategoryId = x.CategoryId,
                Category = x.Category.Name,
                CourseId = x.JobQualifications.Where(xx => xx.IsDeleted == false).Select(xx => xx.CourseId).FirstOrDefault(),
                Qualification = x.JobQualifications.Select(xx => xx.Course.Name).FirstOrDefault(),
                skills = x.JobSkills.Where(xx => xx.IsDeleted == false).Select(xx => xx.SkillId).ToArray(),
                jobSkill = x.JobSkills.Where(xx => xx.IsDeleted == false).Select(y => new JobskillDTO
                {
                    Skill = y.Skill.Name,
                    SkillId = y.SkillId
                }).ToList(),
                CityId = x.JobLocations.Where(xx => xx.IsDeleted == false).Select(xx => xx.CityId).FirstOrDefault(),
                ShortDescription = x.ShortDescription,
                Description = x.Description,
                IsVerified=x.IsPublishd,
                //Location=x.JobLocations.Where(xx=>xx.IsDeleted==false).Select(xx=>xx.PincodeMasterId).FirstOrDefault(),
                SalaryMax = x.SalaryMax,
                SalaryMin = x.SalaryMin,
                ExperienceMax = x.ExperienceMax,
                ExperienceMin = x.ExperienceMin,
                LastDate = x.LastDate,
                PostedDate = x.PostedDate,
            }).OrderBy(x=>x.Id).ToList();
        }
        public EmpDashboardDTO GetDashboardDataForEmployer(int Id)
        {
            var TotalJobPosted = GetAll(x => x.IsDeleted == false && x.UserId == Id && x.IsPublishd==true).Count();
            return GetAll(x => x.IsDeleted == false && x.UserId == Id && x.IsPublishd==true).Select(x => new EmpDashboardDTO
            {
                TotalJobPosted = TotalJobPosted,
                TotalAppliedCandidates = x.UserJobs.Where(xx => xx.IsDeleted == false && xx.Status == PlacementStatus.Applied || xx.Status == PlacementStatus.Placed).Count(),
                TotalShortListedCandidates = x.UserJobs.Where(xx => xx.IsDeleted == false && xx.Status == PlacementStatus.Placed).Count()
            }).FirstOrDefault();
        }

        public bool ShortList(int Id)
        {
            try
            {
                DateTime createdAt = new Constants().IST_DATE_TIME;
                var d = GetById(Id);
                if (d != null)
                {
                    UserJob ud = new UserJob();
                    ud.Status = PlacementStatus.Placed;
                    d.ModifiedAt = createdAt;
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
    }
}
