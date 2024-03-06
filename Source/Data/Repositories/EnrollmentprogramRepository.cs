using Data.DTOs;
using Data.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using Utility;
using Utility.Enums;

namespace Data.Repositories
{
    public class EnrollmentProgramRepository : RepositoryBase<EnrollmentProgram>, IEnrollmentProgramRepository
    {
        string domainName = Util.GetDomainName();
        string IPAddress = Util.GetIPAddress();
        string OSVersion = Util.GetOSVersion();
        public bool Add(EnrollmentProgramDTO req)
        {
            try
            {
                DateTime createdAt = new Constants().IST_DATE_TIME;
                var tt = new EnrollmentProgram();
                tt.CreatedAt = createdAt;
                tt.IsDeleted = false;
                tt.Title = req.Title;
                tt.TitleH = req.TitleH;
                tt.Description = req.Description;
                tt.DescriptionH = req.DescriptionH;
                tt.PublishDate = req.PublishDate;
                tt.DateStart = req.DateStart;
                tt.DateEnd = req.DateEnd;
                tt.LastDate = req.LastDate;
                tt.Image = req.Image;
                Add(tt);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Update(UpdateEnrollmentProgramDTO req)
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
                    d.Description = req.Description;
                    d.DescriptionH = req.DescriptionH;
                    d.PublishDate = req.PublishDate;
                    d.DateStart = req.DateStart;
                    d.DateEnd = req.DateEnd;
                    d.LastDate = req.LastDate;
                    if (req.Image != null)
                    {
                        d.Image = req.Image;
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

        public List<EnrollmentProgram> GetAll()
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
        public EnrollmentProgram GetDetailById(int Id)
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
        public EnrollmentProgramResponseDTO GetEnrollmentProgram(EnrollmentProgramRequestDTO model)
        {
            EnrollmentProgramResponseDTO obj = new EnrollmentProgramResponseDTO();
            UserLog ul = new UserLog();
            DateTime createdAt = new Constants().IST_DATE_TIME;
            try
            {
                var today = DateTime.Now;
                var mdata = from x in db.EnrollmentPrograms
                            where x.IsDeleted == false && x.PublishDate<= today && today <=x.LastDate
                            select x;
                var data = mdata.Select(x => new EnrollmentProgramDTOMobile
                {
                    Title = model.Language == Language.English ? x.Title : x.TitleH,
                    PublishDate = x.PublishDate,
                    LastDate = x.LastDate,
                    Description = model.Language == Language.English ? x.Description : x.DescriptionH,
                    StartDate = x.DateStart,
                    EndDate = x.DateEnd,
                    Id = x.Id,
                    Image=x.Image!=null?Constants.BASE_URL+"FileUpload/Other/"+x.Image:"",
                }).OrderByDescending(x => x.Id).ToList();

                obj.lstProgram = data;

                ul.Remark = Constants.HTTPSTATUS_RECORD_FOUND + createdAt.ToShortDateString();
            }
            catch (Exception ex)
            {
                ul.Remark = Constants.HTTPSTATUS_ERROR_OCCURED + createdAt.ToShortDateString();
                ul.Error = ex.Message;
                obj.lstProgram = null;
            }
            try
            {
                using (CareerMitraContainer db = new Data.CareerMitraContainer())
                {
                    ul.Data = new Security().Serialize<EnrollmentProgramRequestDTO>(model);
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
        public int ApplyEnrollmentProgram(EnrollmentApplyDTO model)
        {
            UserLog ul = new UserLog();
            DateTime createdAt = new Constants().IST_DATE_TIME;
            int res = 0;
            try
            {
                var d = GetAll(x => x.IsDeleted == false && x.Id == model.EnrollmentProgramId).FirstOrDefault();
                if (d != null)
                {
                    if (d.UserEnrollmentPrograms.Where(xx => xx.IsDeleted == false && xx.UserId == model.Id).Any())
                    {
                        ul.Remark = Constants.LOG_APPLY_ENROLLMENT_PROGRAM_ALREADY_APPLIED + createdAt.ToShortDateString();
                        res = -1;//already applied
                    }
                    else
                    {
                        UserEnrollmentProgram u = new UserEnrollmentProgram();
                        u.CreatedAt = createdAt;
                        u.IsDeleted = false;
                        u.UserId = model.Id;
                        d.UserEnrollmentPrograms.Add(u);
                        Update(d);
                        
                        ul.Remark = Constants.LOG_APPLY_ENROLLMENT_PROGRAM_SUCCESSFUL + createdAt.ToShortDateString();
                        res = 1;
                    }
                }
                else
                {
                    ul.Remark = Constants.LOG_APPLY_ENROLLMENT_PROGRAM_ALREADY_APPLIED + createdAt.ToShortDateString();
                    res = -1;//already applied
                }
            }
            catch (Exception ex)
            {
                ul.Remark = Constants.LOG_APPLY_ENROLLMENT_PROGRAM_FAILED + createdAt.ToShortDateString();
                ul.Error = ex.Message;
                res = 0;
            }
            try
            {
                using (CareerMitraContainer db = new Data.CareerMitraContainer())
                {
                    ul.Data = new Security().Serialize<EnrollmentApplyDTO>(model);
                    ul.CreatedAt = createdAt;
                    ul.OS = OSVersion;
                    ul.IsDeleted = false;
                    ul.DeviceId = model.DeviceId!=null?model.DeviceId:"";
                    ul.Lat = model.Lat;
                    ul.Lng = model.Long;
                    ul.Address = model.Address;
                    ul.DeviceOtherInfo = model.DeviceOtherInfo;
                    ul.UserAgent = model.UserAgent;
                    ul.Domain = domainName;
                    ul.DeviceType = DeviceType.Web;
                    ul.IP = IPAddress;
                    db.UserLogs.Add(ul);
                    db.SaveChanges();
                }
            }
            catch (Exception e)
            {

            }
            return res;
        }

        public EnrollmentProgramResponseDTOWeb GetEnrollmentProgramWeb(EnrollmentProgramRequestDTO model, int UserId)
        {
            EnrollmentProgramResponseDTOWeb obj = new EnrollmentProgramResponseDTOWeb();
            UserLog ul = new UserLog();
            DateTime createdAt = new Constants().IST_DATE_TIME;
            try
            {
                var today = DateTime.Now;
                var mdata = from x in db.EnrollmentPrograms
                            where x.IsDeleted == false && x.PublishDate <= today && today <= x.LastDate
                            select x;
                var data = mdata.Select(x => new EnrollmentProgramDTOWeb
                {
                    Title = model.Language == Language.English ? x.Title : x.TitleH,
                    PublishDate = x.PublishDate,
                    //AppliedUserStatus = x.UserEnrollmentPrograms!=null? (x.UserEnrollmentPrograms.Select(xx => xx.IsDeleted == false && xx.UserId == UserId) != null ? true : false):false,
                    LastDate = x.LastDate,
                    Description = model.Language == Language.English ? x.Description : x.DescriptionH,
                    StartDate = x.DateStart,
                    EndDate = x.DateEnd,
                    Id = x.Id,
                    Image = x.Image != null ? Constants.BASE_URL + "FileUpload/Other/" + x.Image : "",
                }).OrderByDescending(x => x.Id).ToList();

                obj.lstProgram = data;

                ul.Remark = Constants.HTTPSTATUS_RECORD_FOUND + createdAt.ToShortDateString();
            }
            catch (Exception ex)
            {
                ul.Remark = Constants.HTTPSTATUS_ERROR_OCCURED + createdAt.ToShortDateString();
                ul.Error = ex.Message;
                obj.lstProgram = null;
            }
            try
            {
                using (CareerMitraContainer db = new Data.CareerMitraContainer())
                {
                    ul.Data = new Security().Serialize<EnrollmentProgramRequestDTO>(model);
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
    }
}
