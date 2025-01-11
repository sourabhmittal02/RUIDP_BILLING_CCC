using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ComplaintTracker.Models
{
    public class PaymentDetail
    {
        public int Id { get; set; }
        public string Kno { get; set; }
        public string Payment_Date { get; set; }
        public double Paid_Amount { get; set; }
        public string TRANSACTION_NO { get; set; }
    }
}