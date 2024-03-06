using Data.DTOs;
using Data.Interfaces.Repositories;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Utility;
using Utility.Enums;

namespace Webapp.Controllers
{
    public class WebAPIController : ApiController
    {
        private readonly IJobRepository _jobRepository;
        private readonly INotificationRepository _notificationRepository;
        private readonly IUserJobRepository _userJobRepository;
        private readonly IUserRepository _userRepository;
        private readonly ISkillRepository _skillRepository;
        private readonly ICourseRepository _courseRepository;
        private readonly IPincodeRepository _pincodeRepository;
        private readonly ICityRepository _cityRepository;
        private readonly ITrainingMaterialRepository _trainingMaterialRepository;
        private readonly IChatRepository _chatRepository;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IFeedbackRepository _feedbackRepository;
        private readonly IEnrollmentProgramRepository _enrollmentProgramRepository;

        public WebAPIController(IJobRepository jobRepository, INotificationRepository notificationRepository, IUserJobRepository userJobRepository, IUserRepository userRepository, ISkillRepository skillRepository, ICourseRepository courseRepository, IPincodeRepository pincodeRepository, ICityRepository cityRepository, ITrainingMaterialRepository trainingMaterialRepository, IChatRepository chatRepository, IDepartmentRepository departmentRepository, IFeedbackRepository feedbackRepository, IEnrollmentProgramRepository enrollmentProgramRepository)
        {
            this._jobRepository = jobRepository;
            this._notificationRepository = notificationRepository;
            this._userJobRepository = userJobRepository;
            this._userRepository = userRepository;
            this._skillRepository = skillRepository;
            this._courseRepository = courseRepository;
            this._pincodeRepository = pincodeRepository;
            this._cityRepository = cityRepository;
            this._trainingMaterialRepository = trainingMaterialRepository;
            this._chatRepository = chatRepository;
            this._departmentRepository = departmentRepository;
            this._feedbackRepository = feedbackRepository;
            this._enrollmentProgramRepository = enrollmentProgramRepository;
        }

        [HttpPost]
        public HttpResponseMessage CheckMobileNo(Request req)
        {
            Response resp = new Response();
            CheckMobileNoResponseDTO model = new CheckMobileNoResponseDTO();
            try
            {
                CheckMobileNoDTO para = new CheckMobileNoDTO();
                string dcData = Security.DecryptString(Constants.EncKey, req.Body);
                para = JsonConvert.DeserializeObject<CheckMobileNoDTO>(dcData);

                para.DeviceType = DeviceType.Mobile;
                var status = _userRepository.CheckMobileNo(para);
                if (status == 1)
                {
                    model.IsExist = true;
                    resp.Body = Security.EncryptString(Constants.EncKey, new Security().SerializeObject<CheckMobileNoResponseDTO>(model));
                    resp.StatusCode = HttpStatusCode.OK;
                    resp.Message = Constants.HTTPSTATUS_SUCCESS;
                }
                else if (status == 0)
                {
                    model.IsExist = false;
                    resp.Body = Security.EncryptString(Constants.EncKey, new Security().SerializeObject<CheckMobileNoResponseDTO>(model));
                    resp.StatusCode = HttpStatusCode.OK;
                    resp.Message = Constants.HTTPSTATUS_SUCCESS;
                }
                else
                {
                    resp.StatusCode = HttpStatusCode.InternalServerError;
                    resp.Message = Constants.HTTPSTATUS_ERROR_OCCURED;
                }
            }
            catch (Exception ex)
            {
                resp.StatusCode = HttpStatusCode.InternalServerError;
                resp.Message = ex.Message;
            }
            return Request.CreateResponse(HttpStatusCode.OK,
                    resp);
        }

        [HttpPost]
        public HttpResponseMessage GenerateOTP(Request req)
        {
            Response resp = new Response();
            try
            {
                GenerateOTPDTO para = new GenerateOTPDTO();
                string dcData = Security.DecryptString(Constants.EncKey, req.Body);
                para = JsonConvert.DeserializeObject<GenerateOTPDTO>(dcData);

                para.DeviceType = DeviceType.Mobile;
                var status = _userRepository.GenerateOTPMobile(para);
                if (status == true)
                {
                    resp.StatusCode = HttpStatusCode.OK;
                    resp.Message = Constants.HTTPSTATUS_OTP_GENERATED;
                }
                else
                {
                    resp.StatusCode = HttpStatusCode.NotFound;
                    resp.Message = Constants.HTTPSTATUS_OTP_GENERATION_FAILED;
                }
            }
            catch (Exception ex)
            {
                resp.StatusCode = HttpStatusCode.InternalServerError;
                resp.Message = ex.Message;
            }
            return Request.CreateResponse(HttpStatusCode.OK,
                    resp);
        }

        [HttpPost]
        public HttpResponseMessage ValidateOTP(Request req)
        {
            Response resp = new Response();
            try
            {
                ValidateOTPDTO para = new ValidateOTPDTO();
                string dcData = Security.DecryptString(Constants.EncKey, req.Body);
                para = JsonConvert.DeserializeObject<ValidateOTPDTO>(dcData);

                para.DeviceType = DeviceType.Mobile;
                var status = _userRepository.ValidateOTPMobile(para);
                if (status == 1)
                {
                    resp.StatusCode = HttpStatusCode.OK;
                    resp.Message = Constants.HTTPSTATUS_OTP_VALIDATION_SUCCESS;
                }
                else if (status == -1)
                {
                    resp.StatusCode = HttpStatusCode.NotFound;
                    resp.Message = Constants.HTTPSTATUS_OTP_EXPIRED;
                }
                else
                {
                    resp.StatusCode = HttpStatusCode.NotFound;
                    resp.Message = Constants.HTTPSTATUS_OTP_VALIDATION_FAILED;
                }
            }
            catch (Exception ex)
            {
                resp.StatusCode = HttpStatusCode.InternalServerError;
                resp.Message = ex.Message;
            }
            return Request.CreateResponse(HttpStatusCode.OK,
                    resp);
        }

        [HttpPost]
        public HttpResponseMessage Register(Request req)
        {
            Response resp = new Response();
            UserDetailResponseDTO model = new UserDetailResponseDTO();
            try
            {
                UserRegistrationDTO para = new UserRegistrationDTO();
                string dcData = Security.DecryptString(Constants.EncKey, req.Body);
                para = JsonConvert.DeserializeObject<UserRegistrationDTO>(dcData);

                para.DeviceType = DeviceType.Mobile;
                model = _userRepository.RegisterUserMobile(para);
                if (model != null)
                {
                    resp.Body = Security.EncryptString(Constants.EncKey, new Security().SerializeObject<UserDetailResponseDTO>(model));
                    resp.StatusCode = HttpStatusCode.OK;
                    resp.Message = Constants.HTTPSTATUS_REGISTRATION_SUCCESSFUL;
                }
                else
                {
                    resp.StatusCode = HttpStatusCode.InternalServerError;
                    resp.Message = Constants.HTTPSTATUS_ERROR_OCCURED;
                }
            }
            catch (Exception ex)
            {
                resp.StatusCode = HttpStatusCode.InternalServerError;
                resp.Message = ex.Message;
            }
            return Request.CreateResponse(HttpStatusCode.OK,
                    resp);
        }

        [HttpPost]
        public HttpResponseMessage Login(Request req)
        {
            Response resp = new Response();
            UserDetailResponseDTO model = new UserDetailResponseDTO();
            try
            {
                UserLoginMobileDTO para = new UserLoginMobileDTO();
                string dcData = Security.DecryptString(Constants.EncKey, req.Body);
                para = JsonConvert.DeserializeObject<UserLoginMobileDTO>(dcData);
                para.DeviceType = DeviceType.Mobile;
                LoginResponse r;
                model = _userRepository.LoginMobile(para, out r);
                if (model != null)
                {
                    resp.Body = Security.EncryptString(Constants.EncKey, new Security().SerializeObject<UserDetailResponseDTO>(model));
                    resp.StatusCode = HttpStatusCode.OK;
                    resp.Message = Constants.HTTPSTATUS_LOGIN_SUCCESSFUL;
                }
                else
                {
                    if (LoginResponse.InvalidPassword.Equals(r))
                    {
                        resp.StatusCode = HttpStatusCode.Unauthorized;
                        resp.Message = Constants.HTTPSTATUS_INVALID_PASSWORD;
                    }
                    else if (LoginResponse.InvalidUser.Equals(r))
                    {
                        resp.StatusCode = HttpStatusCode.Unauthorized;
                        resp.Message = Constants.HTTPSTATUS_INVALID_USER;
                    }
                    else if (LoginResponse.NotVerified.Equals(r))
                    {
                        resp.StatusCode = HttpStatusCode.NotAcceptable;
                        resp.Message = Constants.HTTPSTATUS_USER_NOT_VERIFIED;
                    }
                    else if (LoginResponse.Error.Equals(r))
                    {
                        resp.StatusCode = HttpStatusCode.InternalServerError;
                        resp.Message = Constants.HTTPSTATUS_ERROR_OCCURED;
                    }
                }
            }
            catch (Exception ex)
            {
                resp.StatusCode = HttpStatusCode.InternalServerError;
                resp.Message = ex.Message;
            }
            return Request.CreateResponse(HttpStatusCode.OK,
                    resp);
        }

