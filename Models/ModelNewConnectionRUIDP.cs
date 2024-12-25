using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ComplaintTracker.Models
{
    public class ModelNewConnectionRUIDP
    {
        public DateTime OrderDate { get; set; }
        public int Order_Type { get; set; }
        public decimal Amount { get; set; }
        public Int64 Sno { get; set; }
        public string ApplicantName { get; set; }
        public Int64 RegNo { get; set; }
        public Int64 Prev_acc_no { get; set; }
        public string father_name { get; set; }
        public Int64 Adhaar_no { get; set; }
        public Int64 Bhamasha_no { get; set; }
        public string WaterConnAdd { get; set; }
        public Int64 Neighbour_bill_ac { get; set; }
        public Int64 SewerageCode { get; set; }
        public DateTime oldMetercurr { get; set; }
        public int PlotArea { get; set; }
        public int BuildingArea { get; set; }
        public int BuildingType { get; set; }
        public string AppStats { get; set; }
        public string Address { get; set; }
        public string OffAddress { get; set; }
        public string ConnectionPurpose { get; set; }
        public int ServiceLineType { get; set; }
        public int WaterConnType { get; set; }
        public int DocumentSubmitted { get; set; }
        public Int64 PipeDist { get; set; }
        public int RoadRepairAmm { get; set; }
        public Int64 Receipt_no { get; set; }
        public DateTime Receipt_date { get; set; }
        public int WaterConnecionSecDep { get; set; }
        public int OtherDep { get; set; }
        public int TotalDep { get; set; }
        public DateTime Connection_date { get; set; }
        public Int64 JE_Card_No { get; set; }
        public DateTime Commission_date { get; set; }
        public Int64 R2_Register_no { get; set; }
        public Int64 AllotedAcc_no { get; set; }
        public Int64 Service_no { get; set; }
    }
}