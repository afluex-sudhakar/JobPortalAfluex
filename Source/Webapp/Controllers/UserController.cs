using Data;
using Data.DTOs;
using Data.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Utility;
using Utility.Enums;
using Webapp.Filter;

namespace Webapp.Controllers
{
    [ValidateInput(false)]
    public class UserController : BaseController
    {
        private readonly IUserRepository _userRepository;
        private readonly IJobRepository _jobRepository;
        private readonly ICityRepository _cityRepository;
        private readonly ISkillRepository _skillRepository;
        private readonly ICourseRepository _courseRepository;
        private readonly IJobRoleRepository _jobRoleRepository;
        private readonly IChatRepository _chatRepository;
        private readonly IUserJobRepository _userJobRepository;

        public UserController(ICityRepository cityRepository, IUserRepository userRepository, IJobRepository jobRepository, ISkillRepository skillRepository, ICourseRepository courseRepository, IJobRoleRepository jobRoleRepository, IChatRepository chatRepository, IUserJobRepository userJobRepository)
        {
            this._userRepository = userRepository;
            this._jobRepository = jobRepository;
            this._cityRepository = cityRepository;
            this._skillRepository = skillRepository;
            this._courseRepository = courseRepository;
            this._jobRoleRepository = jobRoleRepository;
            this._chatRepository = chatRepository;
            this._userJobRepository = userJobRepository;
        }

        public ActionResult ResetPassword(UserDTO model)
        {
            if (TempData["MobileNo"] != null)
            {
                model.MobileNo = TempData["MobileNo"].ToString();
                if (TempData["error"] != null)
                {
                    TempData["MobileNo"] = model.MobileNo;
                    TempData["error"] = "dfgfhghgfh";
                    model.TemporaryPassword = null;
                    return View(model);
                }
                else
                {

                    return View(model);
                }
            }
            else
            {
                TempData["MobileNo"] = model.MobileNo;
                return View(model);
            }

        }