        //check login to manage block/unblock by administrator
        [HttpPost]
        public HttpResponseMessage CheckLogIn(Request req)
        {
            Response resp = new Response();
            UserDetailResponseDTO model = new UserDetailResponseDTO();
            try
            {
                UserLogDTO para = new UserLogDTO();
                string dcData = Security.DecryptString(Constants.EncKey, req.Body);
                para = JsonConvert.DeserializeObject<UserLogDTO>(dcData);
                para.DeviceType = DeviceType.Mobile;
                LoginResponse r;
                model = _userRepository.CheckLoginMobile(para, out r);
                if (model != null)
                {
                    resp.Body = Security.EncryptString(Constants.EncKey, new Security().SerializeObject<UserDetailResponseDTO>(model));
                    resp.StatusCode = HttpStatusCode.OK;
                    resp.Message = Constants.HTTPSTATUS_REGISTRATION_SUCCESSFUL;
                }
                else
                {
                    if (LoginResponse.InvalidDeviceId.Equals(r))
                    {
                        resp.StatusCode = HttpStatusCode.Unauthorized;
                        resp.Message = Constants.HTTPSTATUS_INVALID_PASSWORD;
                    }
                    else if (LoginResponse.Blocked.Equals(r))
                    {
                        resp.StatusCode = HttpStatusCode.Gone; //block code
                        resp.Message = Constants.HTTPSTATUS_BLOCKED_USER;
                    }
                    else if (LoginResponse.Error.Equals(r))
                    {
                        resp.StatusCode = HttpStatusCode.InternalServerError;
                        resp.Message = Constants.HTTPSTATUS_ERROR_OCCURED;
                    }
                }
            }
            catch (Exception ex)
            {
                resp.StatusCode = HttpStatusCode.InternalServerError;
                resp.Message = ex.Message;
            }
            return Request.CreateResponse(HttpStatusCode.OK,
                    resp);
        }

        [HttpPost]
        public HttpResponseMessage ForgetPassword(Request req)
        {
            Response resp = new Response();
            try
            {
                ForgetPasswordDTO para = new ForgetPasswordDTO();
                string dcData = Security.DecryptString(Constants.EncKey, req.Body);
                para = JsonConvert.DeserializeObject<ForgetPasswordDTO>(dcData);

                para.DeviceType = DeviceType.Mobile;
                var model = _userRepository.ForgetPasswordMobile(para);
                if (model == true)
                {
                    resp.StatusCode = HttpStatusCode.OK;
                    resp.Message = Constants.HTTPSTATUS_FORGET_PASSWORD_SUCCESS;
                }
                else
                {
                    resp.StatusCode = HttpStatusCode.NotFound;
                    resp.Message = Constants.HTTPSTATUS_RECORD_NOT_FOUND;
                }
            }
            catch (Exception ex)
            {
                resp.StatusCode = HttpStatusCode.InternalServerError;
                resp.Message = ex.Message;
            }
            return Request.CreateResponse(HttpStatusCode.OK,
                    resp);
        }

        [HttpPost]
        public HttpResponseMessage ResetPassword(Request req)
        {
            Response resp = new Response();
            try
            {
                ResetPasswordDTO para = new ResetPasswordDTO();
                string dcData = Security.DecryptString(Constants.EncKey, req.Body);
                para = JsonConvert.DeserializeObject<ResetPasswordDTO>(dcData);

                para.DeviceType = DeviceType.Mobile;
                var model = _userRepository.ResetPasswordMobile(para);
                if (model == 1)
                {
                    resp.StatusCode = HttpStatusCode.OK;
                    resp.Message = Constants.HTTPSTATUS_RESET_PASSWORD_SUCCESS;
                }
                else if (model == 2)
                {
                    resp.StatusCode = HttpStatusCode.GatewayTimeout;
                    resp.Message = Constants.HTTPSTATUS_RESET_PASSWORD_EXPIRED;
                }
                else
                {
                    resp.StatusCode = HttpStatusCode.NotFound;
                    resp.Message = Constants.HTTPSTATUS_RECORD_NOT_FOUND;
                }
            }
            catch (Exception ex)
            {
                resp.StatusCode = HttpStatusCode.InternalServerError;
                resp.Message = ex.Message;
            }
            return Request.CreateResponse(HttpStatusCode.OK,
                    resp);
        }

        [HttpPost]
        public HttpResponseMessage ChangePassword(Request req)
        {
            Response resp = new Response();
            try
            {
                ChangePasswordMobileDTO para = new ChangePasswordMobileDTO();
                string dcData = Security.DecryptString(Constants.EncKey, req.Body);
                para = JsonConvert.DeserializeObject<ChangePasswordMobileDTO>(dcData);

                para.DeviceType = DeviceType.Mobile;
                var model = _userRepository.ChangePasswordMobile(para);
                if (model == true)
                {
                    resp.StatusCode = HttpStatusCode.OK;
                    resp.Message = Constants.HTTPSTATUS_CHANGE_PASSWORD_SUCCESS;
                }
                else
                {
                    resp.StatusCode = HttpStatusCode.NotFound;
                    resp.Message = Constants.HTTPSTATUS_RECORD_NOT_FOUND;
                }
            }
            catch (Exception ex)
            {
                resp.StatusCode = HttpStatusCode.InternalServerError;
                resp.Message = ex.Message;
            }
            return Request.CreateResponse(HttpStatusCode.OK,
                    resp);
        }

        #region Update profile
        [HttpPost]
        public HttpResponseMessage UpdateProfilePersonalDetail(Request req)
        {
            Response resp = new Response();
            try
            {
                UPdateProfilePersonalDetailDTO para = new UPdateProfilePersonalDetailDTO();
                string dcData = Security.DecryptString(Constants.EncKey, req.Body);
                para = JsonConvert.DeserializeObject<UPdateProfilePersonalDetailDTO>(dcData);

                para.DeviceType = DeviceType.Mobile;
                var status = _userRepository.UpdateProfilePersonalDetailMobile(para);
                if (status == true)
                {
                    resp.StatusCode = HttpStatusCode.OK;
                    resp.Message = Constants.HTTPSTATUS_PROFILE_UPDATE_SUCCESS;
                }
                else
                {
                    resp.StatusCode = HttpStatusCode.InternalServerError;
                    resp.Message = Constants.HTTPSTATUS_ERROR_OCCURED;
                }
            }
            catch (Exception ex)
            {
                resp.StatusCode = HttpStatusCode.InternalServerError;
                resp.Message = ex.Message;
            }
            return Request.CreateResponse(HttpStatusCode.OK,
                    resp);
        }

        [HttpPost]
        public HttpResponseMessage UpdateProfileCompanyDetail(Request req)
        {
            Response resp = new Response();
            try
            {
                UPdateProfileCompanyDetailDTO para = new UPdateProfileCompanyDetailDTO();
                string dcData = Security.DecryptString(Constants.EncKey, req.Body);
                para = JsonConvert.DeserializeObject<UPdateProfileCompanyDetailDTO>(dcData);

                para.DeviceType = DeviceType.Mobile;
                var status = _userRepository.UpdateProfileCompanyDetailMobile(para);
                if (status == true)
                {
                    resp.StatusCode = HttpStatusCode.OK;
                    resp.Message = Constants.HTTPSTATUS_PROFILE_UPDATE_SUCCESS;
                }
                else
                {
                    resp.StatusCode = HttpStatusCode.InternalServerError;
                    resp.Message = Constants.HTTPSTATUS_ERROR_OCCURED;
                }
            }
            catch (Exception ex)
            {
                resp.StatusCode = HttpStatusCode.InternalServerError;
                resp.Message = ex.Message;
            }
            return Request.CreateResponse(HttpStatusCode.OK,
                    resp);
        }

