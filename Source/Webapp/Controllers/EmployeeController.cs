using Data.DTOs;
using Data.Interfaces.Repositories;
using System;
using System.Linq;
using System.Web.Mvc;
using Utility;
using Utility.Enums;
namespace Webapp.Controllers
{
    //[Route(Name ="Employer")]
    [ValidateInput(false)]
    public class EmployeeController : BaseController
    {
        private readonly IEmpJobPostRespository _EmpJobPostRepository;
        private readonly ICourseRepository _courseRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IJobRoleRepository _jobRoleRepository;
        private readonly IJobTypeRepository _jobTypeRepository;
        private readonly ISkillRepository _skillRepository;
        private readonly ICityRepository _cityRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUserJobRepository _userJobRepository;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IDepartmentCategoryRepository _departmentCategoryRepository;
        private readonly IPincodeRepository _pincodeRepository;
        private readonly IChatRepository _chatRepository;

        public EmployeeController(IEmpJobPostRespository EmpJobPostRepository, ICourseRepository courseRepository, ICategoryRepository categoryRepository, IJobRoleRepository jobRoleRepository, IJobTypeRepository jobTypeRepository, ISkillRepository skillRepository, ICityRepository cityRepository, IUserRepository userRepository, IUserJobRepository userJobRepository, IDepartmentRepository departmentRepository, IDepartmentCategoryRepository departmentCategoryRepository, IPincodeRepository pincodeRepository, IChatRepository chatRepository)
        {
            this._EmpJobPostRepository = EmpJobPostRepository;
            this._courseRepository = courseRepository;
            this._categoryRepository = categoryRepository;
            this._jobRoleRepository = jobRoleRepository;
            this._jobTypeRepository = jobTypeRepository;
            this._skillRepository = skillRepository;
            this._cityRepository = cityRepository;
            this._userRepository = userRepository;
            this._userJobRepository = userJobRepository;
            this._departmentRepository = departmentRepository;
            this._departmentCategoryRepository = departmentCategoryRepository;
            this._pincodeRepository = pincodeRepository;
            this._chatRepository = chatRepository;
        }
        public ActionResult Dashboard()
        {
            EmpDashboardDTO model = new EmpDashboardDTO();
            int Id = Convert.ToInt32(Session["Id"]);
            model = _EmpJobPostRepository.GetDashboardDataForEmployer(Id);
            return View(model);
        }
        public new ActionResult Profile()
        {
            EmployerDTO model = new EmployerDTO();
            try
            {
                var id = Convert.ToInt32(Session["Id"]);
                model.User = _userRepository.GetDetailById(id);
                if (model.User != null)
                {
                    model.Id = model.User.Id;
                    model.UserDetail = model.User.UserDetails.FirstOrDefault();
                    model.UserName = model.User.UserName;
                    model.Password = model.User.Password;
                    if (model.UserDetail != null)
                    {
                        model.FirstName = model.UserDetail.FirstName;
                        model.LastName = model.UserDetail.LastName;
                        model.FullName = model.UserDetail.FirstName + ' ' + model.UserDetail.LastName;
                        model.Email = model.UserDetail.Email;
                        model.Mobile = model.UserDetail.Mobile;
                        model.DOB = Convert.ToDateTime(model.UserDetail.DOB);
                        model.Age = model.UserDetail.Age;
                        model.Gender = model.UserDetail.Gender;
                        model.State = model.UserDetail.State;
                        model.Address = model.UserDetail.Address;
                        model.Photo = model.UserDetail.Photo;
                        model.CompanyName = model.UserDetail.CompanyName;
                        model.ContactPersonName = model.UserDetail.ContactPersonName;
                        //model.OfficialEmailId = model.UserDetail.OfficialEmailId;
                        model.About = model.UserDetail.About;
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
        [ValidateAntiForgeryToken()]
        public ActionResult Profile(UpdateUserDTO model)
        {
            bool msg;
            try
            {
                msg = _userRepository.Update(model);
                if (msg == true)
                {
                    TempData["msg"] = "Data Updated Successfully !";

                }
            }
            catch (Exception ex)
            {
                TempData["msg"] = ex.Message;
            }
            return RedirectToAction("Profile", "Employee");
        }
        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Login", "Home");
        }
        [HttpGet]
        public ActionResult JobPost(string Id)
        {
            EmpJobPostDTO model = new EmpJobPostDTO();
            ViewBag.Category = "-Select-";
            ViewBag.Role = _jobRoleRepository.GetAll(x => x.IsDeleted == false);
            ViewBag.JobType = _jobTypeRepository.GetAll(x => x.IsDeleted == false);
            ViewBag.Course = _courseRepository.GetAll(x => x.IsDeleted == false);
            ViewBag.City = _cityRepository.GetAll(x => x.IsDeleted == false && x.Id == 338);
            ViewBag.Skill = _skillRepository.GetAll(x => x.IsDeleted == false);
            ViewBag.Department = _departmentRepository.GetAll(x => x.IsDeleted == false);
            ViewBag.Location = _pincodeRepository.GetAll(x => x.IsDeleted == false && x.CityId == 338);
            try
            {
                if (Id != null)
                {
                    model.Id = Convert.ToInt32(Id);
                    model.job = _EmpJobPostRepository.GetDetailById(model.Id);
                    if (model.job != null)
                    {
                        model.Title = model.job.Title;
                        model.HindiTitle = model.job.TitleH;
                        model.Description = model.job.Description;
                        model.HindiDescription = model.job.DescriptionH;
                        model.ShortDescription = model.job.ShortDescription;
                        model.HindiShortDescription = model.job.ShortDescriptionH;
                        model.JobTypeId = Convert.ToInt32(model.job.JobTypeId);
                        model.JobRoleId = Convert.ToInt32(model.job.JobRoleId);
                        model.CategoryId = Convert.ToInt32(model.job.CategoryId);
                        model.CourseId = Convert.ToInt32(model.job.JobQualifications.Select(i => i.CourseId).FirstOrDefault());
                        model.SalaryMax = model.job.SalaryMax;
                        model.SalaryMin = model.job.SalaryMin;
                        model.ExperienceMax = model.job.ExperienceMax;
                        model.ExperienceMin = model.job.ExperienceMin;
                        model.PostedDate = model.job.PostedDate;
                        model.LastDate = Convert.ToDateTime(model.job.LastDate);
                        var lstSkill = model.job.JobSkills.Where(x => x.IsDeleted == false).Select(x => new JobskillDTO
                        {
                            SkillId = x.SkillId,
                            Skill = x.Skill != null ? x.Skill.Name : ""
                        }).ToList();

                        model.jobSkill = lstSkill;
                        model.jobLocation = model.job.JobLocations.FirstOrDefault();

                        if (model.jobLocation != null)
                        {
                            model.CityId = model.jobLocation.City.Id;
                            model.City = model.jobLocation.City.Name;
                        }
                    }
                }
                else
                {
                }
            }
            catch (Exception ex)
            {
                TempData["msg"] = ex.Message;
            }
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult JobPost(UpdateJobPostDTO model)
        {
            bool msg;
            try
            {
                ViewBag.Category = _categoryRepository.GetAll(x => x.IsDeleted == false);
                ViewBag.Role = _jobRoleRepository.GetAll(x => x.IsDeleted == false);
                ViewBag.JobType = _jobTypeRepository.GetAll(x => x.IsDeleted == false);
                ViewBag.Skill = _skillRepository.GetAll(x => x.IsDeleted == false);
                ViewBag.Course = _courseRepository.GetAll(x => x.IsDeleted == false);
                ViewBag.City = _cityRepository.GetAll(x => x.IsDeleted == false);
                ViewBag.Skill = _skillRepository.GetAll(x => x.IsDeleted == false);
                ViewBag.Department = _departmentRepository.GetAll(x => x.IsDeleted == false);
                ViewBag.Location = _pincodeRepository.GetAll(x => x.IsDeleted == false && x.CityId == 338);
                Random rnd = new Random();
                model.DeviceType = DeviceType.Web;
                if (model.postedImage != null)
                {
                    string imageFile = "img_" + rnd.Next(000, 999) + model.postedImage.FileName;
                    string path = Server.MapPath("~/FileUpload/Other/");
                    model.postedImage.SaveAs(path + imageFile);
                    model.Image = imageFile;
                }
                else
                {
                    model.Image = "";
                }
                if (model.Id != 0)
                {
                    msg = _EmpJobPostRepository.Update(model);
                    if (msg == true)
                    {
                        TempData["msg"] = "Updated";
                    }
                    else
                    {
                        TempData["msg"] = "Not Updated! Please try again.";
                    }
                }
                else
                {
                    model.UserId = Convert.ToInt32(Session["Id"]);
                    msg = _EmpJobPostRepository.Add(model);
                    if (msg == true)
                    {
                        TempData["msg"] = "Saved";
                    }
                    else
                    {
                        TempData["msg"] = "Not Saved! Please try again.";
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["msg"] = ex.Message;
            }
            return RedirectToAction("JobPost");
        }
        public ActionResult JobPostList(string Id)
        {
            JobList model = new JobList();
            try
            {
                model.Id = Convert.ToInt32(Session["Id"]);
                model.jobList = _EmpJobPostRepository.GetJobPostList(model.Id);
            }
            catch (Exception ex)
            {
                TempData["Job"] = ex.Message;
            }
            return View(model);
        }
        public ActionResult DeleteJobPost(string Id)
        {
            bool msg;
            int UserId = 1;
            msg = _EmpJobPostRepository.Delete(Convert.ToInt32(Id), UserId);
            if (msg == true)
            {
                TempData["msg"] = "Data deleted successfully !";
            }
            return RedirectToAction("JobPostList", "Employee");
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
                model.DeviceType = DeviceType.Web;
                var msg = _userRepository.ChangePassword(model);
                if (msg == true)
                {
                    TempData["Response"] = Constants.LOG_PASSWORD_CHANGE_SUCCESSFUL;
                    return RedirectToAction("Login", "Home");

                }
                else
                {
                    return RedirectToAction("ChangePassword", "Employee");
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "')<script>");
                return RedirectToAction("ChangePassword", "Employee");
            }

        }

        public ActionResult AppliedCandidate()
        {
            EmpJobPostDTO model = new EmpJobPostDTO();
            ViewBag.JobType = _jobTypeRepository.GetAll(x => x.IsDeleted == false);
            ViewBag.JobRole = _jobRoleRepository.GetAll(x => x.IsDeleted == false);
            try
            {
                model.Language = Language.English;
                model.UserId = Convert.ToInt32(Session["Id"]);
                model.lstCandidate = _userJobRepository.GetApplyCandidate(model);

            }
            catch (Exception ex)
            {
                TempData["Job"] = ex.Message;
            }
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult AppliedCandidate(EmpJobPostDTO model)
        {

            ViewBag.JobType = _jobTypeRepository.GetAll(x => x.IsDeleted == false);
            ViewBag.JobRole = _jobRoleRepository.GetAll(x => x.IsDeleted == false);
            try
            {
                model.Language = Language.English;
                model.UserId = Convert.ToInt32(Session["Id"]);
                model.lstCandidate = _userJobRepository.GetApplyCandidate(model);

            }
            catch (Exception ex)
            {
                TempData["Job"] = ex.Message;
            }
            return View(model);
        }

        public ActionResult ShortListedCandidate()
        {
            EmpJobPostDTO model = new EmpJobPostDTO();
            ViewBag.JobRole = _jobRoleRepository.GetAll(x => x.IsDeleted == false);
            ViewBag.JobType = _jobTypeRepository.GetAll(x => x.IsDeleted == false);
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult ShortListedCandidate(EmpJobPostDTO model)
        {
            ViewBag.JobType = _jobTypeRepository.GetAll(x => x.IsDeleted == false);
            ViewBag.JobRole = _jobRoleRepository.GetAll(x => x.IsDeleted == false);
            try
            {
                model.Language = Language.English;
                model.lstCandidate = _userJobRepository.GetShortListedCandidate(model);

            }
            catch (Exception ex)
            {
                TempData["Job"] = ex.Message;
            }
            return View(model);
        }
        public ActionResult PublishJob(string Id)
        {
            EmpJobPostDTO model = new EmpJobPostDTO();
            bool msg;
            try
            {
                model.Id = Convert.ToInt32(Id);
                msg = _EmpJobPostRepository.PublishJob(model.Id, Convert.ToInt32(Session["Id"]));
                if (msg == true)
                {
                    TempData["msg"] = "Published";
                }
            }
            catch (Exception ex)
            {
                TempData["msg"] = ex.Message;
            }
            return RedirectToAction("JobPostList");
        }

        public JsonResult GetDepartmentwiseCategory(int[] DepartmentId)
        {
            DepartmentCategoryDTO model = new DepartmentCategoryDTO();
            LanguageFilterDTO lang = new LanguageFilterDTO();
            lang.Language = Language.English;
            model.DepartmentCategories = _departmentCategoryRepository.GetCategoryById(DepartmentId, lang);
            return Json(model.DepartmentCategories, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ShortList(string Id)
        {
            EmpJobPostDTO model = new EmpJobPostDTO();
            bool msg;
            try
            {
                model.Id = Convert.ToInt32(Session["Id"]);
                msg = _EmpJobPostRepository.ShortList(model.Id);
                if (msg == true)
                {
                    TempData["msg"] = "Short Listed";
                }
            }
            catch (Exception ex)
            {
                TempData["msg"] = ex.Message;
            }
            return RedirectToAction("AppliedCandidate");
        }
        //public ActionResult ReplyToJobSeeker(int userid, int jobid, string jobtitle, string Name, string ExperienceMax, string ExperienceMin, string City, string ShortDescription)
        //{
        //    if (Session["Id"] != null)
        //    {
        //        MessageSendEmployerRequestDTO model = new MessageSendEmployerRequestDTO();
        //        model.UserId = userid;
        //        model.Id = jobid;
        //        model.Title = jobtitle;
        //        model.UserName = Name;
        //        model.ExperienceMax = ExperienceMax;
        //        model.ExperienceMin = ExperienceMin;
        //        model.ShortDescription = ShortDescription;
        //        model.City = City;

        //        MessageChatRequestDTO r = new MessageChatRequestDTO();
        //        r.UserId = Convert.ToInt32(Session["Id"]);//Employer Id
        //        r.Id = jobid;
        //        r.JobSeekerId = userid;
        //        var msgList = _chatRepository.GetMessageChatsEmployer(r);
        //        model.lstMessage = msgList.Messages;
        //        return View(model);
        //    }
        //    else
        //    {
        //        return RedirectToAction("login", "en");
        //    }
        //}


        public ActionResult ReplyToJobSeeker(int userid, int jobid)
        {
            if (Session["Id"] != null)
            {

                EmpJobPostDTO emp = new EmpJobPostDTO();
                var details = _userJobRepository.GetJobDetailsByJobId(jobid, userid);
                MessageSendEmployerRequestDTO model = new MessageSendEmployerRequestDTO();
                model.UserId = userid;
                model.Id = jobid;
                MessageChatRequestDTO r = new MessageChatRequestDTO();
                r.UserId = Convert.ToInt32(Session["Id"]);//Employer Id
                r.Id = jobid;
                r.JobSeekerId = userid;
                var msgList = _chatRepository.GetMessageChatsEmployer(r);
                model.ExperienceMax = details.ExperienceMax;
                model.ExperienceMin = details.ExperienceMin;
                model.JobLocation = details.JobLocation;
                model.ShortDescription = details.ShortDescription;
                model.Title = details.Title;
                model.UserName = details.UserName;
                model.lstMessage = msgList.Messages;
                return View(model);
            }
            else
            {
                return RedirectToAction("login", "en");
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult ReplyToJobSeeker(MessageSendEmployerRequestDTO model)
        {

            try
            {
                if (Session["Id"] != null)
                {
                    model.Employer = Convert.ToInt32(Session["Id"]);

                    //model.DeviceType = DeviceType.Web;
                    var msg = _chatRepository.SendMessageEmployer(model);

                    MessageChatRequestDTO r = new MessageChatRequestDTO();
                    r.UserId = Convert.ToInt32(Session["Id"]);
                    var msgList = _chatRepository.GetMessageChatsEmployer(r);
                    model.lstMessage = msgList.Messages;
                }
                else
                {
                    return RedirectToAction("login", "en");
                }

            }
            catch (Exception ex)
            {
                TempData["Job"] = ex.Message;
            }
            return RedirectToAction("ReplyToJobSeeker", new { userid = model.UserId, jobid = model.Id });
        }
        //public ActionResult UserRegistrationList(string Id)
        //{
        //    UserDTO model = new UserDTO(); 
        //    try
        //    {
        //        model.User = _userRepository.GetAll();
        //        if (model.User.Count > 0)
        //        {
        //            model.lstUserDetail = model.lst[i].UserDetails.ToList();
        //            model.Name = model.UserDetail.FirstName + ' ' + model.UserDetail.LastName;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        TempData["Job"] = ex.Message;
        //    }
        //    return View(model);
        //}

        public ActionResult Messages()
        {
            MessageSendEmployerRequestDTO obj = new MessageSendEmployerRequestDTO();
            try
            {

                if (Session["Id"] != null)
                {
                    MessageRequestDTO r = new MessageRequestDTO();
                    r.UserId = Convert.ToInt32(Session["Id"]);
                    var msgList = _chatRepository.GetMessageListEmployer(r);
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


        public JsonResult GetUnreadMessgeCount()
        {
            int value = 0;
            try
            {
                if (Session["Id"] != null)
                {
                    value = _chatRepository.GetUnreadMessgeCount(Convert.ToInt32(Session["Id"]));
                }
            }
            catch (Exception ex)
            {

            }
            return Json(value, JsonRequestBehavior.AllowGet);
        }

        public virtual ActionResult EmpoyerChatList(MessageSendEmployerRequestDTO EmpoyerChatList)
        {

            MessageRequestDTO r = new MessageRequestDTO();
            r.UserId = Convert.ToInt32(Session["Id"]);
            var msgList = _chatRepository.GetMessageListEmployer(r);
            EmpoyerChatList.MessageList = msgList.Messages;

            return PartialView("_EmpoyerChatList", EmpoyerChatList);
        }
    }
}