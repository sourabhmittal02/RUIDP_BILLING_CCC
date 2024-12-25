﻿using DirectComplaintRegister.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using DirectComplaintRegister.DAL;
using System.IO;
using System.Text;
using static ComplaintTracker.JqueryDatatableParam;
using ComplaintTracker.DAL;
using SqlHelper = DirectComplaintRegister.DAL.SqlHelper;

namespace ComplaintTracker.Areas.DirectComplaintRegister.Controllers
{
    public class ComplaintRegistrationController : Controller
    {


        private decimal _filesize = 3;

        public decimal Filesize
        {
            get { return _filesize; }
            set { _filesize = value; }
        }
        // GET: DirectComplaintRegister/ComplaintRegistration/Create
        public ActionResult Create()
        {
            COMPLAINT obj = new COMPLAINT();

            obj.OfficeCodeCollection = RepositoryArea.GetOfficeList_Create("3");
            obj.ComplaintTypeCollection = RepositoryArea.GetComplaintTypeList("0");
            obj.ComplaintTypeCollection.RemoveAt(0);
            //obj.ComplaintAssignCollection = Repository.GetAssigneeList();
            //obj.ComplaintSourceCollection = Repository.ComplaintSourceJson_Register();
            return View(obj);
        }

        // POST: DirectComplaintRegister/ComplaintRegistration/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(COMPLAINT modelComplaint)
        {
            string error = string.Empty;
            string iMageName = string.Empty;
            HttpPostedFileBase file = Request.Files["ImageData"];
            HttpPostedFileBase file1 = Request.Files["ImageData1"];
            modelComplaint.SDO_CODE = modelComplaint.OFFICE_CODE_ID.ToString();
            COMPLAINT obj = new COMPLAINT();
            obj.OfficeCodeCollection = RepositoryArea.GetOfficeList(modelComplaint.OFFICE_CODE_ID.ToString());
            obj.ComplaintTypeCollection = RepositoryArea.GetComplaintTypeList("0");
            obj.ComplaintTypeCollection.RemoveAt(0);

            if (modelComplaint.OFFICE_CODE_ID == 0 || string.IsNullOrEmpty(modelComplaint.JE_AREA))
            {
                return View(obj);
            }
            if (modelComplaint.ImageData != null && modelComplaint.ImageData != "")
            {
                if (!IsImageValidate(file, ref error))
                {
                    TempData["AlertMessage"] = error;
                    return View(obj);
                }
            }
            if (modelComplaint.ImageData1 != null && modelComplaint.ImageData1 != "")
            {
                if (!IsImageValidate(file1, ref error))
                {
                    TempData["AlertMessage"] = error;
                    return View(obj);
                }
            }
            try
            {
                modelComplaint.ImageData = file.FileName;
                modelComplaint.ImageData1 = file1.FileName;
                Int64 complaintNo = await RepositoryArea.SaveComplaintRegistration(modelComplaint);
                if (complaintNo > 0)
                {
                    if (modelComplaint.ImageData != null && modelComplaint.ImageData !="")
                    {
                        string path = System.IO.Path.Combine(Server.MapPath("~/areas/directComplaintRegister/images"), modelComplaint.ImageData);
                        // file is uploaded
                        file.SaveAs(path);
                    }
                    if (modelComplaint.ImageData1 != null && modelComplaint.ImageData1 != "")
                    {
                        string path1 = System.IO.Path.Combine(Server.MapPath("~/areas/directComplaintRegister/images"), modelComplaint.ImageData1);
                    // file is uploaded
                        file1.SaveAs(path1);
                    }
                    TempData["AlertMessage"] = "Complaint No. " + complaintNo.ToString() + " Generated Successfully...!";
                    return RedirectToAction("Create", obj);
                }
                else
                {
                    TempData["AlertMessage"] = "Error in complaint Generating...!";
                    return View(obj);
                }
            }
            catch
            {
                return View(obj);
            }
        }