        [HttpPost]
        public HttpResponseMessage UpdateProfileAbout(Request req)
        {
            Response resp = new Response();
            try
            {
                UPdateProfileAboutDTO para = new UPdateProfileAboutDTO();
                string dcData = Security.DecryptString(Constants.EncKey, req.Body);
                para = JsonConvert.DeserializeObject<UPdateProfileAboutDTO>(dcData);

                para.DeviceType = DeviceType.Mobile;
                var status = _userRepository.UpdateProfileAboutMobile(para);
                if (status == true)
                {
                    resp.StatusCode = HttpStatusCode.OK;
                    resp.Message = Constants.HTTPSTATUS_PROFILE_UPDATE_SUCCESS;
                }
                else
                {
                    resp.StatusCode = HttpStatusCode.InternalServerError;
                    resp.Message = Constants.HTTPSTATUS_ERROR_OCCURED;
                }
            }
            catch (Exception ex)
            {
                resp.StatusCode = HttpStatusCode.InternalServerError;
                resp.Message = ex.Message;
            }
            return Request.CreateResponse(HttpStatusCode.OK,
                    resp);
        }

        [HttpPost]
        public HttpResponseMessage GetSkills(Request req)
        {
            Response resp = new Response();
            SkillsResponseDTO model = new SkillsResponseDTO();
            try
            {
                LanguageFilterDTO para = new LanguageFilterDTO();
                string dcData = Security.DecryptString(Constants.EncKey, req.Body);
                para = JsonConvert.DeserializeObject<LanguageFilterDTO>(dcData);

                model.Skills = _skillRepository.GetSkillsMobile(para);
                if (model != null && model.Skills != null && model.Skills.Count > 0)
                {
                    resp.Body = Security.EncryptString(Constants.EncKey, new Security().SerializeObject<SkillsResponseDTO>(model));
                    resp.StatusCode = HttpStatusCode.OK;

                    resp.Message = Language.English == para.Language ? Constants.HTTPSTATUS_SUCCESS : Constants.HTTPSTATUS_SUCCESS_HI;
                }
                else
                {
                    resp.StatusCode = HttpStatusCode.NotFound;
                    resp.Message = Language.English == para.Language ? Constants.HTTPSTATUS_RECORD_NOT_FOUND : Constants.HTTPSTATUS_RECORD_NOT_FOUND_HI;
                }
            }
            catch (Exception ex)
            {
                resp.StatusCode = HttpStatusCode.InternalServerError;
                resp.Message = ex.Message;
            }
            return Request.CreateResponse(HttpStatusCode.OK,
                    resp);
        }

        [HttpPost]
        public HttpResponseMessage UpdateProfileSkills(Request req)
        {
            Response resp = new Response();
            try
            {
                UpdateProfileSkillsDTO para = new UpdateProfileSkillsDTO();
                string dcData = Security.DecryptString(Constants.EncKey, req.Body);
                para = JsonConvert.DeserializeObject<UpdateProfileSkillsDTO>(dcData);

                para.DeviceType = DeviceType.Mobile;
                var status = _userRepository.UpdateProfileSkillsMobile(para);
                if (status == true)
                {
                    resp.StatusCode = HttpStatusCode.OK;
                    resp.Message = Constants.HTTPSTATUS_SUCCESS;
                }
                else
                {
                    resp.StatusCode = HttpStatusCode.InternalServerError;
                    resp.Message = Constants.HTTPSTATUS_ERROR_OCCURED;
                }
            }
            catch (Exception ex)
            {
                resp.StatusCode = HttpStatusCode.InternalServerError;
                resp.Message = ex.Message;
            }
            return Request.CreateResponse(HttpStatusCode.OK,
                    resp);
        }

        [HttpPost]
        public HttpResponseMessage GetCourses(Request req)
        {
            Response resp = new Response();
            CoursesResponseDTO model = new CoursesResponseDTO();
            try
            {
                PinCodeRequestDTO para = new PinCodeRequestDTO();
                string dcData = Security.DecryptString(Constants.EncKey, req.Body);
                para = JsonConvert.DeserializeObject<PinCodeRequestDTO>(dcData);

                model.Courses = _courseRepository.GetCoursesMobile(para);
                if (model != null && model.Courses != null && model.Courses.Count > 0)
                {
                    resp.Body = Security.EncryptString(Constants.EncKey, new Security().SerializeObject<CoursesResponseDTO>(model));
                    resp.StatusCode = HttpStatusCode.OK;
                    //resp.Message = Constants.HTTPSTATUS_SUCCESS;
                    resp.Message = Language.English == para.Language ? Constants.HTTPSTATUS_SUCCESS : Constants.HTTPSTATUS_SUCCESS_HI;
                }
                else
                {
                    resp.StatusCode = HttpStatusCode.NotFound;
                    //resp.Message = Constants.HTTPSTATUS_RECORD_NOT_FOUND;
                    resp.Message = Language.English == para.Language ? Constants.HTTPSTATUS_RECORD_NOT_FOUND : Constants.HTTPSTATUS_RECORD_NOT_FOUND_HI;
                }
            }
            catch (Exception ex)
            {
                resp.StatusCode = HttpStatusCode.InternalServerError;
                resp.Message = ex.Message;
            }
            return Request.CreateResponse(HttpStatusCode.OK,
                    resp);
        }

        [HttpPost]
        public HttpResponseMessage UpdateProfileEducation(Request req)
        {
            Response resp = new Response();
            try
            {
                UpdateProfileEducationDTO para = new UpdateProfileEducationDTO();
                string dcData = Security.DecryptString(Constants.EncKey, req.Body);
                para = JsonConvert.DeserializeObject<UpdateProfileEducationDTO>(dcData);

                para.DeviceType = DeviceType.Mobile;
                var status = _userRepository.UpdateProfileEducationMobile(para);
                if (status == true)
                {
                    resp.StatusCode = HttpStatusCode.OK;
                    resp.Message = Constants.HTTPSTATUS_SUCCESS;

                }
                else
                {
                    resp.StatusCode = HttpStatusCode.InternalServerError;
                    resp.Message = Constants.HTTPSTATUS_ERROR_OCCURED;
                }
            }
            catch (Exception ex)
            {
                resp.StatusCode = HttpStatusCode.InternalServerError;
                resp.Message = ex.Message;
            }
            return Request.CreateResponse(HttpStatusCode.OK,
                    resp);
        }

        [HttpPost]
        public HttpResponseMessage UpdateProfileExperience(Request req)
        {
            Response resp = new Response();
            try
            {
                UpdateProfileExperienceDTO para = new UpdateProfileExperienceDTO();
                string dcData = Security.DecryptString(Constants.EncKey, req.Body);
                var settings = new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    MissingMemberHandling = MissingMemberHandling.Ignore
                };
                para = JsonConvert.DeserializeObject<UpdateProfileExperienceDTO>(dcData, settings);

                para.DeviceType = DeviceType.Mobile;
                var status = _userRepository.UpdateProfileExperienceMobile(para);
                if (status == true)
                {
                    resp.StatusCode = HttpStatusCode.OK;
                    resp.Message = Constants.HTTPSTATUS_SUCCESS;
                }
                else
                {
                    resp.StatusCode = HttpStatusCode.InternalServerError;
                    resp.Message = Constants.HTTPSTATUS_ERROR_OCCURED;
                }
            }
            catch (Exception ex)
            {
                resp.StatusCode = HttpStatusCode.InternalServerError;
                resp.Message = ex.Message;
            }
            return Request.CreateResponse(HttpStatusCode.OK,
                    resp);
        }

