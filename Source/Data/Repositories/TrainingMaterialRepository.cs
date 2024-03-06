using Data.DTOs;
using Data.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using Utility;
using Utility.Enums;

namespace Data.Repositories
{
    public class TrainingMaterialRepository : RepositoryBase<TrainingMaterial>, ITrainingMaterialRepository
    {
        public bool Add(TrainingMaterialDTO req)
        {
            try
            {
                DateTime createdAt = new Constants().IST_DATE_TIME;
                var tt = new TrainingMaterial();
                tt.CreatedAt = createdAt;
                tt.IsDeleted = false;
                tt.Title = req.Title;
                tt.TitleH = req.TitleH;
                tt.ShortDescription = req.ShortDescription;
                tt.ShortDescriptionH = req.ShortDescriptionH;
                tt.Description = req.Description;
                tt.DescriptionH = req.DescriptionH;
                tt.Link = req.Link;
                tt.PublishDate = req.PublishDate;
                tt.Type = req.Type;
                tt.Image = req.Image;
                tt.Attachment = req.Attachment;
                Add(tt);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Update(UpdateTrainingMaterialDTO req)
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
                    d.ShortDescription = req.ShortDescription;
                    d.ShortDescriptionH = req.ShortDescriptionH;
                    d.Description = req.Description;
                    d.DescriptionH = req.DescriptionH;
                    d.Link = req.Link;
                    d.Type = req.Type;
                    if(req.Image!=null)
                    {
                        d.Image = req.Image;
                    }
                    if(req.Attachment!=null)
                    {
                        d.Attachment = req.Attachment;
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

        public List<TrainingMaterial> GetAll()
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
        public TrainingMaterial GetDetailById(int Id)
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
        public TrainingMaterialResponseDTO GetTrainingMaterial(TrainingMaterialRequestDTO model)
        {
            TrainingMaterialResponseDTO obj = new TrainingMaterialResponseDTO();
            UserLog ul = new UserLog();
            DateTime createdAt = new Constants().IST_DATE_TIME;
            try
            {
                var mdata = from x in db.TrainingMaterials
                            where x.IsDeleted == false && x.IsPublished == true
                            select x;

                //Sorting    
                
                //Paging     
                var data = mdata.Select(x => new TrainingMaterialDTOMobile
                {
                    Title = model.Language == Language.English ? x.Title : x.TitleH,
                    ShortDescription = model.Language == Language.English ? x.Title : x.TitleH,
                    PublishDate = x.PublishDate,
                    Description = model.Language == Language.English ? x.Description : x.DescriptionH,
                    Link = x.Link,
                    Attachment = x.Attachment != null ? Constants.TRAINING_MATERIAL_URL+ x.Attachment : "",
                    Id = x.Id,
                    Type = x.Type,
                    Image = x.Image != null ? Constants.TRAINING_MATERIAL_URL + x.Image : "",
                }).OrderByDescending(x => x.Id).ToList();

                obj.lstTMaterial = data;

                ul.Remark = Constants.LOG_JOB_SEARCH_SUCCESS + createdAt.ToShortDateString();
            }
            catch (Exception ex)
            {
                ul.Remark = Constants.LOG_JOB_SEARCH_FAILED + createdAt.ToShortDateString();
                ul.Error = ex.Message;
                obj.lstTMaterial = null;
            }
            try
            {
                using (CareerMitraContainer db = new Data.CareerMitraContainer())
                {
                    ul.Data = new Security().Serialize<TrainingMaterialRequestDTO>(model);
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