        public JsonResult GetComplaintSearch(string Complaint_no) //It will be fired from Jquery ajax call
        {
            ModelSearchComplaint dataObject = new ModelSearchComplaint();
            List<ModelSearchComplaint> data = new List<ModelSearchComplaint>();
            if (ModelState.IsValid)
            {
                try
                {

                    dataObject.draw = 1;
                    dataObject.start = 0;
                    dataObject.length = 10;

                    // Initialization.   
                    dataObject.COMPLAINT_NO = Convert.ToInt64(Complaint_no);
                    dataObject.KNO = 0;
                    dataObject.MOBILE_NO = "0";
                    dataObject.COMPLAINT_TYPE = "0";
                    dataObject.OFFICE_ID = "0";
                    dataObject.COMPLAINT_status = "0";
                    dataObject.COMPLAINT_SOURCE = "1";
                    dataObject.fromdate = "0";
                    dataObject.todate = "";
                    dataObject.assigned_status = "0";
                    //if (dataObject.OFFICE_ID is null || dataObject.OFFICE_ID == "0")
                    //{
                    //    dataObject.OFFICE_ID = Session["OFFICE_ID"].ToString();
                    //}
                    data = RepositoryArea.GetComplaintDetails(dataObject);
                    int count = data.Count() > 0 ? data[0].Total : 0;
                    return Json(data, JsonRequestBehavior.AllowGet);

                }
                catch
                {
                    return Json(data, JsonRequestBehavior.AllowGet);
                }

            }
            else
            {
                return Json("error", JsonRequestBehavior.AllowGet);
            }
        }