        //start from here
        [HttpPost]
        public HttpResponseMessage UpdateProfilePic()
        {
            Response resp = new Response();
            try
            {
                Random rn = new Random();
                string Results = string.Empty;
                if (!Request.Content.IsMimeMultipartContent())
                {
                    throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
                }
                int Id = Convert.ToInt32(Security.DecryptString(Constants.EncKey, HttpContext.Current.Request.Params["Id"]));
                string DeviceId = HttpContext.Current.Request.Params["DeviceId"];
                string Lat = HttpContext.Current.Request.Params["Lat"];
                string Long = HttpContext.Current.Request.Params["Long"];
                string Address = HttpContext.Current.Request.Params["Address"];

                //for (int i = 0; i < HttpContext.Current.Request.Files.Count; i++)
                //{
                var file = HttpContext.Current.Request.Files[0];
                //var fileimagename = file.FileName.Split('/')[0].Split('.')[0];
                //var fileExt = file.FileName.Split('/')[0].Split('.')[1];
                var thisFileName = rn.Next(10, 100) + "_photo_" + DateTime.Now.ToString("ddmmyyhhmmss") + Id.ToString() + "_" + file.FileName;
                string folderPath = HttpContext.Current.Server.MapPath("~/FileUpload/ProfilePhoto/");
                //Check whether Directory(Folder) exists.
                if (!Directory.Exists(folderPath))
                {
                    //If Directory (Folder) does not exists. Create it.
                    Directory.CreateDirectory(folderPath);
                }
                //Save the File to the Directory (Folder).
                file.SaveAs(folderPath + thisFileName);

                //} 
                var status = _userRepository.UpdateProfilePic(Id, thisFileName, DeviceId, Lat, Long, Address);
                if (status == true)
                {
                    resp.StatusCode = HttpStatusCode.OK;
                    resp.Message = Constants.HTTPSTATUS_SUCCESS;

                }
                else
                {
                    resp.StatusCode = HttpStatusCode.InternalServerError;
                    resp.Message = Constants.HTTPSTATUS_ERROR_OCCURED;
                }
            }
            catch (Exception ex)
            {
                resp.StatusCode = HttpStatusCode.InternalServerError;
                resp.Message = ex.Message;
                // return ex.Message;
            }
            return Request.CreateResponse(HttpStatusCode.OK,
                    resp);
        }

        [HttpPost]
        public HttpResponseMessage UpdateResume()
        {
            Response resp = new Response();
            try
            {
                Random rn = new Random();
                string Results = string.Empty;
                if (!Request.Content.IsMimeMultipartContent())
                {
                    throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
                }
                int Id = Convert.ToInt32(Security.DecryptString(Constants.EncKey, HttpContext.Current.Request.Params["Id"]));
                string DeviceId = HttpContext.Current.Request.Params["DeviceId"];
                string Lat = HttpContext.Current.Request.Params["Lat"];
                string Long = HttpContext.Current.Request.Params["Long"];
                string Address = HttpContext.Current.Request.Params["Address"];

                //for (int i = 0; i < HttpContext.Current.Request.Files.Count; i++)
                //{
                var file = HttpContext.Current.Request.Files[0];
                //var fileimagename = file.FileName.Split('/')[0].Split('.')[0];
                //var fileExt = file.FileName.Split('/')[0].Split('.')[1];
                var thisFileName = rn.Next(10, 100) + "_resume_" + DateTime.Now.ToString("ddmmyyhhmmss") + Id.ToString() + "_" + file.FileName;
                string folderPath = HttpContext.Current.Server.MapPath("~/FileUpload/Resume/");
                //Check whether Directory(Folder) exists.
                if (!Directory.Exists(folderPath))
                {
                    //If Directory (Folder) does not exists. Create it.
                    Directory.CreateDirectory(folderPath);
                }
                //Save the File to the Directory (Folder).
                file.SaveAs(folderPath + thisFileName);

                //} 
                var status = _userRepository.UpdateResume(Id, thisFileName, DeviceId, Lat, Long, Address);
                if (status == true)
                {
                    resp.StatusCode = HttpStatusCode.OK;
                    resp.Message = Constants.HTTPSTATUS_SUCCESS;
                }
                else
                {
                    resp.StatusCode = HttpStatusCode.InternalServerError;
                    resp.Message = Constants.HTTPSTATUS_ERROR_OCCURED;
                }
            }
            catch (Exception ex)
            {
                resp.StatusCode = HttpStatusCode.InternalServerError;
                resp.Message = ex.Message;
                // return ex.Message;
            }
            return Request.CreateResponse(HttpStatusCode.OK,
                    resp);
        }

        [HttpPost]
        public HttpResponseMessage GetProfileData(Request req)
        {
            Response resp = new Response();
            UserProfileResponseDTO model = new UserProfileResponseDTO();
            try
            {
                UserProfileReqDTO para = new UserProfileReqDTO();
                string dcData = Security.DecryptString(Constants.EncKey, req.Body);
                para = JsonConvert.DeserializeObject<UserProfileReqDTO>(dcData);

                para.DeviceType = DeviceType.Mobile;
                model = _userRepository.GetProfileData(para);
                if (model != null)
                {
                    resp.Body = Security.EncryptString(Constants.EncKey, new Security().SerializeObject<UserProfileResponseDTO>(model));
                    resp.StatusCode = HttpStatusCode.OK;
                    resp.Message = Constants.HTTPSTATUS_RECORD_FOUND;

                }
                else
                {
                    resp.StatusCode = HttpStatusCode.NotFound;
                    resp.Message = Constants.HTTPSTATUS_RECORD_NOT_FOUND;
                }
            }
            catch (Exception ex)
            {
                resp.StatusCode = HttpStatusCode.InternalServerError;
                resp.Message = ex.Message;
            }
            return Request.CreateResponse(HttpStatusCode.OK,
                    resp);
        }

        #endregion

