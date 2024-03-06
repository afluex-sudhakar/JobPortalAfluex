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
    public class PageSettingController : AdminBaseController
    {
        private readonly ICMSRepository _CMSRepository;
        public PageSettingController(ICMSRepository cmsRepository)
        {
            this._CMSRepository = cmsRepository;
        }

        public ActionResult Index()
        {
            //if (Session["Id"] != null)
            //{

            //}
            //else
            //{
            //    return RedirectToAction("Login", "Account");
            //}
            return View();
        }
      
        public ActionResult CMSMaster(CMSDTO model,string Id)
        {
            
            try
            {
                if (Id != null)
                {
                    model.Id = Convert.ToInt32(Id);
                    model.StaticContent = _CMSRepository.GetPageNameById(model.Id);
                    if (model.StaticContent != null)
                    {
                        model.pageName = model.StaticContent.PageName;
                        model.Description = model.StaticContent.Description;
                        model.Id = model.StaticContent.Id;
                        model.HDescription = model.StaticContent.DescriptionH;
                    }
                }
                model.lst = _CMSRepository.GetAll();
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

        public ActionResult SaveCmsPanel(string PageName, string Description, string HDescription)
        {
            bool msg;
            CMSDTO model = new CMSDTO();
            try
            {
                model.pageName = PageName;
                model.Description = Description;
                model.HDescription = HDescription;
                msg = _CMSRepository.Add(model);
                if (msg == true)
                {
                   model.Result = Constants.CMS_Page_SUCCESS;
                }
            }
            catch (Exception ex)
            {

                model.Result = ex.Message;
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        public ActionResult UpdateCMSPanel(string PageName, string Description, string HDescription,string Id)
        {
            bool msg;
            CMSDTO model = new CMSDTO();
            try
            {
                if (Id != null)
                {
                    model.Id = Convert.ToInt32(Id);
                    model.pageName = PageName;
                    model.Description = Description;
                    model.HDescription = HDescription;
                    msg = _CMSRepository.Update(model);
                    if (msg == true)
                    {
                        model.Result = Constants.CMS_Page_UPdate;
                    }
                }
            }
            catch (Exception ex)
            {

                model.Result = ex.Message;
            }

            return Json(model, JsonRequestBehavior.AllowGet);
        }
        public ActionResult DeletePage(string Id)
        {
            bool msg;
            int UserId = 1;
            msg = _CMSRepository.Delete(Convert.ToInt32(Id), UserId);
            if (msg == true)
            {
                TempData["msg"] = "Deleted";
            }
            return RedirectToAction("CMSMaster");
        }
    }
}