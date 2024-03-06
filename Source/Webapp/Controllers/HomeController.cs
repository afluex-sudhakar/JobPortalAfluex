using CaptchaMvc.HtmlHelpers;
using Data.DTOs;
using Data.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using Utility;
using Utility.Enums;
using Webapp.Models;

namespace Webapp.Controllers
{
    [ValidateInput(false)]
    public class HomeController : Controller
    {
        private readonly IStateRepository _stateRepository;
        private readonly ICityRepository _cityRepository;
        private readonly ICourseRepository _courseRepository;
        private readonly IUserRepository _userRepository;
        private readonly IJobRepository _jobRepository;
        private readonly ISkillRepository _skillRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly INewsLetterRepository _newsLetterRepository;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IDepartmentCategoryRepository _departmentCategoryRepository;
        private readonly IUserJobRepository _userJobRepository;
        private readonly ITrainingMaterialRepository _trainingMaterialRepository;
        private readonly ICMSRepository _CMSRepository;
        private readonly IPincodeRepository _pincodeRepository;
        private readonly IEnrollmentProgramRepository _enrollmentProgramRepository;
        private readonly IFeedbackRepository _feedbackRepository;
        private readonly IJobRoleRepository _jobRoleRepository;

        public HomeController(IStateRepository stateRepository, ICityRepository cityRepository, ICourseRepository courseRepository, IUserRepository userRepository, IJobRepository jobRepository, ISkillRepository skillRepository, ICategoryRepository categoryRepository, INewsLetterRepository newsLetterRepository, IDepartmentRepository departmentRepository, IDepartmentCategoryRepository departmentCategoryRepository, IUserJobRepository userJobRepository, ITrainingMaterialRepository trainingMaterialRepository, ICMSRepository cmsRepository, IPincodeRepository pincodeRepository, IEnrollmentProgramRepository enrollmentProgramRepository, IFeedbackRepository feedbackRepository, IJobRoleRepository jobRoleRepository)
        {
            this._stateRepository = stateRepository;
            this._cityRepository = cityRepository;
            this._courseRepository = courseRepository;
            this._userRepository = userRepository;
            this._jobRepository = jobRepository;
            this._skillRepository = skillRepository;
            this._categoryRepository = categoryRepository;
            this._newsLetterRepository = newsLetterRepository;
            this._departmentRepository = departmentRepository;
            this._departmentCategoryRepository = departmentCategoryRepository;
            this._userJobRepository = userJobRepository;
            this._trainingMaterialRepository = trainingMaterialRepository;
            this._CMSRepository = cmsRepository;
            this._pincodeRepository = pincodeRepository;
            this._enrollmentProgramRepository = enrollmentProgramRepository;
            this._feedbackRepository = feedbackRepository;
            this._jobRoleRepository = jobRoleRepository;
        }