        public ActionResult ValidateTempPassword(string TemporaryPassword, string MobileNo, string userAgent)
        {
            UserDTO model = new UserDTO();
            model.DeviceType = DeviceType.Web;
            model.UserAgent = userAgent;
            model.TemporaryPassword = TemporaryPassword;
            model.MobileNo = MobileNo;
            try
            {
                var msg = _userRepository.ValidateTemporaryPassword(model);
                if (msg == 0)
                {
                    TempData["MobileNo"] = model.MobileNo;
                    model.Response = "1";
                    return Json(model, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    TempData["MobileNo"] = model.MobileNo;
                    model.Result = Constants.LOG_TempPassword_VERIFICATION_Failed;
                    model.Response = "0";
                    return Json(model, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "')<script>");
            }
            return RedirectToAction("ResetPassword");
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult SaveResetPassword(UserDTO model)
        {
            try
            {
                Response resp = new Response();
                //string dcData = Security.EncryptString(Constants.EncKey, model.Password);
                //model.Password = dcData;
                bool msg = _userRepository.ResetPassword(model);
                if (msg == true)
                {
                    Session["MobileNo"] = model.MobileNo;
                    return RedirectToAction("Login", "Home");
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            return RedirectToAction("ResetPassword");
        }

        public ActionResult ChangePassword(ChangePasswordDTO model)
        {
            model.Id = Convert.ToInt32(Session["Id"]);
            var data = _userRepository.GetDetailById(model.Id);
            model.MobileNo = data.UserName;
            return View(model);
        }

        public ActionResult ValidateOldPassword(string Mobile, string OldPassword, string UserAgent)
        {
            string dcData = Security.EncryptString(Constants.EncKey, OldPassword);

            UserDTO model = new UserDTO();
            model.Password = dcData;
            model.MobileNo = Mobile;
            model.UserAgent = UserAgent;
            model.DeviceType = DeviceType.Web;
            int msg = _userRepository.ValidateOldPassword(model);
            if (msg == 0)
            {
                model.Response = "1";
            }
            else
            {
                model.Response = "0";
                model.Result = Constants.LOG_OldPassword_VERIFICATION_Failed;
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult SaveChangePassword(ChangePasswordDTO model)
        {

            try
            {
                //string dcData = Security.EncryptString(Constants.EncKey, model.Password);

                //model.Password = dcData;
                model.DeviceType = DeviceType.Web;
                var msg = _userRepository.ChangePassword(model);
                if (msg == true)
                {
                    TempData["Response"] = Constants.LOG_PASSWORD_CHANGE_SUCCESSFUL;
                    return RedirectToAction("Login", "Home");

                }
                else
                {
                    return RedirectToAction("ChangePassword");
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "')<script>");
                return RedirectToAction("ChangePassword");
            }

        }

        public ActionResult UserProfile(string Id)
        {
            UpdateUserDTO model = new UpdateUserDTO();
            JobSearchFilterDTOWeb req = new JobSearchFilterDTOWeb();
            if (Session["Id"] != null)
            {

                Id = Session["Id"].ToString();
                List<SelectListItem> lstexperienceyears = new List<SelectListItem>();
                lstexperienceyears.Add(new SelectListItem { Text = "--Select--", Value = "1" });
                ViewBag.ExperienceYears = lstexperienceyears;
                List<SelectListItem> lstexperiencemonths = new List<SelectListItem>();
                lstexperiencemonths.Add(new SelectListItem { Text = "--Select--", Value = "1" });
                ViewBag.ExperienceMonths = lstexperiencemonths;
                List<SelectListItem> lsCourse = new List<SelectListItem>();
                ViewBag.Course = _courseRepository.GetAll();
                model.CourseList = _courseRepository.GetAll();
                List<SelectListItem> lstIndustryType = new List<SelectListItem>();
                lstIndustryType.Add(new SelectListItem { Text = "--Select--", Value = "1" });
                ViewBag.IndustryType = lstIndustryType;
                List<SelectListItem> lstFunctionalArea = new List<SelectListItem>();
                lstFunctionalArea.Add(new SelectListItem { Text = "--Select--", Value = "1" });
                ViewBag.FunctionalArea = lstFunctionalArea;

                List<SelectListItem> lstSkill = new List<SelectListItem>();
                lstSkill.Add(new SelectListItem { Text = "--Select--", Value = null });
                ViewBag.Skill = _skillRepository.GetAll();


                List<SelectListItem> lstCity = new List<SelectListItem>();
                lstCity.Add(new SelectListItem { Text = "--Select--", Value = null });
                ViewBag.City = _cityRepository.GetAll();
                ViewBag.JobRole = _jobRoleRepository.GetAll(x => x.IsDeleted == false).ToList();
                model.User = _userRepository.GetDetailById(Convert.ToInt32(Id));
                if (model.User != null)
                {
                    model.Id = model.User.Id;
                    model.UserDetail = model.User.UserDetails.FirstOrDefault();
                    model.LstSkills = model.User.UserSkills.Where(x => x.IsDeleted == false).ToList();
                    model.UserName = model.User.UserName;
                    model.Password = model.User.Password;
                    model.ListEducation = model.User.UserEducations.Where(x => x.IsDeleted == false).ToList();
                    model.ListExperience = model.User.UserExperiences.Where(x => x.IsDeleted == false).ToList();
                    model.lstUserDocument = model.User.UserDocuments.Where(x => x.IsDeleted == false).ToList();
                    if (model.LstSkills != null)
                    {
                        req.Language = Language.English;
                        req.SkillId = model.LstSkills.Select(m => m.Skill.Id).ToArray();
                        model.JobResponse = _jobRepository.SearchJobWeb(req);
                    }
                    if (model.UserDetail != null)
                    {
                        model.FirstName = model.UserDetail.FirstName;
                        model.LastName = model.UserDetail.LastName;
                        model.UserDetailId = model.UserDetail.Id;
                        model.LastName = model.UserDetail.LastName;
                        model.FullName = model.UserDetail.FirstName + ' ' + model.UserDetail.LastName;
                        model.Email = model.UserDetail.Email;
                        model.Mobile = model.UserDetail.Mobile;
                        if (model.UserDetail.DOB != null)
                        {
                            Session["DOB"] = model.UserDetail.DOB.Value.ToShortDateString();
                            model.DOB = model.UserDetail.DOB;
                            //DateTime DOB= DateTime.ParseExact(model.UserDetail.DOB, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        }
                        var s = model.DOB.ToString();
                        if (s == "01-01-0001 00:00:00")
                        {
                            model.DOB = null;
                        }
                        //  Session["DOB"] = model.DOB.Value.ToShortDateString();
                        model.Age = model.UserDetail.Age;
                        model.Gender = model.UserDetail.Gender;
                        Session["Gender"] = model.Gender;
                        model.State = model.UserDetail.State;
                        if (model.UserDetail.City == null)
                        {
                            model.City = "";
                        }
                        else
                        {
                            model.City = model.UserDetail.City.Name;
                        }
                        model.CityId = Convert.ToInt32(model.UserDetail.CityId);
                        model.Address = model.UserDetail.Address;
                        Session["Address"] = model.Address;
                        model.Photo = "../FileUpload/ProfilePhoto/" + model.UserDetail.Photo;
                        model.CompanyName = model.UserDetail.CompanyName;
                        model.ContactPersonName = model.UserDetail.ContactPersonName;
                        //model.OfficialEmailId = model.UserDetail.OfficialEmailId;
                        model.About = model.UserDetail.About;
                        Session["About"] = model.About;

                        model.MotherName = model.UserDetail.MotherName;

                        model.FatherName = model.UserDetail.FatherName;
                        model.SpouseName = model.UserDetail.SpouseName;
                        model.HusbandName = model.UserDetail.HusbandName;
                        model.Designation = model.UserDetail.Designation;
                        model.CompanyName = model.UserDetail.CompanyName;
                        Session["CompanyName"] = model.CompanyName;
                        Session["Designation"] = model.Designation;

                        model.Mobile = model.UserDetail.Mobile;
                        model.PinCode = Convert.ToInt32(model.UserDetail.PinCode);
                        Session["PinCode"] = model.PinCode;
                        model.Email = model.UserDetail.Email;
                    }
                    if (model.lstUserDocument != null)
                    {
                        model.Resume = model.lstUserDocument.Where(x => x.IsDeleted == false && x.DocumentTypeId == DocumentType.Resume).Select(x => x.Attachment).FirstOrDefault();
                        if (model.Resume != null)
                        {
                            model.Resume = "../FileUpload/Resume/" + model.Resume;
                        }
                        model.PANNo = model.lstUserDocument.Where(x => x.IsDeleted == false && x.DocumentTypeId == DocumentType.Pan).Select(x => x.Name).FirstOrDefault();
                        model.PAN = model.lstUserDocument.Where(x => x.IsDeleted == false && x.DocumentTypeId == DocumentType.Pan).Select(x => x.Attachment).FirstOrDefault();
                        if (model.PAN != null)
                        {
                            model.PAN = "../FileUpload/Pan/" + model.PAN;
                        }
                        model.Aadhar = model.lstUserDocument.Where(x => x.IsDeleted == false && x.DocumentTypeId == DocumentType.Aadhar).Select(x => x.Attachment).FirstOrDefault();
                        if (model.Aadhar != null)
                        {
                            model.Aadhar = "../FileUpload/Aadhar/" + model.Aadhar;
                        }
                        model.AadharNo = model.lstUserDocument.Where(x => x.IsDeleted == false && x.DocumentTypeId == DocumentType.Aadhar).Select(x => x.Name).FirstOrDefault();
                    }
                    Session["PANNo"] = model.PANNo;
                    Session["AadharNo"] = model.AadharNo;
                }
                int total = 7;
                int filled = 0;
                if (model.About != "")
                {
                    filled = filled + 1;
                }
                if (model.Photo != "")
                {
                    filled = filled + 1;
                }
                if (model.LstSkills != null && model.LstSkills.Count > 0)
                {
                    filled = filled + 1;
                }
                if (model.ListEducation != null && model.ListEducation.Count > 0)
                {
                    filled = filled + 1;
                }
                if (model.ListExperience != null && model.ListExperience.Count > 0)
                {
                    filled = filled + 1;
                }
                if (model.CompanyName != "" || model.Designation != "")
                {
                    filled = filled + 1;
                }
                if (model.DOB != null || model.Age > 0 || model.Gender != "" || model.FatherName != "" || model.MotherName != "" || model.Address != "")
                {
                    filled = filled + 1;
                }

                decimal filledPercentage = filled * 100 / total;
                model.ProfilePercent = filledPercentage;
                if (Session["Response"] != null)
                {
                    model.Response = Constants.LOG_USER_PROFILE_UPDATE_Success;
                }
            }

            return View(model);
        }


        [HttpPost]
        [OnAction(ButtonName = "btnBasicDetails")]
        [ActionName("UserProfile")]
        public ActionResult SaveUserBasicDetails(UpdateUserDTO model)
        {
            model.DeviceType = DeviceType.Web;
            model.UserAgent = model.UserAgent;
            var s = model.DOB.ToString();
            if (s == "01-01-0001 00:00:00")
            {
                model.DOB = null;
            }
            try
            {
                bool msg = _userRepository.UpdateUser(model);
                if (msg == true)
                {
                    Session["Id"] = model.Id;
                    TempData["msg"] = Constants.LOG_USER_PROFILE_UPDATE_Success;
                }
                else
                {

                }
                ViewBag.City = _cityRepository.GetAll();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "')</script>");
            }
            return RedirectToAction("UserProfile");
        }

        [HttpPost]
        [OnAction(ButtonName = "btnPersonalDetails")]
        [ActionName("UserProfile")]
        public ActionResult SavePersonalDetails(UpdateUserDTO model)
        {
            model.DeviceType = DeviceType.Web;
            model.UserAgent = model.UserAgent;
            Random rnd = new Random();
            string imageFile = "";
            string path = "";
            try
            {
                if (model.postedPan != null)
                {
                    imageFile = "pan_" + rnd.Next(000, 999) + model.postedPan.FileName;
                    path = Server.MapPath("~/FileUpload/Pan/");
                    model.postedPan.SaveAs(path + imageFile);
                    model.PAN = imageFile;
                }
                if (model.postedAadhar != null)
                {
                    imageFile = "aadhar_" + rnd.Next(000, 999) + model.postedAadhar.FileName;
                    path = Server.MapPath("~/FileUpload/Aadhar/");
                    model.postedAadhar.SaveAs(path + imageFile);
                    model.Aadhar = imageFile;
                }

                bool msg = _userRepository.UpdateUser(model);
                if (msg == true)
                {
                    Session["Id"] = model.Id;
                    Session["PinCode"] = model.PinCode;
                    Session["Address"] = model.Address;
                    Session["DOB"] = model.DOB.Value.ToShortDateString();
                    Session["Gender"] = model.Gender;
                    TempData["msg"] = Constants.LOG_USER_PROFILE_UPDATE_Success;
                }
                else
                {

                }
                ViewBag.City = _cityRepository.GetAll();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "')</script>");
            }
            return RedirectToAction("UserProfile");
        }


        [HttpPost]
        [OnAction(ButtonName = "btnResumeHeadlines")]
        [ActionName("UserProfile")]
        public ActionResult SaveResumeHeadlines(UpdateUserDTO model)
        {
            model.DeviceType = DeviceType.Web;
            model.UserAgent = model.UserAgent;
            try
            {
                bool msg = _userRepository.UpdateUser(model);
                if (msg == true)
                {
                    Session["Id"] = model.Id;
                    Session["About"] = model.About;
                    TempData["msg"] = Constants.LOG_USER_PROFILE_UPDATE_Success;
                }
                else
                {

                }
                ViewBag.City = _cityRepository.GetAll();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "')</script>");
            }
            return RedirectToAction("UserProfile");
        }

        [HttpPost]
        [OnAction(ButtonName = "btnSkill")]
        [ActionName("UserProfile")]
        public ActionResult SaveSkill(UpdateUserDTO model)
        {
            model.DeviceType = DeviceType.Web;
            model.UserAgent = model.UserAgent;
            try
            {
                bool msg = _userRepository.UpdateUser(model);
                if (msg == true)
                {
                    Session["Id"] = model.Id;
                    Session["About"] = model.About;
                    TempData["msg"] = Constants.LOG_USER_PROFILE_UPDATE_Success;
                }
                else
                {
                    TempData["msg"] = Constants.LOG_USER_PROFILE_UPDATE_FAIL;
                }
                ViewBag.City = _cityRepository.GetAll();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "')</script>");
            }
            return RedirectToAction("UserProfile");
        }

        [HttpPost]
        [OnAction(ButtonName = "btnSaveEducation")]
        [ActionName("UserProfile")]
        public ActionResult SaveEducationDetails(UpdateUserDTO model)
        {

            model.DeviceType = DeviceType.Web;
            try
            {
                bool msg = _userRepository.SaveUserEducation(model);
                if (msg == true)
                {
                    Session["Id"] = model.Id;
                    Session["PinCode"] = model.PinCode;
                    Session["Address"] = model.Address;
                    Session["DOB"] = Convert.ToString(model.DOB);
                    Session["Gender"] = model.Gender;
                    TempData["msg"] = Constants.LOG_USER_PROFILE_UPDATE_Success;
                }
                else
                {

                }
                ViewBag.City = _cityRepository.GetAll();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "')</script>");
            }
            return RedirectToAction("UserProfile");
        }

        [HttpPost]
        [OnAction(ButtonName = "btnDeleteEducation")]
        [ActionName("UserProfile")]
        public ActionResult DeleteEducationDetails(UpdateUserDTO model)
        {

            model.DeviceType = DeviceType.Web;
            try
            {
                bool msg = _userRepository.DeleteUserEducation(model);
                if (msg == true)
                {
                    Session["Id"] = model.Id;
                    Session["PinCode"] = model.PinCode;
                    Session["Address"] = model.Address;
                    Session["DOB"] = Convert.ToString(model.DOB);
                    Session["Gender"] = model.Gender;
                    TempData["msg"] = Constants.LOG_USER_PROFILE_UPDATE_Success;
                }
                else
                {

                }
                ViewBag.City = _cityRepository.GetAll();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "')</script>");
            }
            return RedirectToAction("UserProfile");
        }
        [HttpPost]
        [OnAction(ButtonName = "btnSaveExperience")]
        [ActionName("UserProfile")]
        public ActionResult SaveExperience(UpdateUserDTO model)
        {

            model.DeviceType = DeviceType.Web;
            try
            {
                bool msg = _userRepository.SaveUserExperience(model);
                if (msg == true)
                {
                    Session["Id"] = model.Id;
                    Session["PinCode"] = model.PinCode;
                    Session["Address"] = model.Address;
                    Session["DOB"] = Convert.ToString(model.DOB);
                    Session["Gender"] = model.Gender;
                    TempData["msg"] = Constants.LOG_USER_PROFILE_UPDATE_Success;
                }
                else
                {

                }
                ViewBag.City = _cityRepository.GetAll();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "')</script>");
            }
            return RedirectToAction("UserProfile");
        }
        //public JsonResult SaveEduction(string education, string userAgent)
        //{
        //    UserEducationDetails model = new UserEducationDetails();
        //    model.Id = Convert.ToInt32(Session["Id"]);
        //    model.DeviceType = DeviceType.Web;
        //    model.UserAgent = userAgent;
        //    JavaScriptSerializer j = new JavaScriptSerializer();
        //    var edudetails = j.Deserialize<UserEducationDetails[]>(education);
        //    List<UserEducationDetails> list = new List<UserEducationDetails>();
        //    foreach (UserEducationDetails r in edudetails)
        //    {
        //        UserEducationDetails Obj = new UserEducationDetails();
        //        Obj.CourseId = r.CourseId;
        //        Obj.University = r.University;
        //        Obj.PassingYear = r.PassingYear;
        //        list.Add(Obj);

        //    }
        //    model.LstUserEducationDetails = list;
        //    bool msg = _userRepository.SaveUserEducation(model);
        //    if (msg == true)
        //    {
        //        TempData["Response"] = Constants.LOG_USER_PROFILE_UPDATE_Success;
        //        model.Response = "1";
        //    }
        //    else
        //    {
        //        model.Response = "0";
        //    }
        //    return Json(model, JsonRequestBehavior.AllowGet);
        //}

        //public ActionResult Upload(IEnumerable<HttpPostedFileBase> postedFile, UpdateUserDTO obj)
        //{
        //    string FormName = "";
        //    string Controller = "";
        //    int count = 0;
        //    try
        //    {
        //        foreach (var file in postedFile)
        //        {
        //            if (file != null && file.ContentLength > 0)
        //            {
        //                if (count == 0)
        //                {
        //                    obj.Resume= "/FileUpload/Resume/" + Guid.NewGuid() + Path.GetExtension(file.FileName);
        //                    file.SaveAs(Path.Combine(Server.MapPath(obj.Resume)));
        //                }

        //            }
        //            count++;
        //        }

        //        obj.Id = obj.Id;

        //        //DataSet ds = obj.UploadKYCDocuments();
        //        //if (ds != null && ds.Tables.Count > 0)
        //        //{
        //        //    if (ds.Tables[0].Rows[0][0].ToString() == "1")
        //        //    {
        //        //        TempData["DocumentUpload"] = "Document uploaded successfully..";
        //        //        FormName = "KYCDocuments";
        //        //        Controller = "KYCDocuments";
        //        //    }
        //        //    else
        //        //    {
        //        //        TempData["DocumentUpload"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
        //        //        FormName = "KYCDocuments";
        //        //        Controller = "KYCDocuments";
        //        //    }
        //        //}
        //    }
        //    catch (Exception ex)
        //    {
        //        TempData["DocumentUpload"] = ex.Message;
        //        FormName = "KYCDocuments";
        //        Controller = "KYCDocuments";
        //    }
        //    return RedirectToAction(FormName, Controller);
        //}
        [HttpPost]
        public ActionResult UploadProfilePhoto()
        {
            bool msg = false;
            if (Session["Id"] != null)
            {
                UPdateProfileDTO model = new UPdateProfileDTO();
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    HttpPostedFileBase file = Request.Files[i]; //Uploaded file
                    Random rn = new Random();
                    model.UserAgent = Request.UserAgent;
                    int fileSize = file.ContentLength;
                    string fileName = file.FileName;
                    System.IO.Stream fileContent = file.InputStream;
                    model.Photo = rn.Next(10, 100) + "_photo_" + DateTime.Now.ToString("ddmmyyhhmmss") + "_" + fileName;
                    file.SaveAs(Server.MapPath("~/FileUpload/ProfilePhoto/") + model.Photo); //File will be saved in application root
                }
                model.DeviceType = DeviceType.Web;
                model.UserId = Convert.ToInt32(Session["Id"]);
                msg = _userRepository.UpdateProfilePicWeb(model);
                if (msg == true)
                {
                    TempData["Photo"] = "Photo uploaded successfully !";
                }
            }
            else
            {
                TempData["Photo"] = "Something Went Wrong";
            }
            return Json(msg);
        }
        [HttpPost]
        public ActionResult UploadResume()
        {
            bool msg = false;
            if (Session["Id"] != null)
            {
                UploadDocumentDTO model = new UploadDocumentDTO();
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    HttpPostedFileBase file = Request.Files[i]; //Uploaded file
                    Random rn = new Random();
                    model.UserAgent = Request.UserAgent;
                    int fileSize = file.ContentLength;
                    string fileName = file.FileName;
                    System.IO.Stream fileContent = file.InputStream;
                    model.File = rn.Next(10, 100) + "_resume_" + DateTime.Now.ToString("ddmmyyhhmmss") + "_" + fileName;
                    file.SaveAs(Server.MapPath("~/FileUpload/Resume/") + model.File); //File will be saved in application root
                }
                model.DeviceType = DeviceType.Web;
                model.UserId = Convert.ToInt32(Session["Id"]);
                msg = _userRepository.UpdateResume(model.UserId, model.File, "", "", "", "");
                if (msg == true)
                {
                    TempData["Resume"] = "Resume uploaded successfully !";
                }
            }
            else
            {
                TempData["Resume"] = "Something Went Wrong";
            }
            return Json(msg);
        }