        public byte[] ConvertToBytes(HttpPostedFileBase image)
        {
            byte[] imageBytes = null;
            BinaryReader reader = new BinaryReader(image.InputStream);
            imageBytes = reader.ReadBytes((int)image.ContentLength);
            return imageBytes;
        }
        [HttpGet]
        public ActionResult GetOfficeListByParentOffice(int parentOfficeId)
        {
            List<ModelOfficeCode> lstOfficeCode = new List<ModelOfficeCode>();
            ModelOfficeCode objBlank = new ModelOfficeCode();
            //objBlank.OfficeId = "0";
            //objBlank.OfficeCode = "Select JEn Area";
            //lstOfficeCode.Insert(0, objBlank);

            SqlParameter[] param ={
                    new SqlParameter("@Parent_OfficeId",parentOfficeId) };
            DataSet ds =  SqlHelper.ExecuteDataset(HelperClass.Connection, CommandType.StoredProcedure, "GetOfficeByParent", param);

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objBlank = new ModelOfficeCode();
                objBlank.OfficeCode = dr.ItemArray[0].ToString();
                objBlank.OfficeId = dr.ItemArray[1].ToString();
                lstOfficeCode.Add(objBlank);
            }
            return Json(lstOfficeCode, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetList(int OfficeCode, int ConsumerType, string Searchparm)
        {
            List<COMPLAINT> data = new List<COMPLAINT>();
            data = RepositoryArea.GetPreviousComplaint(OfficeCode, ConsumerType, Searchparm);
            return Json(data, JsonRequestBehavior.AllowGet);

        }

        [HttpGet]
        public ActionResult GetSubComplaintByComplaintTypeId(int ComplaintTypeId)
        {
            List<ModelComplaintType> lstSubComplaintType = new List<ModelComplaintType>();

            lstSubComplaintType = RepositoryArea.GetSubComplaintTypeList(ComplaintTypeId);


            return Json(lstSubComplaintType, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult ConsumerComplaintList(string searchparam)
        {
            List<COMPLAINT> cOMPLAINT = new List<COMPLAINT>();
            cOMPLAINT = RepositoryArea.GetConsumerDetails(searchparam);

            return PartialView("~/Areas/DirectComplaintRegister/Views/ComplaintRegistration/_ConsumerContactSelection.cshtml", cOMPLAINT);
        }
        [HttpGet]
        public ActionResult checkPowerOutage(int officecode)
        {
            List<ModelPowerOutageList> modelPowerOutageLists = new List<ModelPowerOutageList>();
            ModelPowerOutageList modelPowerOutages = new ModelPowerOutageList();
            ModelPowerOutage modelPowerOutage = new ModelPowerOutage();


            SqlParameter[] param ={
                    new SqlParameter("@OFFICE_CODE",officecode) };
            DataSet ds = SqlHelper.ExecuteDataset(HelperClass.Connection, CommandType.StoredProcedure, "CHECK_POWER_OUTAGE", param);

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                modelPowerOutages = new ModelPowerOutageList();
                modelPowerOutages.OFFICE_CODE = dr.ItemArray[0].ToString();
                modelPowerOutages.START_TIME = dr.ItemArray[1].ToString();
                modelPowerOutages.END_TIME = dr.ItemArray[2].ToString();
                modelPowerOutages.COLONIES = dr.ItemArray[3].ToString();
                modelPowerOutages.SHUT_DOWN_INFORMATION = dr.ItemArray[4].ToString();
                modelPowerOutages.INFORMATION_SOURCE = dr.ItemArray[5].ToString();
                modelPowerOutageLists.Add(modelPowerOutages);

            }

            if (modelPowerOutageLists.Count > 0)
            {
                modelPowerOutage.modelPowerOutages = modelPowerOutageLists;
                modelPowerOutage.IsData = 1;
                return PartialView("~/Areas/DirectComplaintRegister/Views/ComplaintRegistration/_powerOutage.cshtml", modelPowerOutage);
            }
            else
            {
                modelPowerOutages = new ModelPowerOutageList();
                modelPowerOutageLists.Add(modelPowerOutages);
                modelPowerOutage.modelPowerOutages = modelPowerOutageLists;
                modelPowerOutage.IsData = 0;
                return null;
            }
        }

  
        public bool IsImageValidate(HttpPostedFileBase file,ref string errorMessage)
        {
            try
            {
                var supportedTypes = new[] { "jpeg", "jpg", "png" };
                var fileExt = System.IO.Path.GetExtension(file.FileName).Substring(1);
                if (!supportedTypes.Contains(fileExt))
                {
                    errorMessage = "Error : File Extension Is InValid - Only Upload jpeg/jpg/png File";
                    return false;
                }
                else if (file.ContentLength > (Filesize * 1024 * 1024))
                {
                    errorMessage = "Error : File size Should Be UpTo " + Filesize + "MB";
                    return false;
                }
                else
                {
                    errorMessage = "File Is ready to Uploaded";
                    return true;
                }
            }
            catch (Exception ex)
            {
                errorMessage = "Error : Upload Container Should Not Be Empty or Contact Admin";
                return false;
            }
        }

        public ActionResult webchat()
        {
            return View();
        }
        [HttpGet]
        public ActionResult closeSearch()
        {
            ComplaintTracker.Models.COMPLAINT objComplaint = new ComplaintTracker.Models.COMPLAINT();
            ViewBag.fromDate = DateTime.Now;
            ViewBag.toDate = DateTime.Now.AddDays(1);
            ViewBag.RoleID = Session["Roll_ID"];
            objComplaint.ComplaintTypeCollection = Repository.GetComplaintTypeList("0");
            objComplaint.OfficeCodeCollection = Repository.GetOfficeList_Create("3");
            return View(objComplaint);
        }

        #region GetComplaintSearch
        [HttpPost]
        public JsonResult GetComplaintSearch(DataTableAjaxPostModel model) //It will be fired from Jquery ajax call
        {

            ModelSearchComplaint dataObject = new ModelSearchComplaint();
            List<ModelSearchComplaint> data = new List<ModelSearchComplaint>();
            if (ModelState.IsValid)
            {
                try
                {

                    dataObject.draw = model.draw;
                    dataObject.start = model.start;
                    dataObject.length = model.length;

                    // Initialization.   
                    dataObject.COMPLAINT_NO = 0;
                    dataObject.KNO = 0;
                    dataObject.MOBILE_NO = "0";
                    dataObject.COMPLAINT_TYPE = Convert.ToString(Request.Form.GetValues("COMPLAINT_TYPE")[0]);
                    dataObject.OFFICE_ID = Convert.ToString(Request.Form.GetValues("OFFICE_CODE_ID")[0]); ;
                    dataObject.COMPLAINT_status = "1";
                    dataObject.COMPLAINT_SOURCE = "0";
                    dataObject.fromdate = Convert.ToString(Request.Form.GetValues("fromdate")[0]);
                    dataObject.todate = Convert.ToString(Request.Form.GetValues("todate")[0]);
                    dataObject.assigned_status = "0";

                    data = RepositoryArea.GetComplaintDetailsForClose(dataObject);
                    int count = data.Count() > 0 ? data[0].Total : 0;
                    return Json(new { draw = dataObject.draw, recordsFiltered = count, recordsTotal = count, data = data }, JsonRequestBehavior.AllowGet);

                }
                catch
                {
                    return Json(new { draw = dataObject.draw, recordsFiltered = 0, recordsTotal = 0, data = data }, JsonRequestBehavior.AllowGet);
                }

            }
            else
            {
                return Json("error", JsonRequestBehavior.AllowGet);
            }
        }
        #endregion
        [HttpGet]
        public JsonResult Close(string closeIds)
        {
            ComplaintTracker.Models.Response data = RepositoryArea.ComplaintClose(closeIds);

            if (data.status == "-1")
            {
                data.message = "Some error occured in close Complaint No. " + closeIds;
            }
            else
            {
                data.message = "Complaint No. " + closeIds + " closed Successfully ...";
            }

            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}
