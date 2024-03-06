using Data.DTOs;
using Data.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Utility;
using Utility.Enums;

namespace Data.Repositories
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public bool Add(UserDTO req)
        {
            try
            {
                var domainName = Util.GetDomainName();
                var IPAddress = Util.GetIPAddress();
                var OSVersion = Util.GetOSVersion();
                DateTime createdAt = new Constants().IST_DATE_TIME;
                var a = GetAll(x => x.UserName == req.Mobile && x.IsDeleted == false && x.RoleId == 3).FirstOrDefault();
                if (a != null)
                {

                    UserLog ul = new UserLog();
                    ul.Remark = Constants.LOG_USER_REGISTRATION_ATTEMPT_FAILED + createdAt.ToShortDateString();
                    ul.Data = new Security().Serialize<UserDTO>(req);
                    ul.Error = Constants.LOG_USER_REGISTRATION_MOBILENUMBER_ALREADYUSED;
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
                    return false;
                }
                else
                {

                    var tt = new User();
                    UserDetail ud = new UserDetail();
                    ud.FirstName = req.FirstName;
                    ud.LastName = req.LastName;
                    ud.Email = req.Email;
                    ud.Mobile = req.Mobile;
                    ud.Address = req.Address;
                    ud.State = req.State;
                    ud.Address = req.Address;
                    tt.UserName = req.Mobile;
                    tt.RoleId = req.RoleId;
                    tt.IsChangePassword = false;
                    tt.Password = req.Password;
                    tt.TemporaryPassword = "";
                    tt.UserDetails.Add(ud);
                    tt.CreatedAt = createdAt;
                    tt.IsDeleted = false;
                    Add(tt);
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Update(UpdateUserDTO req)
        {
            try
            {
                DateTime createdAt = new Constants().IST_DATE_TIME;
                var d = GetById(req.Id);
                if (d != null)
                {
                    UserDetail ud = new UserDetail();
                    ud.FirstName = req.FirstName;
                    ud.Email = req.Email;
                    ud.Mobile = req.Mobile;
                    ud.Address = req.Address;
                    ud.State = req.State;
                    d.Password = req.Password;
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

        public bool UpdateUser(UpdateUserDTO req)
        {
            DateTime createdAt = new Constants().IST_DATE_TIME;
            var domainName = Util.GetDomainName();
            var IPAddress = Util.GetIPAddress();
            var OSVersion = Util.GetOSVersion();
            var fileName = "";
            UploadDocumentDTO document = new UploadDocumentDTO();
            try
            {
                DateTime createdDate = new Constants().IST_DATE_TIME;
                if (!string.IsNullOrEmpty(req.Photo))
                {
                    fileName = req.FirstName + req.LastName + createdAt.ToLongDateString();// + req.Extension;
                    string directory = "FileUpload/ProfilePhoto/";
                    var bytes = Convert.FromBase64String(req.Photo);
                    File.WriteAllBytes(directory + fileName, bytes);
                }
                var d = GetById(req.Id);
                if (d != null)
                {
                    //user detail update
                    UserDetail ud = d.UserDetails.FirstOrDefault();
                    if (ud == null)
                        ud = new UserDetail();

                    ud.FirstName = req.FirstName;
                    ud.Email = req.Email;
                    ud.Mobile = req.Mobile;
                    ud.Address = req.Address;
                    //ud.State = req.State;
                    ud.About = req.About;
                    ud.Age = req.Age;
                    if (req.CityId != 0)
                    {
                        ud.CityId = req.CityId;
                    }
                    if (req.DOB != null)
                    {
                        ud.DOB = req.DOB;
                    }
                    else
                    {
                        ud.DOB = ud.DOB;
                    }
                    ud.FatherName = req.FatherName;
                    ud.Gender = req.Gender;
                    ud.HusbandName = req.HusbandName;
                    ud.LastName = req.LastName;
                    ud.MiddleName = req.MiddleName;
                    ud.Mobile2 = req.Mobile2;
                    ud.Mobile = req.Mobile;
                    ud.MotherName = req.MotherName;
                    if (!string.IsNullOrEmpty(req.Photo))
                        ud.Photo = fileName;
                    ud.PinCode = req.PinCode;
                    ud.SpouseName = req.SpouseName;
                    ud.CompanyName = req.CompanyName;
                    ud.Designation = req.Designation;
                    d.ModifiedAt = createdAt;

                    if (ud == null)
                    {
                        d.UserDetails.Add(ud);
                    }
                    //user skill update
                    if (req.skills != null)
                    {
                        var us = d.UserSkills.ToList();
                        d.UserDocuments.Where(x => x.IsDeleted == false && x.DocumentTypeId == DocumentType.Resume).FirstOrDefault();

                        if (us != null && us.Count > 0)
                        {
                            //delete previous skills
                            foreach (var skil in us)
                            {
                                skil.IsDeleted = true;
                            }
                        }
                        if (req.skills != null)
                        {
                            var skills = req.skills.Split(',').Select(Int32.Parse).ToList();
                            foreach (var sk in skills)
                            {
                                UserSkill skill = new UserSkill();
                                skill.IsDeleted = false;
                                skill.SkillId = sk;
                                skill.CreatedAt = createdAt;
                                d.UserSkills.Add(skill);
                            }

                        }
                    }
                    //Resume Update 
                    UserDocument doc = d.UserDocuments.Where(x => x.IsDeleted == false && x.DocumentTypeId == DocumentType.Resume).FirstOrDefault();
                    if (doc == null)
                    {
                        if (req.Resume != null)
                        {
                            doc = new UserDocument();
                            doc.Attachment = req.Resume;
                            doc.IsDeleted = false;
                            doc.CreatedAt = createdAt;
                            doc.Name = req.Resume;
                            d.UserDocuments.Add(doc);
                        }
                    }
                    //PAN Update
                    UserDocument pan = d.UserDocuments.Where(x => x.IsDeleted == false && x.DocumentTypeId == DocumentType.Pan).FirstOrDefault();
                    if (pan == null)
                    {
                        if (req.PAN != null || req.PANNo != null)
                        {
                            pan = new UserDocument();
                            pan.Attachment = req.PAN;
                            pan.DocumentTypeId = DocumentType.Pan;
                            pan.IsDeleted = false;
                            pan.CreatedAt = createdAt;
                            pan.Name = req.PANNo;
                            d.UserDocuments.Add(pan);
                        }
                    }
                    else
                    {
                        pan.Attachment = req.PAN;
                        pan.ModifiedAt = createdAt;
                        pan.Name = req.PANNo;
                    }
                    //AAdhar Update
                    UserDocument aadhar = d.UserDocuments.Where(x => x.IsDeleted == false && x.DocumentTypeId == DocumentType.Aadhar).FirstOrDefault();
                    if (aadhar == null)
                    {
                        if (req.Aadhar != null || req.AadharNo != null)
                        {
                            aadhar = new UserDocument();
                            aadhar.Attachment = req.Aadhar;
                            aadhar.DocumentTypeId = DocumentType.Aadhar;
                            aadhar.IsDeleted = false;
                            aadhar.CreatedAt = createdAt;
                            aadhar.Name = req.AadharNo;
                            d.UserDocuments.Add(aadhar);
                        }
                    }
                    else
                    {
                        aadhar.Attachment = req.Aadhar;
                        aadhar.ModifiedAt = createdAt;
                        aadhar.Name = req.AadharNo;
                    }
                    //Education Update
                    //Store User Log
                    UserLog ul = new UserLog();
                    ul.Remark = d.Id.ToString() + Constants.LOG_USER_PROFILE_UPDATE + createdAt.ToShortDateString();
                    req.Photo = "";
                    req.postedAadhar = null;
                    req.postedPan = null;
                    ul.Data = new Security().Serialize<UpdateUserDTO>(req);
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
                    d.UserLogs.Add(ul);
                    Update(d);
                    return true;
                }
                else
                {
                    UserLog ul = new UserLog();
                    ul.Remark = d.Id.ToString() + Constants.LOG_USER_PROFILE_UPDATE_FAILED_NO_RECORD + createdAt.ToShortDateString();
                    req.Photo = "";
                    req.postedAadhar = null;
                    req.postedPan = null;
                    ul.Data = new Security().Serialize<UpdateUserDTO>(req);
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
                    d.UserLogs.Add(ul);

                    Update(d);
                    return false;
                }
            }
            catch (Exception ex)
            {
                try
                {
                    using (CareerMitraContainer db = new Data.CareerMitraContainer())
                    {
                        UserLog ul = new UserLog();
                        ul.Remark = Constants.LOG_USER_PROFILE_UPDATE_FAILED + createdAt.ToShortDateString();
                        ul.Data = new Security().Serialize<UpdateUserDTO>(req);
                        ul.Error = ex.Message;
                        ul.CreatedAt = createdAt;
                        ul.OS = req.OS;
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
                        ul.Error = Constants.LOG_USER_PROFILE_UPDATE_FAIL;
                        db.UserLogs.Add(ul);
                        db.SaveChanges();
                    }
                }
                catch (Exception e)
                {

                }
                return false;
            }
        }

        public bool SaveUserEducation(UpdateUserDTO req)
        {
            DateTime createdAt = new Constants().IST_DATE_TIME;
            var domainName = Util.GetDomainName();
            var IPAddress = Util.GetIPAddress();
            var OSVersion = Util.GetOSVersion();
            try
            {
                DateTime createdDate = new Constants().IST_DATE_TIME;

                var d = GetById(req.Id);
                if (d != null)
                {
                    //user education update
                    var ue = d.UserEducations.Where(x => x.CourseId == req.CourseId && x.UserId == req.Id && x.IsDeleted == false).ToList();
                    if (ue != null && ue.Count > 0)
                    {
                        //delete previous skills
                        foreach (var c in ue)
                        {
                            c.IsDeleted = true;
                        }

                    }
                    UserEducation education = new UserEducation();
                    education.IsDeleted = false;
                    education.CourseId = req.CourseId;
                    education.CollegeName = req.University;
                    education.UserId = req.Id;
                    education.YearOfPassing = req.PassingYear;
                    education.CreatedAt = createdAt;
                    d.UserEducations.Add(education);

                    //Store User Log
                    UserLog ul = new UserLog();
                    ul.Remark = d.Id.ToString() + Constants.LOG_USER_PROFILE_UPDATE + createdAt.ToShortDateString();
                    ul.Data = new Security().Serialize<UpdateUserDTO>(req);
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
                    d.UserLogs.Add(ul);

                    Update(d);
                    return true;
                }
                else
                {
                    UserLog ul = new UserLog();
                    ul.Remark = d.Id.ToString() + Constants.LOG_USER_PROFILE_UPDATE_FAILED_NO_RECORD + createdAt.ToShortDateString();
                    ul.Data = new Security().Serialize<UpdateUserDTO>(req);
                    ul.CreatedAt = createdAt;
                    ul.OS = req.OS;
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
                    d.UserLogs.Add(ul);

                    Update(d);
                    return false;
                }
            }
            catch (Exception ex)
            {
                try
                {
                    using (CareerMitraContainer db = new Data.CareerMitraContainer())
                    {
                        UserLog ul = new UserLog();
                        ul.Remark = Constants.LOG_USER_PROFILE_UPDATE_FAILED + createdAt.ToShortDateString();
                        ul.Data = new Security().Serialize<UpdateUserDTO>(req);
                        ul.Error = ex.Message;
                        ul.CreatedAt = createdAt;
                        ul.OS = req.OS;
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
                }
                catch (Exception e)
                {

                }
                return false;
            }
        }

        public bool DeleteUserEducation(UpdateUserDTO req)
        {
            DateTime createdAt = new Constants().IST_DATE_TIME;
            var domainName = Util.GetDomainName();
            var IPAddress = Util.GetIPAddress();
            var OSVersion = Util.GetOSVersion();
            try
            {
                DateTime createdDate = new Constants().IST_DATE_TIME;

                var d = GetById(req.Id);
                if (d != null)
                {
                    //user education update
                    var ue = d.UserEducations.Where(x => x.CourseId == req.CourseId && x.UserId == req.Id && x.IsDeleted == false).ToList();
                    if (ue != null && ue.Count > 0)
                    {
                        //delete previous skills
                        foreach (var c in ue)
                        {
                            c.IsDeleted = true;
                        }

                    }

                    //Store User Log
                    UserLog ul = new UserLog();
                    ul.Remark = d.Id.ToString() + Constants.LOG_USER_PROFILE_UPDATE + createdAt.ToShortDateString();
                    ul.Data = new Security().Serialize<UpdateUserDTO>(req);
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
                    d.UserLogs.Add(ul);

                    Update(d);
                    return true;
                }
                else
                {
                    UserLog ul = new UserLog();
                    ul.Remark = d.Id.ToString() + Constants.LOG_USER_PROFILE_UPDATE_FAILED_NO_RECORD + createdAt.ToShortDateString();
                    ul.Data = new Security().Serialize<UpdateUserDTO>(req);
                    ul.CreatedAt = createdAt;
                    ul.OS = req.OS;
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
                    d.UserLogs.Add(ul);

                    Update(d);
                    return false;
                }
            }
            catch (Exception ex)
            {
                try
                {
                    using (CareerMitraContainer db = new Data.CareerMitraContainer())
                    {
                        UserLog ul = new UserLog();
                        ul.Remark = Constants.LOG_USER_PROFILE_UPDATE_FAILED + createdAt.ToShortDateString();
                        ul.Data = new Security().Serialize<UpdateUserDTO>(req);
                        ul.Error = ex.Message;
                        ul.CreatedAt = createdAt;
                        ul.OS = req.OS;
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
                }
                catch (Exception e)
                {

                }
                return false;
            }
        }

        public bool SaveUserExperience(UpdateUserDTO req)
        {
            DateTime createdAt = new Constants().IST_DATE_TIME;
            var domainName = Util.GetDomainName();
            var IPAddress = Util.GetIPAddress();
            var OSVersion = Util.GetOSVersion();
            try
            {
                DateTime createdDate = new Constants().IST_DATE_TIME;

                var d = GetById(req.Id);
                if (d != null)
                {
                    //user education update
                    var ue = d.UserExperiences.Where(x => x.JobRoleId == req.JobRoleId && x.UserId == req.Id && x.IsDeleted == false).ToList();
                    if (ue != null && ue.Count > 0)
                    {
                        //delete previous skills
                        foreach (var c in ue)
                        {
                            c.IsDeleted = true;
                        }

                    }
                    UserExperience experience = new UserExperience();
                    experience.IsDeleted = false;
                    experience.JobRoleId = req.JobRoleId;
                    experience.CompanyName = req.ExCompany;
                    experience.UserId = req.Id;
                    experience.Designation = req.ExDesignation;
                    experience.YearFrom = req.YearFrom;
                    experience.YearTo = req.YearTo;
                    experience.CreatedAt = createdAt;
                    d.UserExperiences.Add(experience);

                    //Store User Log
                    UserLog ul = new UserLog();
                    ul.Remark = d.Id.ToString() + Constants.LOG_USER_PROFILE_UPDATE + createdAt.ToShortDateString();
                    ul.Data = new Security().Serialize<UpdateUserDTO>(req);
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
                    d.UserLogs.Add(ul);
                    Update(d);
                    return true;
                }
                else
                {
                    UserLog ul = new UserLog();
                    ul.Remark = d.Id.ToString() + Constants.LOG_USER_PROFILE_UPDATE_FAILED_NO_RECORD + createdAt.ToShortDateString();
                    ul.Data = new Security().Serialize<UpdateUserDTO>(req);
                    ul.CreatedAt = createdAt;
                    ul.OS = req.OS;
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
                    d.UserLogs.Add(ul);

                    Update(d);
                    return false;
                }
            }
            catch (Exception ex)
            {
                try
                {
                    using (CareerMitraContainer db = new Data.CareerMitraContainer())
                    {
                        UserLog ul = new UserLog();
                        ul.Remark = Constants.LOG_USER_PROFILE_UPDATE_FAILED + createdAt.ToShortDateString();
                        ul.Data = new Security().Serialize<UpdateUserDTO>(req);
                        ul.Error = ex.Message;
                        ul.CreatedAt = createdAt;
                        ul.OS = req.OS;
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
                }
                catch (Exception e)
                {

                }
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
                    d.DeletedAt = new Constants().IST_DATE_TIME;
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

        public List<User> GetAll()
        {
            try
            {
                return GetAll(x => x.IsDeleted == false && x.RoleId == 3).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public User GetDetailById(int Id)
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

        public List<EmployerPlacementDTO> GetEmployerWisePlacement(int EmployerId)
        {
            try
            {
                if (EmployerId == 0)//for all
                {
                    return GetAll(x => x.IsDeleted == false && x.RoleId == 2 && x.IsVerified == true).Select(x => new EmployerPlacementDTO
                    {
                        Name = x.UserDetails.Select(xx => xx.FirstName).FirstOrDefault(),
                        TotalJobPosted = x.Jobs.Where(i => i.IsVerified == true && i.IsPublishd == true && i.IsDeleted == false).Count(),
                        AppliedCandidates = x.Jobs.Where(i => i.IsVerified == true && i.IsPublishd == true && i.IsDeleted == false).Select(ii => ii.UserJobs.Where(o => o.IsDeleted == false && o.Status == PlacementStatus.Applied || o.Status == PlacementStatus.Placed).Count()).Sum(),
                        PlacedCandidates = x.Jobs.Where(i => i.IsVerified == true && i.IsPublishd == true && i.IsDeleted == false).Select(ii => ii.UserJobs.Where(o => o.IsDeleted == false && o.Status == PlacementStatus.Placed).Count()).Sum(),
                    }).ToList();
                }
                else
                {
                    return GetAll(x => x.IsDeleted == false && x.RoleId == 2 && x.Id == EmployerId && x.IsVerified == true).Select(x => new EmployerPlacementDTO
                    {
                        Name = x.UserDetails.Select(xx => xx.FirstName).FirstOrDefault(),
                        TotalJobPosted = x.Jobs.Where(i => i.IsPublishd == true && i.IsDeleted == false).Count(),
                        AppliedCandidates = x.Jobs.Where(i => i.IsVerified == true && i.IsPublishd == true && i.IsDeleted == false).Select(ii => ii.UserJobs.Where(o => o.IsDeleted == false && o.Status == PlacementStatus.Applied || o.Status == PlacementStatus.Placed).Count()).Sum(),
                        PlacedCandidates = x.Jobs.Where(i => i.IsPublishd == true && i.IsDeleted == false).Select(ii => ii.UserJobs.Where(o => o.IsDeleted == false && o.Status == PlacementStatus.Placed).Count()).Sum(),
                    }).ToList();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public int RegisterUser(UserRegistrationDTO req)
        {

            var domainName = Util.GetDomainName();
            var IPAddress = Util.GetIPAddress();
            var OSVersion = Util.GetOSVersion();
            DateTime createdAt = new Constants().IST_DATE_TIME;
            var OTP = Util.GenerateOTP();
            try
            {
                var tt = new User();
                tt.UserName = req.MobileNo;
                tt.RoleId = 3; //Job Seeker
                tt.Password = req.Password;
                tt.TemporaryPassword = req.Password;
                tt.CreatedAt = createdAt;
                tt.IsDeleted = false;
                tt.IsChangePassword = false;
                tt.OTP = OTP;
                tt.IsVerified = false;
                tt.OTPExpireAt = createdAt.AddMinutes(10);

                UserDetail ud = new UserDetail();
                ud.FirstName = req.FirstName;
                ud.LastName = req.LastName;
                ud.Email = req.Email;
                ud.Mobile = req.MobileNo;
                tt.UserDetails.Add(ud);

                UserLog ul = new UserLog();
                ul.Remark = tt.Id.ToString() + Constants.LOG_USER_REGISTERED + createdAt.ToShortDateString();
                ul.Data = new Security().Serialize<UserRegistrationDTO>(req);
                ul.CreatedAt = createdAt;
                ul.OS = OSVersion;
                ul.IsDeleted = false;
                ul.DeviceId = req.DeviceId;
                ul.Lat = req.Lat;
                ul.Lng = req.Long;
                ul.Address = req.Address;
                ul.DeviceOtherInfo = req.DeviceOtherInfo;
                ul.UserAgent = req.UserAgent;
                ul.Domain = domainName;
                ul.DeviceType = req.DeviceType;
                ul.IP = IPAddress;
                tt.UserLogs.Add(ul);
                Add(tt);
                string TempId = "1207162427552538874";
                if (!string.IsNullOrEmpty(req.Email))
                    BLMail.SendMail(req.Email, "Career Mitra OTP", "Your CareerMitra  OTP is " + OTP, false);
                BLSMS.SendSMS(req.MobileNo, "Your CareerMitra  OTP is " + OTP, TempId);
                return tt.Id;
            }
            catch (Exception ex)
            {
                try
                {

                    using (CareerMitraContainer db = new Data.CareerMitraContainer())
                    {
                        UserLog ul = new UserLog();
                        ul.Remark = Constants.LOG_USER_REGISTRATION_ATTEMPT_FAILED + createdAt.ToShortDateString();
                        ul.Data = new Security().Serialize<UserRegistrationDTO>(req);
                        ul.Error = ex.Message;
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
                return 0;
            }
        }

        public bool RegisterDevice(RegisterDeviceDTO req)
        {
            DateTime createdAt = new Constants().IST_DATE_TIME;
            try
            {
                var u = GetById(req.UserId);
                if (u != null)
                {
                    var a = u.UserDevices.Where(x => x.MobileNo == req.MobileNo && x.DeviceId == req.DeviceId).FirstOrDefault();
                    UserLog ul = new UserLog();
                    if (a != null)
                    {
                        a.FCMId = req.FCMId;
                        a.ModifiedAt = createdAt;
                        ul.Remark = u.Id.ToString() + Constants.LOG_USER_DEVICE_REGISTRATION_UPDATED + createdAt.ToShortDateString();
                    }
                    else
                    {
                        UserDevice r = new UserDevice();
                        r.CreatedAt = createdAt;
                        r.DeviceId = req.DeviceId;
                        r.DeviceType = req.DeviceType;
                        r.FCMId = req.FCMId;
                        r.IsDeleted = false;
                        r.MobileNo = req.MobileNo;
                        u.UserDevices.Add(r);

                        ul.Remark = u.Id.ToString() + Constants.LOG_USER_DEVICE_REGISTRATION_ADDED + createdAt.ToShortDateString();
                    }
                    ul.Data = new Security().Serialize<RegisterDeviceDTO>(req);
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
                    u.UserLogs.Add(ul);
                    Update(u);
                }
                return true;
            }
            catch (Exception ex)
            {
                try
                {
                    using (CareerMitraContainer db = new Data.CareerMitraContainer())
                    {
                        UserLog ul = new UserLog();
                        ul.Remark = Constants.LOG_USER_DEVICE_REGISTRATION_FAILED + createdAt.ToShortDateString();
                        ul.Data = new Security().Serialize<RegisterDeviceDTO>(req);
                        ul.Error = ex.Message;
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
                return false;
            }
        }

        public bool GenerateOTP(GenerateOTPDTO req)
        {
            var domainName = Util.GetDomainName();
            var IPAddress = Util.GetIPAddress();
            var OSVersion = Util.GetOSVersion();
            DateTime createdAt = new Constants().IST_DATE_TIME;
            var OTP = Util.GenerateOTP();
            try
            {
                var tt = GetAll(x => x.UserName == req.MobileNo && x.IsDeleted == false).FirstOrDefault();
                tt.OTP = OTP;
                tt.OTPExpireAt = createdAt.AddMinutes(10);
                tt.TemporaryPassword = "";
                tt.ModifiedAt = createdAt;

                UserLog ul = new UserLog();
                ul.Remark = OTP + Constants.LOG_OTP_GENERATED + createdAt.ToShortDateString();
                ul.Data = new Security().Serialize<GenerateOTPDTO>(req);
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
                tt.UserLogs.Add(ul);
                Update(tt);
                string TempId = "1207162427552538874";
                BLSMS.SendSMS(req.MobileNo, "Your CareerMitra OTP is " + OTP,TempId);
                return true;
            }
            catch (Exception ex)
            {
                try
                {
                    using (CareerMitraContainer db = new Data.CareerMitraContainer())
                    {
                        UserLog ul = new UserLog();
                        ul.Remark = Constants.LOG_OTP_GENERATION_FAILED + createdAt.ToShortDateString();
                        ul.Data = new Security().Serialize<GenerateOTPDTO>(req);
                        ul.Error = ex.Message;
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
                return false;
            }
        }

        public bool GenerateTemporaryPassword(GenerateOTPDTO req)
        {
            DateTime createdAt = new Constants().IST_DATE_TIME;
            var domainName = Util.GetDomainName();
            var IPAddress = Util.GetIPAddress();
            var OSVersion = Util.GetOSVersion();
            var TempPassword = Util.GenerateAlphanumeric(8);
            try
            {

                var tt = GetAll(x => x.UserName == req.MobileNo && x.IsDeleted == false).FirstOrDefault();
                tt.TemporaryPassword = TempPassword;
                tt.OTPExpireAt = createdAt.AddMinutes(10);
                tt.ModifiedAt = createdAt;

                UserLog ul = new UserLog();
                ul.CreatedAt = createdAt;
                ul.Remark = TempPassword + Constants.LOG_Temporary_Password_generated + createdAt.ToShortDateString();
                ul.OS = OSVersion;
                ul.IsDeleted = false;
                ul.UserAgent = req.UserAgent;
                ul.Domain = domainName;
                ul.DeviceType = req.DeviceType;
                ul.IP = IPAddress;
                ul.DeviceId = req.DeviceId == null ? "" : req.DeviceId;
                ul.Lat = req.Lat;
                ul.Lng = req.Long;
                ul.Address = req.Address;
                ul.DeviceOtherInfo = req.DeviceOtherInfo;
                ul.Data = new Security().Serialize<GenerateOTPDTO>(req);
                tt.UserLogs.Add(ul);
                Update(tt);
                string TempId = "1207162427506623663";
                BLSMS.SendSMS(req.MobileNo, Constants.LOG_Temporary_Password_Msg + TempPassword+ " From CareerMitra", TempId);
                return true;
            }
            catch (Exception ex)
            {
                try
                {
                    using (CareerMitraContainer db = new Data.CareerMitraContainer())
                    {
                        UserLog ul = new UserLog();
                        ul.CreatedAt = createdAt;
                        ul.Remark = Constants.LOG_TempPassword_generation_Failed + createdAt.ToShortDateString();
                        ul.OS = OSVersion;
                        ul.IsDeleted = false;
                        ul.UserAgent = req.UserAgent;
                        ul.Domain = domainName;
                        ul.DeviceType = req.DeviceType;
                        ul.IP = IPAddress;
                        ul.DeviceId = req.DeviceId == null ? "" : req.DeviceId;
                        ul.Lat = req.Lat;
                        ul.Lng = req.Long;
                        ul.Address = req.Address;
                        ul.DeviceOtherInfo = req.DeviceOtherInfo;
                        ul.Data = new Security().Serialize<GenerateOTPDTO>(req);
                        ul.Error = ex.Message;
                        db.UserLogs.Add(ul);
                        db.SaveChanges();
                    }
                }
                catch (Exception e)
                {

                }
                return false;
            }
        }

        public int ValidateTemporaryPassword(UserDTO req)
        {
            DateTime createdAt = new Constants().IST_DATE_TIME;
            var domainName = Util.GetDomainName();
            var IPAddress = Util.GetIPAddress();
            var OSVersion = Util.GetOSVersion();
            var res = 0;
            try
            {
                var tt = GetAll(x => x.UserName == req.MobileNo && x.IsDeleted == false && x.TemporaryPassword == req.TemporaryPassword).FirstOrDefault();
                if (tt != null)
                {
                    UserLog ul = new UserLog();
                    ul.CreatedAt = createdAt;
                    ul.OS = OSVersion;
                    ul.IsDeleted = false;
                    ul.UserAgent = req.UserAgent;
                    ul.Domain = domainName;
                    ul.DeviceType = req.DeviceType;
                    ul.Remark = req.TemporaryPassword + Constants.LOG_TempPassword_VERIFICATION_SUCCESSFUL + createdAt.ToShortDateString();
                    ul.IP = IPAddress;
                    ul.DeviceId = req.DeviceId != null ? req.DeviceId : "";
                    ul.Lat = req.Lat;
                    ul.Lng = req.Long;
                    ul.Address = req.Address;
                    ul.DeviceOtherInfo = req.DeviceOtherInfo;
                    ul.Data = new Security().Serialize<UserDTO>(req);
                    tt.UserLogs.Add(ul);
                }
                else
                {
                    UserLog ul = new UserLog();
                    ul.CreatedAt = createdAt;
                    ul.Remark = req.TemporaryPassword + Constants.LOG_TempPassword_VERIFICATION_FAILED_INVALID + createdAt.ToShortDateString();
                    ul.OS = OSVersion;
                    ul.IsDeleted = false;
                    ul.UserAgent = req.UserAgent;
                    ul.Domain = domainName;
                    ul.DeviceType = req.DeviceType;
                    ul.IP = IPAddress;
                    ul.DeviceId = req.DeviceId != null ? req.DeviceId : "";
                    ul.Lat = req.Lat;
                    ul.Lng = req.Long;
                    ul.Address = req.Address;
                    ul.DeviceOtherInfo = req.DeviceOtherInfo;
                    ul.Data = new Security().Serialize<UserDTO>(req);
                    tt.UserLogs.Add(ul);
                    res = 1;
                }
                Update(tt);
            }
            catch (Exception ex)
            {
                try
                {
                    using (CareerMitraContainer db = new Data.CareerMitraContainer())
                    {
                        UserLog ul = new UserLog();
                        ul.CreatedAt = createdAt;

                        ul.Remark = Constants.LOG_TempPassword_VERIFICATION_FAILED_INVALID + createdAt.ToShortDateString();
                        ul.OS = OSVersion;
                        ul.IsDeleted = false;
                        ul.UserAgent = req.UserAgent;
                        ul.Domain = domainName;
                        ul.DeviceType = req.DeviceType;
                        ul.IP = IPAddress;
                        ul.DeviceId = req.DeviceId != null ? req.DeviceId : "";
                        ul.Lat = req.Lat;
                        ul.Lng = req.Long;
                        ul.Address = req.Address;
                        ul.DeviceOtherInfo = req.DeviceOtherInfo;
                        ul.Data = new Security().Serialize<UserDTO>(req);
                        ul.Error = ex.Message;
                        res = 1;
                        db.UserLogs.Add(ul);
                        db.SaveChanges();
                    }
                }
                catch (Exception e)
                {

                }
            }
            return res;
        }

        public bool ResetPassword(UserDTO req)
        {
            try
            {
                DateTime createdAt = new Constants().IST_DATE_TIME;
                var domainName = Util.GetDomainName();
                var IPAddress = Util.GetIPAddress();
                var OSVersion = Util.GetOSVersion();
                var tt = GetAll(x => x.UserName == req.MobileNo && x.IsDeleted == false && x.TemporaryPassword == req.TemporaryPassword).FirstOrDefault();
                if (tt != null)
                {
                    UserDetail ud = new UserDetail();
                    ud.FirstName = req.FirstName;
                    ud.Email = req.Email;
                    ud.Mobile = req.Mobile;
                    ud.Address = req.Address;
                    ud.State = req.State;
                    tt.Password = Security.EncryptString(Constants.EncKey, req.Password);
                    tt.ModifiedAt = createdAt;
                    Update(tt);
                    return true;
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public int ValidateOldPassword(UserDTO req)
        {
            DateTime createdAt = new Constants().IST_DATE_TIME;
            var domainName = Util.GetDomainName();
            var IPAddress = Util.GetIPAddress();
            var OSVersion = Util.GetOSVersion();
            var res = 0;
            try
            {
                var tt = GetAll(x => x.UserName == req.MobileNo && x.IsDeleted == false && x.Password == req.Password).FirstOrDefault();
                if (tt != null)
                {
                    UserLog ul = new UserLog();
                    ul.CreatedAt = createdAt;
                    ul.OS = OSVersion;
                    ul.IsDeleted = false;
                    ul.UserAgent = req.UserAgent;
                    ul.Domain = domainName;
                    ul.DeviceType = req.DeviceType;
                    ul.Remark = req.Password + Constants.LOG_OldPassword_VERIFICATION_Success + createdAt.ToShortDateString();
                    ul.IP = IPAddress;
                    ul.DeviceId = req.DeviceId != null ? req.DeviceId : "";
                    ul.Lat = req.Lat;
                    ul.Lng = req.Long;
                    ul.Address = req.Address;
                    ul.DeviceOtherInfo = req.DeviceOtherInfo;
                    ul.Data = new Security().Serialize<UserDTO>(req);
                    tt.UserLogs.Add(ul);
                }
                else
                {
                    UserLog ul = new UserLog();
                    ul.CreatedAt = createdAt;
                    ul.Remark = req.Password + Constants.LOG_OldPassword_VERIFICATION_FAILED_INVALID + createdAt.ToShortDateString();
                    ul.OS = OSVersion;
                    ul.IsDeleted = false;
                    ul.UserAgent = req.UserAgent;
                    ul.Domain = domainName;
                    ul.DeviceType = req.DeviceType;
                    ul.IP = IPAddress;
                    ul.DeviceId = req.DeviceId != null ? req.DeviceId : "";
                    ul.Lat = req.Lat;
                    ul.Lng = req.Long;
                    ul.Address = req.Address;
                    ul.DeviceOtherInfo = req.DeviceOtherInfo;
                    ul.Data = new Security().Serialize<UserDTO>(req);
                    tt.UserLogs.Add(ul);
                    res = 1;
                }
                Update(tt);
            }
            catch (Exception ex)
            {
                try
                {
                    using (CareerMitraContainer db = new Data.CareerMitraContainer())
                    {
                        UserLog ul = new UserLog();
                        ul.CreatedAt = createdAt;

                        ul.Remark = req.Password + Constants.LOG_OldPassword_VERIFICATION_FAILED_INVALID + createdAt.ToShortDateString();
                        ul.OS = OSVersion;
                        ul.IsDeleted = false;
                        ul.UserAgent = req.UserAgent;
                        ul.Domain = domainName;
                        ul.DeviceType = req.DeviceType;
                        ul.IP = IPAddress;
                        ul.DeviceId = req.DeviceId != null ? req.DeviceId : "";
                        ul.Lat = req.Lat;
                        ul.Lng = req.Long;
                        ul.Address = req.Address;
                        ul.DeviceOtherInfo = req.DeviceOtherInfo;
                        ul.Data = new Security().Serialize<UserDTO>(req);
                        ul.Error = ex.Message;
                        res = 1;
                        db.UserLogs.Add(ul);
                        db.SaveChanges();
                    }
                }
                catch (Exception e)
                {

                }
            }
            return res;
        }

        public bool ChangePassword(ChangePasswordDTO req)
        {
            try
            {
                DateTime createdAt = new Constants().IST_DATE_TIME;
                var domainName = Util.GetDomainName();
                var IPAddress = Util.GetIPAddress();
                var OSVersion = Util.GetOSVersion();
                var tt = GetAll(x => x.UserName == req.MobileNo && x.IsDeleted == false).FirstOrDefault();
                if (tt != null)
                {
                    User ud = new User();
                    tt.Password = Security.EncryptString(Constants.EncKey, req.Password);
                    tt.IsChangePassword = true;
                    tt.ModifiedAt = createdAt;
                    Update(tt);
                    return true;
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public int ValidateOTP(ValidateOTPDTO req)
        {
            DateTime createdAt = new Constants().IST_DATE_TIME;
            var domainName = Util.GetDomainName();
            var IPAddress = Util.GetIPAddress();
            var OSVersion = Util.GetOSVersion();
            var res = 0;
            try
            {
                var tt = GetAll(x => x.UserName == req.MobileNo && x.IsDeleted == false && x.OTP == req.OTP).FirstOrDefault();
                if (tt != null)
                {
                    UserLog ul = new UserLog();
                    if (tt.OTPExpireAt < createdAt)
                    {
                        //otp expired 
                        res = -1;
                        ul.Remark = req.OTP + Constants.LOG_OTP_VERIFICATION_FAILED_EXPIRED + createdAt.ToShortDateString();
                    }
                    else
                    {
                        tt.IsVerified = true;
                        res = 1;
                        ul.Remark = req.OTP + Constants.LOG_OTP_VERIFICATION_SUCCESSFUL + createdAt.ToShortDateString();
                        var Email = tt.UserDetails.Select(x => x.Email).FirstOrDefault();
                        string TempId = "1207162427537573573";
                        if (!string.IsNullOrEmpty(Email))
                            BLMail.SendMail(Email, "", "Thank you! You are registered in Career Mitra Job Portal. Your User Name is " + tt.UserName + " and Password is " + Security.DecryptString(Constants.EncKey, tt.Password) + " from CareerMitra", false);
                        BLSMS.SendSMS(req.MobileNo, "Thank you! You are registered in Career Mitra Job Portal. Your User Name is " + tt.UserName + " and Password is " + Security.DecryptString(Constants.EncKey, tt.Password) + " from CareerMitra ",TempId);
                    }

                    ul.Data = new Security().Serialize<ValidateOTPDTO>(req);
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
                    tt.UserLogs.Add(ul);
                }
                else
                {
                    UserLog ul = new UserLog();
                    ul.Remark = req.OTP + Constants.LOG_OTP_VERIFICATION_FAILED_INVALID + createdAt.ToShortDateString();
                    ul.Data = new Security().Serialize<ValidateOTPDTO>(req);
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
                    tt.UserLogs.Add(ul);
                    res = 0;
                }
                Update(tt);
            }
            catch (Exception ex)
            {
                try
                {
                    using (CareerMitraContainer db = new Data.CareerMitraContainer())
                    {
                        UserLog ul = new UserLog();
                        ul.Remark = Constants.LOG_OTP_VERIFICATION_FAILED + createdAt.ToShortDateString();
                        ul.Data = new Security().Serialize<ValidateOTPDTO>(req);
                        ul.Error = ex.Message;
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
                }
                catch (Exception e)
                {

                }
                res = 0;
            }
            return res;
        }

        public bool UpdateProfile(UPdateProfileDTO req)
        {
            DateTime createdAt = new Constants().IST_DATE_TIME;
            try
            {
                var fileName = req.FirstName + req.LastName + createdAt.ToLongDateString() + req.Extension;
                string directory = "FileUpload/ProfilePhoto/";
                var bytes = Convert.FromBase64String(req.Photo);

                File.WriteAllBytes(directory + fileName, bytes);

                var d = GetById(req.UserId);
                if (d != null)
                {
                    UserDetail ud = new UserDetail();
                    ud.FirstName = req.FirstName;
                    ud.Email = req.Email;
                    ud.Mobile = req.Mobile;
                    ud.Address = req.Address;
                    //ud.State = req.State;
                    ud.About = req.About;
                    ud.Age = req.Age;
                    ud.CityId = req.CityId;
                    ud.DOB = req.DOB;
                    ud.FatherName = req.FatherName;
                    ud.Gender = req.Gender;
                    ud.HusbandName = req.HusbandName;
                    ud.LastName = req.LastName;
                    ud.MiddleName = req.MiddleName;
                    ud.Mobile2 = req.Mobile2;
                    //ud.Mobile = req.Mobile;
                    ud.MotherName = req.MotherName;
                    ud.Photo = fileName;
                    ud.PinCode = req.PinCode;
                    ud.SpouseName = req.SpouseName;
                    d.ModifiedAt = createdAt;

                    UserLog ul = new UserLog();
                    ul.Remark = d.Id.ToString() + Constants.LOG_USER_PROFILE_UPDATE + createdAt.ToShortDateString();
                    req.Photo = "";
                    ul.Data = new Security().Serialize<UPdateProfileDTO>(req);
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
                    d.UserLogs.Add(ul);

                    Update(d);
                    return true;
                }
                else
                {
                    UserLog ul = new UserLog();
                    ul.Remark = d.Id.ToString() + Constants.LOG_USER_PROFILE_UPDATE_FAILED_NO_RECORD + createdAt.ToShortDateString();
                    req.Photo = "";
                    ul.Data = new Security().Serialize<UPdateProfileDTO>(req);
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
                    d.UserLogs.Add(ul);

                    Update(d);
                    return false;
                }
            }
            catch (Exception ex)
            {
                try
                {
                    using (CareerMitraContainer db = new Data.CareerMitraContainer())
                    {
                        UserLog ul = new UserLog();
                        ul.Remark = Constants.LOG_USER_PROFILE_UPDATE_FAILED + createdAt.ToShortDateString();
                        ul.Data = new Security().Serialize<UPdateProfileDTO>(req);
                        ul.Error = ex.Message;
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
                return false;
            }
        }

        public bool UploadDocument(UploadDocumentDTO req)
        {
            DateTime createdAt = new Constants().IST_DATE_TIME;
            try
            {
                var fileName = req.Name + createdAt.ToLongDateString() + req.Extension;
                string directory = "FileUpload/ProfilePhoto/";
                switch (req.DocumentType)
                {
                    case DocumentType.Resume:
                        directory = directory + "Resume";
                        break;
                    case DocumentType.Aadhar:
                        directory = directory + "Aadhar";
                        break;
                    case DocumentType.VoterId:
                        directory = directory + "VoterId";
                        break;
                    case DocumentType.Certificate:
                        directory = directory + "Certificate";
                        break;
                    case DocumentType.Marksheet:
                        directory = directory + "Marksheet";
                        break;
                    case DocumentType.Pan:
                        directory = directory + "Pan";
                        break;
                    case DocumentType.CompanyCertificate:
                        directory = directory + "CompanyCertificate";
                        break;
                    case DocumentType.Other:
                        directory = directory + "Other";
                        break;
                    default:
                        break;
                }
                var bytes = Convert.FromBase64String(req.File);

                File.WriteAllBytes(directory + fileName, bytes);

                var d = GetById(req.UserId);
                if (d != null)
                {
                    UserDocument ud;
                    UserLog ul = new UserLog();
                    ud = d.UserDocuments.Where(x => x.IsDeleted == false && x.DocumentTypeId == req.DocumentType).FirstOrDefault();
                    if (ud != null)
                    {
                        ud.Attachment = fileName;
                        ud.ModifiedAt = createdAt;
                        ud.DocumentTypeId = req.DocumentType;
                        ud.Name = req.Name;

                        ul.Remark = d.Id.ToString() + Constants.LOG_USER_DOCUMENT_UPLOAD + req.DocumentType + " | " + createdAt.ToShortDateString();
                    }
                    else
                    {
                        ud = new UserDocument();
                        ud.Attachment = fileName;
                        ud.CreatedAt = createdAt;
                        ud.DocumentTypeId = req.DocumentType;
                        ud.IsDeleted = false;
                        ud.Name = req.Name;

                        ul.Remark = d.Id.ToString() + Constants.LOG_USER_DOCUMENT_UPLOAD_UPDATE + req.DocumentType + " | " + createdAt.ToShortDateString();
                    }



                    req.File = "";
                    ul.Data = new Security().Serialize<UploadDocumentDTO>(req);
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
                    d.UserLogs.Add(ul);

                    Update(d);
                    return true;
                }
                else
                    return false;
            }
            catch (Exception ex)
            {
                try
                {
                    using (CareerMitraContainer db = new Data.CareerMitraContainer())
                    {
                        UserLog ul = new UserLog();
                        ul.Remark = Constants.LOG_USER_DOCUMENT_UPLOAD_FAILED + createdAt.ToShortDateString();
                        req.File = "";
                        ul.Data = new Security().Serialize<UploadDocumentDTO>(req);
                        ul.Error = ex.Message;
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
                return false;
            }
        }

        public UserLoginResponseWEBDTO LoginWeb(UserLoginDTO req)
        {
            var domainName = Util.GetDomainName();
            var IPAddress = Util.GetIPAddress();
            var OSVersion = Util.GetOSVersion();
            var Pass = Security.EncryptString(Constants.EncKey, req.Password);
            DateTime createdAt = new Constants().IST_DATE_TIME;
            try
            {

                var data = GetAll(x => x.IsDeleted == false && x.UserName == req.UserName && x.Password == Pass ).Select(x => new UserLoginResponseWEBDTO
                {
                    Id = x.Id,
                    RoleId = x.RoleId
                }).FirstOrDefault();

                if (data != null)
                {
                    var d = GetById(data.Id);

                    UserLog ul = new UserLog();
                    ul.Remark = d.Id.ToString() + Constants.LOG_USER_LOGIN_ATTEMPT_SUCCESSFUL + req.UserName + " | " + createdAt.ToShortDateString();
                    ul.Data = new Security().Serialize<UserLoginDTO>(req);
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
                    d.UserLogs.Add(ul);
                    Update(d);
                }
                else
                {//add unauthorised login attempt log here
                    try
                    {
                        using (CareerMitraContainer db = new Data.CareerMitraContainer())
                        {
                            UserLog ul = new UserLog();
                            ul.Remark = Constants.LOG_USER_LOGIN_ATTEMPT_FAILED_UNAUTHORIZED + createdAt.ToShortDateString();
                            ul.Data = new Security().Serialize<UserLoginDTO>(req);
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
                    }
                    catch (Exception e)
                    {

                    }
                }
                return data;
            }
            catch (Exception ex)
            {
                try
                {
                    using (CareerMitraContainer db = new Data.CareerMitraContainer())
                    {
                        UserLog ul = new UserLog();
                        ul.Remark = Constants.LOG_USER_LOGIN_ATTEMPT_FAILED + createdAt.ToShortDateString();
                        ul.Data = new Security().Serialize<UserLoginDTO>(req);
                        ul.Error = ex.Message;
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
                }
                catch (Exception e)
                {

                }
                return null;
            }
        }

        public int CheckMobileNo(CheckMobileNoDTO req)
        {
            DateTime createdAt = new Constants().IST_DATE_TIME;
            try
            {
                var data = GetAll(x => x.IsDeleted == false && x.UserName == req.MobileNo).Any();
                try
                {
                    using (CareerMitraContainer db = new Data.CareerMitraContainer())
                    {
                        UserLog ul = new UserLog();
                        ul.Remark = data + Constants.LOG_CHECK_MOBILE_NO_EXISTANCE + createdAt.ToShortDateString();
                        ul.Data = new Security().Serialize<CheckMobileNoDTO>(req);
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
                if (data)
                {
                    return 1;
                }
                else
                    return 0;
            }
            catch (Exception ex)
            {
                try
                {
                    using (CareerMitraContainer db = new Data.CareerMitraContainer())
                    {
                        UserLog ul = new UserLog();
                        ul.Remark = Constants.LOG_CHECK_MOBILE_NO_EXISTANCE_FAILED + createdAt.ToShortDateString();
                        ul.Data = new Security().Serialize<CheckMobileNoDTO>(req);
                        ul.Error = ex.Message;
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
                return -1;
            }
        }

        public bool GenerateOTPMobile(GenerateOTPDTO req)
        {
            DateTime createdAt = new Constants().IST_DATE_TIME;
            var OTP = Util.GenerateOTP();
            try
            {
                var tt = GetAll(x => x.UserName == req.MobileNo && x.IsDeleted == false).FirstOrDefault();
                if (tt != null)
                {
                    tt.OTP = OTP;
                    tt.OTPExpireAt = createdAt.AddMinutes(10);
                    tt.ModifiedAt = createdAt;
                }
                else
                {
                    tt = new User();
                    tt.UserName = req.MobileNo;
                    tt.RoleId = 3; //Job Seeker
                    tt.Password = "";
                    tt.TemporaryPassword = "";
                    tt.CreatedAt = createdAt;
                    tt.IsDeleted = false;
                    tt.IsChangePassword = false;
                    tt.OTP = OTP;
                    tt.IsVerified = false;
                    tt.OTPExpireAt = createdAt.AddMinutes(10);

                    UserDetail ud = new UserDetail();
                    ud.FirstName = "";
                    ud.Email = "";
                    ud.Mobile = req.MobileNo;
                    tt.UserDetails.Add(ud);
                    Add(tt);
                }

                UserLog ul = new UserLog();
                ul.Remark = OTP + Constants.LOG_OTP_GENERATED + createdAt.ToShortDateString();
                ul.Data = new Security().Serialize<GenerateOTPDTO>(req);
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
                tt.UserLogs.Add(ul);
                Update(tt);
                string TempId = "1207162427552538874";
                BLSMS.SendSMS(req.MobileNo, "Your OTP is " + OTP,TempId);
                return true;
            }
            catch (Exception ex)
            {
                try
                {
                    using (CareerMitraContainer db = new Data.CareerMitraContainer())
                    {
                        UserLog ul = new UserLog();
                        ul.Remark = Constants.LOG_OTP_GENERATION_FAILED + createdAt.ToShortDateString();
                        ul.Data = new Security().Serialize<GenerateOTPDTO>(req);
                        ul.Error = ex.Message;
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
                return false;
            }
        }

        public int ValidateOTPMobile(ValidateOTPDTO req)
        {
            DateTime createdAt = new Constants().IST_DATE_TIME;
            var res = 0;
            try
            {
                var tt = GetAll(x => x.UserName == req.MobileNo && x.IsDeleted == false && x.OTP == req.OTP).FirstOrDefault();
                if (tt != null)
                {
                    UserLog ul = new UserLog();
                    if (tt.OTPExpireAt < createdAt)
                    {
                        //otp expired 
                        res = -1;
                        ul.Remark = req.OTP + Constants.LOG_OTP_VERIFICATION_FAILED_EXPIRED + createdAt.ToShortDateString();
                    }
                    else
                    {
                        tt.IsVerified = true;
                        res = 1;
                        ul.Remark = req.OTP + Constants.LOG_OTP_VERIFICATION_SUCCESSFUL + createdAt.ToShortDateString();
                    }

                    ul.Data = new Security().Serialize<ValidateOTPDTO>(req);
                    ul.CreatedAt = createdAt;
                    ul.OS = "";
                    ul.IsDeleted = false;
                    ul.DeviceId = req.DeviceId == null ? "" : req.DeviceId;
                    ul.Lat = req.Lat;
                    ul.Lng = req.Long;
                    ul.Address = req.Address;
                    ul.DeviceOtherInfo = req.DeviceOtherInfo;
                    ul.UserAgent = req.UserAgent;
                    ul.Domain = "";
                    ul.DeviceType = req.DeviceType;
                    ul.IP = "";
                    tt.UserLogs.Add(ul);
                }
                else
                {
                    UserLog ul = new UserLog();
                    ul.Remark = req.OTP + Constants.LOG_OTP_VERIFICATION_FAILED_INVALID + createdAt.ToShortDateString();
                    ul.Data = new Security().Serialize<ValidateOTPDTO>(req);
                    ul.CreatedAt = createdAt;
                    ul.OS = "";
                    ul.IsDeleted = false;
                    ul.DeviceId = req.DeviceId == null ? "" : req.DeviceId;
                    ul.Lat = req.Lat;
                    ul.Lng = req.Long;
                    ul.Address = req.Address;
                    ul.DeviceOtherInfo = req.DeviceOtherInfo;
                    ul.UserAgent = req.UserAgent;
                    ul.Domain = "";
                    ul.DeviceType = req.DeviceType;
                    ul.IP = "";
                    tt.UserLogs.Add(ul);
                    res = 0;
                }
                Update(tt);
            }
            catch (Exception ex)
            {
                try
                {
                    using (CareerMitraContainer db = new Data.CareerMitraContainer())
                    {
                        UserLog ul = new UserLog();
                        ul.Remark = Constants.LOG_OTP_VERIFICATION_FAILED + createdAt.ToShortDateString();
                        ul.Data = new Security().Serialize<ValidateOTPDTO>(req);
                        ul.Error = ex.Message;
                        ul.CreatedAt = createdAt;
                        ul.OS = "";
                        ul.IsDeleted = false;
                        ul.DeviceId = req.DeviceId == null ? "" : req.DeviceId;
                        ul.Lat = req.Lat;
                        ul.Lng = req.Long;
                        ul.Address = req.Address;
                        ul.DeviceOtherInfo = req.DeviceOtherInfo;
                        ul.UserAgent = req.UserAgent;
                        ul.Domain = "";
                        ul.DeviceType = req.DeviceType;
                        ul.IP = "";
                        db.UserLogs.Add(ul);
                        db.SaveChanges();
                    }
                }
                catch (Exception e)
                {

                }
                res = 0;
            }
            return res;
        }

        public UserDetailResponseDTO RegisterUserMobile(UserRegistrationDTO req)
        {
            DateTime createdAt = new Constants().IST_DATE_TIME;
            var OTP = Util.GenerateOTP();
            try
            {
                var tt = GetAll(x => x.UserName == req.MobileNo && x.IsDeleted == false && x.IsVerified == true).FirstOrDefault();
                if (tt != null)
                {
                    tt.ModifiedAt = createdAt;
                    tt.IsChangePassword = false;
                    tt.Password = Security.EncryptString(Constants.EncKey, req.Password);

                    var ud = tt.UserDetails.FirstOrDefault();
                    ud.FirstName = req.FirstName;
                    ud.LastName = req.LastName;
                    ud.Email = req.Email;
                    tt.UserDetails.Add(ud);

                    var a = tt.UserDevices.Where(x => x.MobileNo == req.MobileNo && x.DeviceId == req.DeviceId).FirstOrDefault();
                    if (a != null)
                    {
                        a.FCMId = req.FCMId;
                        a.ModifiedAt = createdAt;
                    }
                    else
                    {
                        UserDevice r = new UserDevice();
                        r.CreatedAt = createdAt;
                        r.DeviceId = req.DeviceId;
                        r.DeviceType = req.DeviceType;
                        r.FCMId = req.FCMId;
                        r.IsDeleted = false;
                        r.MobileNo = req.MobileNo;
                        tt.UserDevices.Add(r);
                    }

                    UserLog ul = new UserLog();
                    ul.Remark = tt.Id.ToString() + Constants.LOG_USER_REGISTERED + createdAt.ToShortDateString();
                    ul.Data = new Security().Serialize<UserRegistrationDTO>(req);
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
                    tt.UserLogs.Add(ul);
                    Update(tt);

                    UserDetailResponseDTO res = new UserDetailResponseDTO();
                    res.MobileNo = tt.UserName;
                    res.Id = tt.Id;
                    var udr = tt.UserDetails.FirstOrDefault();
                    if (udr != null)
                    {
                        //res.Gender = udr.Gender;
                        res.Email = udr.Email;
                        res.FirstName = udr.FirstName;
                        res.LastName = udr.LastName;
                        //res.City = udr.City != null ? udr.City.Name : "";
                        //res.About = udr.About;
                        //res.Address = udr.Address;
                        //res.Age = udr.Age;
                        //res.CityId = udr.CityId;
                        //res.Designation = "";
                        //res.DOB = udr.DOB;
                        //res.FatherName = udr.FatherName;
                        //res.MotherName = udr.MotherName;
                        res.Photo = udr.Photo;
                        //res.PinCode = udr.PinCode;

                    }
                    return res;
                }
                else
                {
                    return null;
                }
                //var s = tt.UserSkills.Where(x => x.IsDeleted == false).ToList();
                //if (s != null && s.Count > 0)
                //{
                //    foreach (var sk in s)
                //    {
                //        res.Skills.Add(new UserSkillDTO() { Id = sk.SkillId, Name = sk.Skill != null ? sk.Skill.Name : "" });
                //    }
                //}

                //var resume = tt.UserDocuments.Where(x => x.IsDeleted == false && x.DocumentTypeId == DocumentType.Resume).Select(x => x.Attachment).FirstOrDefault();
                //res.Resume = resume; 
            }
            catch (Exception ex)
            {
                try
                {
                    using (CareerMitraContainer db = new Data.CareerMitraContainer())
                    {
                        UserLog ul = new UserLog();
                        ul.Remark = Constants.LOG_USER_REGISTRATION_ATTEMPT_FAILED + createdAt.ToShortDateString();
                        ul.Data = new Security().Serialize<UserRegistrationDTO>(req);
                        ul.Error = ex.Message;
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
                return null;
            }
        }

        public UserProfileResponseDTO GetProfileData(UserProfileReqDTO req)
        {
            DateTime createdAt = new Constants().IST_DATE_TIME;
            var OTP = Util.GenerateOTP();
            try
            {
                var tt = GetAll(x => x.Id == req.Id && x.IsDeleted == false && x.IsVerified == true).FirstOrDefault();
                if (tt != null)
                {
                    UserLog ul = new UserLog();
                    ul.Remark = tt.Id.ToString() + Constants.LOG_USER_VIEW_PROFILE_SUCCESS + createdAt.ToShortDateString();
                    ul.Data = new Security().Serialize<UserProfileReqDTO>(req);
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
                    tt.UserLogs.Add(ul);
                    Update(tt);

                    UserProfileResponseDTO res = new UserProfileResponseDTO();
                    List<UserSkillDTO> objSkills = new List<UserSkillDTO>();
                    List<EducationRespDTO> objEducations = new List<EducationRespDTO>();
                    List<ExperienceRespDTO> objExperiences = new List<ExperienceRespDTO>();
                    res.MobileNo = tt.UserName;
                    res.Id = tt.Id;
                    var udr = tt.UserDetails.FirstOrDefault();
                    if (udr != null)
                    {
                        res.Gender = udr.Gender;
                        res.Email = udr.Email;
                        res.City = udr.City != null ? udr.City.Name : "";
                        res.About = udr.About;
                        res.FirstName = udr.FirstName;
                        res.LastName = udr.LastName;
                        res.Address = udr.Address;
                        res.Age = udr.Age;
                        res.CityId = udr.CityId;
                        res.Designation = "";
                        res.DOB = udr.DOB;
                        res.FatherName = udr.FatherName;
                        res.MotherName = udr.MotherName;
                        res.Photo = Constants.PROFILE_PIC_URL + udr.Photo;
                        res.PinCode = udr.PinCode;
                        res.Mobile2 = udr.Mobile2;
                        res.CompanyName = udr.CompanyName;
                        res.Designation = udr.Designation;
                    }

                    var s = tt.UserSkills.Where(x => x.IsDeleted == false).ToList();
                    if (s != null && s.Count > 0)
                    {
                        foreach (var sk in s)
                        {
                            objSkills.Add(new UserSkillDTO() { Id = sk.SkillId, Name = sk.Skill != null ? sk.Skill.Name : "" });
                        }
                    }
                    else
                    {
                        objSkills.Add(new UserSkillDTO());
                    }
                    res.Skills = objSkills;
                    var resume = tt.UserDocuments.Where(x => x.IsDeleted == false && x.DocumentTypeId == DocumentType.Resume).Select(x => x.Attachment).FirstOrDefault();
                    res.Resume = !string.IsNullOrEmpty(resume) ? Constants.RESUME_URL + resume : "";

                    var e = tt.UserEducations.Where(x => x.IsDeleted == false).ToList();
                    if (e != null && e.Count > 0)
                    {
                        foreach (var sk in e)
                        {
                            objEducations.Add(new EducationRespDTO() { CourseId = sk.CourseId, Course = sk.Course != null ? sk.Course.Name : "", College = sk.CollegeName, YearOfPassing = sk.YearOfPassing });
                        }
                    }
                    else
                    {
                        objEducations.Add(new EducationRespDTO());
                    }
                    res.Educations = objEducations;

                    var exp = tt.UserExperiences.Where(x => x.IsDeleted == false).ToList();
                    if (exp != null && exp.Count > 0)
                    {
                        foreach (var sk in exp)
                        {
                            objExperiences.Add(new ExperienceRespDTO() { Company = sk.CompanyName, Designation = sk.Designation, IsCurrent = sk.IsCurrent, YearFrom = sk.YearFrom, YearTo = sk.YearTo });
                        }
                    }
                    else
                    {
                        objExperiences.Add(new ExperienceRespDTO());
                    }
                    res.Experiences = objExperiences;

                    int total = 7;
                    int filled = 0;
                    if (res.About != "" && res.About != null)
                    {
                        filled = filled + 1;
                    }
                    if (res.Photo != "")
                    {
                        filled = filled + 1;
                    }
                    if (res.Skills != null && res.Skills.Count > 0)
                    {
                        filled = filled + 1;
                    }
                    if (res.Educations != null && res.Educations.Count > 0)
                    {
                        filled = filled + 1;
                    }
                    if (res.Experiences != null && res.Experiences.Count > 0)
                    {
                        filled = filled + 1;
                    }
                    if (res.CompanyName != "" || res.Designation != "")
                    {
                        filled = filled + 1;
                    }
                    if (res.DOB != null || res.Age > 0 || res.Gender != "" || res.FatherName != "" || res.MotherName != "" || res.Address != "")
                    {
                        filled = filled + 1;
                    }

                    decimal filledPercentage = filled * 100 / total;


                    res.ProfileFillPercent = filledPercentage;

                    return res;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                try
                {
                    using (CareerMitraContainer db = new Data.CareerMitraContainer())
                    {
                        UserLog ul = new UserLog();
                        ul.Remark = Constants.LOG_USER_VIEW_PROFILE_FAILED + createdAt.ToShortDateString();
                        ul.Data = new Security().Serialize<UserProfileReqDTO>(req);
                        ul.Error = ex.Message;
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
                return null;
            }
        }

        public UserLoginResponseDTO AdminLogin(UserLoginDTO req)
        {

            var domainName = Util.GetDomainName();
            var IPAddress = Util.GetIPAddress();
            var OSVersion = Util.GetOSVersion();
            DateTime createdAt = new Constants().IST_DATE_TIME;
            var Pass = Security.EncryptString(Constants.EncKey, req.Password);
            try
            {

                var data = GetAll(x => x.IsDeleted == false && x.RoleId == 1 && x.UserName == req.UserName && x.Password == Pass && x.IsVerified == true).Select(x => new UserLoginResponseDTO
                {
                    About = x.UserDetails.Select(xx => xx.About).FirstOrDefault(),
                    Age = x.UserDetails.Select(xx => xx.Age).FirstOrDefault(),
                    Address = x.UserDetails.Select(xx => xx.Address).FirstOrDefault(),
                    City = x.UserDetails.Select(xx => xx.City != null ? xx.City.Name : "").FirstOrDefault(),
                    CityId = x.UserDetails.Select(xx => xx.CityId).FirstOrDefault(),
                    DOB = x.UserDetails.Select(xx => xx.DOB).FirstOrDefault(),
                    Email = x.UserDetails.Select(xx => xx.Email).FirstOrDefault(),
                    FatherName = x.UserDetails.Select(xx => xx.FatherName).FirstOrDefault(),
                    FirstName = x.UserDetails.Select(xx => xx.FirstName).FirstOrDefault(),
                    Gender = x.UserDetails.Select(xx => xx.Gender).FirstOrDefault(),
                    HusbandName = x.UserDetails.Select(xx => xx.HusbandName).FirstOrDefault(),
                    Id = x.Id,
                    LastName = x.UserDetails.Select(xx => xx.LastName).FirstOrDefault(),
                    //MiddleName = x.UserDetails.Select(xx => xx.MiddleName).FirstOrDefault(),
                    MobileNo = x.UserDetails.Select(xx => xx.Mobile).FirstOrDefault(),
                    Mobile2 = x.UserDetails.Select(xx => xx.Mobile2).FirstOrDefault(),
                    MotherName = x.UserDetails.Select(xx => xx.MotherName).FirstOrDefault(),
                    Photo = x.UserDetails.Select(xx => xx.Photo).FirstOrDefault(),
                    PinCode = x.UserDetails.Select(xx => xx.PinCode).FirstOrDefault(),
                    SpouseName = x.UserDetails.Select(xx => xx.SpouseName).FirstOrDefault(),
                    State = x.UserDetails.Select(xx => xx.City != null ? xx.City.State != null ? xx.City.State
                    .Name : "" : "").FirstOrDefault()
                }).FirstOrDefault();

                if (data != null)
                {
                    var d = GetById(data.Id);

                    UserLog ul = new UserLog();
                    ul.Remark = d.Id.ToString() + Constants.LOG_USER_LOGIN_ATTEMPT_SUCCESSFUL + req.UserName + " | " + createdAt.ToShortDateString();
                    ul.Data = new Security().Serialize<UserLoginDTO>(req);
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
                    d.UserLogs.Add(ul);
                    Update(d);
                }
                else
                {//add unauthorised login attempt log here
                    try
                    {
                        using (CareerMitraContainer db = new Data.CareerMitraContainer())
                        {
                            UserLog ul = new UserLog();
                            ul.Remark = Constants.LOG_USER_LOGIN_ATTEMPT_FAILED_UNAUTHORIZED + createdAt.ToShortDateString();
                            ul.Data = new Security().Serialize<UserLoginDTO>(req);
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
                    }
                    catch (Exception e)
                    {

                    }
                }
                return data;
            }
            catch (Exception ex)
            {
                try
                {
                    using (CareerMitraContainer db = new Data.CareerMitraContainer())
                    {
                        UserLog ul = new UserLog();
                        ul.Remark = Constants.LOG_USER_LOGIN_ATTEMPT_FAILED + createdAt.ToShortDateString();
                        ul.Data = new Security().Serialize<UserLoginDTO>(req);
                        ul.Error = ex.Message;
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
                }
                catch (Exception e)
                {

                }
                return null;
            }
        }

        public UserDetailResponseDTO CheckLoginMobile(UserLogDTO req, out LoginResponse status)
        {
            DateTime createdAt = new Constants().IST_DATE_TIME;
            try
            {
                var tt = GetAll(x => x.UserDevices.Where(xx => xx.IsDeleted == false && xx.DeviceId == req.DeviceId).Any() && x.IsDeleted == false && x.IsVerified == true).FirstOrDefault();
                if (tt != null)
                {
                    if (tt.IsBlocked == true)
                    {
                        status = LoginResponse.Blocked;
                        UserLog ul = new UserLog();
                        ul.Remark = tt.Id.ToString() + Constants.LOG_USER_BLOCKED + createdAt.ToShortDateString();
                        ul.Data = new Security().Serialize<UserLogDTO>(req);
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
                        tt.UserLogs.Add(ul);
                        Update(tt);
                        return null;
                    }
                    else
                    {
                        status = LoginResponse.Success;
                        UserLog ul = new UserLog();
                        ul.Remark = tt.Id.ToString() + Constants.LOG_USER_CHECK_LOGIN_ATTEMPT_SUCCESSFUL + createdAt.ToShortDateString();
                        ul.Data = new Security().Serialize<UserLogDTO>(req);
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
                        tt.UserLogs.Add(ul);
                        Update(tt);

                        UserDetailResponseDTO res = new UserDetailResponseDTO();
                        res.MobileNo = tt.UserName;
                        res.Id = tt.Id;
                        var udr = tt.UserDetails.FirstOrDefault();
                        if (udr != null)
                        {
                            res.FirstName = udr.FirstName;
                            res.LastName = udr.LastName;
                            res.Email = udr.Email;
                            res.Photo = Constants.PROFILE_PIC_URL + udr.Photo;
                        }

                        return res;
                    }
                }
                else
                {
                    //Device id not found
                    status = LoginResponse.InvalidDeviceId;
                    try
                    {
                        using (CareerMitraContainer db = new Data.CareerMitraContainer())
                        {
                            UserLog ul = new UserLog();
                            ul.Remark = Constants.LOG_USER_DEVICEID_NOT_EXIST + createdAt.ToShortDateString();
                            ul.Data = new Security().Serialize<UserLogDTO>(req);
                            ul.Error = "";
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
                    return null;
                }
            }
            catch (Exception ex)
            {
                status = LoginResponse.Error;
                try
                {
                    using (CareerMitraContainer db = new Data.CareerMitraContainer())
                    {
                        UserLog ul = new UserLog();
                        ul.Remark = Constants.LOG_USER_CHECK_LOGIN_ERROR + createdAt.ToShortDateString();
                        ul.Data = new Security().Serialize<UserLogDTO>(req);
                        ul.Error = ex.Message;
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
                return null;
            }
        }

        public UserDetailResponseDTO LoginMobile(UserLoginMobileDTO req, out LoginResponse status)
        {
            DateTime createdAt = new Constants().IST_DATE_TIME;
            try
            {
                //status = LoginResponse.Success;//success
                //return null;
                var Pass = Security.EncryptString(Constants.EncKey, req.Password);
                var tt = GetAll(x => x.UserName == req.MobileNo && x.IsDeleted == false).FirstOrDefault();
                if (tt != null)
                {
                    if (tt.IsVerified == true)
                    {
                        if (tt.Password == Pass)
                        {
                            //authentication successful
                            status = LoginResponse.Success;
                            var a = tt.UserDevices.Where(x => x.MobileNo == req.MobileNo).FirstOrDefault();
                            if (a != null)
                            {
                                a.FCMId = req.FCMId;
                                a.DeviceId = req.DeviceId;
                                a.ModifiedAt = createdAt;
                            }
                            else
                            {
                                UserDevice ud = new UserDevice();
                                ud.FCMId = req.FCMId;
                                ud.MobileNo = req.MobileNo;
                                ud.DeviceId = req.DeviceId;
                                ud.CreatedAt = createdAt;
                                ud.DeviceType = DeviceType.Mobile;
                                ud.IsDeleted = false;
                                tt.UserDevices.Add(ud);
                            }

                            UserLog ul = new UserLog();
                            ul.Remark = tt.Id.ToString() + Constants.LOG_USER_LOGIN_ATTEMPT_SUCCESSFUL + createdAt.ToShortDateString();
                            ul.Data = new Security().Serialize<UserLoginMobileDTO>(req);
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
                            tt.UserLogs.Add(ul);
                            Update(tt);

                            UserDetailResponseDTO res = new UserDetailResponseDTO();
                            res.MobileNo = tt.UserName;
                            res.Id = tt.Id;
                            var udr = tt.UserDetails.FirstOrDefault();
                            if (udr != null)
                            {
                                res.FirstName = udr.FirstName;
                                res.LastName = udr.LastName;
                                res.Email = udr.Email;
                                res.Photo = Constants.PROFILE_PIC_URL + udr.Photo;
                            }

                            return res;
                        }
                        else
                        {
                            //invalid credential
                            status = LoginResponse.InvalidPassword;
                            try
                            {
                                using (CareerMitraContainer db = new Data.CareerMitraContainer())
                                {
                                    UserLog ul = new UserLog();
                                    ul.Remark = Constants.LOG_USER_LOGIN_ATTEMPT_FAILED_UNAUTHORIZED_INVALID_PASSWORD + createdAt.ToShortDateString();
                                    ul.Data = new Security().Serialize<UserLoginMobileDTO>(req);
                                    ul.Error = "";
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
                            return null;
                        }
                    }
                    else
                    {
                        //account not verified
                        status = LoginResponse.NotVerified;
                        try
                        {
                            using (CareerMitraContainer db = new Data.CareerMitraContainer())
                            {
                                UserLog ul = new UserLog();
                                ul.Remark = Constants.LOG_USER_LOGIN_ATTEMPT_FAILED_NOT_VERIFIED + createdAt.ToShortDateString();
                                ul.Data = new Security().Serialize<UserLoginMobileDTO>(req);
                                ul.Error = "";
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
                        return null;
                    }
                }
                else
                {
                    //invalid user
                    status = LoginResponse.InvalidUser;
                    try
                    {
                        using (CareerMitraContainer db = new Data.CareerMitraContainer())
                        {
                            UserLog ul = new UserLog();
                            ul.Remark = Constants.LOG_USER_LOGIN_ATTEMPT_FAILED_UNAUTHORIZED + createdAt.ToShortDateString();
                            ul.Data = new Security().Serialize<UserLoginMobileDTO>(req);
                            ul.Error = "";
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
                    return null;
                }
            }
            catch (Exception ex)
            {
                status = LoginResponse.Error;
                try
                {
                    using (CareerMitraContainer db = new Data.CareerMitraContainer())
                    {
                        UserLog ul = new UserLog();
                        ul.Remark = Constants.LOG_USER_LOGIN_ATTEMPT_FAILED + createdAt.ToShortDateString();
                        ul.Data = new Security().Serialize<UserLoginMobileDTO>(req);
                        ul.Error = ex.Message;
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
                return null;
            }
        }

        public bool ForgetPasswordMobile(ForgetPasswordDTO req)
        {
            DateTime createdAt = new Constants().IST_DATE_TIME;
            var OTP = Util.GenerateAlphanumeric(6);
            try
            {
                var tt = GetAll(x => x.UserName == req.MobileNo && x.IsDeleted == false && x.IsVerified == true).FirstOrDefault();
                if (tt != null)
                {
                    tt.TemporaryPassword = OTP;
                    tt.OTPExpireAt = createdAt.AddMinutes(10);
                    tt.ModifiedAt = createdAt;

                    UserLog ul = new UserLog();
                    ul.Remark = OTP + Constants.LOG_Temporary_Password_generated + createdAt.ToShortDateString();
                    ul.Data = new Security().Serialize<ForgetPasswordDTO>(req);
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
                    tt.UserLogs.Add(ul);
                    Update(tt);
                    string Tempid = "1207162427506623663";
                    BLSMS.SendSMS(req.MobileNo, "Your temporary password is " + OTP + "CareerMitra",Tempid);
                    return true;
                }
                else
                {
                    try
                    {
                        using (CareerMitraContainer db = new Data.CareerMitraContainer())
                        {
                            UserLog ul = new UserLog();
                            ul.Remark = Constants.LOG_TempPassword_generation_Failed + createdAt.ToShortDateString();
                            ul.Data = new Security().Serialize<ForgetPasswordDTO>(req);
                            ul.Error = "";
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
                    return false;
                }
            }
            catch (Exception ex)
            {
                try
                {
                    using (CareerMitraContainer db = new Data.CareerMitraContainer())
                    {
                        UserLog ul = new UserLog();
                        ul.Remark = Constants.LOG_OTP_GENERATION_FAILED + createdAt.ToShortDateString();
                        ul.Data = new Security().Serialize<ForgetPasswordDTO>(req);
                        ul.Error = ex.Message;
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
                return false;
            }
        }

        public int ResetPasswordMobile(ResetPasswordDTO req)
        {
            DateTime createdAt = new Constants().IST_DATE_TIME;
            var res = 0;
            try
            {
                var tt = GetAll(x => x.UserName == req.MobileNo && x.IsDeleted == false && x.TemporaryPassword == req.TempPassword).FirstOrDefault();
                if (tt != null)
                {
                    UserLog ul = new UserLog();
                    if (tt.OTPExpireAt < createdAt)
                    {
                        //otp expired 
                        res = 2;
                        ul.Remark = req.TempPassword + Constants.LOG_TEMP_PASSWORD_VERIFICATION_FAILED_EXPIRED + createdAt.ToShortDateString();
                    }
                    else
                    {
                        tt.Password = Security.EncryptString(Constants.EncKey, req.Password);
                        res = 1;
                        ul.Remark = req.TempPassword + Constants.LOG_RESET_PASSWORD_SUCCESSFUL + createdAt.ToShortDateString();
                    }

                    ul.Data = new Security().Serialize<ResetPasswordDTO>(req);
                    ul.CreatedAt = createdAt;
                    ul.OS = "";
                    ul.IsDeleted = false;
                    ul.DeviceId = req.DeviceId == null ? "" : req.DeviceId;
                    ul.Lat = req.Lat;
                    ul.Lng = req.Long;
                    ul.Address = req.Address;
                    ul.DeviceOtherInfo = req.DeviceOtherInfo;
                    ul.UserAgent = req.UserAgent;
                    ul.Domain = "";
                    ul.DeviceType = req.DeviceType;
                    ul.IP = "";
                    tt.UserLogs.Add(ul);
                }
                else
                {
                    UserLog ul = new UserLog();
                    ul.Remark = req.TempPassword + Constants.LOG_TEMP_PASSWORD_VERIFICATION_FAILED_INVALID + createdAt.ToShortDateString();
                    ul.Data = new Security().Serialize<ResetPasswordDTO>(req);
                    ul.CreatedAt = createdAt;
                    ul.OS = "";
                    ul.IsDeleted = false;
                    ul.DeviceId = req.DeviceId == null ? "" : req.DeviceId;
                    ul.Lat = req.Lat;
                    ul.Lng = req.Long;
                    ul.Address = req.Address;
                    ul.DeviceOtherInfo = req.DeviceOtherInfo;
                    ul.UserAgent = req.UserAgent;
                    ul.Domain = "";
                    ul.DeviceType = req.DeviceType;
                    ul.IP = "";
                    tt.UserLogs.Add(ul);
                    res = 0;
                }
                Update(tt);
            }
            catch (Exception ex)
            {
                try
                {
                    using (CareerMitraContainer db = new Data.CareerMitraContainer())
                    {
                        UserLog ul = new UserLog();
                        ul.Remark = Constants.LOG_OTP_GENERATION_FAILED + createdAt.ToShortDateString();
                        ul.Data = new Security().Serialize<ForgetPasswordDTO>(req);
                        ul.Error = ex.Message;
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
                res = -1;
            }
            return res;
        }

        public bool ChangePasswordMobile(ChangePasswordMobileDTO req)
        {
            DateTime createdAt = new Constants().IST_DATE_TIME;
            try
            {
                var oldPass = Security.EncryptString(Constants.EncKey, req.OldPassword);
                var tt = GetAll(x => x.Id == req.Id && x.IsDeleted == false && x.IsVerified == true && x.Password == oldPass).FirstOrDefault();
                if (tt != null)
                {
                    tt.Password = Security.EncryptString(Constants.EncKey, req.Password);
                    tt.ModifiedAt = createdAt;

                    UserLog ul = new UserLog();
                    ul.Remark = Constants.LOG_PASSWORD_CHANGE_SUCCESSFUL + createdAt.ToShortDateString();
                    ul.Data = new Security().Serialize<ChangePasswordMobileDTO>(req);
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
                    tt.UserLogs.Add(ul);
                    Update(tt);

                    return true;
                }
                else
                {
                    try
                    {
                        using (CareerMitraContainer db = new Data.CareerMitraContainer())
                        {
                            UserLog ul = new UserLog();
                            ul.Remark = Constants.LOG_USER_NOT_FOUND_CHANGE_PASSWORD + createdAt.ToShortDateString();
                            ul.Data = new Security().Serialize<ChangePasswordMobileDTO>(req);
                            ul.Error = "";
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
                    return false;
                }
            }
            catch (Exception ex)
            {
                try
                {
                    using (CareerMitraContainer db = new Data.CareerMitraContainer())
                    {
                        UserLog ul = new UserLog();
                        ul.Remark = Constants.LOG_PASSWORD_CHANGE_FAILED + createdAt.ToShortDateString();
                        ul.Data = new Security().Serialize<ChangePasswordMobileDTO
                            >(req);
                        ul.Error = ex.Message;
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
                return false;
            }
        }

        public bool UpdateProfilePersonalDetailMobile(UPdateProfilePersonalDetailDTO req)
        {
            DateTime createdAt = new Constants().IST_DATE_TIME;
            try
            {
                var tt = GetAll(x => x.Id == req.Id && x.IsDeleted == false && x.IsVerified == true).FirstOrDefault();
                if (tt != null)
                {
                    tt.ModifiedAt = createdAt;
                    var det = tt.UserDetails.FirstOrDefault();
                    if (det != null)
                    {
                        //det.Age = req.Age;
                        det.DOB = req.DOB;
                        det.Email = req.Email;
                        det.FatherName = req.FatherName;
                        det.MotherName = req.MotherName;
                        det.Mobile2 = req.Mobile2;
                        det.PinCode = req.PinCode;
                        det.Gender = req.Gender;
                        det.Address = req.PermanentAddress;
                        if (req.CityId != null)
                        {
                            det.CityId = req.CityId;
                        }
                    }
                    UserLog ul = new UserLog();
                    ul.Remark = Constants.LOG_UPDATE_PROFILE_PERSONAL_INFO_SUCCESS + createdAt.ToShortDateString();
                    ul.Data = new Security().Serialize<UPdateProfilePersonalDetailDTO>(req);
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
                    tt.UserLogs.Add(ul);
                    Update(tt);

                    return true;
                }
                else
                {
                    try
                    {
                        using (CareerMitraContainer db = new Data.CareerMitraContainer())
                        {
                            UserLog ul = new UserLog();
                            ul.Remark = Constants.LOG_UPDATE_PROFILE_PERSONAL_INFO_USER_NOT_FOUND + createdAt.ToShortDateString();
                            ul.Data = new Security().Serialize<UPdateProfilePersonalDetailDTO>(req);
                            ul.Error = "";
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
                    return false;
                }
            }
            catch (Exception ex)
            {
                try
                {
                    using (CareerMitraContainer db = new Data.CareerMitraContainer())
                    {
                        UserLog ul = new UserLog();
                        ul.Remark = Constants.LOG_UPDATE_PROFILE_PERSONAL_INFO_FAILED + createdAt.ToShortDateString();
                        ul.Data = new Security().Serialize<UPdateProfilePersonalDetailDTO
                            >(req);
                        ul.Error = ex.Message;
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
                return false;
            }
        }

        public bool UpdateProfileCompanyDetailMobile(UPdateProfileCompanyDetailDTO req)
        {
            DateTime createdAt = new Constants().IST_DATE_TIME;
            try
            {
                var tt = GetAll(x => x.Id == req.Id && x.IsDeleted == false && x.IsVerified == true).FirstOrDefault();
                if (tt != null)
                {
                    tt.ModifiedAt = createdAt;
                    var det = tt.UserDetails.FirstOrDefault();
                    if (det != null)
                    {
                        det.CompanyName = req.CompanyName;
                        det.Designation = req.Designation;
                    }
                    UserLog ul = new UserLog();
                    ul.Remark = Constants.LOG_UPDATE_PROFILE_COMPANY_INFO_SUCCESS + createdAt.ToShortDateString();
                    ul.Data = new Security().Serialize<UPdateProfileCompanyDetailDTO>(req);
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
                    tt.UserLogs.Add(ul);
                    Update(tt);

                    return true;
                }
                else
                {
                    try
                    {
                        using (CareerMitraContainer db = new Data.CareerMitraContainer())
                        {
                            UserLog ul = new UserLog();
                            ul.Remark = Constants.LOG_UPDATE_PROFILE_COMPANY_INFO_USER_NOT_FOUND + createdAt.ToShortDateString();
                            ul.Data = new Security().Serialize<UPdateProfileCompanyDetailDTO>(req);
                            ul.Error = "";
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
                    return false;
                }
            }
            catch (Exception ex)
            {
                try
                {
                    using (CareerMitraContainer db = new Data.CareerMitraContainer())
                    {
                        UserLog ul = new UserLog();
                        ul.Remark = Constants.LOG_UPDATE_PROFILE_COMPANY_INFO_FAILED + createdAt.ToShortDateString();
                        ul.Data = new Security().Serialize<UPdateProfileCompanyDetailDTO
                            >(req);
                        ul.Error = ex.Message;
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
                return false;
            }
        }

        public bool UpdateProfileAboutMobile(UPdateProfileAboutDTO req)
        {
            DateTime createdAt = new Constants().IST_DATE_TIME;
            try
            {
                var tt = GetAll(x => x.Id == req.Id && x.IsDeleted == false && x.IsVerified == true).FirstOrDefault();
                if (tt != null)
                {
                    tt.ModifiedAt = createdAt;
                    var det = tt.UserDetails.FirstOrDefault();
                    if (det != null)
                    {
                        det.About = req.About;
                    }
                    UserLog ul = new UserLog();
                    ul.Remark = Constants.LOG_UPDATE_PROFILE_ABOUT_SUCCESS + createdAt.ToShortDateString();
                    ul.Data = new Security().Serialize<UPdateProfileAboutDTO>(req);
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
                    tt.UserLogs.Add(ul);
                    Update(tt);

                    return true;
                }
                else
                {
                    try
                    {
                        using (CareerMitraContainer db = new Data.CareerMitraContainer())
                        {
                            UserLog ul = new UserLog();
                            ul.Remark = Constants.LOG_UPDATE_PROFILE_ABOUT_USER_NOT_FOUND + createdAt.ToShortDateString();
                            ul.Data = new Security().Serialize<UPdateProfileAboutDTO>(req);
                            ul.Error = "";
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
                    return false;
                }
            }
            catch (Exception ex)
            {
                try
                {
                    using (CareerMitraContainer db = new Data.CareerMitraContainer())
                    {
                        UserLog ul = new UserLog();
                        ul.Remark = Constants.LOG_UPDATE_PROFILE_ABOUT_FAILED + createdAt.ToShortDateString();
                        ul.Data = new Security().Serialize<UPdateProfileAboutDTO
                            >(req);
                        ul.Error = ex.Message;
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
                return false;
            }
        }

        public bool UpdateProfileSkillsMobile(UpdateProfileSkillsDTO req)
        {
            DateTime createdAt = new Constants().IST_DATE_TIME;
            try
            {
                var tt = GetAll(x => x.Id == req.Id && x.IsDeleted == false && x.IsVerified == true).FirstOrDefault();
                if (tt != null)
                {
                    tt.ModifiedAt = createdAt;
                    var det = tt.UserSkills.ToList();
                    if (det != null && det.Count > 0)
                    {
                        foreach (var item in det)
                        {
                            item.IsDeleted = true;
                        }
                    }

                    foreach (var item in req.SkillIds)
                    {
                        UserSkill us = new UserSkill();
                        us.IsDeleted = false;
                        us.CreatedAt = createdAt;
                        us.SkillId = item;
                        tt.UserSkills.Add(us);
                    }

                    UserLog ul = new UserLog();
                    ul.Remark = Constants.LOG_UPDATE_PROFILE_SKILLS_SUCCESS + createdAt.ToShortDateString();
                    ul.Data = new Security().Serialize<UpdateProfileSkillsDTO>(req);
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
                    tt.UserLogs.Add(ul);
                    Update(tt);

                    return true;
                }
                else
                {
                    try
                    {
                        using (CareerMitraContainer db = new Data.CareerMitraContainer())
                        {
                            UserLog ul = new UserLog();
                            ul.Remark = Constants.LOG_UPDATE_PROFILE_SKILLS_USER_NOT_FOUND + createdAt.ToShortDateString();
                            ul.Data = new Security().Serialize<UpdateProfileSkillsDTO>(req);
                            ul.Error = "";
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
                    return false;
                }
            }
            catch (Exception ex)
            {
                try
                {
                    using (CareerMitraContainer db = new Data.CareerMitraContainer())
                    {
                        UserLog ul = new UserLog();
                        ul.Remark = Constants.LOG_UPDATE_PROFILE_SKILLS_FAILED + createdAt.ToShortDateString();
                        ul.Data = new Security().Serialize<UpdateProfileSkillsDTO
                            >(req);
                        ul.Error = ex.Message;
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
                return false;
            }
        }

        public bool UpdateProfileEducationMobile(UpdateProfileEducationDTO req)
        {
            DateTime createdAt = new Constants().IST_DATE_TIME;
            try
            {
                var tt = GetAll(x => x.Id == req.Id && x.IsDeleted == false && x.IsVerified == true).FirstOrDefault();
                if (tt != null)
                {
                    tt.ModifiedAt = createdAt;
                    var det = tt.UserEducations.ToList();
                    if (det != null && det.Count > 0)
                    {
                        foreach (var item in det)
                        {
                            item.IsDeleted = true;
                        }
                    }

                    foreach (var item in req.Educations)
                    {
                        UserEducation us = new UserEducation();
                        us.IsDeleted = false;
                        us.CreatedAt = createdAt;
                        us.CollegeName = item.College;
                        us.CourseId = item.CourseId;
                        us.YearOfPassing = item.YearOfPassing.ToString();
                        tt.UserEducations.Add(us);
                    }

                    UserLog ul = new UserLog();
                    ul.Remark = Constants.LOG_UPDATE_PROFILE_EDUCATION_SUCCESS + createdAt.ToShortDateString();
                    ul.Data = new Security().Serialize<UpdateProfileEducationDTO>(req);
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
                    tt.UserLogs.Add(ul);
                    Update(tt);

                    return true;
                }
                else
                {
                    try
                    {
                        using (CareerMitraContainer db = new Data.CareerMitraContainer())
                        {
                            UserLog ul = new UserLog();
                            ul.Remark = Constants.LOG_UPDATE_PROFILE_EDUCATION_USER_NOT_FOUND + createdAt.ToShortDateString();
                            ul.Data = new Security().Serialize<UpdateProfileEducationDTO>(req);
                            ul.Error = "";
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
                    return false;
                }
            }
            catch (Exception ex)
            {
                try
                {
                    using (CareerMitraContainer db = new Data.CareerMitraContainer())
                    {
                        UserLog ul = new UserLog();
                        ul.Remark = Constants.LOG_UPDATE_PROFILE_EDUCATION_FAILED + createdAt.ToShortDateString();
                        ul.Data = new Security().Serialize<UpdateProfileEducationDTO
                            >(req);
                        ul.Error = ex.Message;
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
                return false;
            }
        }

        public bool UpdateProfileExperienceMobile(UpdateProfileExperienceDTO req)
        {
            DateTime createdAt = new Constants().IST_DATE_TIME;
            try
            {
                var tt = GetAll(x => x.Id == req.Id && x.IsDeleted == false && x.IsVerified == true).FirstOrDefault();
                if (tt != null)
                {
                    tt.ModifiedAt = createdAt;
                    var det = tt.UserExperiences.ToList();
                    if (det != null && det.Count > 0)
                    {
                        foreach (var item in det)
                        {
                            item.IsDeleted = true;
                        }
                    }

                    foreach (var item in req.Experiences)
                    {
                        UserExperience us = new UserExperience();
                        us.IsDeleted = false;
                        us.CreatedAt = createdAt;
                        us.CompanyName = item.Company;
                        us.Description = item.Designation;
                        us.Designation = item.Designation;
                        us.YearFrom = item.YearFrom.ToString();
                        //if(item.IsCurrent==true)
                        //{
                        //    us.YearTo = "";
                        //}
                        us.YearTo = item.YearTo.ToString();
                        us.IsCurrent = item.IsCurrent;
                        tt.UserExperiences.Add(us);
                    }

                    UserLog ul = new UserLog();
                    ul.Remark = Constants.LOG_UPDATE_PROFILE_EXPERIENCE_SUCCESS + createdAt.ToShortDateString();
                    ul.Data = new Security().Serialize<UpdateProfileExperienceDTO>(req);
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
                    tt.UserLogs.Add(ul);
                    Update(tt);

                    return true;
                }
                else
                {
                    try
                    {
                        using (CareerMitraContainer db = new Data.CareerMitraContainer())
                        {
                            UserLog ul = new UserLog();
                            ul.Remark = Constants.LOG_UPDATE_PROFILE_EXPERIENCE_USER_NOT_FOUND + createdAt.ToShortDateString();
                            ul.Data = new Security().Serialize<UpdateProfileExperienceDTO>(req);
                            ul.Error = "";
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
                    return false;
                }
            }
            catch (Exception ex)
            {
                try
                {
                    using (CareerMitraContainer db = new Data.CareerMitraContainer())
                    {
                        UserLog ul = new UserLog();
                        ul.Remark = Constants.LOG_UPDATE_PROFILE_EXPERIENCE_FAILED + createdAt.ToShortDateString();
                        ul.Data = new Security().Serialize<UpdateProfileExperienceDTO
                            >(req);
                        ul.Error = ex.Message;
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
                return false;
            }
        }

        public bool UpdateProfilePic(int Id, string FileName, string DeviceId, string Lat, string Long, string Address)
        {
            DateTime createdAt = new Constants().IST_DATE_TIME;
            try
            {
                var tt = GetAll(x => x.Id == Id && x.IsDeleted == false && x.IsVerified == true).FirstOrDefault();
                if (tt != null)
                {
                    tt.ModifiedAt = createdAt;
                    var det = tt.UserDetails.FirstOrDefault();
                    if (det != null)
                    {
                        if (!string.IsNullOrEmpty(FileName))
                        {
                            det.Photo = FileName;
                        }
                        else
                        {
                            return false;
                        }
                    }


                    UserLog ul = new UserLog();
                    ul.Remark = Constants.LOG_UPDATE_PROFILE_PHOTO_SUCCESS + createdAt.ToShortDateString();
                    ul.Data = Id + " | " + FileName;
                    ul.CreatedAt = createdAt;
                    ul.OS = "";
                    ul.IsDeleted = false;
                    ul.DeviceId = DeviceId;
                    ul.Lat = Lat;
                    ul.Lng = Long;
                    ul.Address = Address;
                    ul.DeviceOtherInfo = "";
                    ul.UserAgent = "";
                    ul.Domain = "";
                    ul.DeviceType = DeviceType.Mobile;
                    ul.IP = "";
                    tt.UserLogs.Add(ul);
                    Update(tt);

                    return true;
                }
                else
                {
                    try
                    {
                        using (CareerMitraContainer db = new Data.CareerMitraContainer())
                        {
                            UserLog ul = new UserLog();
                            ul.Remark = Constants.LOG_UPDATE_PROFILE_PHOTO_USER_NOT_FOUND + createdAt.ToShortDateString();
                            ul.Data = Id + " | " + FileName;
                            ul.Error = "";
                            ul.CreatedAt = createdAt;
                            ul.OS = "";
                            ul.IsDeleted = false;
                            ul.DeviceId = DeviceId;
                            ul.Lat = Lat;
                            ul.Lng = Long;
                            ul.Address = Address;
                            ul.DeviceOtherInfo = "";
                            ul.UserAgent = "";
                            ul.Domain = "";
                            ul.DeviceType = DeviceType.Mobile;
                            ul.IP = "";
                            db.UserLogs.Add(ul);
                            db.SaveChanges();
                        }
                    }
                    catch (Exception e)
                    {

                    }
                    return false;
                }
            }
            catch (Exception ex)
            {
                try
                {
                    using (CareerMitraContainer db = new Data.CareerMitraContainer())
                    {
                        UserLog ul = new UserLog();
                        ul.Remark = Constants.LOG_UPDATE_PROFILE_PHOTO_FAILED + createdAt.ToShortDateString();
                        ul.Data = Id + " | " + FileName;
                        ul.Error = ex.Message;
                        ul.CreatedAt = createdAt;
                        ul.OS = "";
                        ul.IsDeleted = false;
                        ul.DeviceId = DeviceId;
                        ul.Lat = Lat;
                        ul.Lng = Long;
                        ul.Address = Address;
                        ul.DeviceOtherInfo = "";
                        ul.UserAgent = "";
                        ul.Domain = "";
                        ul.DeviceType = DeviceType.Mobile;
                        ul.IP = "";
                        db.UserLogs.Add(ul);
                        db.SaveChanges();
                    }
                }
                catch (Exception e)
                {

                }
                return false;
            }
        }

        public bool UpdateResume(int Id, string FileName, string DeviceId, string Lat, string Long, string Address)
        {
            DateTime createdAt = new Constants().IST_DATE_TIME;
            try
            {
                var tt = GetAll(x => x.Id == Id && x.IsDeleted == false && x.IsVerified == true).FirstOrDefault();
                if (tt != null)
                {
                    tt.ModifiedAt = createdAt;
                    var det = tt.UserDocuments.Where(x => x.IsDeleted == false && x.DocumentTypeId == DocumentType.Resume).FirstOrDefault();
                    if (det != null)
                    {
                        if (!string.IsNullOrEmpty(FileName))
                        {
                            det.Attachment = FileName;
                            det.Name = FileName;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(FileName))
                        {
                            UserDocument d = new UserDocument();
                            d.IsDeleted = false;
                            d.CreatedAt = createdAt;
                            d.Attachment = FileName;
                            d.DocumentTypeId = DocumentType.Resume;
                            d.Name = FileName;
                            tt.UserDocuments.Add(d);
                        }
                        else
                        {
                            return false;
                        }
                    }


                    UserLog ul = new UserLog();
                    ul.Remark = Constants.LOG_UPDATE_PROFILE_RESUME_SUCCESS + createdAt.ToShortDateString();
                    ul.Data = Id + " | " + FileName;
                    ul.CreatedAt = createdAt;
                    ul.OS = "";
                    ul.IsDeleted = false;
                    ul.DeviceId = DeviceId;
                    ul.Lat = Lat;
                    ul.Lng = Long;
                    ul.Address = Address;
                    ul.DeviceOtherInfo = "";
                    ul.UserAgent = "";
                    ul.Domain = "";
                    ul.DeviceType = DeviceType.Mobile;
                    ul.IP = "";
                    tt.UserLogs.Add(ul);
                    Update(tt);

                    return true;
                }
                else
                {
                    try
                    {
                        using (CareerMitraContainer db = new Data.CareerMitraContainer())
                        {
                            UserLog ul = new UserLog();
                            ul.Remark = Constants.LOG_UPDATE_PROFILE_RESUME_USER_NOT_FOUND + createdAt.ToShortDateString();
                            ul.Data = Id + " | " + FileName;
                            ul.Error = "";
                            ul.CreatedAt = createdAt;
                            ul.OS = "";
                            ul.IsDeleted = false;
                            ul.DeviceId = DeviceId;
                            ul.Lat = Lat;
                            ul.Lng = Long;
                            ul.Address = Address;
                            ul.DeviceOtherInfo = "";
                            ul.UserAgent = "";
                            ul.Domain = "";
                            ul.DeviceType = DeviceType.Mobile;
                            ul.IP = "";
                            db.UserLogs.Add(ul);
                            db.SaveChanges();
                        }
                    }
                    catch (Exception e)
                    {

                    }
                    return false;
                }
            }
            catch (Exception ex)
            {
                try
                {
                    using (CareerMitraContainer db = new Data.CareerMitraContainer())
                    {
                        UserLog ul = new UserLog();
                        ul.Remark = Constants.LOG_UPDATE_PROFILE_RESUME_FAILED + createdAt.ToShortDateString();
                        ul.Data = Id + " | " + FileName;
                        ul.Error = ex.Message;
                        ul.CreatedAt = createdAt;
                        ul.OS = "";
                        ul.IsDeleted = false;
                        ul.DeviceId = DeviceId;
                        ul.Lat = Lat;
                        ul.Lng = Long;
                        ul.Address = Address;
                        ul.DeviceOtherInfo = "";
                        ul.UserAgent = "";
                        ul.Domain = "";
                        ul.DeviceType = DeviceType.Mobile;
                        ul.IP = "";
                        db.UserLogs.Add(ul);
                        db.SaveChanges();
                    }
                }
                catch (Exception e)
                {

                }
                return false;
            }
        }
        public bool ApproveEmployer(int Id, int UserId)
        {
            try
            {
                DateTime createdAt = new Constants().IST_DATE_TIME;
                var d = GetById(Id);
                if (d != null)
                {
                    UserDetail ud = new UserDetail();
                    d.IsVerified = true;
                    d.ModifiedAt = createdAt;
                    Update(d);
                    string TempId = "";
                    BLSMS.SendSMS(d.UserName, "You are successfully verified by Admin. Your User Name is " + d.UserName + " and Password is " + Security.DecryptString(Constants.EncKey, d.Password) + "from CareerMitra ", TempId);
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
        public bool AddEmployee(UpdateUserDTO req)
        {
            try
            {
                var domainName = Util.GetDomainName();
                var IPAddress = Util.GetIPAddress();
                var OSVersion = Util.GetOSVersion();
                DateTime createdAt = new Constants().IST_DATE_TIME;
                var a = GetAll(x => x.UserName == req.Mobile && x.IsDeleted == false).FirstOrDefault();
                if (a != null)
                {

                    UserLog ul = new UserLog();
                    ul.Remark = Constants.LOG_USER_REGISTRATION_ATTEMPT_FAILED + createdAt.ToShortDateString();
                    ul.Error = Constants.LOG_USER_REGISTRATION_MOBILENUMBER_ALREADYUSED;
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
                    return false;
                }
                else
                {

                    var tt = new User();
                    UserDetail ud = new UserDetail();
                    if (req.PAN != null)
                    {
                        UserDocument pan = new UserDocument();
                        pan.Attachment = req.PAN;
                        pan.IsDeleted = false;
                        pan.DocumentTypeId = DocumentType.Pan;
                        pan.CreatedAt = createdAt;
                        pan.Name = req.PANNo;
                        tt.UserDocuments.Add(pan);
                    }
                    if (req.Aadhar != null)
                    {
                        UserDocument aadhar = new UserDocument();
                        aadhar.Attachment = req.Aadhar;
                        aadhar.IsDeleted = false;
                        aadhar.DocumentTypeId = DocumentType.Aadhar;
                        aadhar.CreatedAt = createdAt;
                        aadhar.Name = req.AadharNo;
                        tt.UserDocuments.Add(aadhar);
                    }
                    if (req.CompanyCertificate != null)
                    {
                        UserDocument certificate = new UserDocument();
                        certificate.Attachment = req.CompanyCertificate;
                        certificate.IsDeleted = false;
                        certificate.CreatedAt = createdAt;
                        certificate.DocumentTypeId = DocumentType.Certificate;
                        certificate.Name = req.CompanyCertificate;
                        tt.UserDocuments.Add(certificate);
                    }
                    ud.FirstName = req.FirstName;
                    ud.Email = req.Email;
                    ud.Mobile = req.Mobile;
                    ud.LastName = req.LastName;
                    ud.Address = req.Address;
                    ud.State = req.State;
                    ud.Address = req.Address;
                    ud.PinCode = req.PinCode;
                    ud.CompanyType = req.CompanyType;
                    ud.CompanyName = req.CompanyName;
                    ud.NoOfEmployees = req.NoOfEmployees;
                    ud.Lat = req.Lat;
                    ud.Long = req.Long;
                    ud.CityId = req.CityId;
                    if (req.Photo != null)
                    {
                        ud.Photo = req.Photo;
                    }
                    tt.IsBlocked = false;
                    tt.UserName = req.Mobile;
                    tt.RoleId = req.RoleId;
                    tt.IsChangePassword = false;
                    tt.Password = req.Password;
                    tt.TemporaryPassword = "";
                    tt.UserDetails.Add(ud);
                    tt.CreatedAt = createdAt;
                    tt.IsDeleted = false;
                    Add(tt);
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool LogoutMobile(UserLogoutDTO req)
        {
            DateTime createdAt = new Constants().IST_DATE_TIME;
            try
            {
                var tt = GetAll(x => x.UserDevices.Where(xx => x.Id == req.Id && xx.IsDeleted == false && xx.DeviceId == req.DeviceId).Any() && x.IsDeleted == false && x.IsVerified == true).FirstOrDefault();
                if (tt != null)
                {
                    var ud = tt.UserDevices.Where(xx => xx.IsDeleted == false && xx.DeviceId == req.DeviceId).FirstOrDefault();
                    ud.DeviceId = "";
                    tt.ModifiedAt = createdAt;
                    UserLog ul = new UserLog();
                    ul.Remark = tt.Id.ToString() + Constants.LOG_USER_LOGGED_OUT + createdAt.ToShortDateString();
                    ul.Data = new Security().Serialize<UserLogoutDTO>(req);
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
                    tt.UserLogs.Add(ul);
                    Update(tt);
                    return true;
                }
                else
                {
                    UserLog ul = new UserLog();
                    ul.Remark = tt.Id.ToString() + Constants.LOG_USER_LOG_OUT_FAILED + createdAt.ToShortDateString();
                    ul.Data = new Security().Serialize<UserLogDTO>(req);
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
                    tt.UserLogs.Add(ul);
                    Update(tt);
                    return false;
                }
            }
            catch (Exception ex)
            {
                try
                {
                    using (CareerMitraContainer db = new Data.CareerMitraContainer())
                    {
                        UserLog ul = new UserLog();
                        ul.Remark = Constants.LOG_USER_LOG_OUT_ERROR + createdAt.ToShortDateString();
                        ul.Data = new Security().Serialize<UserLogDTO>(req);
                        ul.Error = ex.Message;
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
                return false;
            }
        }

        public bool UpdateProfilePicWeb(UPdateProfileDTO model)
        {
            DateTime createdAt = new Constants().IST_DATE_TIME;
            try
            {
                var tt = GetAll(x => x.Id == model.UserId && x.IsDeleted == false && x.IsVerified == true).FirstOrDefault();
                if (tt != null)
                {
                    tt.ModifiedAt = createdAt;
                    var det = tt.UserDetails.FirstOrDefault();
                    if (det != null)
                    {
                        if (!string.IsNullOrEmpty(model.Photo))
                        {
                            det.Photo = model.Photo;
                        }
                        else
                        {
                            return false;
                        }
                    }


                    UserLog ul = new UserLog();
                    ul.Remark = Constants.LOG_UPDATE_PROFILE_PHOTO_SUCCESS + createdAt.ToShortDateString();
                    ul.Data = model.UserId + " | " + model.Photo;
                    ul.CreatedAt = createdAt;
                    ul.OS = "";
                    ul.IsDeleted = false;
                    ul.DeviceId = model.DeviceId == null ? "" : model.DeviceId;
                    ul.Lat = model.Lat == null ? "" : model.Lat;
                    ul.Lng = model.Long == null ? "" : model.Long;
                    ul.Address = model.Address == null ? "" : model.Address;
                    ul.DeviceOtherInfo = "";
                    ul.UserAgent = "";
                    ul.Domain = "";
                    ul.DeviceType = DeviceType.Mobile;
                    ul.IP = "";
                    tt.UserLogs.Add(ul);
                    Update(tt);

                    return true;
                }
                else
                {
                    try
                    {
                        using (CareerMitraContainer db = new Data.CareerMitraContainer())
                        {
                            UserLog ul = new UserLog();
                            ul.Remark = Constants.LOG_UPDATE_PROFILE_PHOTO_USER_NOT_FOUND + createdAt.ToShortDateString();
                            ul.Data = model.UserId + " | " + model.Photo;
                            ul.CreatedAt = createdAt;
                            ul.OS = "";
                            ul.IsDeleted = false;
                            ul.DeviceId = model.DeviceId == null ? "" : model.DeviceId;
                            ul.Lat = model.Lat == null ? "" : model.Lat;
                            ul.Lng = model.Long == null ? "" : model.Long;
                            ul.Address = model.Address == null ? "" : model.Address;
                            ul.DeviceOtherInfo = "";
                            ul.UserAgent = "";
                            ul.Domain = "";
                            ul.DeviceType = DeviceType.Mobile;
                            ul.IP = "";
                            db.UserLogs.Add(ul);
                            db.SaveChanges();
                        }
                    }
                    catch (Exception e)
                    {

                    }
                    return false;
                }
            }
            catch (Exception ex)
            {
                try
                {
                    using (CareerMitraContainer db = new Data.CareerMitraContainer())
                    {
                        UserLog ul = new UserLog();
                        ul.Remark = Constants.LOG_UPDATE_PROFILE_PHOTO_FAILED + createdAt.ToShortDateString();
                        ul.Data = model.UserId + " | " + model.Photo;
                        ul.CreatedAt = createdAt;
                        ul.OS = "";
                        ul.IsDeleted = false;
                        ul.DeviceId = model.DeviceId == null ? "" : model.DeviceId;
                        ul.Lat = model.Lat == null ? "" : model.Lat;
                        ul.Lng = model.Long == null ? "" : model.Long;
                        ul.Address = model.Address == null ? "" : model.Address;
                        ul.DeviceOtherInfo = "";
                        ul.UserAgent = "";
                        ul.Domain = "";
                        ul.DeviceType = DeviceType.Mobile;
                        ul.IP = "";
                        db.UserLogs.Add(ul);
                        db.SaveChanges();
                    }
                }
                catch (Exception e)
                {

                }
                return false;
            }
        }
        public List<UserNotificationDTO> GetFCMIdofUsers(string MobileNo)
        {
            if (MobileNo != null && MobileNo != "")
            {
                var fcm = from x in db.UserDevices
                          where x.IsDeleted == false && x.FCMId != null && x.MobileNo == MobileNo
                          select x;
                var lst = fcm.Select(x => new UserNotificationDTO
                {
                    UserId = x.UserId,
                    UserDeviceId = x.Id,
                    FCMId = x.FCMId,
                }).ToList();
                return lst;
            }
            else
            {
                var fcm = from x in db.UserDevices
                          where x.IsDeleted == false && x.FCMId != null
                          select x;
                var lst = fcm.Select(x => new UserNotificationDTO
                {
                    UserId = x.UserId,
                    UserDeviceId = x.Id,
                    FCMId = x.FCMId,
                }).ToList();
                return lst;
            }
        }
    }
}
