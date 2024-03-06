using CaptchaMvc.HtmlHelpers;
using Data.DTOs;
using Data.Interfaces.Repositories;
using System;
using System.Web.Mvc;
using System.Web.Security;
using Utility;
using Utility.Enums;

namespace Webapp.Controllers
{
    [ValidateInput(false)]
    public class AccountController : Controller
    {
        private readonly IUserRepository _userRepository;

        public AccountController(IUserRepository userRepository)
        {
            this._userRepository = userRepository;
        }
        public ActionResult Login()
        {
            UserLoginDTO model = new UserLoginDTO();
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
                var msg = _userRepository.AdminLogin(model);
                if (msg != null)
                {
                    Session["Id"] = msg.Id;
                    FormsAuthentication.SetAuthCookie(model.UserName, false);
                    return RedirectToAction("Index", "Admin");
                }
                else
                {
                    TempData["msg"] = Constants.LOG_USER_LOGIN_FAILED;
                }
            }
            catch (Exception ex)
            {
                TempData["msg"] = ex.Message;
            }
            return View(model);
        }
        public ActionResult ForgotPassword(UserDTO model)
        {
            return View(model);
        }

        public ActionResult BtnForgotPassword(UserDTO model)
        {
            bool msg;
            GenerateOTPDTO gotp = new GenerateOTPDTO();
            gotp.MobileNo = model.MobileNo;
            gotp.DeviceType = "Web";
            gotp.UserAgent = model.UserAgent;
            msg = _userRepository.GenerateTemporaryPassword(gotp);
            if (msg == true)
            {
                TempData["MobileNo"] = model.MobileNo;
                //set session id for maintaining session
                Session["Id"] = 0;
                return RedirectToAction("ResetPassword", "User");
            }
            return RedirectToAction("ResetPassword", "User");
        }

        public ActionResult ChangePassword(UserDTO model)
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
            model.DeviceType = "Web";
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
            return RedirectToAction("ChangePassword");
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult SaveChangePassword(UserDTO model)
        {
            try
            {
                //bool msg = _userRepository.ChangePassword(model);
                //if (msg == true)
                //{
                //    return RedirectToAction("Login", "Home");
                //}
            }
            catch (Exception)
            {

                throw;
            }
            return RedirectToAction("ChangePassword");
        }

    }
}