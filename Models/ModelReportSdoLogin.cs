using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ComplaintTracker.Models
{
    public class ModelReportSdoLogin
    {
        public string OFFICE_ID {  get ; set; }
        public string OFFICE_NAME { get; set; }
        public string LOGIN_TIME { get; set; }
        public string Pending_Complaint_Count { get; set; }
    }
}