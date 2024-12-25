using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ComplaintTracker.Models
{
    public class ModelPaymentInfo
    {
        public string cons_no { get; set; }
        public Master Master { get; set; }
        public Billing billing { get; set; }
        public Payment payment { get; set; }

    }
    public class Master
    {
        public string name { get; set; }
        public string address1 { get; set; }
        public string address2 { get; set; }
        public string tariff { get; set; }
        public string load { get; set; }
    }
    public class Billing
    {
        public List<string> billdate { get; set; }
        public List<string> unit { get; set; }
        public List<string> billamount { get; set; }
        public List<string> billmonth { get; set; }
    }

   

    public class Payment
    {
        public List<string> paiddate { get; set; }
        public List<string> paidamount { get; set; }
        public List<string> receiptno { get; set; }
        public List<string> paymode { get; set; }
    }

    public class ModelBillingRequest
    {
        public string cons_no { get; set; }
    }

}