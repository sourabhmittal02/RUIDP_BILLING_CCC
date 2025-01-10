using ComplaintTracker.DAL;
using ComplaintTracker.Models;
using DirectComplaintRegister.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ComplaintTracker.Controllers
{
    public class BillingController : Controller
    {
        // GET: Advice
        public ActionResult CreateBilling()
        {
            ModelBilling data = new ModelBilling();
            return View(data);
        }

        public JsonResult GetList(string kno)
        {
            ModelBilling data = new ModelBilling();
            data = Repository.GetConsumerDetailForBillingRUIDP(kno);
            return Json(data, JsonRequestBehavior.AllowGet);

        }


        [HttpGet]
        public JsonResult ShowComplaint(string Id) //It will be fired from Jquery ajax call
        {
            ModelBilling data = new ModelBilling();
            data = Repository.GetConsumerDetailForBillingRUIDP(Id);
            return Json(data, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public ActionResult CreateBilling(ModelBilling modelBilling) //It will be fired from Jquery ajax call
        {

            ModelBilling data = new ModelBilling();
            if (ModelState.IsValid)
            {
               
                ModelStatus modelStatuis = Repository.Save_Billing(modelBilling);

                if (modelStatuis.StatusId == "1")
                {
                    TempData["AlertMessage"] = modelStatuis.StatusMessage;
                    TempData["retStatus"] = modelStatuis.StatusId;
       
                    TempData["Bm"] = modelBilling.BIll_Month;
                    TempData["By"] = modelBilling.BIll_Year;

                    TempData["msc"] = modelBilling.Meter_status_Code;

                    return View(modelBilling);
                    //return RedirectToAction("CreateBilling", data);
                }
                else
                {
                    TempData["AlertMessage"] = modelStatuis.StatusMessage;
                    TempData["retStatus"] = modelStatuis.StatusId;

                    return View(modelBilling);
                }
            }
            return View(modelBilling);
        }


        
    }
}