        [HttpPost]
        public HttpResponseMessage SearchJob(Request req)
        {
            Response resp = new Response();
            JobReposneDTO model = new JobReposneDTO();
            try
            {
                JobSearchFilterDTO para = new JobSearchFilterDTO();
                //req.Body = Security.EncryptString(Constants.EncKey, "{\"address\":\"\",\"categoryId\":null,\"city\":\"\",\"cityId\":null,\"courseId\":null,\"deviceId\":\"53c77ce7b31551ca\",\"deviceOtherInfo\":\"\",\"deviceType\":\"\",\"domain\":\"\",\"employerId\":68,\"ip\":\"\",\"long\":\"80.9968591\",\"language\":\"1\",\"lat\":\"26.8627529\",\"os\":\"\",\"page\":0,\"salaryMax\":\"\",\"salaryMin\":\"\",\"searchTerm\":\"\",\"skillId\":null,\"sortBy\":\"\",\"userAgent\":\"\"}");
                string dcData = Security.DecryptString(Constants.EncKey, req.Body);
                var settings = new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    MissingMemberHandling = MissingMemberHandling.Ignore
                };
                para = JsonConvert.DeserializeObject<JobSearchFilterDTO>(dcData, settings);

                para.DeviceType = DeviceType.Mobile;
                model = _jobRepository.SearchJob(para);
                if (model.ListJob != null)
                {
                    resp.StatusCode = HttpStatusCode.OK;
                    int i = 0;
                    for (i = 0; i < model.ListJob.Count; i++)
                    {
                        if (para.Language == Language.English)
                            model.ListJob[i].Time = Common.TimeLeft(Convert.ToDateTime(model.ListJob[i].PostedDate));
                        else
                            model.ListJob[i].Time = Common.TimeLeftHindi(Convert.ToDateTime(model.ListJob[i].PostedDate));
                    }
                    //resp.Message = Constants.HTTPSTATUS_RECORD_FOUND;
                    resp.Message = Language.English == para.Language ? Constants.HTTPSTATUS_RECORD_FOUND : Constants.HTTPSTATUS_RECORD_FOUND_HI;
                }
                else
                {
                    resp.StatusCode = HttpStatusCode.NotFound;
                    //resp.Message = Constants.HTTPSTATUS_RECORD_NOT_FOUND;
                    resp.Message = Language.English == para.Language ? Constants.HTTPSTATUS_RECORD_NOT_FOUND : Constants.HTTPSTATUS_RECORD_NOT_FOUND_HI;
                }
                resp.Body = Security.EncryptString(Constants.EncKey, new Security().SerializeObject<JobReposneDTO>(model));
            }
            catch (Exception ex)
            {
                resp.StatusCode = HttpStatusCode.InternalServerError;
                resp.Message = ex.Message;
            }
            return Request.CreateResponse(HttpStatusCode.OK,
                    resp);
        }

        [HttpPost]
        public HttpResponseMessage JobDetail(Request req)
        {
            Response resp = new Response();
            try
            {
                JOBDetailDTO para = new JOBDetailDTO();
                string dcData = Security.DecryptString(Constants.EncKey, req.Body);
                para = JsonConvert.DeserializeObject<JOBDetailDTO>(dcData);

                para.DeviceType = DeviceType.Mobile;
                var detail = _jobRepository.GetCustomById(para);
                if (detail != null)
                {
                    resp.Body = Security.EncryptString(Constants.EncKey, new Security().SerializeObject<JobFilteredDTO>(detail));
                    resp.StatusCode = HttpStatusCode.OK;
                    //resp.Message = Constants.HTTPSTATUS_RECORD_FOUND;
                    resp.Message = Language.English == para.Language ? Constants.HTTPSTATUS_RECORD_FOUND : Constants.HTTPSTATUS_RECORD_FOUND_HI;
                }
                else
                {
                    resp.StatusCode = HttpStatusCode.NotFound;
                    resp.Message = Language.English == para.Language ? Constants.HTTPSTATUS_RECORD_NOT_FOUND : Constants.HTTPSTATUS_RECORD_NOT_FOUND_HI;
                }
            }
            catch (Exception ex)
            {
                resp.StatusCode = HttpStatusCode.InternalServerError;
                resp.Message = ex.Message;
            }
            return Request.CreateResponse(HttpStatusCode.OK,
                    resp);
        }

        [HttpPost]
        public HttpResponseMessage ApplyJob(Request req)
        {
            Response resp = new Response();
            try
            {
                JobApplyDTO para = new JobApplyDTO();
                string dcData = Security.DecryptString(Constants.EncKey, req.Body);
                para = JsonConvert.DeserializeObject<JobApplyDTO>(dcData);

                para.DeviceType = DeviceType.Mobile;
                var chk = _jobRepository.ApplyJob(para);
                if (chk == 1)
                {
                    resp.Body = "";
                    resp.StatusCode = HttpStatusCode.OK;
                    resp.Message = Constants.HTTPSTATUS_SUCCESSFULLY_APPLIED_FOR_JOB;

                }
                else if (chk == -1)
                {
                    resp.StatusCode = HttpStatusCode.NotAcceptable;
                    resp.Message = Constants.HTTPSTATUS_ALREADY_APPLIED_FOR_JOB;
                }
                else
                {
                    resp.StatusCode = HttpStatusCode.InternalServerError;
                    resp.Message = Constants.HTTPSTATUS_ERROR_OCCURED;
                }
            }
            catch (Exception ex)
            {
                resp.StatusCode = HttpStatusCode.InternalServerError;
                resp.Message = ex.Message;
            }
            return Request.CreateResponse(HttpStatusCode.OK,
                    resp);
        }

        [HttpPost]
        public HttpResponseMessage AppliedJobs(Request req)
        {
            Response resp = new Response();
            AppliedJobResponseDTO model = new AppliedJobResponseDTO();
            try
            {
                AppliedJobFilterDTO para = new AppliedJobFilterDTO();
                string dcData = Security.DecryptString(Constants.EncKey, req.Body);
                para = JsonConvert.DeserializeObject<AppliedJobFilterDTO>(dcData);

                para.DeviceType = DeviceType.Mobile;
                model.ListJob = _userJobRepository.AppliedJobs(para);
                if (model.ListJob != null)
                {
                    resp.Body = Security.EncryptString(Constants.EncKey, new Security().SerializeObject<AppliedJobResponseDTO>(model));
                    resp.StatusCode = HttpStatusCode.OK;
                    //resp.Message = Constants.HTTPSTATUS_RECORD_FOUND;
                    resp.Message = Language.English == para.Language ? Constants.HTTPSTATUS_RECORD_FOUND : Constants.HTTPSTATUS_RECORD_FOUND_HI;
                }
                else
                {
                    resp.StatusCode = HttpStatusCode.NotFound;
                    //resp.Message = Constants.HTTPSTATUS_RECORD_NOT_FOUND;
                    resp.Message = Language.English == para.Language ? Constants.HTTPSTATUS_RECORD_NOT_FOUND : Constants.HTTPSTATUS_RECORD_NOT_FOUND_HI;
                }
            }
            catch (Exception ex)
            {
                resp.StatusCode = HttpStatusCode.InternalServerError;
                resp.Message = ex.Message;
            }
            return Request.CreateResponse(HttpStatusCode.OK,
                    resp);
        }

        [HttpPost]
        public HttpResponseMessage GetPincodeData(Request req)
        {
            Response resp = new Response();
            CityStateDTO model = new CityStateDTO();
            try
            {
                PinCodeRequestDTO para = new PinCodeRequestDTO();
                string dcData = Security.DecryptString(Constants.EncKey, req.Body);
                para = JsonConvert.DeserializeObject<PinCodeRequestDTO>(dcData);

                model = _pincodeRepository.GetCityState(para.Language, para.PinCode);
                if (model != null)
                {
                    resp.Body = Security.EncryptString(Constants.EncKey, new Security().SerializeObject<CityStateDTO>(model));
                    resp.StatusCode = HttpStatusCode.OK;
                    //resp.Message = Constants.HTTPSTATUS_SUCCESS;
                    resp.Message = Language.English == para.Language ? Constants.HTTPSTATUS_SUCCESS : Constants.HTTPSTATUS_SUCCESS_HI;
                }
                else
                {
                    resp.StatusCode = HttpStatusCode.NotFound;
                    //resp.Message = Constants.HTTPSTATUS_RECORD_NOT_FOUND;
                    resp.Message = Language.English == para.Language ? Constants.HTTPSTATUS_RECORD_NOT_FOUND : Constants.HTTPSTATUS_RECORD_NOT_FOUND_HI;
                }
            }
            catch (Exception ex)
            {
                resp.StatusCode = HttpStatusCode.InternalServerError;
                resp.Message = ex.Message;
            }
            return Request.CreateResponse(HttpStatusCode.OK,
                    resp);
        }

        [HttpPost]
        public HttpResponseMessage Logout(Request req)
        {
            Response resp = new Response();
            try
            {
                UserLogoutDTO para = new UserLogoutDTO();
                string dcData = Security.DecryptString(Constants.EncKey, req.Body);
                para = JsonConvert.DeserializeObject<UserLogoutDTO>(dcData);
                para.DeviceType = DeviceType.Mobile;
                LoginResponse r;
                var status = _userRepository.LogoutMobile(para);
                if (status == true)
                {
                    resp.StatusCode = HttpStatusCode.OK;
                    resp.Message = Constants.HTTPSTATUS_LOGOUT_SUCCESSFUL;

                }
                else
                {
                    resp.StatusCode = HttpStatusCode.InternalServerError;
                    resp.Message = Constants.HTTPSTATUS_ERROR_OCCURED;
                }
            }
            catch (Exception ex)
            {
                resp.StatusCode = HttpStatusCode.InternalServerError;
                resp.Message = ex.Message;
            }
            return Request.CreateResponse(HttpStatusCode.OK,
                    resp);
        }

        [HttpPost]
        public HttpResponseMessage GetLocations(Request req)
        {
            Response resp = new Response();
            CitiesResponseDTO model = new CitiesResponseDTO();
            try
            {
                CityRequestDTO para = new CityRequestDTO();
                string dcData = Security.DecryptString(Constants.EncKey, req.Body);
                para = JsonConvert.DeserializeObject<CityRequestDTO>(dcData);

                model.Cities = _cityRepository.GetCitiesMobile(para);
                if (model != null && model.Cities != null && model.Cities.Count > 0)
                {
                    resp.Body = Security.EncryptString(Constants.EncKey, new Security().SerializeObject<CitiesResponseDTO>(model));
                    resp.StatusCode = HttpStatusCode.OK;
                    //resp.Message = Constants.HTTPSTATUS_SUCCESS;
                    resp.Message = Language.English == para.Language ? Constants.HTTPSTATUS_SUCCESS : Constants.HTTPSTATUS_SUCCESS_HI;
                }
                else
                {
                    resp.StatusCode = HttpStatusCode.NotFound;
                    //resp.Message = Constants.HTTPSTATUS_RECORD_NOT_FOUND;
                    resp.Message = Language.English == para.Language ? Constants.HTTPSTATUS_RECORD_NOT_FOUND : Constants.HTTPSTATUS_RECORD_NOT_FOUND_HI;
                }
            }
            catch (Exception ex)
            {
                resp.StatusCode = HttpStatusCode.InternalServerError;
                resp.Message = ex.Message;
            }
            return Request.CreateResponse(HttpStatusCode.OK,
                    resp);
        }
        [HttpPost]
        public HttpResponseMessage GetRecentJobs(Request req)
        {
            Response resp = new Response();
            JobReposneDTO model = new JobReposneDTO();
            try
            {
                RecentJobRequestDTO para = new RecentJobRequestDTO();
                string dcData = Security.DecryptString(Constants.EncKey, req.Body);
                para = JsonConvert.DeserializeObject<RecentJobRequestDTO>(dcData);
                para.DeviceType = DeviceType.Mobile;
                model = _jobRepository.GetRecentJobs(para);
                if (model.ListJob != null)
                {
                    resp.StatusCode = HttpStatusCode.OK;
                    //resp.Message = Constants.HTTPSTATUS_RECORD_FOUND;
                    resp.Message = Language.English == para.Language ? Constants.HTTPSTATUS_RECORD_FOUND : Constants.HTTPSTATUS_RECORD_FOUND_HI;
                }
                else
                {
                    resp.StatusCode = HttpStatusCode.NotFound;
                    //resp.Message = Constants.HTTPSTATUS_RECORD_NOT_FOUND;
                    resp.Message = Language.English == para.Language ? Constants.HTTPSTATUS_RECORD_NOT_FOUND : Constants.HTTPSTATUS_RECORD_NOT_FOUND_HI;
                }
                resp.Body = Security.EncryptString(Constants.EncKey, new Security().SerializeObject<JobReposneDTO>(model));
            }
            catch (Exception ex)
            {
                resp.StatusCode = HttpStatusCode.InternalServerError;
                resp.Message = ex.Message;
            }
            return Request.CreateResponse(HttpStatusCode.OK,
                    resp);
        }
        [HttpPost]
        public HttpResponseMessage SubmitFeedBack(Request req)
        {
            Response resp = new Response();
            try
            {
                FeedbackDTO para = new FeedbackDTO();
                string dcData = Security.DecryptString(Constants.EncKey, req.Body);
                para = JsonConvert.DeserializeObject<FeedbackDTO>(dcData);
                para.DeviceType = DeviceType.Mobile;
                if (para.Email != "")
                {
                    BLMail.SendMail(para.Email, "Feedback", "Thank you for your feedback in Career Mitra Job Portal.", false);
                    resp.Body = "";
                    resp.StatusCode = HttpStatusCode.OK;
                    resp.Message = Constants.HTTPSTATUS_SUCCESS;

                }
                var status = _feedbackRepository.Add(para);
                if (status == true)
                {
                    resp.StatusCode = HttpStatusCode.OK;
                    resp.Message = Constants.HTTPSTATUS_SUCCESS;
                }
                else
                {
                    resp.StatusCode = HttpStatusCode.InternalServerError;
                    resp.Message = Constants.HTTPSTATUS_FAILED;
                }
            }
            catch (Exception ex)
            {
                resp.StatusCode = HttpStatusCode.InternalServerError;
                resp.Message = ex.Message;
            }
            return Request.CreateResponse(HttpStatusCode.OK,
                    resp);
        }
        [HttpPost]
        public HttpResponseMessage GetCMSData(Request req)
        {
            Response resp = new Response();
            CMSResponseDTO model = new CMSResponseDTO();
            try
            {
                CMSDTO para = new CMSDTO();
                string dcData = Security.DecryptString(Constants.EncKey, req.Body);
                para = JsonConvert.DeserializeObject<CMSDTO>(dcData);
                // model = _cityRepository.GetCitiesMobile(para);

                if (para.pageName == PageName.AboutUs)
                {
                    if (para.language == Language.English)
                    {
                        model.Text = " “Job portal“ is a web-based application, which helps to find brass workers, Artisan, Kaamgaar to find a job with searching criteria like preferred skill and location and work profile. It also helpful to “post job” and “search Kaamgaar” based on job - seeker profile.Industries or Employer can download resume of job seeker.Search Kaamgaar candidate based on their job profile. “Job portal” is a web - based application, which helps to find brass workers, Artisan, Kaamgaar to find a job with searching criteria like preferred skill and location and work profile. It also helpful to “post job” and “search Kaamgaar” based on job - seeker profile.Industries or Employer can download resume of job seeker.Search Kaamgaar candidate based on their job profile.";
                    }
                    else
                    {
                        model.Text = "\"जॉब पोर्टल\" एक वेब-आधारित एप्लिकेशन है, जो पसंदीदा कौशल और स्थान और कार्य प्रोफ़ाइल जैसे खोज मापदंड के साथ नौकरी खोजने के लिए पीतल श्रमिकों, कारीगर, कामगार को खोजने में मदद करता है। \n यह नौकरी तलाशने वाले के प्रोफाइल के आधार पर \"पोस्ट जॉब\" और \"सर्च कामगार\" के लिए भी सहायक है। उद्योग या नियोक्ता नौकरी तलाशने वाले का खोज कामगार उम्मीदवार अपनी नौकरी प्रोफाइल के आधार पर फिर से शुरू कर सकते हैं।";
                    }
                    resp.Body = Security.EncryptString(Constants.EncKey, new Security().SerializeObject<CMSResponseDTO>(model));
                    resp.StatusCode = HttpStatusCode.OK;
                    resp.Message = Constants.HTTPSTATUS_SUCCESS;

                }
                else if (para.pageName == PageName.TermandCondition)
                {
                    model.Text = "Career Mitra is intended only to serve as a preliminary medium of contact and exchange of information for its users / members / visitors who have a bona fide intention to contact and/or be contacted for the purposes related to genuine existing job vacancies and for other career enhancement services. The site is a public site with free access. The individual/company would have to conduct its own background checks on the bonafide nature of all response(s). You give us permission to use the information about actions that you have taken on Career Mitra in connection with ads, offers and other content(whether sponsored or not) that we display across our services, without any compensation to you.We use data and information about you to make relevant suggestions and recommendation to you and others.The platform may contain links to third party websites, these links are provided solely as convenience to You and the presence of these links should not under any circumstances be considered as an endorsement of the contents of the same, if You chose to access these websites you do so at your own risk. Users undertake that the services offered by Career Mitra shall not be utilized to upload, post, email, transmit or otherwise make available either directly or indirectly, any unsolicited bulk e-mail or unsolicited commercial e-mail.Career Mitra reserves the right to filter and monitor and block the emails sent by you / user using the servers maintained by Career Mitra to relay emails.All attempts shall be made by Career Mitra and the user to abide by International Best Practices in containing and eliminating Spam. Users shall not spam the platform maintained by Career Mitra or indiscriminately and repeatedly post jobs/ forward mail indiscriminately etc.Any conduct of the user in violation of this clause shall entitle Career Mitra to forthwith terminate all services to the user without notice and to forfeit any amounts paid by him. The user shall not upload, post, transmit, publish, or distribute any material or information that is unlawful, or which may potentially be perceived as being harmful, threatening, abusive, harassing, defamatory, libelous, vulgar, obscene, or racially, ethnically, or otherwise objectionable. The User is solely responsible for maintaining confidentiality of the User password and user identification and all activities and transmission performed by the User through his user identification and shall be solely responsible for carrying out any online or off - line transaction involving credit cards / debit cards or such other forms of instruments or documents for making such transactions.";
                    resp.Body = Security.EncryptString(Constants.EncKey, new Security().SerializeObject<CMSResponseDTO>(model));
                    resp.StatusCode = HttpStatusCode.OK;
                    resp.Message = Constants.HTTPSTATUS_SUCCESS;
                }
                else if (para.pageName == PageName.PrivacyPolicy)
                {
                    model.Text = "We, at Career Mitra and our affiliated companies worldwide (hereinafter collectively referred to as Career Mitra), are committed to respecting your online privacy and recognize the need for appropriate protection and management of any personally identifiable information you share with us. This Privacy Policy (Policy) describes how Career Mitra collects, uses, discloses and transfers personal information of users through its websites and applications, including through Career Mitra, mobile applications and online services (collectively, the platform). This policy applies to those who visit the Platform, or whose information Career Mitra otherwise receives in connection with its services (such as contact information of individuals associated with Career Mitra including partners) (hereinafter collectively referred to as Users). For the purposes of the Privacy Policy, You or Your shall mean the person who is accessing the Platform. Personal information(PI) - means any information relating to an identified or identifiable natural person including common identifiers such as a name, an identification number, location data, an online identifier or one or more factors specific to the physical, physiological, genetic, mental, economic, cultural or social identity of that natural person and any other information that is so categorized by applicable laws. We collect information about you and/ or your usage to provide better services and offerings. The Personal Information that we collect, and how we collect it, depends upon how you interact with us. We collect the following categories of Personal Information in the following ways: We will only use your personal data in a fair and reasonable manner, and where we have a lawful reason to do so.Some of our web pages utilize cookies and other tracking technologies. A cookie is a small text file that may be used, for example, to collect information about web - site activity.Some cookies and other technologies may serve to recall Personal Information previously indicated by a user.Most browsers allow you to control cookies, including whether or not to accept them and how to remove them.You may set most browsers to notify you if you receive a cookie, or you may choose to block cookies with your browser, but please note that if you choose to erase or block your cookies, you will need to re - enter your original user ID and password to gain access to certain parts of the Platform. Tracking technologies may record information such as Internet domain and host names; Internet protocol (IP)addresses; browser software and operating system types; clickstream patterns; and dates and times that our site is accessed.Our use of cookies and other tracking technologies allows us to improve our Platform and the overall website experience. We may also analyse information that does not contain Personal Information for trends and statistics.";
                    resp.Body = Security.EncryptString(Constants.EncKey, new Security().SerializeObject<CMSResponseDTO>(model));
                    resp.StatusCode = HttpStatusCode.OK;
                    resp.Message = Constants.HTTPSTATUS_SUCCESS;
                }
                else
                {
                    resp.StatusCode = HttpStatusCode.NotFound;
                    resp.Message = Constants.HTTPSTATUS_RECORD_NOT_FOUND;
                }
                //  status = _userRepository.SubmitFeedback(para);

            }
            catch (Exception ex)
            {
                resp.StatusCode = HttpStatusCode.InternalServerError;
                resp.Message = ex.Message;
            }
            return Request.CreateResponse(HttpStatusCode.OK,
                    resp);
        }

        [HttpPost]
        public HttpResponseMessage GetTrainingMaterial(Request req)
        {
            Response resp = new Response();
            TrainingMaterialResponseDTO model = new TrainingMaterialResponseDTO();
            try
            {
                TrainingMaterialRequestDTO para = new TrainingMaterialRequestDTO();
                string dcData = Security.DecryptString(Constants.EncKey, req.Body);
                para = JsonConvert.DeserializeObject<TrainingMaterialRequestDTO>(dcData);
                para.DeviceType = DeviceType.Mobile;
                model = _trainingMaterialRepository.GetTrainingMaterial(para);
                if (model != null && model.lstTMaterial != null && model.lstTMaterial.Count > 0)
                {
                    resp.Body = Security.EncryptString(Constants.EncKey, new Security().SerializeObject<TrainingMaterialResponseDTO>(model));
                    resp.StatusCode = HttpStatusCode.OK;
                    //resp.Message = Constants.HTTPSTATUS_RECORD_FOUND;
                    resp.Message = Language.English == para.Language ? Constants.HTTPSTATUS_RECORD_FOUND : Constants.HTTPSTATUS_RECORD_FOUND_HI;
                }
                else
                {
                    resp.StatusCode = HttpStatusCode.NotFound;
                    //resp.Message = Constants.HTTPSTATUS_RECORD_NOT_FOUND;
                    resp.Message = Language.English == para.Language ? Constants.HTTPSTATUS_RECORD_NOT_FOUND : Constants.HTTPSTATUS_RECORD_NOT_FOUND_HI;
                }
                //  status = _userRepository.SubmitFeedback(para);

            }
            catch (Exception ex)
            {
                resp.StatusCode = HttpStatusCode.InternalServerError;
                resp.Message = ex.Message;
            }
            return Request.CreateResponse(HttpStatusCode.OK,
                    resp);
        }

        [HttpPost]
        public HttpResponseMessage GetMessageList(Request req)
        {
            Response resp = new Response();
            MessageListResponseDTO model = new MessageListResponseDTO();
            try
            {
                MessageRequestDTO para = new MessageRequestDTO();
                string dcData = Security.DecryptString(Constants.EncKey, req.Body);
                para = JsonConvert.DeserializeObject<MessageRequestDTO>(dcData);
                para.DeviceType = DeviceType.Mobile;
                model = _chatRepository.GetMessageList(para);
                if (model != null && model.Messages != null && model.Messages.Count > 0)
                {
                    resp.Body = Security.EncryptString(Constants.EncKey, new Security().SerializeObject<MessageListResponseDTO>(model));
                    resp.StatusCode = HttpStatusCode.OK;
                    resp.Message = Constants.HTTPSTATUS_RECORD_FOUND;

                }
                else
                {
                    resp.StatusCode = HttpStatusCode.NotFound;
                    resp.Message = Constants.HTTPSTATUS_RECORD_NOT_FOUND;
                }
                //  status = _userRepository.SubmitFeedback(para);

            }
            catch (Exception ex)
            {
                resp.StatusCode = HttpStatusCode.InternalServerError;
                resp.Message = ex.Message;
            }
            return Request.CreateResponse(HttpStatusCode.OK,
                    resp);
        }

        [HttpPost]
        public HttpResponseMessage GetMessageConversation(Request req)
        {
            Response resp = new Response();
            MessageChatListResponseDTO model = new MessageChatListResponseDTO();
            try
            {
                MessageChatRequestDTO para = new MessageChatRequestDTO();
                string dcData = Security.DecryptString(Constants.EncKey, req.Body);
                para = JsonConvert.DeserializeObject<MessageChatRequestDTO>(dcData);
                para.DeviceType = DeviceType.Mobile;
                model = _chatRepository.GetMessageChats(para);
                if (model != null && model.Messages != null && model.Messages.Count > 0)
                {
                    resp.Body = Security.EncryptString(Constants.EncKey, new Security().SerializeObject<MessageChatListResponseDTO>(model));
                    resp.StatusCode = HttpStatusCode.OK;
                    resp.Message = Constants.HTTPSTATUS_RECORD_FOUND;

                }
                else
                {
                    resp.StatusCode = HttpStatusCode.NotFound;
                    resp.Message = Constants.HTTPSTATUS_RECORD_NOT_FOUND;
                }
                //  status = _userRepository.SubmitFeedback(para);

            }
            catch (Exception ex)
            {
                resp.StatusCode = HttpStatusCode.InternalServerError;
                resp.Message = ex.Message;
            }
            return Request.CreateResponse(HttpStatusCode.OK,
                    resp);
        }

        [HttpPost]
        public HttpResponseMessage SendMessage(Request req)
        {
            Response resp = new Response();
            try
            {
                MessageSendRequestDTO para = new MessageSendRequestDTO();
                string dcData = Security.DecryptString(Constants.EncKey, req.Body);
                para = JsonConvert.DeserializeObject<MessageSendRequestDTO>(dcData);
                para.DeviceType = DeviceType.Mobile;
                var status = _chatRepository.SendMessage(para);
                if (status == true)
                {
                    resp.StatusCode = HttpStatusCode.OK;
                    resp.Message = Constants.HTTPSTATUS_SUCCESS;

                }
                else
                {
                    resp.StatusCode = HttpStatusCode.InternalServerError;
                    resp.Message = Constants.HTTPSTATUS_FAILED;
                }

            }
            catch (Exception ex)
            {
                resp.StatusCode = HttpStatusCode.InternalServerError;
                resp.Message = ex.Message;
            }
            return Request.CreateResponse(HttpStatusCode.OK,
                    resp);
        }

        [HttpPost]
        public HttpResponseMessage GetFilters(Request req)
        {
            Response resp = new Response();
            MessageChatListResponseDTO model = new MessageChatListResponseDTO();
            try
            {


                var skills = _skillRepository.GetAll().Select(x => new { x.Id, x.Name, x.NameH }).ToList();
                var departments = _departmentRepository.GetAll().Select(x => new
                {
                    x.Id,
                    x.Name,
                    x.NameH,
                    Categories = x.DepartmentCategories.Where(xx => xx.IsDeleted == false).Select(u => new
                    {
                        u.CategoryId,
                        u.Category.Name,
                        u.Category.NameH
                    })
                }).ToList();
                var qualifiation = _courseRepository.GetAll().Select(x => new { x.Id, x.Name, x.NameH }).ToList();
                var salary = new { Min = 0, Max = 10000000 };

                var FilterData = new { Skills = skills, Departments = departments, Qualification = qualifiation, Salary = salary };


                resp.Body = Security.EncryptString(Constants.EncKey, new Security().SerializeObject(FilterData));
                resp.StatusCode = HttpStatusCode.OK;
                resp.Message = Constants.HTTPSTATUS_RECORD_FOUND;

            }
            catch (Exception ex)
            {
                resp.StatusCode = HttpStatusCode.InternalServerError;
                resp.Message = ex.Message;
            }
            return Request.CreateResponse(HttpStatusCode.OK,
                    resp);
        }
        [HttpPost]
        public HttpResponseMessage GetEnrollmentProgram(Request req)
        {
            Response resp = new Response();
            EnrollmentProgramResponseDTO response = new EnrollmentProgramResponseDTO();
            try
            {
                EnrollmentProgramRequestDTO para = new EnrollmentProgramRequestDTO();
                string dcData = Security.DecryptString(Constants.EncKey, req.Body);
                para = JsonConvert.DeserializeObject<EnrollmentProgramRequestDTO>(dcData);
                para.DeviceType = DeviceType.Mobile;
                response = _enrollmentProgramRepository.GetEnrollmentProgram(para);
                if (response != null && response.lstProgram != null && response.lstProgram.Count > 0)
                {
                    resp.Body = Security.EncryptString(Constants.EncKey, new Security().SerializeObject<EnrollmentProgramResponseDTO>(response));
                    resp.StatusCode = HttpStatusCode.OK;
                    resp.Message = Language.English == para.Language ? Constants.HTTPSTATUS_RECORD_FOUND : Constants.HTTPSTATUS_RECORD_FOUND_HI;
                }
                else
                {
                    resp.StatusCode = HttpStatusCode.NotFound;
                    resp.Message = Language.English == para.Language ? Constants.HTTPSTATUS_RECORD_NOT_FOUND : Constants.HTTPSTATUS_RECORD_NOT_FOUND_HI;
                }

            }
            catch (Exception ex)
            {
                resp.StatusCode = HttpStatusCode.InternalServerError;
                resp.Message = ex.Message;
            }
            return Request.CreateResponse(HttpStatusCode.OK,
                    resp);
        }

        [HttpGet]
        public HttpResponseMessage Sendpush()
        {
            string[] fcmId = { "e05DcgDDmxE:APA91bHNHCGcoJelburXbJiDieYHYT_lxWoU_a5t6G6mnh8m6f_hGwOQzwX68TpJqd8VrVnb0E3NbDHxkKlo9jzezD1E_X8ktCyxHkmdx9n8yCahGqRPa970u3IXilb7ka5iXkIFdxEX" };
            string title = "Welcome to career mitra";
            string body = "testing purpose description";
            var resp = new BLFCM().SendMessage(fcmId, title, body);
            return Request.CreateResponse(HttpStatusCode.OK,
                    resp);
        }
        [HttpPost]
        public HttpResponseMessage ApplyForEnrollment(Request req)
        {
            Response resp = new Response();
            try
            {
                EnrollmentApplyDTO para = new EnrollmentApplyDTO();
                string dcData = Security.DecryptString(Constants.EncKey, req.Body);
                para = JsonConvert.DeserializeObject<EnrollmentApplyDTO>(dcData);
                para.DeviceType = DeviceType.Mobile;
                var chk = _enrollmentProgramRepository.ApplyEnrollmentProgram(para);
                if (chk == 1)
                {
                    resp.Body = "";
                    resp.StatusCode = HttpStatusCode.OK;
                    if (para.Language == Language.English)
                    {
                        resp.Message = Constants.HTTPSTATUS_SUCCESSFULLY_APPLIED_FOR_ENROLLMENT_PROGRAM;
                    }
                    else
                    {
                        resp.Message = Constants.HTTPSTATUS_ALREADY_APPLIED_FOR_ENROLLMENT_PROGRAM_HI;
                    }
                }
                else if (chk == -1)
                {
                    resp.StatusCode = HttpStatusCode.NotFound;
                    resp.Message = Constants.HTTPSTATUS_ALREADY_APPLIED_FOR_ENROLLMENT_PROGRAM;
                }
                else
                {
                    resp.StatusCode = HttpStatusCode.InternalServerError;
                    resp.Message = Constants.HTTPSTATUS_ERROR_OCCURED;
                }
            }
            catch (Exception ex)
            {
                resp.StatusCode = HttpStatusCode.InternalServerError;
                resp.Message = ex.Message;
            }
            return Request.CreateResponse(HttpStatusCode.OK,
                    resp);
        }
        [HttpPost]
        public HttpResponseMessage GetAllNotification(Request req)
        {
            Response resp = new Response();
            NotificationReposnseDTOMobile model = new NotificationReposnseDTOMobile();
            try
            {
                NotificationRequestDTO para = new NotificationRequestDTO();
                string dcData = Security.DecryptString(Constants.EncKey, req.Body);
                para = JsonConvert.DeserializeObject<NotificationRequestDTO>(dcData);
                para.DeviceType = DeviceType.Mobile;
                model.lstNotification = _notificationRepository.GetAllNotification(para);
                if (model != null && model.lstNotification.Count > 0)
                {
                    resp.Body = Security.EncryptString(Constants.EncKey, new Security().SerializeObject<NotificationReposnseDTOMobile>(model));
                    resp.StatusCode = HttpStatusCode.OK;
                    resp.Message = Constants.HTTPSTATUS_RECORD_FOUND;

                }
                else
                {
                    resp.StatusCode = HttpStatusCode.NotFound;
                    resp.Message = Constants.HTTPSTATUS_RECORD_NOT_FOUND;
                }
                //  status = _userRepository.SubmitFeedback(para);

            }
            catch (Exception ex)
            {
                resp.StatusCode = HttpStatusCode.InternalServerError;
                resp.Message = ex.Message;
            }
            return Request.CreateResponse(HttpStatusCode.OK,
                    resp);
        }

        //public HttpResponseMessage ListOfApplicants(JobDTO obj)
        //{
        //    JobReposneDTO model = new JobReposneDTO();
        //    try
        //    {
        //        var detail = _jobRepository.GetDetail(obj.Id, obj.LanguageId);
        //        if (detail != null)
        //        {
        //            model.Status = "0";
        //            model.Message = "Record found !";
        //            //foreach (DataRow r in ds.Tables[0].Rows)
        //            //{
        //            //    UserJob obj1 = new UserJob();
        //            //    obj1.JobId = Convert.ToInt32(r["Id"]);
        //            //    obj1.UserId = Convert.ToInt32(r["UserId"]);
        //            //    obj1.UserName = r["UserName"].ToString();
        //            //    obj1.Gender = r["Gender"].ToString();
        //            //    obj1.MobileNo = r["MobileNo"].ToString();
        //            //    obj1.Email = r["Email"].ToString();
        //            //    obj1.City = r["City"].ToString();
        //            //    obj1.Experience = r["Experience"].ToString();
        //            //    obj1.City = r["City"].ToString();
        //            //    obj1.ExpectedSalary = r["ExpectedSalary"].ToString();
        //            //    obj1.UploadedResume = r["UploadedResume"].ToString();
        //            //    lst.Add(obj1);
        //            //}
        //            //model.lstuser = lst;
        //        }
        //        else
        //        {
        //            model.Status = "1";
        //            model.Message = "No record found !";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        model.Status = "1";
        //        model.Message = ex.Message;
        //    }
        //    return Request.CreateResponse(HttpStatusCode.OK,
        // model);
        //}
        //public HttpResponseMessage GetAllNotifications(NotificationReposnseDTO req)
        //{
        //    NotificationReposnseDTO model = new NotificationReposnseDTO();
        //    try
        //    {
        //        var detail = _notificationRepository.GetById(req.UserId, req.LanguageId);
        //        if (detail != null)
        //        {
        //            model.Status = "0";
        //            model.Message = "Record found !";
        //            //foreach (DataRow r in ds.Tables[0].Rows)
        //            //{
        //            //    Notification obj1 = new Notification();
        //            //    obj1.NotificationType = r["NotificationType"].ToString();
        //            //    obj1.Title = r["Title"].ToString();
        //            //    obj1.Description = r["Description"].ToString();
        //            //    obj1.Link = r["Link"].ToString();
        //            //    obj1.Date = Convert.ToDateTime(r["Date"]);
        //            //    lst.Add(obj1);
        //            //}
        //            //obj.lstnotification = lst;
        //        }
        //        else
        //        {
        //            model.Status = "1";
        //            model.Message = "No record found !";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        model.Status = "1";
        //        model.Message = ex.Message;
        //    }
        //    return Request.CreateResponse(HttpStatusCode.OK,
        // model);
        //}

        [Route("api/PostCallLogs")]
        [HttpGet]
        public HttpResponseMessage PostCallLogs(string authorizedtoken, string number)
        {
            if (string.IsNullOrEmpty(authorizedtoken) || authorizedtoken != "Xsz3c7yuxioxnB78Io05==")
            {
                return Request.CreateResponse(HttpStatusCode.OK,
                   new
                   {
                       status = HttpStatusCode.BadRequest,
                       message = "Bad Request"
                   });
            }
            try
            {
                var model = _userRepository.GetAll(x => x.IsDeleted == false && x.UserName == number).Any();
                if (model)
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                   new
                   {
                       status = HttpStatusCode.OK,
                       message = "Registered"
                   });
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                  new
                  {
                      status = HttpStatusCode.NotFound,
                      message = "Not Registered"
                  });
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK,
                new
                {
                    status = HttpStatusCode.InternalServerError,
                    message = ex.Message
                });
            }

        }
    }
}