        public ActionResult EmployeeRegistration()
        {
            UpdateUserDTO model = new UpdateUserDTO();
            ViewBag.State = _stateRepository.GetAll(x => x.IsDeleted == false && x.Id == 34);
            ViewBag.City = _cityRepository.GetAll(x => x.IsDeleted == false && x.Id == 338);
            ViewBag.Location = _pincodeRepository.GetAll(x => x.IsDeleted == false && x.CityId == 338);
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult EmployeeRegistration(UpdateUserDTO model)
        {
            bool msg;
            try
            {
                string imageFile = "";
                string path = "";
                Random rnd = new Random();
                if (model.postedFile != null)
                {
                    // model.UserName = model.Mobile;
                    if (model.postedFile != null)
                    {
                        imageFile = "img_" + rnd.Next(000, 999) + model.postedFile.FileName;
                        path = Server.MapPath("~/FileUpload/ProfilePhoto/");
                        model.postedFile.SaveAs(path + imageFile);
                        model.Photo = imageFile;
                    }
                }
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
                if (model.postedCompanyCertificate != null)
                {
                    imageFile = "certificate_" + rnd.Next(000, 999) + model.postedCompanyCertificate.FileName;
                    path = Server.MapPath("~/FileUpload/CompanyCertificate/");
                    model.postedCompanyCertificate.SaveAs(path + imageFile);
                    model.CompanyCertificate = imageFile;
                }
                if (ModelState.IsValid)
                {
                    model.RoleId = 2;
                    model.DeviceType = DeviceType.Web;
                    string dcData = Security.EncryptString(Constants.EncKey, model.Password);
                    model.Password = dcData;
                    var a = _userRepository.GetAll(x => x.UserName == model.Mobile && x.IsDeleted == false).FirstOrDefault();
                    if (a == null)
                    {
                        msg = _userRepository.AddEmployee(model);
                        if (msg == true)
                        {
                            TempData["msg"] = "Saved";
                        }
                        else
                        {
                            TempData["msg"] = "Not Registered !";
                        }
                    }
                    else
                    {
                        TempData["msg"] = "Already Registered !";
                    }
                }
                /* if (postedFile != null)
                 {
                     model.Photo = "../ProfilePhoto/" + Guid.NewGuid() + Path.GetExtension(postedFile.FileName);
                     model.PostedFile.SaveAs(Path.Combine(Server.MapPath(model.Photo)));*/

            }
            catch (Exception ex)
            {
                TempData["msg"] = ex.Message;
            }
            return RedirectToAction("EmployeeRegistration", "en");
        }
        public ActionResult Index()
        {
            JobDTOWeb model = new JobDTOWeb();
            JobSearchFilterDTOWeb obj = new JobSearchFilterDTOWeb();
            RecentJobRequestDTO req = new RecentJobRequestDTO();
            int i = 0;
            try
            {
                obj.Language = Language.English;
                model.lstSkill = _skillRepository.GetSkill(obj.Language);
                model.lstCity = _cityRepository.GetAll(x => x.IsDeleted == false && x.StateId == 34 && x.Id >= 338 && x.Id <= 480).ToList();
                model.lstLocation = _pincodeRepository.GetAll(x => x.IsDeleted == false && x.CityId == 338).ToList();
                model.lstEmployer = _userRepository.GetAll(x => x.RoleId == Role.Employer && x.IsDeleted == false && x.IsVerified == true).ToList();
                model.lstDepartment = _departmentRepository.GetAll(x => x.IsDeleted == false).ToList();
                model.lstCategory = _categoryRepository.GetAll();
                obj.SortBy = Search.Date.ToString();
                req.Language = obj.Language;
                model.JobResponse = _jobRepository.GetRecentJobsWeb(req);

            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "')</script>");
            }
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult Index(JobDTOWeb model)
        {
            try
            {
                Language lang = Language.English;
                model.lstSkill = _skillRepository.GetSkill(lang);
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "')</script>");
            }
            return RedirectToAction("AdvanceSearch", "en");
        }

