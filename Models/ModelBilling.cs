using Microsoft.Ajax.Utilities;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Security.Cryptography.Xml;
using System.Security.Principal;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Services.Description;
using System.Xml.Linq;

namespace ComplaintTracker.Models
{


    public class ModelBilling
    {
        public string Kno { get; set; }
        public string name { get; set; }
        public string Address { get; set; }
        public string Last_Reading { get; set; }
        public string Service_Status { get; set; }
        public string Last_Reading_Date { get; set; }
        public string Binder_No { get; set; }
        public string Account_No { get; set; }


        [Required(ErrorMessage = "The Current Reading Date must be submitted")]
        public DateTime Current_Reading_Date { get; set; }
        [Required]
        public string Current_Reading { get; set; }
        [Required]
        public string BIll_Month { get; set; }
        [Required]
        public string BIll_Year { get; set; }



        public List<BillingHistory> billingHistories { get; set; }

    }


    public class BillingHistory
    {
        public string Kno { get; set; }
        public string name { get; set; }
        public string Address { get; set; }
        public string Last_Reading { get; set; }
        public string Service_Status { get; set; }
        public string Last_Reading_Date { get; set; }
        public string Binder_No { get; set; }
        public string Account_No { get; set; }

    }

    public class ModelPayment
    {
        public string Kno { get; set; }
        public string name { get; set; }
        public string Address { get; set; }

        [Required(ErrorMessage = "The Payment Date must be submitted")]
        public DateTime Payment_Date { get; set; }
        [Required]
        public string Payment_Amount { get; set; }
        [Required]
        public string Transaction_No { get; set; }
        [Required]
        public string SelectedPaymentMode { get; set; }



        public List<PaymentModes> paymentModes { get; set; }

    }

    public class ExcelData
    {
        public int Id { get; set; }
        public string KNO { get; set; }
        public DateTime PaymentDate { get; set; }
        public double PaidAmount { get; set; }
        public string TransactionNo { get; set; }
        public string PaymentMode { get; set; }
    }
    public class PaymentModes
    {
        public int ID { get; set; }
        public string PaymentMode { get; set; }
    }

    public class ModelSundry
    {
        public string Kno { get; set; }
        public string name { get; set; }
        public string Address { get; set; }

        [Required(ErrorMessage = "The Payment Date must be submitted")]
        public DateTime Sundry_Date { get; set; }
        [Required]
        public string Sundry_Amount { get; set; }
        [Required]
        public string Remark { get; set; }
        [Required]
        public string SelectedsundrySide { get; set; }



        public List<SundrySide> sundrySides { get; set; }

    }
    public class SundrySide
    {
        public int ID { get; set; }
        public string sundrySide { get; set; }
    }
    public class ModelBillingPrint
    {
        public string KNO { get; set; }
        public string Chokdi { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Account_No { get; set; }
        public string Binder_No { get; set; }
        public string Office_Code { get; set; }
        public string Category { get; set; }
        public string Meter_No { get; set; }
        public string Previous_Reading_Date { get; set; }
        public string Payment_Date { get; set; }
        public string Bill_Month { get; set; }
        public string Bill_Year { get; set; }
        public string Current_Reading_Date { get; set; }
        public string Bill_Date { get; set; }
        public string Due_Date_Cash { get; set; }
        public string Due_Date_Cheque { get; set; }
        public string Current_Reading { get; set; }
        public string Precious_Reading { get; set; }
        public string Net_Consumption { get; set; }
        public string Pending_Amount { get; set; }
        public string Service_No { get; set; }
        public string Old_Account_No { get; set; }
        public string Meter_Result { get; set; }
        public string Adjusted_Consumption_Amount_Water { get; set; }
        public string Adjusted_Consumption_Amount_Sewerage { get; set; }
        public string Pure_Consumption_Amount_Water { get; set; }
        public string Pure_Consumption_Amount_Sewerage { get; set; }
        public string Excess_Sewerage { get; set; }
        public string Government_Rebate { get; set; }
        public string Water_Amount { get; set; }
        public string Sewerage_Amount { get; set; }
        public string Meter_Service_Charge { get; set; }
        public string Infrastructure_Development_Surcharge { get; set; }
        public string Fix_Charge { get; set; }

        public string LPS { get; set; }
        public string Other { get; set; }
        public string Interest { get; set; }
        public string Amount_On_Due_Date { get; set; }
        public string Amount_After_Due_Date { get; set; }

        public string zoneId { get; set; }
        //public List<ModelZone> LstBillingZone { get; set; }
        public List<ModelChokdi> LstBillingChokdi { get; set; }
    }

    public class ModelZone
    {
        public string ZoneId { get; set; }
        public string ZoneName { get; set; }
    }
    public class ModelChokdi
    {
        public string Chokdi { get; set; }
    }

    public class ModelDma
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }

    public class ModelBinder
    {
        public string Id { get; set; }
        public string Name { get; set; }


    }

}


