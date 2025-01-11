using MathNet.Numerics.Distributions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;

namespace ComplaintTracker.Models
{
    public class CAT
    {
        public int ID { get; set; }
        public string KNO { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string MobileNo { get; set; }
        public string Binder_No { get; set; }
        public string AccountNo { get; set; }
        public string Service_Status { get; set; }
    }

    public class MeterDetail
    {
        public int ID { get; set; }
        public string KNO { get; set; }
        public string MeterNo { get; set; }
        public string MeterInstallDate { get; set; }
        public string Active { get; set; }
    }
    public class AdviceDetail
    {
        public int ID { get; set; }
        public string KNO { get; set; }
        public string OrderType{ get; set; }
        public string OrderDate{ get; set; }
        public string Status { get; set; }
        public string Amount { get; set; }
        public string ReceiptNo { get; set; }
        public string ReceiptDate { get; set; }
        public string MeterNo { get; set; }
        public string TransactionType { get; set; }
        public string PrevReadingDate { get; set; }
        public string CurrReadingDate { get; set; }
        public string PrevReading { get; set; }
        public string CurrReading { get; set; }
    }
    public class ConsumptionDetail
    {
        public int ID { get; set; }
        public string KNO { get; set; }
        public string BillNo{ get; set; }
        public string BillMonth{ get; set; }
        public string BillYear { get; set; }
        public string MeterNo { get; set; }
        public string PrevReadingDate { get; set; }
        public string CurrReadingDate { get; set; }
        public string PrevReading { get; set; }
        public string CurrReading { get; set; }
        public string Consumption { get; set; }
    }
}