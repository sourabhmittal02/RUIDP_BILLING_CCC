using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ComplaintTracker.Models
{
    public class PaymentModeModel
    {
        public int modeId {  get; set; }
        public string modeName {  get; set; }

        public List<PaymentModeModel> modes { get; set; }
    }
}