using Data.DTOs;
using Data.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using Utility;
using Utility.Enums;

namespace Webapp.Controllers
{
    [ValidateInput(false)]
    public class AdminController : AdminBaseController
    {
        private readonly ICourseRepository _courseRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IJobRoleRepository _jobRoleRepository;
        private readonly IJobTypeRepository _jobTypeRepository;
        private readonly ISkillRepository _skillRepository;
        private readonly INotificationRepository _notificationRepository;
        private readonly ICityRepository _cityRepository;
        private readonly IEnrollmentProgramRepository _enrollmentProgramRepository;
        private readonly ITrainingMaterialRepository _trainingMaterialRepository;
        private readonly IEmpJobPostRespository _EmpJobPostRepository;
        private readonly IUserJobRepository _userJobRepository;
        private readonly IUserRepository _userRepository;
        private readonly IJobRepository _jobRepository;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IDepartmentCategoryRepository _departmentCategoryRepository;
        private readonly IAdminDashboardRepository _admindashboardRepository;
        private readonly IPincodeRepository _pincodeRepository;
        private readonly IEmailMasterRepository _emailmasterRepository;
        private readonly ISmslMasterRepository _smsmasterRepository;
        private readonly IAudittrailRepository _audittrailRepository;
        private readonly IFeedbackRepository _feedbackRepository;

        public AdminController(ICourseRepository courseRepository, ICategoryRepository categoryRepository, IJobRoleRepository jobRoleRepository, IJobTypeRepository jobTypeRepository, ISkillRepository skillRepository, ICityRepository cityRepository, INotificationRepository notificationRepository, IEnrollmentProgramRepository enrollmentProgramRepository, ITrainingMaterialRepository trainingMaterialRepository, IEmpJobPostRespository EmpJobPostRepository, IUserJobRepository userJobRepository, IUserRepository userRepository, IJobRepository jobRepository, IDepartmentRepository departmentRepository, IDepartmentCategoryRepository departmentCategoryRepository, IAdminDashboardRepository admindashboardRepository, IPincodeRepository pincodeRepository, IEmailMasterRepository emailmasterRepository, ISmslMasterRepository smsmasterRepository, IAudittrailRepository audittrailRepository, IFeedbackRepository feedbackRepository)
        {
            this._courseRepository = courseRepository;
            this._categoryRepository = categoryRepository;
            this._jobRoleRepository = jobRoleRepository;
            this._jobTypeRepository = jobTypeRepository;
            this._skillRepository = skillRepository;
            this._cityRepository = cityRepository;
            this._notificationRepository = notificationRepository;
            this._enrollmentProgramRepository = enrollmentProgramRepository;
            this._trainingMaterialRepository = trainingMaterialRepository;
            this._EmpJobPostRepository = EmpJobPostRepository;
            this._userJobRepository = userJobRepository;
            this._userRepository = userRepository;
            this._jobRepository = jobRepository;
            this._departmentRepository = departmentRepository;
            this._departmentCategoryRepository = departmentCategoryRepository;
            this._admindashboardRepository = admindashboardRepository;
            this._pincodeRepository = pincodeRepository;
            this._emailmasterRepository = emailmasterRepository;
            this._smsmasterRepository = smsmasterRepository;
            this._audittrailRepository = audittrailRepository;
            this._feedbackRepository = feedbackRepository;
        }
        public ActionResult Index()
        {
            AdminDashboardDTO model = new AdminDashboardDTO();
            model = _admindashboardRepository.GetDashboardDataForAdmin();
            model.jobList = _admindashboardRepository.GetJobPostList();
            model.RecentJobApplied = _admindashboardRepository.GetJobAppliedList();
            //var data=_admindashboardRepository.GetBarchartData();
            //model.BarCahrtData = JsonConvert.SerializeObject(data);
            return View(model);
        }

