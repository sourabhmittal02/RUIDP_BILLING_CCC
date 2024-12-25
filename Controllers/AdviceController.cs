using ComplaintTracker.DAL;
using ComplaintTracker.Models;
using DirectComplaintRegister.Models;
using IronXL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Data.OleDb;

namespace ComplaintTracker.Controllers
{
    public class AdviceController : Controller
    {
        // GET: Advice
        public ActionResult CreateAdvice()
        {
            ModelAdvice data = new ModelAdvice();
            return View(data);
        }

        public JsonResult GetList(string kno)
        {
            ModelAdvice data = new ModelAdvice();
            data = Repository.GetConsumerDetailForAdviceRUIDP(kno);
            return Json(data, JsonRequestBehavior.AllowGet);

        }
        public ActionResult PaymentPosting()
        {
            ModelPayment data = new ModelPayment();
            data.paymentModes = Repository.GetPaymentModes();
            return View(data);
        }
        public ActionResult PaymentPostingBulk()
        {

            return View();
        }
        [HttpPost]
        public ActionResult PaymentPostingBulk(HttpPostedFileBase file)
        {
            string path = Server.MapPath("~/Upload/" + file.FileName);
            file.SaveAs(path);

            //string excelConnectionString = @"Provider='Microsoft.ACE.OLEDB.12.0';Data Source='" + path + "';Extended Properties='Excel 12.0 Xml;IMEX=1'";
            string excelConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source='" + path + "';Extended Properties='Excel 12.0 Xml;HDR=Yes;IMEX=1'";
            OleDbConnection excelConnection = new OleDbConnection(excelConnectionString);

            //Sheet Name
            excelConnection.Open();
            string tableName = excelConnection.GetSchema("Tables").Rows[0]["TABLE_NAME"].ToString();
            DataTable dataTable = new DataTable();
            OleDbDataAdapter adapter = new OleDbDataAdapter("Select * from [" + tableName + "]", excelConnection);
            adapter.Fill(dataTable);
            Session["ExcelData"] = dataTable;
            excelConnection.Close();
            ExcelData data = new ExcelData();
            ModelStatus modelStatuis = Repository.SavePayment(dataTable);
            TempData["AlertMessage"] = modelStatuis.StatusMessage;
            TempData["retStatus"] = modelStatuis.StatusId;
            return RedirectToAction("PaymentPostingBulk", data);
        }

        

        public ActionResult SundryPosting()
        {
            ModelSundry data = new ModelSundry();
            data.sundrySides = Repository.GetSundrySides();
            return View(data);
        }

        [HttpGet]
        public JsonResult ShowComplaint(string Id) //It will be fired from Jquery ajax call
        {
            ModelAdvice data = new ModelAdvice();
            data = Repository.GetConsumerDetailForAdviceRUIDP(Id);
            return Json(data, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public ActionResult CreateAdvice(ModelAdvice modelAdvice) //It will be fired from Jquery ajax call
        {
            ModelAdvice data = new ModelAdvice();
            ModelStatus modelStatuis = Repository.SaveAdvice(modelAdvice);
            if (modelStatuis.StatusId == "1")
            {
                TempData["AlertMessage"] = modelStatuis.StatusMessage;
                TempData["retStatus"] = modelStatuis.StatusId;
                return RedirectToAction("CreateAdvice", data);
            }
            else
            {
                TempData["AlertMessage"] = modelStatuis.StatusMessage;
                TempData["retStatus"] = modelStatuis.StatusId;

                return View(modelAdvice);
            }
        }

        [HttpPost]
        public ActionResult PaymentPosting(ModelPayment modelPayment) //It will be fired from Jquery ajax call
        {
            ModelPayment data = new ModelPayment();

            ModelStatus modelStatuis = Repository.Save_Payment(modelPayment);
            if (modelStatuis.StatusId == "1")
            {
                TempData["AlertMessage"] = modelStatuis.StatusMessage;
                TempData["retStatus"] = modelStatuis.StatusId;
                data.paymentModes = Repository.GetPaymentModes();
                return View(data);
                //return RedirectToAction("PaymentPosting", data);
            }
            else
            {
                TempData["AlertMessage"] = modelStatuis.StatusMessage;
                TempData["retStatus"] = modelStatuis.StatusId;

                return View(modelPayment);
            }
        }

        [HttpPost]
        public ActionResult CreateSundry(ModelSundry modelSundry) //It will be fired from Jquery ajax call
        {
            ModelSundry data = new ModelSundry();
            ModelStatus modelStatuis = Repository.Save_Sundry(modelSundry);
            if (modelStatuis.StatusId == "1")
            {
                TempData["AlertMessage"] = modelStatuis.StatusMessage;
                TempData["retStatus"] = modelStatuis.StatusId;
                return RedirectToAction("SundryPosting", data);
            }
            else
            {
                TempData["AlertMessage"] = modelStatuis.StatusMessage;
                TempData["retStatus"] = modelStatuis.StatusId;

                return View(modelSundry);
            }
        }

    }
}