        public ActionResult RecruitersMessage()
        {
            MessageSendUserRequestDTO obj = new MessageSendUserRequestDTO();
            try
            {
                if (Session["Id"] != null)
                {
                    MessageRequestDTO r = new MessageRequestDTO();
                    r.UserId = Convert.ToInt32(Session["Id"]);
                    var msgList = _chatRepository.GetMessageList(r);
                    obj.MessageList = msgList.Messages;
                }
                else
                {
                    return RedirectToAction("login", "en");
                }
            }
            catch (Exception ex)
            {
                TempData["Messages"] = ex.Message;
            }
            return View(obj);
        }

        public ActionResult ReplyToEmployer(string id)
        {
            if (Session["Id"] != null)
            {
                if (id != null)
                {
                    int Id = Convert.ToInt32(id);
                    Language lang = Language.English;
                    EmpJobPostDTO emp = new EmpJobPostDTO();
                    var details = _userJobRepository.GetJobDetailsByJobId(Id, lang);
                    MessageSendUserRequestDTO model = new MessageSendUserRequestDTO();

                    MessageChatRequestDTO r = new MessageChatRequestDTO();
                    r.Id = Convert.ToInt32(id);

                    var msgList = _chatRepository.GetMessageChats(r);
                    model.ChatId = Id;
                    model.ExperienceMax = details.ExperienceMax;
                    model.ExperienceMin = details.ExperienceMin;
                    model.ShortDescription = details.ShortDescription;
                    model.Title = details.Title;
                    if (details.CompanyName != null)
                    {
                        model.UserName = details.CompanyName;
                    }
                    else
                    {
                        model.UserName = details.UserName;
                    }


                    model.Location = details.JobLocation;
                    model.lstMessage = msgList.Messages;
                    return View(model);
                }
                else
                {
                    return RedirectToAction("RecruitersMessage", "uhi");
                }
            }
            else
            {
                return RedirectToAction("login", "en");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult ReplyToEmployer(MessageSendUserRequestDTO model)
        {

            try
            {
                if (Session["Id"] != null)
                {
                    model.UserId = Convert.ToInt32(Session["Id"]);
                    model.Id = model.ChatId;
                    var msg = _chatRepository.SendMessage(model);
                    MessageChatRequestDTO r = new MessageChatRequestDTO();

                    r.Id = model.ChatId;
                    var msgList = _chatRepository.GetMessageChats(r);
                    model.lstMessage = msgList.Messages;
                }
                else
                {
                    return RedirectToAction("login", "en");
                }

            }
            catch (Exception ex)
            {
                TempData["ReplyToEmployer"] = ex.Message;
            }
            return RedirectToAction("ReplyToEmployer", new { id = model.ChatId });
        }
    }
}