        public JsonResult GetDashboardChart()
        {
            return Json(_admindashboardRepository.GetBarchartData(), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetDashboardChartUserRegistredDetails()
        {
            return Json(_admindashboardRepository.GetBarchartDataUserRegistration(), JsonRequestBehavior.AllowGet);
        }
        public ActionResult JobRoleMaster(string Id)
        {
            JobRoleDTO model = new JobRoleDTO();
            try
            {
                if (Id != null)
                {
                    model.Id = Convert.ToInt32(Id);
                    model.jobRole = _jobRoleRepository.GetJobRoleById(model.Id);
                    if (model.jobRole != null)
                    {
                        model.Name = model.jobRole.Name;
                        model.NameH = model.jobRole.NameH;
                        model.Id = model.jobRole.Id;
                    }
                }
                model.lst = _jobRoleRepository.GetAll(x => x.IsDeleted == false).OrderByDescending(x => x.Id).ToList();
                if (model.lst.Count > 0)
                {
                    // TempData["msg"] = "Record found";
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "')</script>");
            }
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult JobRoleMaster(UpdateJobRoleDTO model)
        {
            bool msg;
            try
            {
                if (ModelState.IsValid)
                {
                    if (model.Id != 0)
                    {
                        msg = _jobRoleRepository.Update(model);
                        if (msg == true)
                        {
                            TempData["msg"] = "Updated";
                        }
                    }
                    else
                    {
                        msg = _jobRoleRepository.Add(model);
                        if (msg == true)
                        {
                            TempData["msg"] = "Saved";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["msg"] = ex.Message;
            }
            return RedirectToAction("JobRoleMaster", "Admin");
        }
        public ActionResult DeleteJobRole(string Id)
        {
            bool msg;
            int UserId = 1;
            msg = _jobRoleRepository.Delete(Convert.ToInt32(Id), UserId);
            if (msg == true)
            {
                TempData["msg"] = "Deleted";
            }
            return RedirectToAction("JobRoleMaster", "Admin");
        }

        public ActionResult BlockJobRole(string Id)
        {
            bool msg;
            int UserId = 1;
            msg = _jobRoleRepository.BlockJobRole(Convert.ToInt32(Id), UserId);
            if (msg == true)
            {
                TempData["msg"] = "Blocked";
            }
            return RedirectToAction("JobRoleMaster", "Admin");
        }
        public ActionResult UnBlockJobRole(string Id)
        {
            bool msg;
            int UserId = 1;
            msg = _jobRoleRepository.UnBlockJobRole(Convert.ToInt32(Id), UserId);
            if (msg == true)
            {
                TempData["msg"] = "Unblocked";
            }
            return RedirectToAction("JobRoleMaster", "Admin");
        }
        public ActionResult CourseMaster(string Id)
        {
            CourseDTO model = new CourseDTO();
            try
            {
                if (Id != null)
                {
                    model.Id = Convert.ToInt32(Id);
                    model.course = _courseRepository.GetCourseById(model.Id);
                    if (model.course != null)
                    {
                        model.Name = model.course.Name;
                        model.NameH = model.course.NameH;
                        model.Id = model.course.Id;
                    }
                }
                model.lst = _courseRepository.GetAll(x => x.IsDeleted == false).OrderByDescending(x => x.Id).ToList();
                if (model.lst.Count > 0)
                {
                    //TempData["msg"] = "Record Found";
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
        public ActionResult CourseMaster(UpdateCourseDTO model)
        {
            bool msg;
            try
            {
                if (ModelState.IsValid)
                {
                    if (model.Id != 0)
                    {
                        msg = _courseRepository.Update(model);
                        if (msg == true)
                        {
                            TempData["msg"] = "Updated";
                        }
                    }
                    else
                    {
                        msg = _courseRepository.Add(model);
                        if (msg == true)
                        {
                            TempData["msg"] = "Saved";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["msg"] = ex.Message;
            }
            return RedirectToAction("CourseMaster", "Admin");
        }
        public ActionResult DeleteCourse(string Id)
        {
            bool msg;
            int UserId = 1;
            msg = _courseRepository.Delete(Convert.ToInt32(Id), UserId);
            if (msg == true)
            {
                TempData["msg"] = "Deleted";
            }
            return RedirectToAction("CourseMaster", "Admin");
        }
        public ActionResult SkillMaster(string Id)
        {
            SkillDTO model = new SkillDTO();
            try
            {
                if (Id != null)
                {
                    model.Id = Convert.ToInt32(Id);
                    model.skill = _skillRepository.GetSkillById(model.Id);
                    if (model.skill != null)
                    {
                        model.Name = model.skill.Name;
                        model.NameH = model.skill.NameH;
                        model.Id = model.skill.Id;
                    }
                }
                model.lst = _skillRepository.GetAll();
                if (model.lst.Count > 0)
                {
                    //TempData["msg"] = "Record Found";
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
        public ActionResult SkillMaster(UpdateSkillDTO model)
        {
            bool msg;
            try
            {
                if (ModelState.IsValid)
                {
                    if (model.Id != 0)
                    {
                        msg = _skillRepository.Update(model);
                        if (msg == true)
                        {
                            TempData["msg"] = "Updated";
                        }
                    }
                    else
                    {
                        msg = _skillRepository.Add(model);
                        if (msg == true)
                        {
                            TempData["msg"] = "Saved";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["msg"] = ex.Message;
            }
            return RedirectToAction("SkillMaster", "Admin");
        }
        public ActionResult DeleteSkill(string Id)
        {
            bool msg;
            int UserId = 1;
            msg = _skillRepository.Delete(Convert.ToInt32(Id), UserId);
            if (msg == true)
            {
                TempData["msg"] = "Deleted";
            }
            return RedirectToAction("SkillMaster", "Admin");
        }

        public ActionResult DepartmentMaster(string Id)
        {
            DepartmentDTO model = new DepartmentDTO();
            try
            {
                if (Id != null)
                {
                    model.Id = Convert.ToInt32(Id);
                    model.Department = _departmentRepository.GetByIdCustom(model.Id);
                    if (model.Department != null)
                    {
                        model.Name = model.Department.Name;
                        model.NameH = model.Department.NameH;
                        model.Id = model.Department.Id;
                    }
                }
                model.lst = _departmentRepository.GetAll(x => x.IsDeleted == false).OrderByDescending(x => x.Id).ToList();
                if (model.lst.Count > 0)
                {
                    //TempData["msg"] = "Record Found";
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
        public ActionResult DepartmentMaster(UpdateDepartmentDTO model)
        {
            bool msg;
            try
            {
                if (ModelState.IsValid)
                {
                    if (model.Id != 0)
                    {
                        msg = _departmentRepository.Update(model);
                        if (msg == true)
                        {
                            TempData["msg"] = "Updated";
                        }
                    }
                    else
                    {
                        msg = _departmentRepository.Add(model);
                        if (msg == true)
                        {
                            TempData["msg"] = "Saved";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["msg"] = ex.Message;
            }
            return RedirectToAction("DepartmentMaster", "Admin");
        }
        public ActionResult DeleteDepartment(string Id)
        {
            bool msg;
            int UserId = 1;
            msg = _departmentRepository.Delete(Convert.ToInt32(Id), UserId);
            if (msg == true)
            {
                TempData["msg"] = "Deleted";
            }
            return RedirectToAction("DepartmentMaster", "Admin");
        }

        public ActionResult CategoryMaster(string Id)
        {
            CategoryDTO model = new CategoryDTO();
            try
            {
                if (Id != null)
                {
                    model.Id = Convert.ToInt32(Id);
                    model.category = _categoryRepository.GetCategoryById(model.Id);
                    if (model.category != null)
                    {
                        model.Name = model.category.Name;
                        model.NameH = model.category.NameH;
                        model.Id = model.category.Id;
                    }
                }
                model.lst = _categoryRepository.GetAll(x => x.IsDeleted == false).OrderByDescending(x => x.Id).ToList();
                if (model.lst.Count > 0)
                {
                    //TempData["msg"] = "Record Found";
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
        public ActionResult CategoryMaster(UpdateCategoryDTO model)
        {
            bool msg;
            try
            {
                if (ModelState.IsValid)
                {
                    if (model.Id != 0)
                    {
                        msg = _categoryRepository.Update(model);
                        if (msg == true)
                        {
                            TempData["msg"] = "Updated";
                        }
                    }
                    else
                    {
                        msg = _categoryRepository.Add(model);
                        if (msg == true)
                        {
                            TempData["msg"] = "Saved";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["msg"] = ex.Message;
            }
            return RedirectToAction("CategoryMaster", "Admin");
        }
        public ActionResult DeleteCategory(string Id)
        {
            bool msg;
            int UserId = 1;
            msg = _categoryRepository.Delete(Convert.ToInt32(Id), UserId);
            if (msg == true)
            {
                TempData["msg"] = "Deleted";
            }
            return RedirectToAction("CategoryMaster", "Admin");
        }


        public ActionResult DepartmentCategoryMapping(string Id)
        {
            DepartmentCategoryDTO model = new DepartmentCategoryDTO();
            ViewBag.Department = _departmentRepository.GetAll(x => x.IsDeleted == false).ToList();
            ViewBag.Categories = _categoryRepository.GetAll(x => x.IsDeleted == false).ToList();
            try
            {
                model.DeviceType = DeviceType.Web;
                if (Id != null)
                {
                    model.Id = Convert.ToInt32(Id);
                    model.dcMapping = _departmentCategoryRepository.GetByIdCustom(model.Id);
                    if (model.dcMapping != null)
                    {
                        //model.CategoryId = model.dcMapping.CategoryId;
                        model.DepartmentId = model.dcMapping.DepartmentId;

                        var categories = _departmentCategoryRepository.GetAll(x => x.IsDeleted == false && x.DepartmentId == model.DepartmentId).ToList();
                        List<DepartmentWiseCategoryDTO> DepartmentCategories = new List<DepartmentWiseCategoryDTO>();
                        foreach (var item in categories)
                        {
                            DepartmentWiseCategoryDTO d = new DepartmentWiseCategoryDTO();
                            d.Id = item.CategoryId;
                            d.Name = item.Category != null ? item.Category.Name : "";
                            DepartmentCategories.Add(d);
                        }
                        model.DepartmentCategories = DepartmentCategories;
                    }
                }
                model.lst = _departmentCategoryRepository.GetAll(x => x.IsDeleted == false).OrderByDescending(x => x.Id).ToList();

            }
            catch (Exception ex)
            {
                TempData["msg"] = ex.Message;
            }
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult DepartmentCategoryMapping(DepartmentCategoryDTO model)
        {
            bool msg;
            try
            {
                model.DeviceType = DeviceType.Web;
                if (ModelState.IsValid)
                {
                    if (model.Id != 0)
                    {
                        msg = _departmentRepository.AddOrUpdateMapping(model);
                        if (msg == true)
                        {
                            TempData["msg"] = "Updated";
                        }
                        else
                        {
                            TempData["msg"] = "Not Updated";
                        }
                    }
                    else
                    {
                        msg = _departmentRepository.AddOrUpdateMapping(model);
                        if (msg == true)
                        {
                            TempData["msg"] = "Saved";
                        }
                        else
                        {
                            TempData["msg"] = "Not Saved";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["msg"] = ex.Message;
            }
            return RedirectToAction("DepartmentCategoryMapping", "Admin");
        }
        public ActionResult DeleteDepartmentCategoryMapping(string Id)
        {
            bool msg;
            int UserId = 1;
            msg = _departmentCategoryRepository.Delete(Convert.ToInt32(Id), UserId);
            if (msg == true)
            {
                TempData["msg"] = "Deleted";
            }
            return RedirectToAction("DepartmentCategoryMapping", "Admin");
        }

        public ActionResult PinCodeMaster(string Id)
        {
            PinCodeDTO model = new PinCodeDTO();
            int i = 0;
            try
            {
                ViewBag.City = _cityRepository.GetAll(x => x.IsDeleted == false);
                if (Id != null)
                {
                    model.Id = Convert.ToInt32(Id);
                    model.pinCodeMaster = _pincodeRepository.GetPinCodeById(model.Id);
                    if (model.pinCodeMaster != null)
                    {
                        model.Name = model.pinCodeMaster.Name;
                        model.NameH = model.pinCodeMaster.NameH;
                        model.Id = model.pinCodeMaster.Id;
                        model.PinCode = model.pinCodeMaster.PinCode;
                        model.CityId = (int)model.pinCodeMaster.CityId;
                    }
                }
                model.lst = _pincodeRepository.GetAll(x => x.IsDeleted == false && x.CityId == 370).ToList();
            }
            catch (Exception ex)
            {
                TempData["msg"] = ex.Message;
            }
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult PinCodeMaster(PinCodeDTO model)
        {
            bool msg;
            try
            {
                if (model.Id != 0)
                {
                    msg = _pincodeRepository.Update(model);
                    if (msg == true)
                    {
                        TempData["msg"] = "Updated";
                    }
                }
                else
                {
                    msg = _pincodeRepository.Add(model);
                    if (msg == true)
                    {
                        TempData["msg"] = "Saved";
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["msg"] = ex.Message;
            }
            return RedirectToAction("PinCodeMaster", "Admin");
        }
        public ActionResult DeletePinCode(string Id)
        {
            bool msg;
            int UserId = 1;
            msg = _pincodeRepository.Delete(Convert.ToInt32(Id), UserId);
            if (msg == true)
            {
                TempData["msg"] = "Deleted";
            }
            return RedirectToAction("PinCodeMaster", "Admin");
        }
        //public ActionResult JobTypeMaster(string Id)
        //{
        //    JobTypeDTO model = new JobTypeDTO();
        //    int i = 0;
        //    try
        //    {
        //        if (Id != null)
        //        {
        //            model.Id = Convert.ToInt32(Id);
        //            model.lst = _jobTypeRepository.GetDetailById(model.Id);
        //            if (model.lst.Count > 0)
        //            {
        //                model.Name = model.lst[i].Name;
        //                model.NameH = model.lst[i].NameH;
        //                model.Id = model.lst[i].Id;
        //            }
        //        }
        //        model.lst = _jobTypeRepository.GetAll();
        //        if (model.lst.Count > 0)
        //        {
        //            // TempData["msg"] = "Record Found";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        TempData["msg"] = ex.Message;
        //    }
        //    return View(model);
        //}
        //[HttpPost]
        //[ValidateAntiForgeryToken()]
        //public ActionResult JobTypeMaster(UpdateJobTypeDTO model)
        //{
        //    bool msg;
        //    try
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            if (model.Id != 0)
        //            {
        //                msg = _jobTypeRepository.Update(model);
        //                if (msg == true)
        //                {
        //                    TempData["msg"] = "Updated";
        //                }
        //            }
        //            else
        //            {
        //                msg = _jobTypeRepository.Add(model);
        //                if (msg == true)
        //                {
        //                    TempData["msg"] = "Saved";
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        TempData["msg"] = ex.Message;
        //    }
        //    return RedirectToAction("JobTypeMaster", "Admin");
        //}
        //public ActionResult DeleteJobType(string Id)
        //{
        //    bool msg;
        //    int UserId = 1;
        //    msg = _jobTypeRepository.Delete(Convert.ToInt32(Id), UserId);
        //    if (msg == true)
        //    {
        //        TempData["msg"] = "Deleted";
        //    }
        //    return RedirectToAction("JobTypeMaster", "Admin");
        //}

        //public ActionResult StateMaster(string Id)
        //{
        //    StateDTO model = new StateDTO();
        //    int i = 0;
        //    try
        //    {
        //        if (Id != null)
        //        {
        //            model.Id = Convert.ToInt32(Id);
        //            model.lst = _stateRepository.GetDetailById(model.Id);
        //            if (model.lst.Count > 0)
        //            {
        //                model.Name = model.lst[i].Name;
        //                model.NameH = model.lst[i].NameH;
        //                model.Id = model.lst[i].Id;
        //            }
        //        }
        //        else
        //        {
        //            model.lst = _stateRepository.GetAll();
        //            if (model.lst.Count > 0)
        //            {
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        TempData["msg"] = ex.Message;
        //    }
        //    return View(model);
        //}
        //[HttpPost]
        //[ValidateAntiForgeryToken()]
        //public ActionResult StateMaster(UpdateStateDTO model)
        //{
        //    bool msg;
        //    try
        //    {
        //        if (model.Id != 0)
        //        {
        //            msg = _stateRepository.Update(model);
        //            if (msg == true)
        //            {
        //                TempData["msg"] = "Updated";
        //            }
        //        }
        //        else
        //        {
        //            msg = _stateRepository.Add(model);
        //            if (msg == true)
        //            {
        //                TempData["msg"] = "Saved";
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        TempData["msg"] = ex.Message;
        //    }
        //    return RedirectToAction("StateMaster", "Admin");
        //}
        //public ActionResult DeleteState(string Id)
        //{
        //    bool msg;
        //    int UserId = 1;
        //    msg = _stateRepository.Delete(Convert.ToInt32(Id), UserId);
        //    if (msg == true)
        //    {
        //        TempData["msg"] = "Deleted";
        //    }
        //    return RedirectToAction("StateMaster", "Admin");
        //}
        //public ActionResult CityMaster(string Id)
        //{
        //    CityDTO model = new CityDTO();
        //    int i = 0;
        //    try
        //    {
        //        ViewBag.State = _stateRepository.GetAll();
        //        if (Id != null)
        //        {
        //            model.Id = Convert.ToInt32(Id);
        //            model.lst = _cityRepository.GetDetailById(model.Id);
        //            if (model.lst.Count > 0)
        //            {
        //                model.Name = model.lst[i].Name;
        //                model.NameH = model.lst[i].NameH;
        //                model.Id = model.lst[i].Id;
        //            }
        //        }
        //        else
        //        {
        //            model.lst = _cityRepository.GetAll();
        //            if (model.lst.Count > 0)
        //            {
        //                TempData["msg"] = "Record Found";
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        TempData["msg"] = ex.Message;
        //    }
        //    return View(model);
        //}
        //[HttpPost]
        //[ValidateAntiForgeryToken()]
        //public ActionResult CityMaster(UpdateCityDTO model)
        //{
        //    bool msg;
        //    try
        //    {
        //        if (model.Id != 0)
        //        {
        //            msg = _cityRepository.Update(model);
        //            if (msg == true)
        //            {
        //                TempData["msg"] = "Updated";
        //            }
        //        }
        //        else
        //        {
        //            msg = _cityRepository.Add(model);
        //            if (msg == true)
        //            {
        //                TempData["msg"] = "Saved";
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        TempData["msg"] = ex.Message;
        //    }
        //    return RedirectToAction("CityMaster", "Admin");
        //}
        //public ActionResult DeleteCity(string Id)
        //{
        //    bool msg;
        //    int UserId = 1;
        //    msg = _cityRepository.Delete(Convert.ToInt32(Id), UserId);
        //    if (msg == true)
        //    {
        //        TempData["msg"] = "Deleted";
        //    }
        //    return RedirectToAction("CityMaster", "Admin");
        //}
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
                string dcData = Security.EncryptString(Constants.EncKey, model.Password);

                model.Password = dcData;
                model.DeviceType = DeviceType.Web;
                var msg = _userRepository.ChangePassword(model);
                if (msg == true)
                {
                    TempData["Response"] = Constants.LOG_PASSWORD_CHANGE_SUCCESSFUL;
                    return RedirectToAction("Login", "Account");

                }
                else
                {
                    return RedirectToAction("ChangePassword", "Admin");
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "')<script>");
                return RedirectToAction("ChangePassword", "Admin");
            }

        }
        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Login", "Account");
        }
        public ActionResult EmployeeList()
        {
            EmployerListDTO model = new EmployerListDTO();
            try
            {
                model.Employees = _userRepository.GetAll(x => x.RoleId == Role.Employer && x.IsDeleted == false).ToList();
                
            }
            catch (Exception ex)
            {
                TempData["msg"] = ex.Message;
            }
            return View(model);
        }
        public ActionResult ApproveEmployer(string Id)
        {
            EmployerDTO model = new EmployerDTO();
            bool msg;
            try
            {
                model.Id = Convert.ToInt32(Id);
                msg = _userRepository.ApproveEmployer(model.Id, Convert.ToInt32(Session["Id"]));
                if (msg == true)
                {
                    TempData["msg"] = "Approved Successfully";
                }
            }
            catch (Exception ex)
            {
                TempData["msg"] = ex.Message;
            }
            return RedirectToAction("EmployeeList");
        }
        public ActionResult EmployerDetail(string Id)
        {
            EmployerDTO model = new EmployerDTO();
            try
            {
                model.User = _userRepository.GetDetailById(Convert.ToInt32(Id));
                if (model.User != null)
                {
                    model.Id = model.User.Id;
                    model.UserDetail = model.User.UserDetails.FirstOrDefault();
                    model.UserDocument = model.User.UserDocuments.ToList();
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
                        model.Address = model.UserDetail.Address;
                        model.Photo = model.UserDetail.Photo != null ? Constants.BASE_URL + "FileUpload/ProfilePhoto/" + model.UserDetail.Photo : "";
                        model.CompanyName = model.UserDetail.CompanyName;
                        model.ContactPersonName = model.UserDetail.ContactPersonName;
                        model.NoOfEmployees = model.UserDetail.NoOfEmployees;
                        model.CompanyType = model.UserDetail.CompanyType;
                        //model.OfficialEmailId = model.UserDetail.OfficialEmailId;
                        model.About = model.UserDetail.About;
                    }
                    if (model.UserDocument != null)
                    {
                        //model.CompanyCertificate = Constants.BASE_URL+ "FileUpload/CompanyCertificate/" + model.UserDocument.Where(x=>x.IsDeleted == false && x.DocumentTypeId == Data.DocumentType.CompanyCertificate).Select(x=>x.Attachment).FirstOrDefault().ToString();
                        model.Aadhar = Constants.BASE_URL + "FileUpload/Aadhar/" + model.UserDocument.Where(x => x.IsDeleted == false && x.DocumentTypeId == Data.DocumentType.Aadhar).Select(x => x.Attachment).FirstOrDefault().ToString();
                        model.PAN = Constants.BASE_URL + "FileUpload/Pan/" + model.UserDocument.Where(x => x.IsDeleted == false && x.DocumentTypeId == Data.DocumentType.Pan).Select(x => x.Attachment).FirstOrDefault().ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["msg"] = ex.Message;
            }
            return View(model);
        }
        public ActionResult JobSeekerProfile(string Id)
        {
            UserDTO model = new UserDTO();
            try
            {
                model.Id = Convert.ToInt32(Id);
                model.User = _userRepository.GetDetailById(model.Id);
                if (model.User != null)
                {
                    model.UserDetail = model.User.UserDetails.First();
                    model.ListEducation = model.User.UserEducations.Where(x => x.IsDeleted == false).ToList();
                    model.LstSkills = model.User.UserSkills.Where(x => x.IsDeleted == false).ToList();
                    model.ListExperience = model.User.UserExperiences.Where(x => x.IsDeleted == false).ToList();
                    model.UserName = model.User.UserName;
                    if (model.UserDetail != null)
                    {
                        model.FirstName = model.UserDetail.FirstName;
                        model.LastName = model.UserDetail.LastName;
                        model.DOB = Convert.ToDateTime(model.UserDetail.DOB);
                        model.Mobile = model.UserDetail.Mobile;
                        model.Email = model.UserDetail.Email;
                        model.State = model.UserDetail.State;
                        model.Address = model.UserDetail.Address;
                        model.Gender = model.UserDetail.Gender;
                        model.FatherName = model.UserDetail.FatherName;
                        model.About = model.UserDetail.About;
                        model.Photo = model.UserDetail.Photo != null ? Constants.BASE_URL + "FileUpload/ProfilePhoto/" + model.UserDetail.Photo : "";
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["msg"] = ex.Message;
            }
            return View(model);
        }
        public ActionResult AvailableJobs()
        {
            JobDTOWeb model = new JobDTOWeb();
            int i = 0;
            try
            {
                model.lst = _jobRepository.GetAvailableJobs();
            }
            catch (Exception ex)
            {
                TempData["msg"] = ex.Message;
            }
            return View(model);
        }
        public ActionResult JobDetails(string Id)
        {
            JOBDetailDTOWeb model = new JOBDetailDTOWeb();
            JobDTOWeb m = new JobDTOWeb();
            JobSearchFilterDTOWeb req = new JobSearchFilterDTOWeb();
            model.Id = Convert.ToInt32(Id);
            model.Language = Language.English;
            req.Language = model.Language;
            try
            {
                m.job = _jobRepository.GetCustomByIdWeb(model);
                if (m.job != null)
                {
                    m.job.Time = Common.TimeLeft(Convert.ToDateTime(m.job.PostedDate));
                    m.Skills = m.job.Skill;
                }
                req.City = m.job.City;
                m.JobResponse = _jobRepository.SearchJobWeb(req);
            }
            catch (Exception ex)
            {
                TempData["msg"] = ex.Message;
            }
            return View(m);
        }
        //[HttpPost]
        //[ValidateAntiForgeryToken()]
        //public ActionResult AvailableJobs(EmpJobPostDTO model)
        //{

        //}
        public ActionResult PlacementDetails()
        {
            UserJobsDTO model = new UserJobsDTO();
            ViewBag.JobRole = _jobRoleRepository.GetAll(x => x.IsDeleted == false);
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult PlacementDetails(UserJobsDTO model)
        {
            ViewBag.JobRole = _jobRoleRepository.GetAll(x => x.IsDeleted == false);
            try
            {
                model.lst = _userJobRepository.GetPlacementDetails(model);
            }
            catch (Exception ex)
            {
                TempData["msg"] = ex.Message;
            }
            return View(model);
        }
        public ActionResult LocationWisePlacement()
        {
            UserJobsDTO model = new UserJobsDTO();
            ViewBag.City = _cityRepository.GetAll(x => x.IsDeleted == false);
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult LocationWisePlacement(UserJobsDTO model)
        {
            ViewBag.City = _cityRepository.GetAll(x => x.IsDeleted == false);
            try
            {
                model.lst = _userJobRepository.LocationWisePlacement(model);
            }
            catch (Exception ex)
            {
                TempData["msg"] = ex.Message;
            }
            return View(model);
        }
        public ActionResult EmployerWisePlacement()
        {
            EmployerPlacementDTO model = new EmployerPlacementDTO();
            ViewBag.Employee = _userRepository.GetAll(x => x.RoleId == Role.Employer && x.IsDeleted == false).Select(x => new EmployerPlacementDTO
            {
                Name = x.UserDetails.Select(i => i.FirstName).FirstOrDefault(),
                EmployerId = x.Id
            });
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult EmployerWisePlacement(EmployerPlacementDTO model)
        {
            ViewBag.Employee = _userRepository.GetAll(x => x.RoleId == Role.Employer && x.IsDeleted == false).Select(x => new EmployerPlacementDTO
            {
                Name = x.UserDetails.Select(i => i.FirstName).FirstOrDefault(),
                EmployerId = x.Id
            });
            try
            {
                model.lst = _userRepository.GetEmployerWisePlacement(model.EmployerId);
                if (model.lst != null)
                {
                }
            }
            catch (Exception ex)
            {
                TempData["msg"] = ex.Message;
            }
            return View(model);
        }
        public ActionResult EnrollmentProgram(string Id)
        {
            EnrollmentProgramDTO model = new EnrollmentProgramDTO();
            try
            {
                if (Id != null)
                {
                    model.Id = Convert.ToInt32(Id);
                    model.enProgram = _enrollmentProgramRepository.GetDetailById(model.Id);
                    if (model.enProgram != null)
                    {
                        model.Title = model.enProgram.Title;
                        model.TitleH = model.enProgram.TitleH;
                        model.Description = model.enProgram.Description;
                        model.DescriptionH = model.enProgram.DescriptionH;
                        model.DateStart = Convert.ToDateTime(model.enProgram.DateStart).Date;
                        model.DateEnd = Convert.ToDateTime(model.enProgram.DateEnd).Date;
                        model.PublishDate = Convert.ToDateTime(model.enProgram.PublishDate).Date;
                        model.LastDate = Convert.ToDateTime(model.enProgram.LastDate).Date;
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
        [ValidateAntiForgeryToken]
        public ActionResult EnrollmentProgram(UpdateEnrollmentProgramDTO model)
        {
            bool msg;
            try
            {
                if (ModelState.IsValid)
                {
                    Random rnd = new Random();
                    if (model.postedImage != null)
                    {
                        string imageFile = "img_" + rnd.Next(000, 999) + model.postedImage.FileName;
                        string path = Server.MapPath("~/FileUpload/Other/");
                        model.postedImage.SaveAs(path + imageFile);
                        model.Image = imageFile;
                    }
                    if (model.Id != 0)
                    {
                        msg = _enrollmentProgramRepository.Update(model);
                        if (msg == true)
                        {
                            TempData["msg"] = "Updated";
                        }
                    }
                    else
                    {
                        msg = _enrollmentProgramRepository.Add(model);
                        if (msg == true)
                        {
                            TempData["msg"] = "Saved";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["msg"] = ex.Message;
            }
            return RedirectToAction("EnrollmentProgram");
        }
        public ActionResult EnrollmentProgramList(string Id)
        {
            EnrollmentProgramDTO model = new EnrollmentProgramDTO();
            try
            {
                model.lst = _enrollmentProgramRepository.GetAll();
                if (model.lst.Count > 0)
                {

                }
            }
            catch (Exception ex)
            {
                TempData["msg"] = ex.Message;
            }
            return View(model);
        }
        public ActionResult DeleteEnrollmentProgram(string Id)
        {
            bool msg;
            int UserId = 1;
            msg = _enrollmentProgramRepository.Delete(Convert.ToInt32(Id), UserId);
            if (msg == true)
            {
                TempData["msg"] = "Deleted";
            }
            return RedirectToAction("EnrollmentProgramList");
        }
        public ActionResult TrainingMaterial(string Id)
        {
            TrainingMaterialDTO model = new TrainingMaterialDTO();
            try
            {
                if (Id != null)
                {
                    model.Id = Convert.ToInt32(Id);
                    model.trMaterial = _trainingMaterialRepository.GetDetailById(model.Id);
                    if (model.trMaterial != null)
                    {
                        model.Title = model.trMaterial.Title;
                        model.TitleH = model.trMaterial.TitleH;
                        model.ShortDescription = model.trMaterial.ShortDescription;
                        model.ShortDescriptionH = model.trMaterial.ShortDescriptionH;
                        model.Description = model.trMaterial.Description;
                        model.DescriptionH = model.trMaterial.DescriptionH;
                        model.Link = model.trMaterial.Link;
                        model.Type = model.trMaterial.Type;
                        model.PublishDate = Convert.ToDateTime(model.trMaterial.PublishDate).Date;
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
        public ActionResult TrainingMaterial(UpdateTrainingMaterialDTO model)
        {
            bool msg;
            try
            {
                Random rnd = new Random();
                if (model.postedImage != null)
                {
                    string imageFile = "img_" + rnd.Next(000, 999) + model.postedImage.FileName;
                    string path = Server.MapPath("~/FileUpload/Other/");
                    model.postedImage.SaveAs(path + imageFile);
                    model.Image = imageFile;
                }
                if (model.postedFile != null)
                {
                    string attachment = "attach_" + rnd.Next(000, 999) + model.postedFile.FileName;
                    string path = Server.MapPath("~/FileUpload/Other/");
                    model.postedFile.SaveAs(path + attachment);
                    model.Attachment = attachment;
                }
                if (model.Id != 0)
                {

                    msg = _trainingMaterialRepository.Update(model);
                    if (msg == true)
                    {
                        TempData["msg"] = "Updated";
                    }
                }
                else
                {
                    msg = _trainingMaterialRepository.Add(model);
                    if (msg == true)
                    {
                        TempData["msg"] = "Saved";
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["msg"] = ex.Message;
            }
            return RedirectToAction("TrainingMaterial");
        }
        public ActionResult TrainingMaterialList()
        {
            TrainingMaterialDTO model = new TrainingMaterialDTO();
            try
            {
                int i = 0;
                model.lst = _trainingMaterialRepository.GetAll(x => x.IsDeleted == false).ToList();
            }
            catch (Exception ex)
            {
                TempData["msg"] = ex.Message;
            }
            return View(model);
        }

        public ActionResult DeleteTrainingMaterial(string Id)
        {
            bool msg;
            int UserId = 1;
            msg = _trainingMaterialRepository.Delete(Convert.ToInt32(Id), UserId);
            if (msg == true)
            {
                TempData["msg"] = "Deleted";
            }
            return RedirectToAction("TrainingMaterialList");
        }
        public ActionResult ManageNotification()
        {
            NotificationDTO model = new NotificationDTO();
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult ManageNotification(NotificationDTO model)
        {
            try
            {
                string[] fcmId = { "" };
                bool msg = false;
                var resp = "";
                model.NotificationType = 1;
                if (ModelState.IsValid)
                {
                    if (model.NotifiedDevice != null && model.NotifiedDevice != "")
                    {
                        model.lstNotification = _userRepository.GetFCMIdofUsers(model.MobileNo);
                    }
                    if (model.lstNotification != null && model.lstNotification.Count > 0)
                    {
                        Random rnd = new Random();
                        if (model.postedImage != null)
                        {
                            string imageFile = "img_" + rnd.Next(000, 999) + model.postedImage.FileName;
                            string path = Server.MapPath("~/FileUpload/Other/");
                            model.postedImage.SaveAs(path + imageFile);
                            model.Image = imageFile;
                        }
                        fcmId = model.lstNotification.Select(x => x.FCMId).ToArray();
                        resp = new BLFCM().SendMessage(fcmId, model.Title, model.Description);
                        if (resp != null && resp != "")
                        {
                            model.Status = "Sent";
                            msg = _notificationRepository.Add(model);
                            if (msg == true)
                            {
                                TempData["msg"] = "Saved";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["msg"] = "Some Error Occurred !";
            }
            return RedirectToAction("ManageNotification");
        }
        public ActionResult AdminProfile()
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

        public ActionResult JobSeekerList()
        {
            UserListDTO model = new UserListDTO();
            try
            {
                model.Users = _userRepository.GetAll(x => x.RoleId == Role.User && x.IsDeleted == false).ToList();
            }
            catch (Exception ex)
            {
                TempData["msg"] = ex.Message;
            }
            return View(model);
        }
        [HttpPost]
        public ActionResult JobSeekerList(UserListDTO model)
        {
            try
            {
                if (model.FromDate != null && model.ToDate != null)
                {
                    model.Users = _userRepository.GetAll(x => x.RoleId == Role.User && x.IsDeleted == false && x.CreatedAt >= Convert.ToDateTime(model.FromDate) && x.CreatedAt <= Convert.ToDateTime(model.ToDate)).ToList();
                }
            }
            catch (Exception ex)
            {
                TempData["msg"] = ex.Message;
            }
            return View(model);
        }
        public ActionResult Test()
        {
            return View();
        }

        public ActionResult Enc(string txt)
        {
            return Json(Security.EncryptString(Constants.EncKey, txt.Replace("\\", @"\")), JsonRequestBehavior.AllowGet);
        }
        public ActionResult Dec(string txt)
        {
            return Json(Security.DecryptString(Constants.EncKey, txt), JsonRequestBehavior.AllowGet);
        }
        public ActionResult EmailPanel(EmailMasterDTO model)
        {
            //EmailMasterDTO model = new EmailMasterDTO();
            List<SelectListItem> User = new List<SelectListItem>();
            User.Add(new SelectListItem { Text = "Select User", Value = "0" });
            User.Add(new SelectListItem { Text = "All Employers", Value = "1" });
            User.Add(new SelectListItem { Text = "All Job Seekers", Value = "2" });
            User.Add(new SelectListItem { Text = "Other/Specific", Value = "3" });
            ViewBag.User = User;
            model.TemplateList = _emailmasterRepository.GetAll();
            if (model.TemplateList.Count > 0)
            {
                // TempData["msg"] = "Record found";
            }
            return View(model);
        }
        public ActionResult SendEmail(string Email, string Subject, string EmailMessage, bool IsTemplate)
        {
            EmailMasterDTO model = new EmailMasterDTO();
            bool msg;
            try
            {
                if (ModelState.IsValid)
                {
                    model.Email = Email;
                    model.Subject = Subject;
                    model.Body = EmailMessage;
                    model.IsTemplate = IsTemplate;
                    msg = _emailmasterRepository.Add(model);
                    if (msg == true)
                    {
                        string[] arr = model.Email.Split(',');
                        int h = arr.Length - 1;
                        string mailbody = "";
                        for (int i = 0; i < h; i++)
                        {
                            Email = arr[i];
                            mailbody = model.Body;
                            BLMail.SendMail(Email, Subject, mailbody, IsTemplate);
                            if (msg == true)
                            {
                                TempData["msg"] = Constants.MAIL_SENT_SUCCESSFUL_MESSAGE;
                            }

                        }
                        TempData["msg"] = "Saved";
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["msg"] = ex.Message;
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetUserList(string UserId)
        {

            UserDTO model = new UserDTO();
            model.Id = Convert.ToInt32(UserId);
            #region Users
            if (model.Id == 1)
            {
                model.lstUsers = _userRepository.GetAll(x => x.IsDeleted == false && x.RoleId == Role.Employer).Select(x => new UserListForEmailDTO
                {
                    Email = x.UserDetails.Select(xx => xx.Email).FirstOrDefault(),
                    Name = x.UserDetails.Select(xx => xx.FirstName).FirstOrDefault() + " " + x.UserDetails.Select(xx => xx.LastName).FirstOrDefault(),
                    UserId = x.UserDetails.Select(xx => xx.UserId).FirstOrDefault(),
                    Mobile = x.UserDetails.Select(xx => xx.Mobile).FirstOrDefault()
                }).Where(xy => xy.Email != "" && xy.Email != null).ToList();
            }
            else if ((model.Id == 2))
            {
                model.lstUsers = _userRepository.GetAll(x => x.IsDeleted == false && x.RoleId == Role.User).Select(x => new UserListForEmailDTO
                {
                    Email = x.UserDetails.Select(xx => xx.Email).FirstOrDefault(),
                    Name = x.UserDetails.Select(xx => xx.FirstName).FirstOrDefault() + " " + x.UserDetails.Select(xx => xx.LastName).LastOrDefault(),
                    Mobile = x.UserDetails.Select(xx => xx.Mobile).FirstOrDefault(),
                    UserId = x.UserDetails.Select(xx => xx.UserId).FirstOrDefault()
                }).Where(xy => xy.Email != "" && xy.Email != null).ToList();
            }
            if (model.lstUsers != null)
            {
                List<UserListForEmailDTO> Users = new List<UserListForEmailDTO>();
                foreach (var item in model.lstUsers)
                {
                    UserListForEmailDTO d = new UserListForEmailDTO();
                    d.UserId = item.UserId;
                    d.Name = item.Name;
                    d.Email = item.Email;
                    d.Mobile = item.Mobile;
                    Users.Add(d);
                }
                model.lstUsers = Users;


            }

            #endregion

            return Json(model, JsonRequestBehavior.AllowGet);
        }
        public ActionResult SmsPanel(SmsMasterDTO model)
        {
            //EmailMasterDTO model = new EmailMasterDTO();
            List<SelectListItem> User = new List<SelectListItem>();
            User.Add(new SelectListItem { Text = "Select User", Value = "0" });
            User.Add(new SelectListItem { Text = "All Employers", Value = "1" });
            User.Add(new SelectListItem { Text = "All Job Seekers", Value = "2" });
            User.Add(new SelectListItem { Text = "Other/Specific", Value = "3" });
            ViewBag.User = User;
            model.OldSmsList = _smsmasterRepository.GetAll();
            if (model.OldSmsList.Count > 0)
            {
                // TempData["msg"] = "Record found";
            }
            return View(model);
        }
        public ActionResult SendSMS(string Mobile, string SMS, bool IsTemplate)
        {
            SmsMasterDTO model = new SmsMasterDTO();
            bool msg;
            try
            {
                if (ModelState.IsValid)
                {
                    model.Mobile = Mobile;
                    model.SMS = SMS;
                    model.IsTemplate = IsTemplate;
                    msg = _smsmasterRepository.Add(model);
                    if (msg == true)
                    {
                        string[] arr = model.Mobile.Split(',');
                        int h = arr.Length - 1;
                        for (int i = 0; i < h; i++)
                        {
                            Mobile = arr[i];
                            SMS = model.SMS;
                            string TempId=
                            BLSMS.SendSMS(Mobile, SMS,"");
                            if (msg == true)
                            {
                                TempData["msg"] = Constants.SMS_SENT_SUCCESSFUL_MESSAGE;
                            }

                        }
                    }
                    TempData["msg"] = "Saved";
                }
            }
            catch (Exception ex)
            {
                TempData["msg"] = ex.Message;
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        public ActionResult AuditTrail(AudittrailDTO model)
        {
            try
            {
                model.lst = _audittrailRepository.GetAll();
                if (model.lst.Count > 0)
                {
                    //TempData["msg"] = "Record Found";
                }

            }
            catch (Exception ex)
            {
                TempData["msg"] = ex.Message;
            }
            return View(model);
        }
        public ActionResult ContactUsList(ContactListDTO model)
        {
            try
            {
                model.lst = _feedbackRepository.GetAll(x => x.IsDeleted == false).ToList();
            }
            catch (Exception ex)
            {
                TempData["msg"] = ex.Message;
            }
            return View(model);
        }
    }
}
