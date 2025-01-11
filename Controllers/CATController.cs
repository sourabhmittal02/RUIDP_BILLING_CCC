using ComplaintTracker.DAL;
using ComplaintTracker.Models;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ComplaintTracker.Controllers
{
    public class CATController : Controller
    {
        // GET: CAT
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(string kno)
        {
            List<CAT> lst = new List<CAT>(); 
            lst = Repository.GetConsumberDetailForCAT(kno);
            ViewBag.Kno = kno;
            return View(lst);
        }

        public JsonResult GetPaymentDetail(string kno)
        {
            List<PaymentDetail> data = new List<PaymentDetail>();
            data = Repository.GetPaymentDetail(kno);
            return Json(data, JsonRequestBehavior.AllowGet);

        }
        public JsonResult GetMeterDetail(string kno)
        {
            List<MeterDetail> data = new List<MeterDetail>();
            data = Repository.GetMeterDetail(kno);
            return Json(data, JsonRequestBehavior.AllowGet);

        }
        public JsonResult GetAdviceDetail(string kno)
        {
            List<AdviceDetail> data = new List<AdviceDetail>();
            data = Repository.GetAdviceDetail(kno);
            return Json(data, JsonRequestBehavior.AllowGet);

        }
        public JsonResult GetConsumeDetail(string kno)
        {
            List<ConsumptionDetail> data = new List<ConsumptionDetail>();
            data = Repository.GetConsumeDetail(kno);
            return Json(data, JsonRequestBehavior.AllowGet);

        }

        public ActionResult ReportBillingPrint(string kno)
        {
            List<ModelBillingPrint> records = new List<ModelBillingPrint>();
            records = Repository.GetBillDetail(kno);


            //string imagePath = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "templates", "bill_page-0001.jpg");

            // Output file path
            string appDataPath = Server.MapPath("~/Templates");
            string outputPath1 = System.IO.Path.Combine(Directory.GetCurrentDirectory());
            string outputPath = appDataPath + "/filled_form.pdf";
            string fontPath = appDataPath + "/AnekDevanagari.ttf";

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
                    cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, records[i].Address ?? " ", startingX[0], currentY + 15, 0);
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
            return File(stream, "application/pdf", "Bill.pdf");
        }
    }
}
