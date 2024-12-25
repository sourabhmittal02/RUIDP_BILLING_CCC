﻿using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.Xml;
using System.Security.Principal;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Xml.Linq;

namespace ComplaintTracker.Models
{

    public class MST_COMPLAINT_STEPS
    {
        [Key]
        public int ID { get; set; }
        public string Description { get; set; }
        public int COMPLAINT_status { get; set; }
        public bool IS_ACTIVE { get; set; }
        public bool IS_DELETED { get; set; }
        public DateTime TIME_STAMP { get; set; }
    }

    public class RuleCriteria
    {
        public List<Metas> Meta { get; set; }

    }
    public class Metas
    {
        public int Id { get; set; }
        public string item { get; set; }
        public string criteria { get; set; }
        public string Condition { get; set; }
        public string val { get; set; }
    }
    public class ModelComplaintSendToCMS
    {
        public string compl_number { get; set; }
        public string cons_no { get; set; }
        public string compl_category { get; set; }
        public string compl_subcategory { get; set; }
        public string complaint_source { get; set; }
        public string compl_Details { get; set; }
        public string consumer_mobile { get; set; }
    }
    public class ModelComplaintSendNonConsumerToCMS
    {
        public string compl_number { get; set; }
        public string compl_category { get; set; }
        public string compl_subcategory { get; set; }
        public string complaint_source { get; set; }
        public string compl_Details { get; set; }
        public string consumer_mobile { get; set; }
        public string email_id { get; set; }
        public string consumer_name { get; set; }
        public string address1 { get; set; }
        public string address2 { get; set; }
        public string VlgID { get; set; }
        public string OfficeCode { get; set; }
    }
    public class ModelComplaintTagChangeToCMS
    {
        public string compl_number { get; set; }
        public string compl_category { get; set; }
        public string compl_subcategory { get; set; }
    }
    public class ModelHelpDesk
    {

        public string HDticketID { get; set; }
        public string HDTicketDate { get; set; }
        public string HDTicketType { get; set; }
        public string HDTicketDescription { get; set; }

        public string ConsumerID { get; set; }
        public string Meter_No { get; set; }
        public string Complaint_raised_by { get; set; }
        public string Raised_by_mobile_No { get; set; }

    }
    public class ModelComplaintSendStatusToCMS
    {
        public string compl_number { get; set; }
        public string compl_status { get; set; }
        public string compl_action_reason { get; set; }
        public string compl_action_description { get; set; }
    }
    public class COMPLAINT
    {
        public bool IsResolvedByFrt { get; set; }
        public long OFFICE_CODE_ID { get; set; }
        [NotMapped]
        public List<ModelOfficeCode> OfficeCodeCollection { get; set; }

        [NotMapped]
        public List<ModelOfficeCode> OfficeCodeByParentCollection { get; set; }
        public string CONSUMER_TYPE { get; set; }

        public string ComplaintTypeName { get; set; } //ref MST_COMPLAINT_TYPE
        public int ComplaintTypeId { get; set; } //ref MST_COMPLAINT_TYPE
        public int ASSIGNEEId { get; set; }
        public string OfficeName { get; set; }
        public string ASSIGNEEName { get; set; }
        [NotMapped]
        public List<ModelComplaintType> ComplaintTypeCollection { get; set; }
        public List<ModelComplaintAssign> ComplaintAssignCollection { get; set; }

        public List<SelectListItem> ComplaintSourceCollection { get; set; }
        public int COMPLAINT_status_ID { get; set; } // ref MST_COMPLAINT_STEPS
        public string JE_AREA { get; set; }
        public string SDO_CODE { get; set; }

