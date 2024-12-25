using Microsoft.Ajax.Utilities;
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
using System.Xml.Linq;

namespace ComplaintTracker.Models
{

   
    public class ModelAdvice
    {
        public string Kno { get; set; }
        public string name { get; set; }
        public string Address { get; set; }
        public string MeterNo { get; set; }
        public string PrevReading { get; set; }

        public string PrevReadingdate { get; set; }
        public string CurrentReadingdate { get; set; }


        public DateTime OrderDate1 { get; set; }
        
        public int Order_Type { get; set; }


        public decimal Amount { get; set; }
        public string Receipt_no { get; set; }

        public DateTime Receipt_Date { get; set; }


        public string New_Meter_No { get; set; }
        public string Old_Meter_Current_reading { get; set; }
        public string Current_Meter_Previous_reading { get; set; }
        public string Current_Meter_Current_reading { get; set; }
        public string MeterChangeDate { get; set; }

        public DateTime OrderDate2 { get; set; }
        public string Meter_Final_reading { get; set; }


        public List<ComplaintHistory> complaintHistories    { get; set; }

}


    public class ComplaintHistory
    {
        public string Kno { get; set; }
        public string name{ get; set; }
        public string Address{ get; set; }
        public string MeterNo { get; set; }
        public string PrevReading { get; set; }
        public string PrevReadingdate { get; set; }
        public string CurrentReadingdate { get; set; }


    }


    public class ModelStatus
    {
        public string StatusId { get; set; }
        public string StatusMessage { get; set; }
    }

   
  }