        public ActionResult AboutUs(CMSDTO model)
        {

            try
            {
                model.pageName = "AboutUs";
                if (model.pageName != null)
                {
                    model.pageName = model.pageName;
                    model.StaticContent = _CMSRepository.GetPageDetailsByPageName(model.pageName);
                    if (model.StaticContent != null)
                    {
                        model.pageName = model.StaticContent.PageName;
                        model.Description = model.StaticContent.Description;
                        model.Id = model.StaticContent.Id;
                        model.HDescription = model.StaticContent.DescriptionH;
                    }
                    else
                    {

                    }
                }
            }
            catch (Exception ex)
            {
                TempData["msg"] = ex.Message;
            }
            return View(model);
        }
        public ActionResult CompanyList()
        {
            EmployerListDTO model = new EmployerListDTO();
            try
            {
                if (ModelState.IsValid)
                {
                    model.Employees = _userRepository.GetAll(x => x.RoleId == Role.Employer).ToList();
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "')</script>");
            }
            return View(model);
        }
        public ActionResult SignUp()
        {
            UserDTO model = new UserDTO();
            if (Session["Response"] != null)
            {
                model.Response = Session["Response"].ToString();
            }
            model.lstCity = _cityRepository.GetAll();
            ViewBag.City = _cityRepository.GetAll();
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult SignUp(UserDTO model)

        {
            // ViewBag.City = _cityRepository.GetAll();
            bool msg;
            try
            {
                model.DeviceType = DeviceType.Web;
                model.UserName = model.FirstName;
                model.RoleId = 3;
                string dcData = Security.EncryptString(Constants.EncKey, model.Password);
                model.Password = dcData;
                var a = _userRepository.GetAll(x => x.UserName == model.Mobile && x.IsDeleted == false && x.RoleId == model.RoleId).FirstOrDefault();
                if (a == null)
                {
                    msg = _userRepository.Add(model);
                    if (msg == true)
                    {
                        //  TempData["msg"] = Constants.HTTPSTATUS_REGISTRATION_SUCCESSFUL;
                        //GenerateOTPDTO re = new GenerateOTPDTO();
                        //re.MobileNo = model.Mobile;
                        //re.UserAgent = model.UserAgent;
                        //re.DeviceType = DeviceType.Web;
                        //msg = _userRepository.GenerateOTP(re);
                        //if (msg == true)
                        //{
                        //    TempData["MobileNo"] = model.Mobile;
                        //    return RedirectToAction("VerifyOTP");
                        //}
                        //else
                        //{
                        //    TempData["Msg"] = Constants.HTTPSTATUS_FAILED;
                        //    return RedirectToAction("SignUp", "en");
                        //}

                        if (msg == true)
                        {
                            TempData["Msg"] = Constants.HTTPSTATUS_REGISTRATION_SUCCESSFUL;
                            return RedirectToAction("SignUp", "en");
                        }
                        else
                        {
                            TempData["Msg"] = Constants.HTTPSTATUS_FAILED;
                            return RedirectToAction("SignUp", "en");
                        }
                       
                    }
                    else
                    {
                        TempData["Msg"] = Constants.LOG_USER_REGISTRATION_MOBILENUMBER_ALREADYUSED;
                        return RedirectToAction("SignUp", "Hi");
                    }
                }
                else
                {
                    TempData["Msg"] = Constants.LOG_USER_REGISTRATION_MOBILENUMBER_ALREADYUSED;
                    return RedirectToAction("SignUp", "en");
                }

            }
            catch (Exception ex)
            {
                TempData["msg"] = ex.Message;
            }
            return RedirectToAction("SignUp", "en");
        }

        public ActionResult ResendOTP(string MobileNo)
        {
            bool msg;
            GenerateOTPDTO model = new GenerateOTPDTO();
            model.MobileNo = MobileNo;
            msg = _userRepository.GenerateOTP(model);
            if (msg == true)
            {
                TempData["MobileNo"] = model.MobileNo;
                return RedirectToAction("VerifyOTP", "en");
            }
            return RedirectToAction("VerifyOTP", "en");
        }

        public ActionResult VerifyOTP(ValidateOTPDTO model)
        {
            model.DeviceType = DeviceType.Web;
            if (TempData["MobileNo"] != null)
            {
                model.MobileNo = TempData["MobileNo"].ToString();

                if (TempData["msg"] != null)
                {
                    TempData["msg"] = Constants.HTTPSTATUS_OTP_VALIDATION_FAILED;
                }
            }
            else
            {
                TempData["msg"] = Constants.HTTPSTATUS_REGISTRATION_SUCCESSFUL;
                return RedirectToAction("ReturnToLogin", "en");
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult VerifingOTPSave(ValidateOTPDTO model)
        {
            int msg;
            try
            {
                model.DeviceType = DeviceType.Web;
                var status = _userRepository.ValidateOTP(model);
                if (status == 1)
                {
                    TempData["msg"] = Constants.HTTPSTATUS_REGISTRATION_SUCCESSFUL;


                }
                else if (status == -1)
                {
                    TempData["msg"] = Constants.HTTPSTATUS_OTP_EXPIRED;
                }
                else
                {
                    TempData["MobileNo"] = model.MobileNo;
                    TempData["msg"] = Constants.HTTPSTATUS_OTP_VALIDATION_FAILED;
                    return RedirectToAction("VerifyOTP", "en");
                }
            }
            catch (Exception ex)
            {
                TempData["msg"] = ex.Message;
            }
            return RedirectToAction("ReturnToLogin", "en");
        }


        //public ActionResult UserProfile(string Id)
        //{
        //    UserDTO model = new UserDTO();
        //    int i = 0;
        //    List<SelectListItem> lstexperienceyears = new List<SelectListItem>();
        //    lstexperienceyears.Add(new SelectListItem { Text = "--Select--", Value = "1" });
        //    ViewBag.ExperienceYears = lstexperienceyears;
        //    List<SelectListItem> lstexperiencemonths = new List<SelectListItem>();
        //    lstexperiencemonths.Add(new SelectListItem { Text = "--Select--", Value = "1" });
        //    ViewBag.ExperienceMonths = lstexperiencemonths;
        //    List<SelectListItem> lsCourse = new List<SelectListItem>();
        //    lsCourse.Add(new SelectListItem { Text = "--Select--", Value = "1" });
        //    ViewBag.Course = lsCourse;
        //    List<SelectListItem> lstIndustryType = new List<SelectListItem>();
        //    lstIndustryType.Add(new SelectListItem { Text = "--Select--", Value = "1" });
        //    ViewBag.IndustryType = lstIndustryType;
        //    List<SelectListItem> lstFunctionalArea = new List<SelectListItem>();
        //    lstFunctionalArea.Add(new SelectListItem { Text = "--Select--", Value = "1" });
        //    ViewBag.FunctionalArea = lstFunctionalArea;
        //    DataSet ds = new DataSet();
        //    //model.lst = _userRepository.GetDetailById(Convert.ToInt32(Id));
        //    model.lstJobs = _jobRepository.GetAll();
        //    model.lstEmployers = _employerRepository.GetAll();
        //    if (model.lst != null)
        //    {
        //        model.Id = model.lst[i].Id;
        //        model.lstUserDetail = model.lst[i].UserDetails.ToList();
        //        model.UserName = model.lst[i].UserName;
        //        model.Password = model.lst[i].Password;
        //        if (model.lstUserDetail != null)
        //        {
        //            model.FirstName = model.lstUserDetail[i].FirstName;
        //            model.LastName = model.lstUserDetail[i].LastName;
        //            model.FullName = model.lstUserDetail[i].FirstName + ' ' + model.lstUserDetail[i].LastName;
        //            model.Email = model.lstUserDetail[i].Email;
        //            model.Mobile = model.lstUserDetail[i].Mobile;
        //            model.DOB = Convert.ToDateTime(model.lstUserDetail[i].DOB);
        //            model.Age = model.lstUserDetail[i].Age;
        //            model.Gender = model.lstUserDetail[i].Gender;
        //            model.State = model.lstUserDetail[i].State;
        //            model.Address = model.lstUserDetail[i].Address;
        //            model.Photo = model.lstUserDetail[i].Photo;
        //            model.CompanyName = model.lstUserDetail[i].CompanyName;
        //            model.ContactPersonName = model.lstUserDetail[i].ContactPersonName;
        //            //model.OfficialEmailId = model.lstUserDetail[i].OfficialEmailId;
        //            model.About = model.lstUserDetail[i].About;
        //            model.Mobile = model.lstUserDetail[i].Mobile;
        //            model.Email = model.lstUserDetail[i].Email;
        //            model.CompanyName = model.lstUserDetail[i].CompanyName;
        //        }
        //        //model.JobRole = ds.Tables[0].Rows[0]["JobRole"].ToString();
        //        //model.CourseName = ds.Tables[0].Rows[0]["CourseName"].ToString();
        //        //model.CollegeName = ds.Tables[0].Rows[0]["CollegeName"].ToString();
        //        //model.Projects = ds.Tables[0].Rows[0]["Projects"].ToString();
        //        //model.ProfileSummary = ds.Tables[0].Rows[0]["ProfileSummary"].ToString();
        //        //model.LinkedinLink = ds.Tables[0].Rows[0]["LinkedinLink"].ToString();
        //        //model.FacebookLink = ds.Tables[0].Rows[0]["FacebookLink"].ToString();
        //        //model.GithubLink = ds.Tables[0].Rows[0]["GithubLink"].ToString();
        //        //model.PublicationLink = ds.Tables[0].Rows[0]["PublicationLink"].ToString();
        //        //model.Patent = ds.Tables[0].Rows[0]["Patent"].ToString();
        //        //model.Certification = ds.Tables[0].Rows[0]["Certification"].ToString();
        //        //model.IndustryType = ds.Tables[0].Rows[0]["IndustryType"].ToString();
        //        //model.JobType = ds.Tables[0].Rows[0]["JobType"].ToString();
        //        //model.ExpectedSalary = Convert.ToDecimal(ds.Tables[0].Rows[0]["ExpectedSalary"]);
        //        //model.FunctionalArea = ds.Tables[0].Rows[0]["FunctionalArea"].ToString();
        //        //model.DOB = ds.Tables[0].Rows[0]["DOB"].ToString();
        //        //model.Gender = ds.Tables[0].Rows[0]["Gender"].ToString();
        //        //model.MaritalStatus = ds.Tables[0].Rows[0]["MaritalStatus"].ToString();
        //        //model.Category = ds.Tables[0].Rows[0]["Category"].ToString();
        //        //model.HomeTown = ds.Tables[0].Rows[0]["HomeTown"].ToString();
        //        //model.PinCode = ds.Tables[0].Rows[0]["PinCode"].ToString();
        //        //model.PermanentAddress = ds.Tables[0].Rows[0]["PermanentAddress"].ToString();
        //        //model.SalaryMax = Convert.ToDecimal(ds.Tables[0].Rows[0]["Salary"]);
        //        //model.PassingYear = ds.Tables[0].Rows[0]["PassingYear"].ToString();
        //    }
        //    return View(model);
        //}
        public ActionResult ContactUs(CMSDTO model)
        {

            try
            {
                model.pageName = "Contact Us";
                if (model.pageName != null)
                {
                    model.pageName = model.pageName;
                    model.StaticContent = _CMSRepository.GetPageDetailsByPageName(model.pageName);
                    if (model.StaticContent != null)
                    {
                        model.pageName = model.StaticContent.PageName;
                        model.Description = model.StaticContent.Description;
                        model.Id = model.StaticContent.Id;
                        model.HDescription = model.StaticContent.DescriptionH;
                    }
                    else
                    {

                    }
                }
            }
            catch (Exception ex)
            {
                TempData["msg"] = ex.Message;
            }
            return View(model);
        }
        [HttpPost]
        public ActionResult ContactUs(UpdateCMSDTO model)
        {
            try
            {
                bool msg = _feedbackRepository.AddForWeb(model);
                if (msg == true)
                {
                    if (model.Email != null)
                    {
                        BLMail.SendMail(model.Email, "Contact", "Dear " + model.UserName + ", Thank you to contact us.", false);
                    }
                    TempData["msg"] = "Thank you to contact us.";
                }
                else
                {
                    TempData["msg"] = "Error. Please try again.";
                }
            }
            catch (Exception ex)
            {
                TempData["msg"] = ex.Message;
            }
            return RedirectToAction("ContactUs", "Home");
        }

        public ActionResult JobDetails(string Id)
        {
            JOBDetailDTOWeb model = new JOBDetailDTOWeb();
            JobDTOWeb m = new JobDTOWeb();
            JobSearchFilterDTOWeb req = new JobSearchFilterDTOWeb();
            model.Id = Convert.ToInt32(Id);
            model.Language = Language.English;
            req.Language = model.Language;
            int i = 0;
            try
            {
                if (Id != null)
                {
                    m.job = _jobRepository.GetCustomByIdWeb(model);
                    if (Session["Id"] != null)
                    {
                        int UserId = Convert.ToInt32(Session["Id"]);
                        m.AppliedUserStatus = _userJobRepository.GetAppliedStatusByUser(UserId, model.Id);
                    }
                    m.job.Time = Common.TimeLeft(Convert.ToDateTime(m.job.PostedDate));

                    if (m.job != null && m.job.SkillId != null)
                    {
                        req.SkillId = m.job.SkillId;
                    }
                    m.JobResponse = _jobRepository.SearchJobWeb(req);
                    if (m.JobResponse != null)
                    {
                        for (i = 0; i < m.JobResponse.ListJob.Count; i++)
                        {
                            m.JobResponse.ListJob[i].Time = Common.TimeLeft(Convert.ToDateTime(m.JobResponse.ListJob[i].PostedDate));
                        }
                    }
                }
                else
                {
                    return RedirectToAction("AdvanceSearch", "en");
                }
            }
            catch (Exception ex)
            {
                TempData["msg"] = ex.Message;
            }
            return View(m);
        }
        public ActionResult ApplyJob(string UserId, string JobId)
        {
            JobApplyDTO model = new JobApplyDTO();
            int status = 0;
            if (UserId != "" && JobId != "")
            {
                model.Id = Convert.ToInt32(UserId);
                model.JobId = Convert.ToInt32(JobId);
                status = _jobRepository.ApplyJob(model);
                if (status == 1)
                {
                    TempData["msg"] = "Sucessfully Applied !";
                }
                else if (status == -1)
                {
                    TempData["msg"] = "Already Applied !";
                }
                else
                {
                    TempData["msg"] = "Error !";
                }
            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Login()
        {
            Session["Id"] = null;
            Session["RoleId"] = null;
            UserLoginDTO model = new UserLoginDTO();
            //Change Password alert
            if (TempData["Response"] != null)
            {
                if (Session["Response"] != null)
                {
                    model.Response = Session["Response"].ToString();
                }
            }
            return View(model);
        }

        public ActionResult ReturnToLogin()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult Login(UserLoginDTO model)
        {
            try
            {
                if (!this.IsCaptchaValid(errorText: "Invalid Captcha"))
                {
                    ViewBag.Message = "Captcha validation failed";
                    return View(model);
                }
                model.DeviceType = DeviceType.Web;
                //string dcData = Security.EncryptString(Constants.EncKey, model.Password);
                //model.Password = dcData;
                var msg = _userRepository.LoginWeb(model);

                if (msg == null)
                {
                    TempData["msg"] = Constants.LOG_USER_LOGIN_FAILED;
                }
                else
                {
                    Session["Id"] = msg.Id;
                    Session["RoleId"] = msg.RoleId;
                    var ud = _userRepository.GetDetailById(Convert.ToInt32(Session["Id"]));
                    var d = ud.UserDetails.FirstOrDefault();
                    Session["FirstName"] = d.FirstName;
                    if (msg.RoleId == Role.User)
                    {
                        return RedirectToAction("UserProfile", "uen");
                    }
                    else if (msg.RoleId == Role.Employer)
                    {
                        return RedirectToAction("dashboard", "employee");
                    }
                    else
                    {
                        TempData["msg"] = Constants.LOG_USER_LOGIN_FAILED;
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "')</script>");
            }
            return View(model);
        }
        public ActionResult PrivacyPolicy(CMSDTO model)
        {

            try
            {
                model.pageName = "Privacy Policy";
                if (model.pageName != null)
                {
                    model.pageName = model.pageName;
                    model.StaticContent = _CMSRepository.GetPageDetailsByPageName(model.pageName);
                    if (model.StaticContent != null)
                    {
                        model.pageName = model.StaticContent.PageName;
                        model.Description = model.StaticContent.Description;
                        model.Id = model.StaticContent.Id;
                        model.HDescription = model.StaticContent.DescriptionH;
                    }
                    else
                    {

                    }
                }
            }
            catch (Exception ex)
            {
                TempData["msg"] = ex.Message;
            }
            return View(model);
        }
        public ActionResult TermsandCondition(CMSDTO model)
        {

            try
            {
                model.pageName = "Term and Condition";
                if (model.pageName != null)
                {
                    model.pageName = model.pageName;
                    model.StaticContent = _CMSRepository.GetPageDetailsByPageName(model.pageName);
                    if (model.StaticContent != null)
                    {
                        model.pageName = model.StaticContent.PageName;
                        model.Description = model.StaticContent.Description;
                        model.Id = model.StaticContent.Id;
                        model.HDescription = model.StaticContent.DescriptionH;
                    }
                    else
                    {

                    }
                }
            }
            catch (Exception ex)
            {
                TempData["msg"] = ex.Message;
            }
            return View(model);
        }
        public ActionResult AdvanceSearch(JobSearchFilterDTOWeb model)
        {

            JobDTOWeb obj = new JobDTOWeb();
            try
            {
                int i = 0;
                obj.PageSize = Constants.PAGE_SIZE;
                Language lang = Language.English;
                model.Language = Language.English;
                model.DeviceType = DeviceType.Web;
                obj.lstCity = _cityRepository.GetAll(x => x.IsDeleted == false && x.StateId == 34 && x.Id >= 338 && x.Id < 480).ToList();
                obj.lstCourse = _courseRepository.GetAll(x => x.IsDeleted == false).ToList();
                obj.lstCategory = _categoryRepository.GetAll(x => x.IsDeleted == false).ToList();
                obj.lstDepartment = _departmentRepository.GetAll(x => x.IsDeleted == false).ToList();
                obj.lstSkill = _skillRepository.GetSkill(lang);
                obj.EmployerId = model.EmployerId;
                if (model.SkillId != null)
                {
                    foreach (var item in model.SkillId)
                    {
                        obj.SkillId = item;
                    }
                }
                if (model.DepartmentId != null)
                {
                    foreach (var item in model.DepartmentId)
                    {
                        obj.Department_Id = item;
                    }
                }
                if (model.CityId != null)
                {
                    foreach (var item in model.CityId)
                    {
                        obj.CityId = item;
                    }
                }
                if (model.CategoryId != null)
                {
                    foreach (var item in model.CategoryId)
                    {
                        obj.CategoryId = item;
                    }
                }
                obj.JobResponse = _jobRepository.SearchJobWeb(model);
                if (obj.JobResponse != null)
                {
                    for (i = 0; i < obj.JobResponse.ListJob.Count; i++)
                    {
                        obj.JobResponse.ListJob[i].Time = Common.TimeLeft(Convert.ToDateTime(obj.JobResponse.ListJob[i].PostedDate));
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "')<script>");
            }
            return View(obj);
        }
        
        public JsonResult AdvanceSearchAction(JobSearchFilterDTOWeb model, string SalaryMx, string SalaryMn, string SortBy, string Page)
        {
            JobDTOWeb obj = new JobDTOWeb();
            int i = 0;
            try
            {
                List<JobReposneDTO> reponse = new List<JobReposneDTO>();
                model.Language = Language.English;
                model.Page = Convert.ToInt32(Page);
                model.SortBy = SortBy;
                if (SalaryMn != null && SalaryMx != null)
                {
                    model.SalaryMax = Convert.ToDecimal(SalaryMx) / 12;
                    model.SalaryMin = Convert.ToDecimal(SalaryMn) / 12;
                }
                obj.JobResponse = _jobRepository.SearchJobWeb(model);
                if (obj.JobResponse != null)
                {
                    for (i = 0; i < obj.JobResponse.ListJob.Count; i++)
                    {
                        obj.JobResponse.ListJob[i].Time = Common.TimeLeft(Convert.ToDateTime(obj.JobResponse.ListJob[i].PostedDate));
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "')<script>");
            }
            return Json(obj.JobResponse, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Error_Page()
        {
            return View();
        }
        public ActionResult ErrorPage()
        {
            return View();
        }
        public ActionResult UserRegistration()
        {
            UserDTO model = new UserDTO();
            ViewBag.State = _stateRepository.GetAll();
            ViewBag.City = _cityRepository.GetAll();
            ViewBag.Course = _courseRepository.GetAll();
            return View(model);
        }





        public ActionResult Test()
        {
            return View();
        }
        public ActionResult commingsoon()
        {
            return View();
        }
        public ActionResult registration()
        {
            return View();
        }
        public ActionResult sitemap()
        {
            return View();
        }
        public ActionResult listofappliedjob()
        {
            if (Session.IsNewSession || Session["Id"] == null)
            {
                return RedirectToAction("Login");
            }
            AppliedJobResponseDTOWeb model = new AppliedJobResponseDTOWeb();
            AppliedJobFilterDTOWeb req = new AppliedJobFilterDTOWeb();
            req.Language = Language.English;
            if (Session["Id"] != null)
            {
                req.Id = Convert.ToInt32(Session["Id"]);
            }
            model.ListJob = _userJobRepository.AppliedJobsWeb(req);
            return View(model);
        }
        public ActionResult training()
        {
            TrainingMaterialResponseDTO model = new TrainingMaterialResponseDTO();
            TrainingMaterialRequestDTO obj = new TrainingMaterialRequestDTO();
            ViewBag.Course = _courseRepository.GetAll(x => x.IsDeleted == false);
            obj.Language = Language.English;
            model = _trainingMaterialRepository.GetTrainingMaterial(obj);
            return View(model);
        }
        public ActionResult SubscribeNewsLetter(string Email)
        {
            NewsLetterDTO model = new NewsLetterDTO();
            bool msg = false;
            if (Email != "")
            {
                model.Email = Email;
                msg = _newsLetterRepository.Add(model);
                if (msg == true)
                {
                    TempData["msg"] = "Suscribed !";
                    BLMail.SendMail(Email, "Subscribe News Letter", "Thank you for subscribing our News Letter !", false);
                }
                else
                {
                    TempData["msg"] = "Not Suscribed. Please try after some time.";
                }
            }
            return Json(msg, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetCategoryByDepartment(int[] Id)
        {
            JobDTOWeb model = new JobDTOWeb();
            LanguageFilterDTO lang = new LanguageFilterDTO();
            lang.Language = Language.English;
            if (Id != null)
            {
                model.DepartmentId = Id;
                model.lstDepCategories = _departmentCategoryRepository.GetCategoryById(model.DepartmentId, lang);
            }
            return Json(model.lstDepCategories, JsonRequestBehavior.AllowGet);
        }
        public ActionResult PrivacyPolicyMobile()
        {
            return View();
        }

        public ActionResult GetLocations(CityRequestDTO req)
        {
            var data = _cityRepository.GetLocations(req);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        //public ActionResult SendLinkToUser(string Mobile)
        //{
        //    string msg = BLSMS.SendSMS(Mobile, "Please click on the below link to download Career Mitra Job Seeker Application. https://play.google.com/store/apps/details?id=com.carriermitra ");
        //    return Json(msg, JsonRequestBehavior.AllowGet);
        //}

        public ActionResult SendLinkToUser(mymodel obj)
        {
            if (Recaptcha.Validate(obj.captcharesponse) == false)
            {
                return Json("Captcha Validation failed", JsonRequestBehavior.AllowGet);
            }
            string TempId = "1207162427517508418";
            string msg = BLSMS.SendSMS(obj.Mobile, "Please click on the below link to download Career Mitra Job Seeker Application. https://play.google.com/store/apps/details?id=com.carriermitra  from CareerMitra",TempId);
            return Json(msg, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Home()
        {
            return View();
        }
        public JsonResult GetPinByLocation(string Id)
        {
            int? pincode = 0;
            if (!string.IsNullOrEmpty(Id))
            {
                pincode = _pincodeRepository.GetAll(x => x.Id == Convert.ToInt32(Id)).Select(x => x.PinCode).FirstOrDefault();
            }
            return Json(pincode, JsonRequestBehavior.AllowGet);
        }
        public ActionResult EnrollmentProgram()
        {
            EnrollmentProgramResponseDTOWeb model = new EnrollmentProgramResponseDTOWeb();
            EnrollmentProgramRequestDTO obj = new EnrollmentProgramRequestDTO();
            obj.Language = Language.English;
            int UserId = 0;
            if (Session["Id"] != null)
            {
                UserId = Convert.ToInt32(Session["Id"]);
            }
            model = _enrollmentProgramRepository.GetEnrollmentProgramWeb(obj, UserId);
            return View(model);
        }
        public ActionResult ApplyEnrollmentProgram(string Id, string UserAgent)
        {
            EnrollmentApplyDTO obj = new EnrollmentApplyDTO();
            obj.Language = Language.English;
            int msg = 0;
            if (Session["Id"] != null)
            {
                if (Id != null)
                {
                    obj.EnrollmentProgramId = Convert.ToInt32(Id);
                    obj.Id = Convert.ToInt32(Session["Id"]);
                    obj.UserAgent = UserAgent;
                    msg = _enrollmentProgramRepository.ApplyEnrollmentProgram(obj);
                }
            }
            else
            {
                return RedirectToAction("Login", "en");
            }
            return Json(msg, JsonRequestBehavior.AllowGet);
        }

        public ActionResult BlackListedDesignation()
        {
            JobRoleDTO model = new JobRoleDTO();
            model.lst = _jobRoleRepository.GetAll(x => x.IsDeleted == false && x.IsBlocked == true).ToList();
            return View(model);
        }
    }

}