        [Required(ErrorMessage = "Name Required")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string NAME { get; set; }
        public string FATHER_NAME { get; set; }
        public string KNO { get; set; }
        public string LANDLINE_NO { get; set; }
        public string Esclation_time { get; set; }
        public string Esclated_BY { get; set; }

        [Required(ErrorMessage = "Mobile No. Required")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [StringLength(10, ErrorMessage = "cannot be longer than 10 characters.")]
        public string MOBILE_NO { get; set; }
        public string REJECTED_CAUSE { get; set; }

        [Required(ErrorMessage = "Mobile No. Required")]
        [StringLength(10, ErrorMessage = "cannot be longer than 10 characters.")]
        public string ALTERNATE_MOBILE_NO { get; set; }
        public string EMAIL { get; set; }
        public string ACCOUNT_NO { get; set; }
        [Required(ErrorMessage = "ADDRESS 1 Required")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string ADDRESS1 { get; set; }
        public string ADDRESS2 { get; set; }
        public string ADDRESS3 { get; set; }
        public string LANDMARK { get; set; }
        public string CONSUMER_STATUS { get; set; }
        public string FEEDER_NAME { get; set; }
        public string AREA_CODE { get; set; }
        public int SUB_COMPLAINT_TYPE_ID { get; set; } //ref MST_SUB_COMPLAINT_TYPE
        public string REMARKS { get; set; }

        public string COMPLAINT_status { get; set; }
        public bool IS_UPDATED { get; set; }
        public DateTime UPDATED_TIME_STAMP { get; set; }
        public string Complaintdate { get; set; }
        public string COMPLAINT_NO { get; set; }

        public string COMPLAINT_TYPE { get; set; }
        public bool IS_ACTIVE { get; set; }
        public int UserId { get; set; }

        public int ComplaintSource { get; set; }
        public string ComplaintSourceName { get; set; }
        public int currentStatus { get; set; }
        public DateTime fromdate { get; set; }
        public DateTime todate { get; set; }
        public string SMS { get; set; }
        public string SMS_FRT { get; set; }
        public string EMAIL_SEND { get; set; }
        public bool IS_SMS { get; set; }
        public bool IS_EMAIL { get; set; }
        public List<COMPLAINT_REMARK> ComplaintRemarkCollection { get; set; }

        public List<COMPLAINT_LOG> ComplaintLogCollection { get; set; }

        public string Assign_FRTMobile { get; set; }


        public string excel_OFFICE_CODE { get; set; }
        public string excel_ComplaintStatus { get; set; }
        public string excel_ComplainttypeId { get; set; }
        public int villageId { get; set; }
    }

    public class MST_SUB_COMPLAINT_TYPE
    {
        public int ID { get; set; }
        public string SUB_COMPLAINT_TYPE { get; set; }
        public int COMPLAINT_TYPE_ID { get; set; } //ref MST_COMPLAINT_TYPE
        public bool IS_ACTIVE { get; set; }
        public bool IS_DELETED { get; set; }

        public DateTime TIME_STAMP { get; set; }


    }
    public class COMPLAINT_REMARK
    {
        public DateTime REMARK_DATE_TIME { get; set; }
        public string REMARK { get; set; }
        public string REMARKBY { get; set; }
        public string ComplaintNumber { get; set; }
        public bool IS_ACTIVE { get; set; }
        public bool IS_DELETED { get; set; }
        public DateTime TIME_STAMP { get; set; }

    }

    public class COMPLAINT_LOG
    {
        public string ActionType { get; set; }
        public DateTime DateTime { get; set; }

        public string State { get; set; }
        public string Remarks { get; set; }

        public string Source { get; set; }
        public string UserID { get; set; }
        public string ComplaintNumber { get; set; }
        public string Image { get; set; }
    }


    public class EsclationComplaintExcel
    {
        public string OfficeName { get; set; }
        public string COMPLAINT_NO { get; set; }
        public string NAME { get; set; }

        public string KNO { get; set; }
        public string MOBILE_NO { get; set; }
        public string ALTERNATE_MOBILE_NO { get; set; }
        public string Complaintdate { get; set; }
        public string ComplaintTypeName { get; set; } //ref MST_COMPLAINT_TYPE

        public int ComplaintSource { get; set; }
        public string ASSIGNEEName { get; set; }
        public string Esclation_time { get; set; }
        public string Esclated_BY { get; set; }

    }

    public class RawComplaintExcel
    {
        public string Circle { get; set; }
        public string Division { get; set; }
        public string ComplaintNo { get; set; }
        public string SDOCode { get; set; }
        public string SubDivisionName { get; set; }
        public string KNO { get; set; }
        public string Name { get; set; }
        public string FatherName { get; set; }
        public string Address { get; set; }
        public string MobileNo { get; set; }
        public string AlternateNumber { get; set; }
        public string ComplaintType { get; set; }
        public string SubComplaintType { get; set; }
        public string ComplaintDateTime { get; set; }
        public string ClosedDateTime { get; set; }
        public string Duration { get; set; }
        public string AreaCode { get; set; }
        public string CurrentStatus { get; set; }
        public string ComplaintSource { get; set; }
        public string CreatedByUserID { get; set; }
        public string ClosedByUserID { get; set; }
    }

    public class RawComplaintNewConnectionExcel
    {
        public string COMPLAINT_NO { get; set; }
        public string SDO_CODE { get; set; }
        public string SDO_NAME { get; set; }
        public string KNO { get; set; }
        public string NAME { get; set; }
        public string FATHER_NAME { get; set; }
        public string ADDRESS { get; set; }
        public string MOBILE_NO { get; set; }
        public string ALTERNATE_MOBILE_NO { get; set; }
        public string COMPLAINT_TYPE { get; set; }
        public string SUB_COMPLAINT_TYPE { get; set; }
        public string OUTAGE_TYPE { get; set; }
        public string SUB_OUTAGE_TYPE { get; set; }
        public string DS_NDS { get; set; }
        public string COMPLAINT_DATE_TIME { get; set; }
        public string CLOSED_DATE_TIME { get; set; }
        public string DURATION { get; set; }
        public string COMPLAINT_STATUS { get; set; }
        public string CURRENT_STATUS { get; set; }
        public string CREATED_BY_USER { get; set; }
        public string CLOSED_BY_USER { get; set; }
    }
    public class SearchComplaintExcel
    {
        public string ComplaintNo { get; set; }
        public string Knumber { get; set; }
        public string ConsumerName { get; set; }
        public string Date { get; set; }
        public string CurrentStatus { get; set; }
        public string Duration { get; set; }
        public string OfficeName { get; set; }
        public string Address { get; set; }
        public string MobileNo { get; set; }
        public string AlternateMobileNo { get; set; }
        public string ComplaintType { get; set; }
        public string Remark { get; set; }
        public string Complaint { get; set; }
        public string Assign { get; set; }
        public string Close { get; set; }
        public string SubComplaintType { get; set; }
        public string SourceStatus { get; set; }
        public string AssignedTo { get; set; }
        public string ClosedBy { get; set; }

    }

    public class LoginOTP
    {
        public string otpforLogin { get; set; }
        public string LoginId { get; set; }

    }
  }