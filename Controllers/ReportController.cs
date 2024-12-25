using ComplaintTracker.DAL;
using ComplaintTracker.Models;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Security.Policy;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.Xml.Linq;
using static ComplaintTracker.JqueryDatatableParam;
using System.Reflection;
using System.Windows;
using System.Windows.Media;
using Rotativa;
using IronSoftware;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using IronXL.Styles;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace ComplaintTracker.Controllers
{
    [Authorize]
    public class ReportController : Controller
    {
        // GET: Report
        public ActionResult ReportRawComplaint()
        {
            ViewBag.fromDate = DateTime.Now;
            ViewBag.toDate = DateTime.Now.AddDays(1);
            COMPLAINT objComplaint = new COMPLAINT();
            objComplaint.ComplaintTypeCollection = Repository.GetComplaintTypeList("0");
            return View(objComplaint);
        }


        //====================================================================

        #region Raw Complaint
        [HttpPost]
        [Authorize]
        public JsonResult ReportRawComplaintSearch(DataTableAjaxPostModel model)
        {
            ModelReport dataObject = new ModelReport();
            List<ModelRawComplaintReport> data = new List<ModelRawComplaintReport>();
            if (ModelState.IsValid)
            {
                try
                {

                    dataObject.draw = model.draw;
                    dataObject.start = model.start;
                    dataObject.length = model.length;

                    // Initialization.   
                    dataObject.fromdate = Convert.ToString(Request.Form.GetValues("fromdate")[0]);
                    dataObject.todate = Convert.ToString(Request.Form.GetValues("todate")[0]);
                    dataObject.OfficeCode = Convert.ToString(Request.Form.GetValues("OfficeCode")[0]);
                    dataObject.ComplaintType = Convert.ToInt16(Request.Form.GetValues("ComplaintType")[0]);
                    dataObject.ComplaintSource = Convert.ToInt16(Request.Form.GetValues("ComplaintSource")[0]);
                    if (dataObject.OfficeCode is null || dataObject.OfficeCode == "0")
                    {
                        dataObject.OfficeCode = Session["OFFICE_ID"].ToString();
                    }
                    data = Repository.ReportRawComplaintData(dataObject);
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


        public ActionResult QueryBuilder()
        {
            ViewBag.Fields = new List<string> { "KNO", "Complaint no", "Mobile", "Office code", "Complaint date", "Complaint type", "Complaint status", "Complaint source" };
            COMPLAINT objComplaint = new COMPLAINT();
            //objComplaint.ComplaintTypeCollection = Repository.GetComplaintTypeList("0"); 
            ViewBag.Com_Type = Repository.GetComplaintTypeList("0");
            ViewBag.Com_Source = Repository.ComplaintSourceJson();
            ViewBag.OfficeCode = Repository.GetOfficeList(Session["OFFICE_ID"].ToString());
            return View();
        }
        public ActionResult AddQuery(RuleCriteria dataObject)
        {
            string sql = "";
            foreach (var item in dataObject.Meta)
            {
                if (item.criteria == "Equal")
                    sql += item.item + "=" + item.val + " " + item.Condition + " ";
                else
                    sql += item.item + " like %" + item.val + "% " + item.Condition + " ";

            }
            List<ModelQueryBuilderReport> data = new List<ModelQueryBuilderReport>();
            data = Repository.ReportQueryBuilder(sql, Session["OFFICE_ID"].ToString());
            var jsonData = data;
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult ExportToExcelRawComplaint(string fromDate, string toDate, string ddlSource, string ddlOfficecode, string ComplaintTypeId)
        {

            List<ModelRawComplaintReport> lstdata = new List<ModelRawComplaintReport>();
            ModelReport dataObject = new ModelReport();

            dataObject.draw = 0;
            dataObject.start = 0;
            dataObject.length = 10000000;

            // Initialization.   
            dataObject.fromdate = fromDate;
            dataObject.todate = toDate;
            dataObject.OfficeCode = Convert.ToString(ddlOfficecode);
            dataObject.ComplaintType = Convert.ToInt16(ComplaintTypeId);
            dataObject.ComplaintSource = Convert.ToInt16(ddlSource);

            lstdata = Repository.ReportRawComplaintData(dataObject);
            List<RawComplaintExcel> list = lstdata.AsEnumerable()
                                      .Select(o => new RawComplaintExcel
                                      {
                                          Circle = o.Circle,
                                          Division = o.Division,
                                          ComplaintNo = o.COMPLAINT_NO,
                                          SDOCode = o.SDO_CODE,
                                          SubDivisionName = o.SubDivisionName,
                                          KNO = o.KNO,
                                          Name = o.Name,
                                          FatherName = o.FatherName,
                                          Address = o.Address,
                                          MobileNo = o.MobileNo,
                                          AlternateNumber = o.AlternateNo,
                                          ComplaintType = o.ComplaintType,
                                          SubComplaintType = o.SubComplaintType,
                                          ComplaintDateTime = o.ComplaintDate,
                                          ClosedDateTime = o.ClosedDate,
                                          Duration = o.Duration,
                                          AreaCode = o.AreaCode,
                                          CurrentStatus = o.CurrentStatus,
                                          ComplaintSource = o.SOURCE_NAME,
                                          CreatedByUserID = o.CreatedUserID,
                                          ClosedByUserID = o.ClosedUserID

                                      }).ToList();

            var gv = new GridView();
            gv.DataSource = list;
            gv.DataBind();
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment; filename=Raw_Complaint.xls");
            Response.ContentType = "application/ms-excel";
            Response.Charset = "";

            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                {
                    gv.RenderControl(htw);
                    Response.Output.Write(sw.ToString());
                    Response.Flush();
                    Response.End();
                }
            }
            return View("Index");
        }

        public ActionResult ReportBillingInfo()
        {
            ModelPaymentInfo modelDashboardHaresments = new ModelPaymentInfo();

            return View(modelDashboardHaresments);
        }

        [HttpPost]
        public ActionResult GetReportBillingInfo()
        {
            ModelPaymentInfo modelDashboardHaresments = new ModelPaymentInfo();
            ModelBillingRequest modelBillingRequest = new ModelBillingRequest();

            string consumer_No = Request.Form.GetValues("consumerno")[0];
            modelBillingRequest.cons_no = consumer_No;
            modelDashboardHaresments = Repository.GetPaymentBillInfoFromCMS(modelBillingRequest);
            ViewBag.cno = consumer_No;
            return View("ReportBillingInfo", modelDashboardHaresments);
        }
        public ActionResult ReportSDOLogin()
        {
            List<ModelReportSdoLogin> lstmodelReportSdoLogins = new List<ModelReportSdoLogin>();
            ViewBag.fromDate = DateTime.Now;
            return View(lstmodelReportSdoLogins);
        }

        [HttpPost]
        public ActionResult GetReportSDOLoginTime()
        {
            ModelBillingRequest modelBillingRequest = new ModelBillingRequest();
            List<ModelReportSdoLogin> lstmodelReportSdoLogins = null;
            string consumer_No = Request.Form.GetValues("fromDate")[0];
            modelBillingRequest.cons_no = consumer_No;
            lstmodelReportSdoLogins = Repository.GetSdoLoginTime(modelBillingRequest);
            ViewBag.fromDate = Convert.ToDateTime(consumer_No);
            return View("ReportSDOLogin", lstmodelReportSdoLogins);
        }
        #region Ruidp
        public ActionResult ReportBillingPrint()
        {
            ModelBillingPrint modelDashboardHaresments = new ModelBillingPrint();
            //modelDashboardHaresments.LstBillingZone = Repository.GetZoneList();
            modelDashboardHaresments.LstBillingChokdi = Repository.GetChokdiList();

            return View(modelDashboardHaresments);
        }

        public ActionResult ReportLedgerData()
        {
            ModelBillingPrint modelDashboardHaresments = new ModelBillingPrint();
            //modelDashboardHaresments.LstBillingZone = Repository.GetZoneList();
            modelDashboardHaresments.LstBillingChokdi = Repository.GetChokdiList();

            return View(modelDashboardHaresments);
        }
        //[HttpGet]
        //public ActionResult GetList(string strchokdiId, string strbinderNo, string strbm, string strby)
        //{
        //    List<ModelBillingPrint> data = new List<ModelBillingPrint>();
        //    data = Repository.GetConsumerBillForPrint(strchokdiId, strbinderNo, strbm, strby);
        //    return PartialView("_printBill", data);
        //    //return View(data);
        //    //return RedirectToAction("PrintBill", data);
        //}

        [HttpPost]
        public ActionResult ReportLedgerData(string zoneId, string ddlBinder, string ddlBM, string ddlBY)
        {
            string choki_No = zoneId;
            string Bill_Month = ddlBM;
            string Bill_Year = ddlBY;

            DataTable records = Repository.GetLedgerForPrint(zoneId, ddlBinder, ddlBM, ddlBY);
            string appDataPath = Server.MapPath("~/Templates");
            string outputPath = appDataPath + "/Export.xls";
            string templatePath = appDataPath + "/template.xlsx";
            if (System.IO.File.Exists(outputPath))
            {
                System.IO.File.Delete(outputPath);
            }
            System.IO.File.Copy(templatePath, outputPath);
            //=======================================
            using (FileStream fs = new FileStream(templatePath, FileMode.Open, FileAccess.Read))
            {
                IWorkbook workbook = new XSSFWorkbook(fs);
                ISheet sheet = workbook.GetSheetAt(0); // Get the first sheet (or specify by name)
                NPOI.SS.UserModel.IFont baseFont = workbook.CreateFont();
                baseFont.FontHeightInPoints = 8;

                // Create a bold font style
                NPOI.SS.UserModel.IFont boldFont = workbook.CreateFont();
                boldFont.IsBold = true;
                boldFont.FontHeightInPoints = 8;

                // Create a bold cell style with a grey background
                ICellStyle boldStyleWithGreyBackground = workbook.CreateCellStyle();
                boldStyleWithGreyBackground.SetFont(boldFont);
                boldStyleWithGreyBackground.FillForegroundColor = NPOI.SS.UserModel.IndexedColors.Grey25Percent.Index;
                boldStyleWithGreyBackground.FillPattern = NPOI.SS.UserModel.FillPattern.SolidForeground;

                // Create a cell style with borders
                ICellStyle borderedStyle = workbook.CreateCellStyle();
                borderedStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
                borderedStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
                borderedStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
                borderedStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
                borderedStyle.SetFont(baseFont);

                // Start writing data from a specific row (e.g., row 2 to leave room for headers)
                int startRow = 6; // Adjust based on your template
                IRow excelRow1 = sheet.GetRow(2) ?? sheet.CreateRow(2);  // Row 3 (index 2)
                ICell cell1 = excelRow1.GetCell(0) ?? excelRow1.CreateCell(0);  // Column A (index 0)
                                                                                // Construct the text value
                object value1 = "बिल माह " + Bill_Month + "-" + Bill_Year;
                // Set the cell value
                cell1.SetCellValue(value1?.ToString() ?? string.Empty);
                IRow excelRow2 = sheet.GetRow(3) ?? sheet.CreateRow(3);  // Row 3 (index 2)
                ICell cell2 = excelRow2.GetCell(0) ?? excelRow2.CreateCell(0);  // Column A (index 0)
                value1 = "Chokdi No.: " + choki_No;
                cell2.SetCellValue(value1?.ToString() ?? string.Empty);

                for (int row = 0; row < records.Rows.Count; row++)
                {
                    IRow excelRow = sheet.GetRow(startRow + row) ?? sheet.CreateRow(startRow + row);
                    bool isBoldRow = false;
                    //for (int col = 0; col < records.Columns.Count; col++)
                    for (int col = 0; col < 23; col++)
                    {
                        ICell cell = excelRow.GetCell(col) ?? excelRow.CreateCell(col);
                        object value = records.Rows[row][col];
                        if (value != null && DateTime.TryParse(value.ToString(), out DateTime dateValue))
                        {
                            // If the value is a date, format it as 'dd-MM-yyyy'
                            value = dateValue.ToString("dd-MM-yyyy");
                        }
                        else if (value != null && double.TryParse(value.ToString(), out double numericValue))
                        {
                            if (col != 2 && col != 4 && col != 19)
                                value = numericValue.ToString("F2");
                        }
                        else if (value != null && value.ToString() == "Total")
                        {
                            value = "";
                            isBoldRow = true;
                        }
                        cell.SetCellValue(value?.ToString() ?? string.Empty);
                        cell.CellStyle = borderedStyle;
                    }
                    // If the row is marked as bold, apply the bold style to all cells in the row
                    if (isBoldRow)
                    {
                        for (int col = 0; col < 23; col++)
                        {
                            ICell cell = excelRow.GetCell(col);
                            ICellStyle boldBorderedGreyStyle = workbook.CreateCellStyle();
                            boldBorderedGreyStyle.CloneStyleFrom(borderedStyle); // Copy border style
                            boldBorderedGreyStyle.SetFont(boldFont); // Set the bold font
                            boldBorderedGreyStyle.FillForegroundColor = NPOI.SS.UserModel.IndexedColors.Grey25Percent.Index; // Set background color
                            boldBorderedGreyStyle.FillPattern = NPOI.SS.UserModel.FillPattern.SolidForeground; // Set fill pattern
                            cell.CellStyle = boldBorderedGreyStyle;
                        }
                    }
                }
                // Auto-size each column based on its content
                for (int col = 0; col < 23; col++)
                {
                    sheet.AutoSizeColumn(col);
                }

                // Save the updated Excel file to a memory stream
                using (var stream = new MemoryStream())
                {
                    workbook.Write(stream);
                    string fileName = "Export.xlsx";
                    string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    return File(stream.ToArray(), contentType, fileName);
                }
            }
            //=======================================

        }

        [HttpPost]
        public ActionResult ReportBillingPrint(string zoneId, string ddlBinder, string ddlBM, string ddlBY)
        {
            List<ModelBillingPrint> records = new List<ModelBillingPrint>();
            records = Repository.GetConsumerBillForPrint(zoneId, ddlBinder, ddlBM, ddlBY);


            //string imagePath = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "templates", "bill_page-0001.jpg");

            // Output file path
            string appDataPath = Server.MapPath("~/Templates");
            string outputPath1 = System.IO.Path.Combine(Directory.GetCurrentDirectory());
            string outputPath = appDataPath+ "/filled_form.pdf";
            string fontPath = appDataPath+"/AnekDevanagari.ttf";

            // Coordinates and spacing
            int[] startingX = { 50, 160, 350, 50, 150, 350 }; // X-coordinate
            int[] startingY = { 535, 340, 145, 520, 310, 130 }; // Starting Y-coordinate for the first record
            int verticalSpacing = 190; // Space between records
            int recordsPerPage = 3; // Number of records per page

            // Generate PDF with overlay text
            using (var fs = new FileStream(outputPath, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                // Set the document to landscape format (A4 rotated)
                Document document = new Document(iTextSharp.text.PageSize.A4.Rotate());
                iTextSharp.text.pdf.PdfWriter writer = iTextSharp.text.pdf.PdfWriter.GetInstance(document, fs);
                document.Open();

                // Add the image as a background
                //iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(imagePath);
                //img.ScaleToFit(iTextSharp.text.PageSize.A4.Rotate().Width, iTextSharp.text.PageSize.A4.Rotate().Height);
                //img.SetAbsolutePosition(0, 0);
                //img.RotationDegrees = 90; // Ensure no additional rotation
                //img.ScaleToFit(iTextSharp.text.PageSize.A4.Height, iTextSharp.text.PageSize.A4.Width);
                //img.SetAbsolutePosition(0, 0); // Position at bottom-left of page
                //document.Add(img);

                // Font settings
                PdfContentByte cb = writer.DirectContent;
                BaseFont bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.EMBEDDED);
                BaseFont unicodeFont = BaseFont.CreateFont(fontPath, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
                cb.SetColorFill(BaseColor.Black);
                cb.SetFontAndSize(bf, 10);

                int recordCount = 0; // Track the record count
                int j = 0;
                for (int i = 0; i < records.Count; i++)
                {
                    int currentY = 0;
                    // Start a new page after every 3 records
                    if (recordCount % recordsPerPage == 0)
                    {
                        if (recordCount > 0) document.NewPage(); // Start a new page
                        //document.Add(img); // Add the background image
                        currentY = (500 - (recordCount % recordsPerPage) * verticalSpacing);
                        j = 0;
                    }
                    else
                    {
                        currentY = 500 - (recordCount % recordsPerPage) * verticalSpacing - 20;
                    }

                    // Calculate dynamic positions for each record

                    //int currentY2 = 530 - (recordCount % recordsPerPage) * verticalSpacing;


                    // Print the record's fields
                    cb.BeginText();
                    cb.SetFontAndSize(unicodeFont, 10); // Set font and size before writing text
                    cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, records[i].Name ?? " ", startingX[0], currentY, 0);
                    cb.EndText();
                    cb.BeginText();
                    cb.SetFontAndSize(unicodeFont, 10); // Set font and size before writing text
                    cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, records[i].Address ?? " ", startingX[0], currentY+15, 0);
                    cb.EndText();

                    cb.BeginText();
                    cb.SetFontAndSize(bf, 10); // Set font and size before writing text
                    cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, records[i].Binder_No ?? " ", startingX[1], startingY[j], 0);
                    cb.EndText();

                    cb.BeginText();
                    cb.SetFontAndSize(bf, 10); // Set font and size before writing text
                    cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, records[i].Service_No ?? " ", startingX[1] + 50, startingY[j], 0);
                    cb.EndText();

                    cb.BeginText();
                    cb.SetFontAndSize(bf, 10); // Set font and size before writing text
                    cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, records[i].Account_No ?? " ", startingX[1] + 110, startingY[j], 0);
                    cb.EndText();

                    cb.BeginText();
                    cb.SetFontAndSize(bf, 10); // Set font and size before writing text
                    cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, records[i].Old_Account_No ?? " ", startingX[1] + 190, startingY[j], 0);
                    cb.EndText();

                    cb.BeginText();
                    cb.SetFontAndSize(bf, 10); // Set font and size before writing text
                    cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, records[i].Chokdi ?? " ", startingX[1] + 240, startingY[j], 0);
                    cb.EndText();

                    cb.BeginText();
                    cb.SetFontAndSize(bf, 10); // Set font and size before writing text
                    cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, records[i].Meter_Result ?? " ", startingX[1] + 290, startingY[j], 0);
                    cb.EndText();

                    cb.BeginText();
                    cb.SetFontAndSize(bf, 10); // Set font and size before writing text
                    cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, records[i].Category ?? " ", startingX[1] + 345, startingY[j], 0);
                    cb.EndText();

                    cb.BeginText();
                    cb.SetFontAndSize(bf, 10); // Set font and size before writing text
                    cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, records[i].Bill_Month + "-" + records[i].Bill_Year ?? " ", startingX[1] + 390, startingY[j], 0);
                    cb.EndText();

                    //=============Row 2=================================
                    cb.BeginText();
                    cb.SetFontAndSize(bf, 9); // Set font and size before writing text
                    cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, records[i].Meter_No ?? " ", startingX[1] + 10, startingY[j] - 33, 0);
                    cb.EndText();

                    cb.BeginText();
                    cb.SetFontAndSize(bf, 9); // Set font and size before writing text
                    cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, records[i].Current_Reading_Date ?? " ", startingX[1] + 77, startingY[j] - 33, 0);
                    cb.EndText();

                    cb.BeginText();
                    cb.SetFontAndSize(bf, 9); // Set font and size before writing text
                    cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, records[i].Current_Reading ?? " ", startingX[1] + 150, startingY[j] - 33, 0);
                    cb.EndText();

                    cb.BeginText();
                    cb.SetFontAndSize(bf, 9); // Set font and size before writing text
                    cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, records[i].Precious_Reading ?? " ", startingX[1] + 200, startingY[j] - 33, 0);
                    cb.EndText();

                    cb.BeginText();
                    cb.SetFontAndSize(bf, 9); // Set font and size before writing text
                    cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, records[i].Current_Reading_Date ?? " ", startingX[1] + 250, startingY[j] - 33, 0);
                    cb.EndText();

                    cb.BeginText();
                    cb.SetFontAndSize(bf, 9); // Set font and size before writing text
                    cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, records[i].Due_Date_Cash ?? " ", startingX[1] + 315, startingY[j] - 33, 0);
                    cb.EndText();

                    cb.BeginText();
                    cb.SetFontAndSize(bf, 9); // Set font and size before writing text
                    cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, records[i].Due_Date_Cash ?? " ", startingX[1] + 375, startingY[j] - 33, 0);
                    cb.EndText();

                    //======================Row 3====================================
                    cb.BeginText();
                    cb.SetFontAndSize(bf, 9); // Set font and size before writing text
                    cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, records[i].Net_Consumption ?? " ", startingX[1] + 20, startingY[j] - 70, 0);
                    cb.EndText();

                    cb.BeginText();
                    cb.SetFontAndSize(bf, 9); // Set font and size before writing text
                    cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, records[i].Adjusted_Consumption_Amount_Water ?? " ", startingX[1] + 60, startingY[j] - 70, 0);
                    cb.EndText();

                    cb.BeginText();
                    cb.SetFontAndSize(bf, 9); // Set font and size before writing text
                    cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, records[i].Net_Consumption ?? " ", startingX[1] + 120, startingY[j] - 70, 0);
                    cb.EndText();

                    cb.BeginText();
                    cb.SetFontAndSize(bf, 9); // Set font and size before writing text
                    cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, records[i].Adjusted_Consumption_Amount_Water ?? " ", startingX[1] + 185, startingY[j] - 70, 0);
                    cb.EndText();

                    cb.BeginText();
                    cb.SetFontAndSize(bf, 9); // Set font and size before writing text
                    cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, records[i].Water_Amount ?? " ", startingX[1] + 225, startingY[j] - 70, 0);
                    cb.EndText();

                    cb.BeginText();
                    cb.SetFontAndSize(bf, 9); // Set font and size before writing text
                    cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, records[i].Adjusted_Consumption_Amount_Water ?? " ", startingX[1] + 285, startingY[j] - 70, 0);
                    cb.EndText();

                    cb.BeginText();
                    cb.SetFontAndSize(bf, 9); // Set font and size before writing text
                    cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, records[i].Water_Amount ?? " ", startingX[1] + 325, startingY[j] - 70, 0);
                    cb.EndText();

                    cb.BeginText();
                    cb.SetFontAndSize(bf, 9); // Set font and size before writing text
                    cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, records[i].Sewerage_Amount ?? " ", startingX[1] + 380, startingY[j] - 70, 0);
                    cb.EndText();

                    cb.BeginText();
                    cb.SetFontAndSize(bf, 9); // Set font and size before writing text
                    cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, records[i].Government_Rebate ?? " ", startingX[1] + 410, startingY[j] - 70, 0);
                    cb.EndText();
                    //======================Row 4====================================
                    cb.BeginText();
                    cb.SetFontAndSize(bf, 9); // Set font and size before writing text
                    cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, records[i].Water_Amount ?? " ", startingX[1] - 140, startingY[j] - 120, 0);
                    cb.EndText();

                    cb.BeginText();
                    cb.SetFontAndSize(bf, 9); // Set font and size before writing text
                    cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, records[i].Sewerage_Amount ?? " ", startingX[1] - 80, startingY[j] - 120, 0);
                    cb.EndText();

                    cb.BeginText();
                    cb.SetFontAndSize(bf, 9); // Set font and size before writing text
                    cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, records[i].Excess_Sewerage ?? " ", startingX[1] - 40, startingY[j] - 120, 0);
                    cb.EndText();

                    cb.BeginText();
                    cb.SetFontAndSize(bf, 9); // Set font and size before writing text
                    cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, records[i].Meter_Service_Charge ?? " ", startingX[1], startingY[j] - 120, 0);
                    cb.EndText();

                    cb.BeginText();
                    cb.SetFontAndSize(bf, 9); // Set font and size before writing text
                    cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, records[i].Infrastructure_Development_Surcharge ?? " ", startingX[1] + 40, startingY[j] - 120, 0);
                    cb.EndText();

                    cb.BeginText();
                    cb.SetFontAndSize(bf, 9); // Set font and size before writing text
                    cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, records[i].Fix_Charge ?? " ", startingX[1] + 80, startingY[j] - 120, 0);
                    cb.EndText();

                    cb.BeginText();
                    cb.SetFontAndSize(bf, 9); // Set font and size before writing text
                    cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, records[i].Pending_Amount ?? " ", startingX[1] + 130, startingY[j] - 120, 0);
                    cb.EndText();

                    cb.BeginText();
                    cb.SetFontAndSize(bf, 9); // Set font and size before writing text
                    cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, records[i].Other ?? " ", startingX[1] + 200, startingY[j] - 120, 0);
                    cb.EndText();

                    cb.BeginText();
                    cb.SetFontAndSize(bf, 9); // Set font and size before writing text
                    cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, records[i].Interest ?? " ", startingX[1] + 240, startingY[j] - 120, 0);
                    cb.EndText();

                    cb.BeginText();
                    cb.SetFontAndSize(bf, 9); // Set font and size before writing text
                    cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, records[i].Amount_On_Due_Date ?? " ", startingX[1] + 285, startingY[j] - 120, 0);
                    cb.EndText();

                    cb.BeginText();
                    cb.SetFontAndSize(bf, 9); // Set font and size before writing text
                    cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, records[i].LPS ?? " ", startingX[1] + 345, startingY[j] - 120, 0);
                    cb.EndText();

                    cb.BeginText();
                    cb.SetFontAndSize(bf, 9); // Set font and size before writing text
                    cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, records[i].Amount_After_Due_Date ?? " ", startingX[1] + 410, startingY[j] - 120, 0);
                    cb.EndText();

                    //======================Table 2 - Row 1====================================
                    cb.BeginText();
                    cb.SetFontAndSize(bf, 9); // Set font and size before writing text
                    cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, records[i].Binder_No ?? " ", startingX[1] + 440, startingY[j], 0);
                    cb.EndText();

                    cb.BeginText();
                    cb.SetFontAndSize(bf, 9); // Set font and size before writing text
                    cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, records[i].Service_No ?? " ", startingX[1] + 475, startingY[j], 0);
                    cb.EndText();

                    cb.BeginText();
                    cb.SetFontAndSize(bf, 9); // Set font and size before writing text
                    cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, records[i].Binder_No ?? " ", startingX[1] + 525, startingY[j], 0);
                    cb.EndText();

                    cb.BeginText();
                    cb.SetFontAndSize(bf, 9); // Set font and size before writing text
                    cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, records[i].Account_No ?? " ", startingX[1] + 570, startingY[j], 0);
                    cb.EndText();

                    cb.BeginText();
                    cb.SetFontAndSize(bf, 10); // Set font and size before writing text
                    cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, records[i].Bill_Month + "-" + records[i].Bill_Year ?? " ", startingX[1] + 615, startingY[j], 0);
                    cb.EndText();
                    //======================Table 2 - Row 2====================================
                    cb.BeginText();
                    cb.SetFontAndSize(bf, 9); // Set font and size before writing text
                    cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, records[i].Due_Date_Cash ?? " ", startingX[1] + 450, startingY[j] - 35, 0);
                    cb.EndText();

                    cb.BeginText();
                    cb.SetFontAndSize(bf, 9); // Set font and size before writing text
                    cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, records[i].Due_Date_Cash ?? " ", startingX[1] + 525, startingY[j] - 35, 0);
                    cb.EndText();
                    //======================Table 2 - Row 3====================================
                    cb.BeginText();
                    cb.SetFontAndSize(bf, 9); // Set font and size before writing text
                    cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, records[i].Amount_On_Due_Date ?? " ", startingX[1] + 450, startingY[j] - 75, 0);
                    cb.EndText();

                    cb.BeginText();
                    cb.SetFontAndSize(bf, 9); // Set font and size before writing text
                    cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, records[i].Amount_After_Due_Date ?? " ", startingX[1] + 525, startingY[j] - 75, 0);
                    cb.EndText();
                    //======================Table 2 - Row 4====================================
                    cb.BeginText();
                    cb.SetFontAndSize(bf, 9); // Set font and size before writing text
                    cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, records[i].Account_No ?? " ", startingX[1] + 450, startingY[j] - 110, 0);
                    cb.EndText();

                    //cb.BeginText();
                    //cb.SetFontAndSize(bf, 9); // Set font and size before writing text
                    //cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, records[i].Binder_No ?? " ", startingX[1] + 505, startingY[j] - 105, 0);
                    //cb.EndText();

                    j++;
                    recordCount++;
                }

                document.Close();
            }

            // Return the PDF file to download
            var stream = System.IO.File.OpenRead(outputPath);
            return File(stream, "application/pdf", "filled_form.pdf");
        }

        public ActionResult PrintBill()
        {
            ModelBillingPrint data = new ModelBillingPrint();
            data.Name = "Ajay";
            return View(data);
        }

        [HttpGet]
        public ActionResult GetDmaById(string zoneId)
        {
            List<ModelDma> lstZone = new List<ModelDma>();

            lstZone = Repository.GetDmaById(zoneId);


            return Json(lstZone, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetBinderNo(string zoneId, string dmaId)
        {
            List<ModelBinder> lstBinder = new List<ModelBinder>();

            lstBinder = Repository.GetBinderNo(zoneId, dmaId);

            return Json(lstBinder, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetBinderNoByChokdi(string ChokdiId)
        {
            List<ModelBinder> lstBinder = new List<ModelBinder>();

            lstBinder = Repository.GetBinderNoByChokdi(ChokdiId);

            return Json(lstBinder, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ExportPdf()
        {
            ActionAsPdf result = new ActionAsPdf("GetList")
            {
                FileName = Server.MapPath("~/Content/Bill.pdf"),PageOrientation=Rotativa.Options.Orientation.Landscape,PageSize=Rotativa.Options.Size.A4
            };
            return result;
        }
        #endregion
    }
}
