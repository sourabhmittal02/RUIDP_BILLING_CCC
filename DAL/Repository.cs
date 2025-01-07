using ComplaintTracker.ExternalAPI;
using ComplaintTracker.Handler;
using ComplaintTracker.Models;
using IronXL.Styles;
using Serilog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Security.Cryptography.Xml;
using System.Security.Policy;
using System.Threading.Tasks;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI.WebControls;
using System.Windows;
using System.Xml.Linq;

namespace ComplaintTracker.DAL
{
    public static class Repository
    {
        static readonly Serilog.ILogger log = EventLogger._log;
        public static List<COMPLAINT> SearchComplaint()
        {
            List<COMPLAINT> lstComplaint = new List<COMPLAINT>();


            //DataSet ds = SqlHelper.ExecuteDataset(HelperClass.Connection, CommandType.StoredProcedure, "GetOfficeCode");

            //foreach (DataRow dr in ds.Tables[0].Rows)
            //{
            //    objBlank = new ModelOfficeCode();
            //    objBlank.OfficeCode = dr.ItemArray[0].ToString();
            //    objBlank.OfficeId = dr.ItemArray[1].ToString();
            //    lstOfficeCode.Add(objBlank);
            //}
            return lstComplaint;
        }
        public static List<ModelOfficeCode> GetOfficeList(String OFFICE_ID)
        {
            List<ModelOfficeCode> lstOfficeCode = new List<ModelOfficeCode>();
            ModelOfficeCode objBlank = new ModelOfficeCode();
            objBlank.OfficeId = "0";
            objBlank.OfficeCode = "Select Office Code";
            lstOfficeCode.Insert(0, objBlank);
            SqlParameter[] param ={
                    new SqlParameter("@OFFICE_ID",OFFICE_ID)
                                       };
            DataSet ds = SqlHelper.ExecuteDataset(HelperClass.Connection, CommandType.StoredProcedure, "GetOfficeCode", param);

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objBlank = new ModelOfficeCode();
                objBlank.OfficeCode = dr.ItemArray[0].ToString();
                objBlank.OfficeId = dr.ItemArray[1].ToString();
                lstOfficeCode.Add(objBlank);
            }
            return lstOfficeCode;
        }

        public static List<ModelOfficeCode> GetOfficeListCircle(String OFFICE_ID)
        {
            List<ModelOfficeCode> lstOfficeCode = new List<ModelOfficeCode>();
            ModelOfficeCode objBlank = new ModelOfficeCode();
            objBlank.OfficeId = "0";
            objBlank.OfficeCode = "Select Office Code";
            lstOfficeCode.Insert(0, objBlank);
            SqlParameter[] param ={
                    new SqlParameter("@OFFICE_ID",OFFICE_ID)
                                       };
            DataSet ds = SqlHelper.ExecuteDataset(HelperClass.Connection, CommandType.StoredProcedure, "GetOfficeCodeCircle", param);

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objBlank = new ModelOfficeCode();
                objBlank.OfficeCode = dr.ItemArray[0].ToString();
                objBlank.OfficeId = dr.ItemArray[1].ToString();
                lstOfficeCode.Add(objBlank);
            }
            return lstOfficeCode;
        }
        public static List<ModelOfficeCode> GetOfficeList_Create(String OFFICE_ID)
        {
            List<ModelOfficeCode> lstOfficeCode = new List<ModelOfficeCode>();
            ModelOfficeCode objBlank = new ModelOfficeCode();
            //objBlank.OfficeId = "0";
            //objBlank.OfficeCode = "Select Office Code";
            //lstOfficeCode.Insert(0, objBlank);
            SqlParameter[] param ={
                    new SqlParameter("@OFFICE_ID",OFFICE_ID)
                                       };
            DataSet ds = SqlHelper.ExecuteDataset(HelperClass.Connection, CommandType.StoredProcedure, "GetOfficeCode_CREATE", param);

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objBlank = new ModelOfficeCode();
                objBlank.OfficeCode = dr.ItemArray[0].ToString();
                objBlank.OfficeId = dr.ItemArray[1].ToString();
                lstOfficeCode.Add(objBlank);
            }
            return lstOfficeCode;
        }
        public static List<ModelComplaintType> GetComplaintTypeList(string OFFICE_ID)
        {
            List<ModelComplaintType> obj = new List<ModelComplaintType>();
            SqlParameter[] param ={
                    new SqlParameter("@OFFICE_ID",OFFICE_ID)
                                       };
            DataSet ds = SqlHelper.ExecuteDataset(HelperClass.Connection, CommandType.StoredProcedure, "GetComplaintType", param);
            //ds.Tables[0].Rows.RemoveAt(0);
            //Bind Complaint generic list using dataRow     
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                obj.Add(

                    new ModelComplaintType
                    {
                        ComplaintTypeId = Convert.ToInt32(dr["Id"]),
                        ComplaintType = Convert.ToString(dr["Complaint_Type"]),
                        ComplaintTileColor = Convert.ToString(dr["TileColor"]),
                        Status = Convert.ToBoolean(dr["IS_ACTIVE"]),
                        COMPLAINT_COUNT = Convert.ToString(dr["COMPLAINT_COUNT"]),
                    }
                    );
            }
            return (obj);
        }

        public static List<ModelComplaintType> GetOutageTypeList(string OFFICE_ID)
        {
            List<ModelComplaintType> obj = new List<ModelComplaintType>();
            SqlParameter[] param ={
                    new SqlParameter("@OFFICE_ID",OFFICE_ID)
                                       };
            DataSet ds = SqlHelper.ExecuteDataset(HelperClass.Connection, CommandType.StoredProcedure, "GetPowerOutageType", param);
            //ds.Tables[0].Rows.RemoveAt(0);
            //Bind Complaint generic list using dataRow     
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                obj.Add(

                    new ModelComplaintType
                    {
                        ComplaintTypeId = Convert.ToInt32(dr["Id"]),
                        ComplaintType = Convert.ToString(dr["Complaint_Type"]),
                        ComplaintTileColor = Convert.ToString(dr["TileColor"]),
                        Status = Convert.ToBoolean(dr["IS_ACTIVE"]),
                        COMPLAINT_COUNT = Convert.ToString(dr["COMPLAINT_COUNT"]),
                    }
                    );
            }
            return (obj);
        }
        public static List<ModelComplaintType> GetSubComplaintTypeList(int ComplaintTypeId)
        {
            List<ModelComplaintType> obj = new List<ModelComplaintType>();
            SqlParameter[] param ={
                    new SqlParameter("@ComplaintTypeId",ComplaintTypeId)
                                       };
            DataSet ds = SqlHelper.ExecuteDataset(HelperClass.Connection, CommandType.StoredProcedure, "GetSubComplaintByComplaintType", param);
            //Bind Complaint generic list using dataRow     
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                obj.Add(

                    new ModelComplaintType
                    {
                        SubComplaintTypeId = Convert.ToInt32(dr["Id"]),
                        SubComplaintType = Convert.ToString(dr["SUB_COMPLAINT_TYPE"]),
                        Status = Convert.ToBoolean(dr["IS_ACTIVE"]),
                    }
                    );
            }
            return (obj);
        }
        public static List<COMPLAINT> GetPreviousComplaint(int OfficeCode, int ConsumerType, string searchparm)
        {
            List<COMPLAINT> obj = new List<COMPLAINT>();
            SqlParameter[] param ={
                    new SqlParameter("@OfficeCode",OfficeCode),
                    new SqlParameter("@ConsumerType",ConsumerType),
                    new SqlParameter("@Searchparm",searchparm) };

            DataSet ds = SqlHelper.ExecuteDataset(HelperClass.Connection, CommandType.StoredProcedure, "GetPreviousComplaint", param);
            //Bind Complaint generic list using dataRow     
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                obj.Add(

                    new COMPLAINT
                    {
                        //Consumer Info
                        SDO_CODE = Convert.ToString(dr["SDO_CODE"]),

                        ADDRESS1 = Convert.ToString(dr["ADDRESS1"]),
                        ADDRESS2 = Convert.ToString(dr["ADDRESS2"]),
                        ADDRESS3 = Convert.ToString(dr["ADDRESS3"]),
                        NAME = Convert.ToString(dr["NAME"]),
                        FATHER_NAME = Convert.ToString(dr["FATHER_NAME"]),
                        KNO = Convert.ToString(dr["KNO"]),
                        LANDMARK = Convert.ToString(dr["LANDMARK"]),
                        LANDLINE_NO = Convert.ToString(dr["LANDLINE_NO"]),
                        MOBILE_NO = Convert.ToString(dr["MOBILE_NO"]),
                        ALTERNATE_MOBILE_NO = Convert.ToString(dr["ALTERNATE_MOBILE_NO"]),
                        CONSUMER_STATUS = Convert.ToString(dr["CONSUMER_STATUS"]),
                        EMAIL = Convert.ToString(dr["EMAIL"]),
                        FEEDER_NAME = Convert.ToString(dr["FEEDER_NAME"]),
                        ACCOUNT_NO = Convert.ToString(dr["ACCOUNT_NO"]),
                        AREA_CODE = Convert.ToString(dr["AREA_CODE"]),

                    }
                    );
            }
            foreach (DataRow dr in ds.Tables[1].Rows)
            {
                obj.Add(

                    new COMPLAINT
                    {
                        //Consumer Previous Complaint
                        Complaintdate = Convert.ToString(dr["Complaintdate"]),
                        COMPLAINT_NO = Convert.ToString(dr["COMPLAINT_NO"]),
                        COMPLAINT_TYPE = Convert.ToString(dr["COMPLAINT_TYPE"]),
                        REMARKS = Convert.ToString(dr["REMARKS"]),
                        COMPLAINT_status = Convert.ToString(dr["COMPLAINT_status"]),
                    }
                    );
            }
            return (obj);
        }
        public static List<ModelComplaintDashboard> GetComplaintTypeSummary(int OfficeCode, int ComplaintType)
        {
            List<ModelComplaintDashboard> obj = new List<ModelComplaintDashboard>();
            SqlParameter[] param ={
                    new SqlParameter("@OfficeCode",OfficeCode),
                    new SqlParameter("@ComplaintType",ComplaintType),
                   };
            DataSet ds = SqlHelper.ExecuteDataset(HelperClass.Connection, CommandType.StoredProcedure, "GetComplaintTypeSummary", param);
            //Bind Complaint generic list using dataRow     
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                obj.Add(

                    new ModelComplaintDashboard
                    {
                        SDOName = Convert.ToString(dr["SDOName"]),
                        PreviousDayPending = Convert.ToString(dr["PreviousDayPending"]),
                        TodayRegister = Convert.ToString(dr["TodayRegister"]),
                        TotalComplaint = Convert.ToString(dr["TotalComplaint"]),
                        PreviousResolvedComplaint = Convert.ToString(dr["PreviousResolvedComplaint"]),
                        TodayResolved = Convert.ToString(dr["TodayResolved"]),
                        TotalResolved = Convert.ToString(dr["TotalResolved"]),
                        TotalPending = Convert.ToString(dr["TotalPending"]),
                    }
                    );
            }
            return (obj);
        }

        public static async Task<Int64> SaveComplaint(COMPLAINT modelComplaint)
        {
            Int64 retStatus = 0;
            Int64 retStatus1 = 0;
            string retMsg = String.Empty; ;
            COMPLAINT obj = new COMPLAINT();
            obj = modelComplaint;

            SqlParameter parmretStatus = new SqlParameter();
            parmretStatus.ParameterName = "@retStatus";
            parmretStatus.DbType = DbType.Int32;
            parmretStatus.Size = 8;
            parmretStatus.Direction = ParameterDirection.Output;

            SqlParameter parmretMsg = new SqlParameter();
            parmretMsg.ParameterName = "@retMsg";
            parmretMsg.DbType = DbType.String;
            parmretMsg.Size = 8;
            parmretMsg.Direction = ParameterDirection.Output;


            SqlParameter parmretComplaint_no = new SqlParameter();
            parmretComplaint_no.ParameterName = "@retComplaint_no";
            parmretComplaint_no.DbType = DbType.Int64;
            parmretComplaint_no.Size = 8;
            parmretComplaint_no.Direction = ParameterDirection.Output;




            SqlParameter[] param ={
                    new SqlParameter("@OFFICE_CODE",modelComplaint.OFFICE_CODE_ID),
                    new SqlParameter("@CONSUMER_TYPE",modelComplaint.CONSUMER_TYPE),
                    new SqlParameter("@COMPLAINT_TYPE",modelComplaint.ComplaintTypeId),
                    new SqlParameter("@COMPLAINT_SOURCE_ID",modelComplaint.ComplaintSource),//modelComplaint.com),
                    new SqlParameter("@COMPLAINT_CURRENT_STATUS_ID",3),//modelComplaint.COMPLAINT_status_ID),
                    new SqlParameter("@JE_AREA_ID",modelComplaint.JE_AREA),
                    new SqlParameter("@COMPLAINT_status",1),//modelComplaint.COMPLAINT_status_ID),
                    new SqlParameter("@SDO_CODE",modelComplaint.SDO_CODE),
                    new SqlParameter("@NAME",modelComplaint.NAME),

                    new SqlParameter("@FATHER_NAME",modelComplaint.FATHER_NAME),
                    new SqlParameter("@KNO",modelComplaint.KNO),
                    new SqlParameter("@LANDLINE_NO",modelComplaint.LANDLINE_NO),
                    new SqlParameter("@MOBILE_NO",modelComplaint.MOBILE_NO),
                    new SqlParameter("@ALTERNATE_MOBILE_NO",modelComplaint.ALTERNATE_MOBILE_NO),
                    new SqlParameter("@EMAIL",modelComplaint.EMAIL),
                    new SqlParameter("@ACCOUNT_NO",modelComplaint.ACCOUNT_NO),
                    new SqlParameter("@ADDRESS1",modelComplaint.ADDRESS1),
                    new SqlParameter("@ADDRESS2",modelComplaint.ADDRESS2),
                    new SqlParameter("@ADDRESS3",modelComplaint.ADDRESS3),

                    new SqlParameter("@LANDMARK",modelComplaint.LANDMARK),
                    new SqlParameter("@CONSUMER_STATUS",modelComplaint.CONSUMER_STATUS),
                    new SqlParameter("@FEEDER_NAME",modelComplaint.FEEDER_NAME),
                    new SqlParameter("@AREA_CODE",modelComplaint.AREA_CODE),
                    new SqlParameter("@SUB_COMPLAINT_TYPE",modelComplaint.SUB_COMPLAINT_TYPE_ID),//modelComplaint.SUB_COMPLAINT_TYPE_ID),
                    new SqlParameter("@REMARKS",modelComplaint.REMARKS),
                    new SqlParameter("@IS_ACTIVE",true),
                    new SqlParameter("@IS_DELETED",false),
                    new SqlParameter("@TIME_STAMP",DateTime.Now),
                    new SqlParameter("@IS_UPDATED",false),

                    new SqlParameter("@UPDATED_TIME_STAMP",DateTime.Now),
                    new SqlParameter("@PROCESS","I"),
                    new SqlParameter("@REMARK",modelComplaint.REMARKS),
                    new SqlParameter("@REMARK_DATE_TIME",DateTime.Now),
                     new SqlParameter("@USER_ID",modelComplaint.UserId),
                     new SqlParameter("@AssigneeId",1),
                    parmretStatus,parmretMsg,parmretComplaint_no};

            try
            {
                SqlHelper.ExecuteNonQuery(HelperClass.Connection, CommandType.StoredProcedure, "COMPLAINTS_REGISTER", param);



                if (param[37].Value != DBNull.Value)// status
                    retStatus = Convert.ToInt64(param[37].Value);

                try
                {
                    if (retStatus > 0 && modelComplaint.MOBILE_NO.Length == 10)
                    {
                        TextSmsAPI textSmsAPI1 = new TextSmsAPI();
                        if (modelComplaint.KNO == null || modelComplaint.CONSUMER_TYPE == "2")
                        {
                            if (modelComplaint.EMAIL == "" || modelComplaint.EMAIL is null)
                            {
                                modelComplaint.EMAIL = "0";
                            }
                            if (modelComplaint.ADDRESS2 == "" || modelComplaint.ADDRESS2 is null)
                            {
                                modelComplaint.ADDRESS2 = "0";
                            }
                            ModelComplaintSendNonConsumerToCMS modelComplaintSendToCMS = new ModelComplaintSendNonConsumerToCMS();
                            modelComplaintSendToCMS.compl_number = retStatus.ToString();
                            modelComplaintSendToCMS.compl_category = modelComplaint.ComplaintTypeId.ToString();
                            modelComplaintSendToCMS.compl_subcategory = modelComplaint.SUB_COMPLAINT_TYPE_ID.ToString();
                            modelComplaintSendToCMS.complaint_source = modelComplaint.ComplaintSource.ToString();
                            modelComplaintSendToCMS.compl_Details = modelComplaint.REMARKS;
                            modelComplaintSendToCMS.consumer_mobile = modelComplaint.MOBILE_NO;
                            modelComplaintSendToCMS.email_id = modelComplaint.EMAIL;
                            modelComplaintSendToCMS.consumer_name = modelComplaint.NAME;
                            modelComplaintSendToCMS.address1 = modelComplaint.ADDRESS1;
                            modelComplaintSendToCMS.address2 = modelComplaint.ADDRESS2;
                            if (modelComplaint.villageId == 0 || modelComplaint.villageId.ToString() == "")
                            {
                                modelComplaintSendToCMS.VlgID = "1";
                            }
                            else
                            {
                                modelComplaintSendToCMS.VlgID = modelComplaint.villageId.ToString();
                            }

                            modelComplaintSendToCMS.OfficeCode = modelComplaint.OFFICE_CODE_ID.ToString();
                            string response1 = await textSmsAPI1.SendComplaintRegisterNonConsumerToCMS(modelComplaintSendToCMS);
                        }
                        else
                        {
                            ModelComplaintSendToCMS modelComplaintSendToCMS = new ModelComplaintSendToCMS();
                            modelComplaintSendToCMS.compl_number = retStatus.ToString();
                            modelComplaintSendToCMS.cons_no = modelComplaint.KNO;
                            modelComplaintSendToCMS.compl_category = modelComplaint.ComplaintTypeId.ToString();
                            modelComplaintSendToCMS.compl_subcategory = modelComplaint.SUB_COMPLAINT_TYPE_ID.ToString();
                            modelComplaintSendToCMS.complaint_source = modelComplaint.ComplaintSource.ToString();
                            modelComplaintSendToCMS.compl_Details = modelComplaint.REMARKS;
                            modelComplaintSendToCMS.consumer_mobile = modelComplaint.MOBILE_NO;
                            string response1 = await textSmsAPI1.SendComplaintRegisterToCMS(modelComplaintSendToCMS);
                        }
                        ModelComplaintSendStatusToCMS modelComplaintSendToCMS1 = new ModelComplaintSendStatusToCMS();
                        modelComplaintSendToCMS1.compl_number = retStatus.ToString();
                        modelComplaintSendToCMS1.compl_status = "In-Process";
                        modelComplaintSendToCMS1.compl_action_reason = modelComplaint.REMARKS;
                        modelComplaintSendToCMS1.compl_action_description = "Assigned to Team";
                        string response2 = await textSmsAPI1.SendComplaintStatusToCMS(modelComplaintSendToCMS1);
                        if (modelComplaint.ComplaintTypeId == 15)
                        {
                            try
                            {
                                SqlParameter[] param1 ={
                            new SqlParameter("@COMPLAINT_TYPE",modelComplaint.ComplaintTypeId),
                            new SqlParameter("@SUB_COMPLAINT_TYPE",modelComplaint.SUB_COMPLAINT_TYPE_ID),
                            new SqlParameter("@SourceID",1)};
                                string complaintType = "";
                                string subcomplainttype = "";
                                string ComplaintSource = "";
                                DataSet ds = SqlHelper.ExecuteDataset(HelperClass.Connection, CommandType.StoredProcedure, "GET_COMPLAINT_TYPE_SUBTYPE", param1);
                                //Bind Complaint generic list using dataRow     
                                foreach (DataRow dr in ds.Tables[0].Rows)
                                {
                                    complaintType = Convert.ToString(dr["COMPLAINT_TYPE"]);
                                    subcomplainttype = Convert.ToString(dr["SUB_COMPLAINT_TYPE"]);
                                    ComplaintSource = Convert.ToString(dr["COMPLAINT_SOURCE"]);
                                }
                                ModelHelpDesk SmartMeter = new ModelHelpDesk();
                                SmartMeter.HDticketID = retStatus.ToString();
                                SmartMeter.HDTicketDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                                SmartMeter.HDTicketType = complaintType;
                                SmartMeter.HDTicketDescription = subcomplainttype;
                                SmartMeter.ConsumerID = modelComplaint.KNO;
                                SmartMeter.Meter_No = "NA";
                                SmartMeter.Complaint_raised_by = modelComplaint.NAME;
                                SmartMeter.Raised_by_mobile_No = modelComplaint.MOBILE_NO;
                                string response3 = await textSmsAPI1.PushHdTicketAPI(SmartMeter);
                            }
                            catch (Exception ex)
                            { }
                        }
                    }
                }
                catch (Exception ex)
                { }


                log.Information(retStatus.ToString());


            }
            catch (Exception ex)
            {
                retStatus = -1;
            }
            //try
            //{
            //    if (retStatus > 0 && modelComplaint.MOBILE_NO.Length == 10)
            //    {
            //        #region OldSMSAPI
            //        ////////log.Information(modelComplaint.MOBILE_NO.ToString());
            //        ////////ModelSmsAPI modelSmsAPI = new ModelSmsAPI();
            //        ////////TextSmsAPI textSmsAPI = new TextSmsAPI();

            //        ////////modelSmsAPI.To = modelComplaint.MOBILE_NO.ToString();
            //        ////////modelSmsAPI.Smstext = "प्रिय उपभोक्ता, आपका शिकायत क्रमांक " + retStatus + " दिनांक " + DateTime.Now.ToString("dd-MMM-yyyy") + " है। विद्युत सम्बन्धित शिकायत एवं अन्य सुविधाओं के लिए https://bit.ly/JDVVNLCCC का प्रयोग करें। जोधपुर डिस्कॉम।";
            //        ////////modelSmsAPI.Smstemplete = "1307171445679499387";
            //        ////////string response = await textSmsAPI.RegisterComplaintSMSEncode(modelSmsAPI);





            //        ////////modelComplaint.SMS = modelSmsAPI.Smstext;
            //        ////////log.Information(response.ToString());

            //        //////////if (response.Contains("avvnlalt"))
            //        //////////{
            //        ////////PUSH_SMS_DETAIL_Consumer(modelComplaint, response);

            //        //////////ModelSmsAPI modelSmsAPI1 = new ModelSmsAPI();
            //        //////////TextSmsAPI textSmsAPI1 = new TextSmsAPI();
            //        //////////modelSmsAPI1.To = modelComplaint.MOBILE_NO.ToString();
            //        //////////modelSmsAPI1.Smstext = "प्रिय उपभोक्ता, शिकायत क्रमांक " + retStatus + " फाॅल्ट रेक्टिफिकेषन टीम को निर्दिष्ट कर दी गई है। जोधपुर डिस्कॉम।";
            //        //////////modelSmsAPI1.Smstemplete = "1307160688865523002";
            //        //////////string response1 = await textSmsAPI1.RegisterComplaintSMSEncode(modelSmsAPI1);
            //        //////////modelComplaint.SMS = modelSmsAPI1.Smstext;
            //        //////////log.Information(response1.ToString());
            //        //////////PUSH_SMS_DETAIL_Consumer(modelComplaint, response1);
            //        //////////}
            //        #endregion 

            //        log.Information(modelComplaint.MOBILE_NO.ToString());
            //        ModelSmsAPISendSMS modelSmsAPIOWN = new ModelSmsAPISendSMS();
            //        TextSmsAPI textSmsAPI1 = new TextSmsAPI();
            //        modelSmsAPIOWN.id = "0";
            //        modelSmsAPIOWN.to = modelComplaint.MOBILE_NO.ToString();
            //        modelSmsAPIOWN.smsText = "प्रिय उपभोक्ता, आपका शिकायत क्रमांक " + retStatus + " दिनांक " + DateTime.Now.ToString("dd-MMM-yyyy") + " है। विद्युत सम्बन्धित शिकायत एवं अन्य सुविधाओं के लिए https://bit.ly/JDVVNLCCC का प्रयोग करें। जोधपुर डिस्कॉम।";
            //        modelSmsAPIOWN.templateid = "1307171445679499387";
            //        string response1 = await textSmsAPI1.RegisterComplaintSendSMSWeb(modelSmsAPIOWN);

            //        modelSmsAPIOWN.smsText = "प्रिय उपभोक्ता, शिकायत क्रमांक " + retStatus + " फाॅल्ट रेक्टिफिकेषन टीम को निर्दिष्ट कर दी गई है। जोधपुर डिस्कॉम।";
            //        modelSmsAPIOWN.templateid = "1307160688865523002";
            //        string response2 = await textSmsAPI1.RegisterComplaintSendSMSWeb(modelSmsAPIOWN);

            //        //modelComplaint.SMS = modelSmsAPIOWN.smsText;
            //        log.Information(response1.ToString());
            //        //PUSH_SMS_DETAIL_Consumer(modelComplaint, response1);
            //    }
            //    else
            //        retStatus1 = 0;
            //}
            //catch(Exception ex) { retStatus1 = -1;}
            return retStatus;

        }



        public static async Task<Int64> SaveComplaintRegistration(COMPLAINT modelComplaint, string iMageName)
        {
            Int64 retStatus = 0;
            Int64 retStatus1 = 0;
            string retMsg = String.Empty; ;
            COMPLAINT obj = new COMPLAINT();
            obj = modelComplaint;

            SqlParameter parmretStatus = new SqlParameter();
            parmretStatus.ParameterName = "@retStatus";
            parmretStatus.DbType = DbType.Int32;
            parmretStatus.Size = 8;
            parmretStatus.Direction = ParameterDirection.Output;

            SqlParameter parmretMsg = new SqlParameter();
            parmretMsg.ParameterName = "@retMsg";
            parmretMsg.DbType = DbType.String;
            parmretMsg.Size = 8;
            parmretMsg.Direction = ParameterDirection.Output;


            SqlParameter parmretComplaint_no = new SqlParameter();
            parmretComplaint_no.ParameterName = "@retComplaint_no";
            parmretComplaint_no.DbType = DbType.Int64;
            parmretComplaint_no.Size = 8;
            parmretComplaint_no.Direction = ParameterDirection.Output;




            SqlParameter[] param ={
                    new SqlParameter("@OFFICE_CODE",modelComplaint.OFFICE_CODE_ID),
                    new SqlParameter("@CONSUMER_TYPE",modelComplaint.CONSUMER_TYPE),
                    new SqlParameter("@COMPLAINT_TYPE",modelComplaint.ComplaintTypeId),
                    new SqlParameter("@COMPLAINT_SOURCE_ID",15),
                    new SqlParameter("@COMPLAINT_CURRENT_STATUS_ID",3),//modelComplaint.COMPLAINT_status_ID),
                    new SqlParameter("@JE_AREA_ID",modelComplaint.JE_AREA),
                    new SqlParameter("@COMPLAINT_status",1),//modelComplaint.COMPLAINT_status_ID),
                    new SqlParameter("@SDO_CODE",modelComplaint.SDO_CODE),
                    new SqlParameter("@NAME",modelComplaint.NAME),

                    new SqlParameter("@FATHER_NAME",modelComplaint.FATHER_NAME),
                    new SqlParameter("@KNO",modelComplaint.KNO),
                    new SqlParameter("@LANDLINE_NO",modelComplaint.LANDLINE_NO),
                    new SqlParameter("@MOBILE_NO",modelComplaint.MOBILE_NO),
                    new SqlParameter("@ALTERNATE_MOBILE_NO",modelComplaint.ALTERNATE_MOBILE_NO),
                    new SqlParameter("@EMAIL",modelComplaint.EMAIL),
                    new SqlParameter("@ACCOUNT_NO",modelComplaint.ACCOUNT_NO),
                    new SqlParameter("@ADDRESS1",modelComplaint.ADDRESS1),
                    new SqlParameter("@ADDRESS2",modelComplaint.ADDRESS2),
                    new SqlParameter("@ADDRESS3",modelComplaint.ADDRESS3),

                    new SqlParameter("@LANDMARK",modelComplaint.LANDMARK),
                    new SqlParameter("@CONSUMER_STATUS",modelComplaint.CONSUMER_STATUS),
                    new SqlParameter("@FEEDER_NAME",modelComplaint.FEEDER_NAME),
                    new SqlParameter("@AREA_CODE",modelComplaint.AREA_CODE),
                    new SqlParameter("@SUB_COMPLAINT_TYPE",modelComplaint.SUB_COMPLAINT_TYPE_ID),//modelComplaint.SUB_COMPLAINT_TYPE_ID),
                    new SqlParameter("@REMARKS",modelComplaint.REMARKS),
                    new SqlParameter("@IS_ACTIVE",true),
                    new SqlParameter("@IS_DELETED",false),
                    new SqlParameter("@TIME_STAMP",DateTime.Now),
                    new SqlParameter("@IS_UPDATED",false),

                    new SqlParameter("@UPDATED_TIME_STAMP",DateTime.Now),
                    new SqlParameter("@PROCESS","I"),
                    new SqlParameter("@REMARK",modelComplaint.REMARKS),
                    new SqlParameter("@REMARK_DATE_TIME",DateTime.Now),
                     new SqlParameter("@USER_ID",modelComplaint.UserId),
                     new SqlParameter("@AssigneeId",1),
                    parmretStatus,parmretMsg,parmretComplaint_no};

            try
            {
                SqlHelper.ExecuteNonQuery(HelperClass.Connection, CommandType.StoredProcedure, "COMPLAINTS_REGISTER", param);



                if (param[37].Value != DBNull.Value)// status
                    retStatus = Convert.ToInt64(param[37].Value);


                if (retStatus > 0)
                {
                    if (modelComplaint.KNO == null)
                        modelComplaint.KNO = "0";
                    ModelComplaintSendToCMS modelComplaintSendToCMS = new ModelComplaintSendToCMS();
                    modelComplaintSendToCMS.compl_number = retStatus.ToString();
                    modelComplaintSendToCMS.cons_no = modelComplaint.KNO;
                    modelComplaintSendToCMS.compl_category = modelComplaint.ComplaintTypeId.ToString();
                    modelComplaintSendToCMS.compl_subcategory = modelComplaint.SUB_COMPLAINT_TYPE_ID.ToString();
                    modelComplaintSendToCMS.complaint_source = modelComplaint.ComplaintSource.ToString();
                    modelComplaintSendToCMS.compl_Details = modelComplaint.REMARKS;
                    modelComplaintSendToCMS.consumer_mobile = modelComplaint.MOBILE_NO;
                    TextSmsAPI textSmsAPI1 = new TextSmsAPI();
                    string response1 = await textSmsAPI1.SendComplaintRegisterToCMS(modelComplaintSendToCMS);
                    ModelComplaintSendStatusToCMS modelComplaintSendToCMS1 = new ModelComplaintSendStatusToCMS();
                    modelComplaintSendToCMS1.compl_number = retStatus.ToString();
                    modelComplaintSendToCMS1.compl_status = "In-Process";
                    modelComplaintSendToCMS1.compl_action_reason = modelComplaint.REMARKS;
                    modelComplaintSendToCMS1.compl_action_description = "Assigned to Team";
                    string response2 = await textSmsAPI1.SendComplaintStatusToCMS(modelComplaintSendToCMS1);
                    log.Information("In Image Save");
                    SqlParameter[] paramImg ={
                    new SqlParameter("@Complaint_NO",retStatus),
                    new SqlParameter("@Image",iMageName)
                    };

                    SqlHelper.ExecuteNonQuery(HelperClass.Connection, CommandType.StoredProcedure, "Complaint_Image_Save", paramImg);
                    log.Information("Image Saveed");
                }


                log.Information(retStatus.ToString());


            }
            catch (Exception ex)
            {
                retStatus = -1;
            }
            //try
            //{
            //    if (retStatus > 0 && modelComplaint.MOBILE_NO.Length == 10)
            //    {
            //        log.Information(modelComplaint.MOBILE_NO.ToString());
            //        ModelSmsAPI modelSmsAPI = new ModelSmsAPI();
            //        TextSmsAPI textSmsAPI = new TextSmsAPI();

            //        modelSmsAPI.To = modelComplaint.MOBILE_NO.ToString();
            //        modelSmsAPI.Smstext = "प्रिय उपभोक्ता, आपका शिकायत क्रमांक " + retStatus + " दिनांक " + DateTime.Now.ToString("dd-MMM-yyyy") + " है। विद्युत सम्बन्धित शिकायत एवं अन्य सुविधाओं के लिए \"\"VIDYUT SAATHI\"\" ऐप का प्रयोग करें। जोधपुर डिस्कॉम।";
            //        modelSmsAPI.Smstemplete = "1307160688860548923";
            //        string response = await textSmsAPI.RegisterComplaintSMS(modelSmsAPI);
            //        modelComplaint.SMS = modelSmsAPI.Smstext;
            //        log.Information(response.ToString());

            //        //if (response.Contains("avvnlalt"))
            //        //{
            //        PUSH_SMS_DETAIL_Consumer(modelComplaint, response);
            //        //}
            //    }
            //    else
            //        retStatus1 = 0;
            //}
            //catch (Exception ex)
            //{
            //    retStatus1 = -1;
            //}
            return retStatus;

        }



        public static async Task<Int64> EditComplaint(COMPLAINT modelComplaint)
        {
            Int64 retStatus = 0;
            string retMsg = String.Empty; ;
            COMPLAINT obj = new COMPLAINT();
            obj = modelComplaint;

            SqlParameter parmretStatus = new SqlParameter();
            parmretStatus.ParameterName = "@retStatus";
            parmretStatus.DbType = DbType.Int32;
            parmretStatus.Size = 8;
            parmretStatus.Direction = ParameterDirection.Output;

            SqlParameter parmretMsg = new SqlParameter();
            parmretMsg.ParameterName = "@retMsg";
            parmretMsg.DbType = DbType.String;
            parmretMsg.Size = 8;
            parmretMsg.Direction = ParameterDirection.Output;


            SqlParameter parmretComplaint_no = new SqlParameter();
            parmretComplaint_no.ParameterName = "@retComplaint_no";
            parmretComplaint_no.DbType = DbType.Int32;
            parmretComplaint_no.Size = 8;
            parmretComplaint_no.Direction = ParameterDirection.Output;




            SqlParameter[] param ={
                    new SqlParameter("@COMPLAINT_NO",modelComplaint.COMPLAINT_NO),
                    new SqlParameter("@NAME",modelComplaint.NAME),
                    new SqlParameter("@FATHER_NAME",modelComplaint.FATHER_NAME),
                    new SqlParameter("@KNO",modelComplaint.KNO),
                    new SqlParameter("@LANDLINE_NO",modelComplaint.LANDLINE_NO),
                    new SqlParameter("@MOBILE_NO",modelComplaint.MOBILE_NO),
                    new SqlParameter("@ALTERNATE_MOBILE_NO",modelComplaint.ALTERNATE_MOBILE_NO),
                    new SqlParameter("@EMAIL",modelComplaint.EMAIL),
                    new SqlParameter("@AreaCode",modelComplaint.AREA_CODE),
                    new SqlParameter("@ADDRESS1",modelComplaint.ADDRESS1),
                    new SqlParameter("@ADDRESS2",modelComplaint.ADDRESS2),
                    new SqlParameter("@ADDRESS3",modelComplaint.ADDRESS3),
                    new SqlParameter("@LANDMARK",modelComplaint.LANDMARK),
                    parmretStatus,parmretMsg,parmretComplaint_no};

            try
            {
                SqlHelper.ExecuteNonQuery(HelperClass.Connection, CommandType.StoredProcedure, "COMPLAINTS_EDIT", param);

                if (param[15].Value != DBNull.Value)// status
                    retStatus = Convert.ToInt32(param[15].Value);
                //if (retStatus > 0 && modelComplaint.MOBILE_NO.Length == 10)
                //{
                //    ModelSmsAPI modelSmsAPI = new ModelSmsAPI();
                //    TextSmsAPI textSmsAPI = new TextSmsAPI();

                //    modelSmsAPI.To = "91" + modelComplaint.MOBILE_NO.ToString();
                //    modelSmsAPI.Smstext = "Dear Consumer,Your Complaint has been registered with complaint No. " + retStatus + " on Date:" + DateTime.Now.ToString("dd-MMM-yyyy") + " AVVNL";
                //    string response = await textSmsAPI.RegisterComplaintSMS(modelSmsAPI);
                //    if (response.Contains("success"))
                //    {
                //        PUSH_SMS_DETAIL_Consumer(modelComplaint, response);
                //    }
                //}
                else
                    retStatus = 0;
            }
            catch (Exception ex)
            {
                retStatus = -1;
            }
            return retStatus;

        }
        public static bool IsDuplicateComplaint(string kNO)
        {

            SqlParameter[] param = { new SqlParameter("@KNO", kNO) };

            object requestCount = SqlHelper.ExecuteScalar(HelperClass.Connection, CommandType.StoredProcedure, "IsDuplicateRequest", param);

            if (Convert.ToInt16(requestCount) == 0)
            {
                return false;
            }

            return true;

        }
        public static List<SelectListItem> ComplaintSourceJson()
        {
            List<SelectListItem> lstComplaintSource = new List<SelectListItem>();
            SelectListItem objBlank = new SelectListItem();
            //objBlank.Value = "0";
            //objBlank.Text = "Select Source";
            //lstComplaintSource.Insert(0, objBlank);

            DataSet ds = SqlHelper.ExecuteDataset(HelperClass.Connection, CommandType.StoredProcedure, "GetComplaintSource");

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objBlank = new SelectListItem();
                objBlank.Value = dr.ItemArray[0].ToString();
                objBlank.Text = dr.ItemArray[1].ToString();
                lstComplaintSource.Add(objBlank);
            }
            return lstComplaintSource;
        }
        public static List<SelectListItem> ComplaintSourceJson_Register()
        {
            List<SelectListItem> lstComplaintSource = new List<SelectListItem>();
            SelectListItem objBlank = new SelectListItem();
            //objBlank.Value = "0";
            //objBlank.Text = "Select Source";
            //lstComplaintSource.Insert(0, objBlank);

            DataSet ds = SqlHelper.ExecuteDataset(HelperClass.Connection, CommandType.StoredProcedure, "GetComplaintSource_register");

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objBlank = new SelectListItem();
                objBlank.Value = dr.ItemArray[0].ToString();
                objBlank.Text = dr.ItemArray[1].ToString();
                lstComplaintSource.Add(objBlank);
            }
            return lstComplaintSource;
        }
        public static List<ModelSearchComplaint> GetComplaintDetails(ModelSearchComplaint dataObject)
        {
            int TotalRec = 0;

            List<ModelSearchComplaint> lstComplaintSource = new List<ModelSearchComplaint>();
            ModelSearchComplaint objBlank = new ModelSearchComplaint();
            SqlParameter[] param ={
                new SqlParameter("@complaint_no",dataObject.COMPLAINT_NO),
                    new SqlParameter("@kno",dataObject.KNO),
                    new SqlParameter("@mobile_no",dataObject.MOBILE_NO),

                    new SqlParameter("@complaint_type",dataObject.COMPLAINT_TYPE),
                    new SqlParameter("@office_id",dataObject.OFFICE_ID),
                     new SqlParameter("@complaint_status",dataObject.COMPLAINT_status),
                    new SqlParameter("@complaint_source",dataObject.COMPLAINT_SOURCE),
                    new SqlParameter("@from_Date",dataObject.fromdate),
                    new SqlParameter("@to_date",dataObject.todate),
                    new SqlParameter("@Assigned_status",dataObject.assigned_status),
                    new SqlParameter("@STARTROWINDEX",dataObject.start),
                    new SqlParameter("@MAXIMUMROWS",dataObject.length)};

            log.Debug(" GetComplaintDetails IP " + HelperClass.GetIPHelper() + " Proc Start Time :  " + DateTime.Now.ToString());
            DataSet ds = SqlHelper.ExecuteDataset(HelperClass.Connection, CommandType.StoredProcedure, "GetComplaintDetails", param);
            log.Debug(" GetComplaintDetails IP " + HelperClass.GetIPHelper() + " Proc End Time :  " + DateTime.Now.ToString());


            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[1].Rows.Count > 0)
                    TotalRec = Convert.ToInt32(ds.Tables[1].Rows[0][0]);
                else
                    TotalRec = 0;
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    objBlank = new ModelSearchComplaint();
                    objBlank.KNO = (dr.ItemArray[0].ToString());
                    objBlank.NAME = dr.ItemArray[1].ToString();
                    objBlank.COMPLAINT_DATE = dr.ItemArray[2].ToString();
                    objBlank.DURATION = dr.ItemArray[3].ToString();
                    objBlank.COMPLAINT_NO = Convert.ToInt64(dr.ItemArray[4].ToString());
                    objBlank.OFFICE_NAME = dr.ItemArray[5].ToString();
                    objBlank.ADDRESS = dr.ItemArray[6].ToString();
                    objBlank.COMPLAINT_TYPE = dr.ItemArray[7].ToString();
                    objBlank.SUB_COMPLAINT_TYPE = dr.ItemArray[8].ToString();
                    objBlank.SOURCE_NAME = dr.ItemArray[9].ToString();
                    objBlank.COMPLAINT_status = dr.ItemArray[10].ToString();
                    objBlank.ASSIGNED_TO = dr.ItemArray[11].ToString();
                    objBlank.OUTAGE_TYPE = dr.ItemArray[12].ToString();
                    objBlank.RECTIFICATION = dr.ItemArray[13].ToString();
                    objBlank.CAUSE = dr.ItemArray[14].ToString();
                    objBlank.METER_NO = dr.ItemArray[15].ToString();
                    objBlank.USP_GETFRT = dr.ItemArray[16].ToString();


                    objBlank.METER_TYPE = dr.ItemArray[17].ToString();
                    objBlank.BEFORE_RECTIFICATION = dr.ItemArray[18].ToString();
                    objBlank.AFTER_RECTIFICATION = dr.ItemArray[19].ToString();
                    objBlank.ANY_ABNORMALITY = dr.ItemArray[20].ToString();
                    objBlank.FILE = dr.ItemArray[21].ToString();
                    objBlank.SIGNATURE = dr.ItemArray[22].ToString();
                    objBlank.UPLOAD = dr.ItemArray[24].ToString();
                    objBlank.CLOSED_BY = dr.ItemArray[25].ToString();
                    objBlank.CLOSED_SOURCE = dr.ItemArray[26].ToString();
                    objBlank.MOBILE_NO = dr.ItemArray[27].ToString();
                    objBlank.ALTERNATE_MOBILE_NO = dr.ItemArray[28].ToString();
                    objBlank.REMARK = dr.ItemArray[29].ToString();

                    objBlank.Current_status = dr.ItemArray[31].ToString();
                    objBlank.Total = TotalRec;
                    lstComplaintSource.Add(objBlank);
                }

            }

            return lstComplaintSource;
        }



        private static readonly IDictionary<Type, ICollection<PropertyInfo>> _Properties =
      new Dictionary<Type, ICollection<PropertyInfo>>();

        /// <summary>
        /// Converts a DataTable to a list with generic objects
        /// </summary>
        /// <typeparam name="T">Generic object</typeparam>
        /// <param name="table">DataTable</param>
        /// <returns>List with generic objects</returns>
        public static IEnumerable<T> DataTableToList<T>(this DataTable table) where T : class, new()
        {
            try
            {
                var objType = typeof(T);
                ICollection<PropertyInfo> properties;

                lock (_Properties)
                {
                    if (!_Properties.TryGetValue(objType, out properties))
                    {
                        properties = objType.GetProperties().Where(property => property.CanWrite).ToList();
                        _Properties.Add(objType, properties);
                    }
                }

                var list = new List<T>(table.Rows.Count);

                foreach (var row in table.AsEnumerable().Skip(1))
                {
                    var obj = new T();

                    foreach (var prop in properties)
                    {
                        try
                        {
                            var propType = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;
                            var safeValue = row[prop.Name] == null ? null : Convert.ChangeType(row[prop.Name], propType);

                            prop.SetValue(obj, safeValue, null);
                        }
                        catch
                        {
                            // ignored
                        }
                    }

                    list.Add(obj);
                }

                return list;
            }
            catch
            {
                return Enumerable.Empty<T>();
            }
        }


        public static List<ModelUser> GetUserList()
        {
            List<ModelUser> obj = new List<ModelUser>();
            DataSet ds = SqlHelper.ExecuteDataset(HelperClass.Connection, CommandType.StoredProcedure, "GetUsers");
            //Bind Complaint generic list using dataRow     
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                obj.Add(

                    new ModelUser
                    {
                        User_id = Convert.ToInt32(dr["USER_ID"]),
                        User_Name = Convert.ToString(dr["USER_NAME"]),
                        Name = Convert.ToString(dr["NAME"]),
                        Role = Convert.ToString(dr["ROLE_NAME"]),
                        Mobile_NO = Convert.ToInt64(dr["MOBILE_NO"]),
                        Email = Convert.ToString(dr["EMAIL_ID"]),
                        Address = Convert.ToString(dr["ADDRESS"]),
                        Is_Active = Convert.ToBoolean(dr["IS_ACTIVE"]),
                        Is_deleted = Convert.ToBoolean(dr["IS_DELETED"]),
                    }
                    );
            }
            return (obj);
        }
        public static int SaveUser(ModelUser modelUser, string s)
        {
            int retStatus = 0;
            string retMsg = String.Empty; ;
            ModelUser obj = new ModelUser();
            obj = modelUser;

            SqlParameter parmretStatus = new SqlParameter();
            parmretStatus.ParameterName = "@retStatus";
            parmretStatus.DbType = DbType.Int32;
            parmretStatus.Size = 8;
            parmretStatus.Direction = ParameterDirection.Output;

            SqlParameter parmretMsg = new SqlParameter();
            parmretMsg.ParameterName = "@retMsg";
            parmretMsg.DbType = DbType.String;
            parmretMsg.Size = 8;
            parmretMsg.Direction = ParameterDirection.Output;

            string msg = String.Empty;


            if (s == "I")
            {
                msg = Utility.EncryptText(modelUser.Password);
            }
            else
            {
                msg = GetOldPassword(modelUser.User_Name);
            }
            //string msg = modelUser.Password;
            SqlParameter[] param ={
                new SqlParameter("@USER_ID",modelUser.User_id),
                new SqlParameter("@USER_NAME",modelUser.User_Name),
                new SqlParameter("@PASSWORD",msg),
                new SqlParameter("@NAME",modelUser.Name),
                new SqlParameter("@ROLE_ID",modelUser.RoleID),
                new SqlParameter("@ADDRESS",modelUser.Address),
                new SqlParameter("@MOBILE_NO",modelUser.Mobile_NO),
                new SqlParameter("@EMAIL_ID",modelUser.Email),
                new SqlParameter("@Phone_Login",modelUser.PhoneLogin),
                new SqlParameter("@Phone_Pass",modelUser.PhonePassword),
                new SqlParameter("@action",s),
                new SqlParameter("@OFFICE_ID",modelUser.Office_Id),
                new SqlParameter("@DIALER_STATUS",modelUser.DIALER_STATUS),
                parmretStatus,parmretMsg};
            try
            {
                SqlHelper.ExecuteNonQuery(HelperClass.Connection, CommandType.StoredProcedure, "INSERTUSER", param);

                if (param[3].Value != DBNull.Value)// status
                    retStatus = Convert.ToInt32(param[3].Value);
                else
                    retStatus = 0;
            }
            catch (Exception ex)
            {
                retStatus = -1;
            }



            return retStatus;

        }
        public static string GetOldPassword(string userid)
        {
            SqlParameter[] param ={
                    new SqlParameter("@User_Name",userid)
                                       };
            string old_password = string.Empty;
            DataSet ds = SqlHelper.ExecuteDataset(HelperClass.Connection, CommandType.StoredProcedure, "GetUsersPassword", param);
            //Bind Complaint generic list using dataRow     
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                old_password = Convert.ToString(dr["PASSWORD"]);

            }
            return (old_password);
        }

        public static ModelUser EditUser(int id)
        {
            ModelUser obj = new ModelUser();
            SqlParameter[] param ={
                    new SqlParameter("@User_ID",id)
                                       };
            DataSet ds = SqlHelper.ExecuteDataset(HelperClass.Connection, CommandType.StoredProcedure, "GetUsers", param);
            //Bind Complaint generic list using dataRow     
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                obj.User_Name = Convert.ToString(dr["USER_NAME"]);
                obj.Name = Convert.ToString(dr["NAME"]);
                obj.Role = Convert.ToString(dr["ROLE_NAME"]);
                obj.Mobile_NO = Convert.ToInt64(dr["MOBILE_NO"]);
                obj.Email = Convert.ToString(dr["EMAIL_ID"]);
                obj.Address = Convert.ToString(dr["ADDRESS"]);
                obj.Is_Active = Convert.ToBoolean(dr["IS_ACTIVE"]);
                obj.Is_deleted = Convert.ToBoolean(dr["IS_DELETED"]);

            }
            return (obj);
        }


        public static List<ModelRoles> GetRolesList()
        {
            List<ModelRoles> lstRoles = new List<ModelRoles>();
            ModelRoles objBlank = new ModelRoles();
            objBlank.RoleID = 0;
            objBlank.RoleName = "Select Roles";
            lstRoles.Insert(0, objBlank);

            DataSet ds = SqlHelper.ExecuteDataset(HelperClass.Connection, CommandType.StoredProcedure, "GetRoles");

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objBlank = new ModelRoles();
                objBlank.RoleName = dr.ItemArray[0].ToString();
                objBlank.RoleID = Convert.ToInt32(dr.ItemArray[1].ToString());
                lstRoles.Add(objBlank);
            }
            return lstRoles;
        }



        public static List<SelectListItem> UserList(int RoleId)
        {
            List<SelectListItem> lstComplaintSource = new List<SelectListItem>();
            SelectListItem objBlank = new SelectListItem();
            SqlParameter[] param ={
                    new SqlParameter("@RoleId",RoleId)};
            DataSet ds = SqlHelper.ExecuteDataset(HelperClass.Connection, CommandType.StoredProcedure, "GetUserList", param);

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objBlank = new SelectListItem();
                objBlank.Value = dr.ItemArray[0].ToString();
                objBlank.Text = dr.ItemArray[1].ToString();
                lstComplaintSource.Add(objBlank);
            }
            return lstComplaintSource;
        }

        #region Sourabh
        public static async Task<Response> SaveRemark(COMPLAINT modelRemark)
        {
            Response response = new Response();
            string retStatus = "-1";
            string retMsg = String.Empty; ;
            COMPLAINT obj = new COMPLAINT();
            obj = modelRemark;

            SqlParameter parmretStatus = new SqlParameter();
            parmretStatus.ParameterName = "@retStatus";
            parmretStatus.DbType = DbType.Int32;
            parmretStatus.Size = 8;
            parmretStatus.Direction = ParameterDirection.Output;

            SqlParameter parmretMsg = new SqlParameter();
            parmretMsg.ParameterName = "@retMsg";
            parmretMsg.DbType = DbType.String;
            parmretMsg.Size = 18;
            parmretMsg.Direction = ParameterDirection.Output;

            SqlParameter[] param ={
                new SqlParameter("@COMPLAINT_NO",modelRemark.COMPLAINT_NO),
                    new SqlParameter("@REMARK",modelRemark.REMARKS),
                    new SqlParameter("@USER_ID",modelRemark.UserId),

                    new SqlParameter("@IsResolvedByFRT",modelRemark.IsResolvedByFrt),
                    parmretStatus,parmretMsg};


            try
            {
                SqlHelper.ExecuteNonQuery(HelperClass.Connection, CommandType.StoredProcedure, "SAVE_REMARK", param);

                if (parmretStatus.Value != DBNull.Value)
                {
                    retStatus = parmretStatus.Value.ToString();
                }
                if (parmretMsg.Value != DBNull.Value)
                {
                    retMsg = parmretMsg.Value.ToString();
                }

                if (modelRemark.IsResolvedByFrt == true || modelRemark.IsResolvedByFrt is true)
                {
                    ModelComplaintSendStatusToCMS modelComplaintSendToCMS = new ModelComplaintSendStatusToCMS();
                    modelComplaintSendToCMS.compl_number = modelRemark.COMPLAINT_NO.ToString();
                    modelComplaintSendToCMS.compl_status = "Closed";
                    modelComplaintSendToCMS.compl_action_reason = "Closed By Sub-division";
                    modelComplaintSendToCMS.compl_action_description = modelRemark.REMARKS;
                    TextSmsAPI textSmsAPI1 = new TextSmsAPI();
                    string response1 = await textSmsAPI1.SendComplaintStatusToCMS(modelComplaintSendToCMS);
                }

                response.status = retStatus;
                response.message = retMsg;
            }
            catch (Exception ex)
            {
                log.Information("SaveRemark :  " + ex.Message);
                return response;
            }



            return response;

        }

        public static COMPLAINT ChangeComplaintType(Int64 id, string s, int user_id)
        {
            COMPLAINT obj = new COMPLAINT();
            SqlParameter parmretStatus = new SqlParameter();
            parmretStatus.ParameterName = "@retStatus";
            parmretStatus.DbType = DbType.Int32;
            parmretStatus.Size = 8;
            parmretStatus.Direction = ParameterDirection.Output;

            SqlParameter parmretMsg = new SqlParameter();
            parmretMsg.ParameterName = "@retMsg";
            parmretMsg.DbType = DbType.String;
            parmretMsg.Size = 8;
            parmretMsg.Direction = ParameterDirection.Output;
            SqlParameter[] param ={
                    new SqlParameter("@COMPLAINT_NO",id),
                    new SqlParameter("@PROCESS",s),
                    new SqlParameter("@USER_ID",user_id),
                    parmretStatus,parmretMsg
                                       };
            DataSet ds = SqlHelper.ExecuteDataset(HelperClass.Connection, CommandType.StoredProcedure, "CHANGE_COMPLAINT_TYPE", param);
            //Bind Complaint generic list using dataRow     
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                obj.COMPLAINT_NO = Convert.ToString(dr["COMPLAINT_NO"]);
                obj.NAME = Convert.ToString(dr["NAME"]);
                obj.FATHER_NAME = Convert.ToString(dr["FATHER_NAME"]);
                obj.KNO = Convert.ToString(dr["KNO"]);
                obj.MOBILE_NO = Convert.ToString(dr["MOBILE_NO"]);
                obj.ALTERNATE_MOBILE_NO = Convert.ToString(dr["ALTERNATE_MOBILE_NO"]);
                obj.EMAIL = Convert.ToString(dr["EMAIL"]);
                obj.COMPLAINT_TYPE = Convert.ToString(dr["COMPLAINT_TYPE"]);
                obj.ADDRESS1 = Convert.ToString(dr["ADDRESS1"]);
                obj.ADDRESS2 = Convert.ToString(dr["ADDRESS2"]);
                obj.ADDRESS3 = Convert.ToString(dr["ADDRESS3"]);
                obj.LANDMARK = Convert.ToString(dr["LANDMARK"]);
                obj.AREA_CODE = Convert.ToString(dr["AREA_CODE"]);
                obj.CONSUMER_STATUS = Convert.ToString(dr["CONSUMER_STATUS"]);
                obj.FEEDER_NAME = Convert.ToString(dr["FEEDER_NAME"]);
                obj.SUB_COMPLAINT_TYPE_ID = Convert.ToInt32(dr["SUB_COMPLAINT_TYPE_ID"]);
                obj.ComplaintTypeId = Convert.ToInt32(dr["COMPLAINT_TYPE_ID"]);

            }
            return (obj);
        }

        public static async Task<int> ChangeComplaintType_Save(COMPLAINT modelRemark, int UserID)
        {
            Response response = new Response();
            int retStatus = 0;
            string retMsg = String.Empty; ;
            COMPLAINT obj = new COMPLAINT();
            obj = modelRemark;

            SqlParameter parmretStatus = new SqlParameter();
            parmretStatus.ParameterName = "@retStatus";
            parmretStatus.DbType = DbType.Int32;
            parmretStatus.Size = 8;
            parmretStatus.Direction = ParameterDirection.Output;

            SqlParameter parmretMsg = new SqlParameter();
            parmretMsg.ParameterName = "@retMsg";
            parmretMsg.DbType = DbType.String;
            parmretMsg.Size = 8;
            parmretMsg.Direction = ParameterDirection.Output;

            SqlParameter[] param ={
                new SqlParameter("@COMPLAINT_NO",modelRemark.COMPLAINT_NO),
                    new SqlParameter("@COMPLAINT_TYPE",modelRemark.ComplaintTypeId),
                    new SqlParameter("@USER_ID",UserID),
                    new SqlParameter("@SUB_COMPLAINT_TYPE",modelRemark.SUB_COMPLAINT_TYPE_ID),
                    new SqlParameter("@PROCESS","I"),
                    parmretStatus,parmretMsg};


            try
            {
                SqlHelper.ExecuteNonQuery(HelperClass.Connection, CommandType.StoredProcedure, "CHANGE_COMPLAINT_TYPE", param);

                if (parmretStatus.Value != DBNull.Value)
                {
                    retStatus = Convert.ToInt32(parmretStatus.Value);
                    ModelComplaintTagChangeToCMS modelComplaintSendToCMS = new ModelComplaintTagChangeToCMS();
                    modelComplaintSendToCMS.compl_number = modelRemark.COMPLAINT_NO;
                    modelComplaintSendToCMS.compl_category = modelRemark.ComplaintTypeId.ToString();
                    modelComplaintSendToCMS.compl_subcategory = modelRemark.SUB_COMPLAINT_TYPE_ID.ToString();
                    TextSmsAPI textSmsAPI1 = new TextSmsAPI();
                    string response1 = await textSmsAPI1.SendComplaintTagChangeToCMS(modelComplaintSendToCMS);
                }
                if (parmretMsg.Value != DBNull.Value)
                {
                    retMsg = parmretMsg.Value.ToString();
                }

                response.status = retStatus.ToString();
                response.message = retMsg;
            }
            catch (Exception ex)
            {
                log.Information("ChangeComplaintType_Save :  " + ex.Message);
                return retStatus;
            }



            return retStatus;

        }
        public static COMPLAINT GetMobileEmail(Int64 id)
        {
            COMPLAINT obj = new COMPLAINT();
            SqlParameter[] param ={
                    new SqlParameter("@COMPLAINT_NO",id),
                                       };
            DataSet ds = SqlHelper.ExecuteDataset(HelperClass.Connection, CommandType.StoredProcedure, "GetComplaintDetail", param);
            //Bind Complaint generic list using dataRow     
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                obj.MOBILE_NO = Convert.ToString(dr["MOBILE_NO"]);
                obj.EMAIL = Convert.ToString(dr["EMAIL"]);
                obj.Complaintdate = Convert.ToString(dr["Complaint_date"]);
                obj.OFFICE_CODE_ID = Convert.ToInt64(dr["OFFICE_CODE"]);
                obj.SDO_CODE = dr["SDO_CODE"].ToString();
                obj.KNO = dr["KNO"].ToString();
                obj.NAME = dr["NAME"].ToString();
                obj.FATHER_NAME = dr["FATHER_NAME"].ToString();
                obj.ADDRESS1 = dr["ADDRESS1"].ToString();
                obj.ADDRESS2 = dr["ADDRESS2"].ToString();
                obj.ADDRESS3 = dr["ADDRESS3"].ToString();
                obj.LANDMARK = dr["LANDMARK"].ToString();
                obj.LANDLINE_NO = dr["LANDLINE_NO"].ToString();
                obj.MOBILE_NO = dr["MOBILE_NO"].ToString();
                obj.ALTERNATE_MOBILE_NO = dr["ALTERNATE_MOBILE_NO"].ToString();
                obj.CONSUMER_STATUS = dr["CONSUMER_STATUS"].ToString();
                obj.EMAIL = dr["EMAIL"].ToString();
                obj.FEEDER_NAME = dr["FEEDER_NAME"].ToString();
                obj.ACCOUNT_NO = dr["ACCOUNT_NO"].ToString();
                obj.AREA_CODE = dr["AREA_CODE"].ToString();
                obj.COMPLAINT_NO = id.ToString();


            }
            return (obj);
        }
        public static List<ModelComplaintAssign> GetAssigneeList()
        {
            List<ModelComplaintAssign> obj = new List<ModelComplaintAssign>();
            DataSet ds = SqlHelper.ExecuteDataset(HelperClass.Connection, CommandType.StoredProcedure, "GetComplaintAssignee");
            //Bind Complaint generic list using dataRow     
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                obj.Add(

                    new ModelComplaintAssign
                    {
                        AssignId = Convert.ToInt32(dr["ID"]),
                        AssigneeName = Convert.ToString(dr["ASSIGNEE"]),
                    }
                    );
            }
            return (obj);
        }
        public static async Task<int> ChangeAssignee_Save(COMPLAINT modelRemark, int UserID)
        {
            ModelSmsAPISendSMS modelSmsAPIOWN = new ModelSmsAPISendSMS();
            TextSmsAPI textSmsAPI1 = new TextSmsAPI();
            int retStatus = 0;
            int retStatus1 = 0;
            string retMsg = String.Empty; ;
            COMPLAINT obj = new COMPLAINT();
            obj = modelRemark;

            SqlParameter parmretStatus = new SqlParameter();
            parmretStatus.ParameterName = "@retStatus";
            parmretStatus.DbType = DbType.Int32;
            parmretStatus.Size = 8;
            parmretStatus.Direction = ParameterDirection.Output;

            SqlParameter parmretMsg = new SqlParameter();
            parmretMsg.ParameterName = "@retMsg";
            parmretMsg.DbType = DbType.String;
            parmretMsg.Size = 8;
            parmretMsg.Direction = ParameterDirection.Output;

            SqlParameter[] param ={
                new SqlParameter("@complaint_no",modelRemark.COMPLAINT_NO),
                    new SqlParameter("@ASSIGNEE",modelRemark.ASSIGNEEId),
                    new SqlParameter("@MOBILE_NO",modelRemark.MOBILE_NO),
                    new SqlParameter("@EMAIL",modelRemark.EMAIL),
                    new SqlParameter("@SMS",modelRemark.SMS),
                    new SqlParameter("@EMAIL_SENT",modelRemark.EMAIL_SEND),
                    new SqlParameter("@USER_ID",UserID),
                    parmretStatus,parmretMsg};


            try
            {
                SqlHelper.ExecuteNonQuery(HelperClass.Connection, CommandType.StoredProcedure, "Save_Complaint_Assignee", param);

                if (param[7].Value != DBNull.Value)// status
                    retStatus = Convert.ToInt32(param[7].Value);
                else
                    retStatus = 0;

                if (retStatus > 0)
                {
                    SqlParameter[] param1 ={
                    new SqlParameter("@ASSIGNEEId",modelRemark.ASSIGNEEId)};
                    string ASSIGNEE = "";
                    DataSet ds = SqlHelper.ExecuteDataset(HelperClass.Connection, CommandType.StoredProcedure, "GET_ASSIGNEE_NAME", param1);
                    //Bind Complaint generic list using dataRow     
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        ASSIGNEE = Convert.ToString(dr["ASSIGNEE"]);
                    }
                    ModelComplaintSendStatusToCMS modelComplaintSendToCMS = new ModelComplaintSendStatusToCMS();
                    modelComplaintSendToCMS.compl_number = modelRemark.COMPLAINT_NO.ToString();
                    modelComplaintSendToCMS.compl_status = "Complaint Assigned to " + ASSIGNEE;
                    modelComplaintSendToCMS.compl_action_reason = "Complaint Assigned to " + ASSIGNEE;
                    modelComplaintSendToCMS.compl_action_description = "Complaint Assigned";
                    string response1 = await textSmsAPI1.SendComplaintStatusToCMS(modelComplaintSendToCMS);
                }
                log.Information(modelRemark.MOBILE_NO.ToString());

            }
            catch (Exception ex)
            {
                retStatus = -1;
            }
            //try
            //{


            //    modelSmsAPIOWN.id = "0";
            //    modelSmsAPIOWN.to = modelRemark.MOBILE_NO.ToString();
            //    modelSmsAPIOWN.smsText = modelRemark.SMS;
            //    modelSmsAPIOWN.templateid = "1307160688865523002";
            //    string response2 = await textSmsAPI1.RegisterComplaintSendSMSWeb(modelSmsAPIOWN);


            //    //ModelSmsAPI modelSmsAPI = new ModelSmsAPI();
            //    //TextSmsAPI textSmsAPI = new TextSmsAPI();
            //    //modelSmsAPI.To = modelRemark.MOBILE_NO.ToString();
            //    //modelSmsAPI.Smstext = modelRemark.SMS;
            //    //modelSmsAPI.Smstemplete = "1307160688865523002";
            //    //string response = await textSmsAPI.RegisterComplaintSMSEncode(modelSmsAPI);
            //    //log.Information(response.ToString());
            //    //PUSH_SMS_DETAIL_Consumer(modelRemark, response);
            //    if (modelRemark.Assign_FRTMobile.Length == 10)
            //    {
            //        SqlParameter[] param1 ={
            //        new SqlParameter("@COMPLAINT_NO",modelRemark.COMPLAINT_NO),
            //                           };
            //        DataSet ds = SqlHelper.ExecuteDataset(HelperClass.Connection, CommandType.StoredProcedure, "GetComplaintDetail", param1);
            //        //Bind Complaint generic list using dataRow     
            //        foreach (DataRow dr in ds.Tables[0].Rows)
            //        {
            //            modelRemark.Complaintdate = Convert.ToString(dr["Complaint_date"]);
            //            modelRemark.NAME = dr["NAME"].ToString();
            //            modelRemark.FATHER_NAME = dr["FATHER_NAME"].ToString();
            //            modelRemark.ADDRESS1 = dr["ADDRESS1"].ToString();
            //            modelRemark.ADDRESS2 = dr["ADDRESS2"].ToString();
            //            modelRemark.ADDRESS3 = dr["ADDRESS3"].ToString();
            //            modelRemark.LANDMARK = dr["LANDMARK"].ToString();
            //            modelRemark.COMPLAINT_TYPE = dr["COMPLAINT_TYPE"].ToString();
            //            modelRemark.ALTERNATE_MOBILE_NO = dr["ALTERNATE_MOBILE_NO"].ToString();

            //        }
            //        log.Information(modelRemark.MOBILE_NO.ToString());
            //        modelSmsAPIOWN.id = "0";
            //        modelSmsAPIOWN.to = modelRemark.Assign_FRTMobile.ToString();
            //        string address = modelRemark.ADDRESS1 + "," + modelRemark.ADDRESS2 + "," + modelRemark.ADDRESS3;
            //        if (modelRemark.ASSIGNEEId == 8)
            //        {
            //            modelSmsAPIOWN.smsText = "Dear FRT Complaint has been assigned to you with the details below COMPLAINT TYPE : " + modelRemark.COMPLAINT_TYPE + " ,COMPLAINT NO: " + modelRemark.COMPLAINT_NO + " ,NAME OF CONSUMER: " + modelRemark.NAME + " ,ADDRESS OF CONSUMER: " + address.Substring(0, 10) + ", Mobile No. " + modelRemark.MOBILE_NO + "-JDVVNL";
            //        }
            //        else
            //        {
            //            modelSmsAPIOWN.smsText = "Dear Sir Complaint has been assigned to you with the details below COMPLAINT TYPE : " + modelRemark.COMPLAINT_TYPE + " ,COMPLAINT NO: " + modelRemark.COMPLAINT_NO + " ,NAME OF CONSUMER: " + modelRemark.NAME + " ,ADDRESS OF CONSUMER: " + address.Substring(0, 10) + ", Mobile No. " + modelRemark.MOBILE_NO + "-JDVVNL";
            //        }
            //        //modelSmsAPIOWN.smsText = modelRemark.SMS;
            //        modelSmsAPIOWN.templateid = "1307160472989225821";
            //        string response3 = await textSmsAPI1.RegisterComplaintSendSMSWebEng(modelSmsAPIOWN);



            //        //ModelSmsAPI modelSmsAPI_FRT = new ModelSmsAPI();
            //        //TextSmsAPI textSmsAPI1 = new TextSmsAPI();
            //        //modelSmsAPI_FRT.To = modelRemark.Assign_FRTMobile.ToString();
            //        //modelSmsAPI_FRT.Smstemplete = "1307160472989225821";
            //        //string address = modelRemark.ADDRESS1 + "," + modelRemark.ADDRESS2 + "," + modelRemark.ADDRESS3;
            //        //if (modelRemark.ASSIGNEEId == 8)
            //        //{
            //        //    modelSmsAPI_FRT.Smstext = "Dear FRT Complaint has been assigned to you with the details below COMPLAINT TYPE : " + modelRemark.COMPLAINT_TYPE + " ,COMPLAINT NO: " + modelRemark.COMPLAINT_NO + " ,NAME OF CONSUMER: " + modelRemark.NAME + " ,ADDRESS OF CONSUMER: " + address.Substring(0, 10) + ", Mobile No. " + modelRemark.MOBILE_NO + "-JDVVNL";
            //        //}
            //        //else
            //        //{
            //        //    modelSmsAPI_FRT.Smstext = "Dear Sir Complaint has been assigned to you with the details below COMPLAINT TYPE : " + modelRemark.COMPLAINT_TYPE + " ,COMPLAINT NO: " + modelRemark.COMPLAINT_NO + " ,NAME OF CONSUMER: " + modelRemark.NAME + " ,ADDRESS OF CONSUMER: " + address.Substring(0, 10) + ", Mobile No. " + modelRemark.MOBILE_NO + "-JDVVNL";
            //        //}
            //        //string response1 = await textSmsAPI1.RegisterComplaintSMS(modelSmsAPI_FRT);
            //        //modelRemark.SMS = modelSmsAPI_FRT.Smstext;
            //        //modelRemark.MOBILE_NO = modelSmsAPI_FRT.To;
            //        //log.Information(response1.ToString());
            //        //PUSH_SMS_DETAIL_Consumer(modelRemark, response1);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    retStatus1 = -1;
            //}

            return retStatus;

        }
        public static List<ModelSearchComplaint> GetReopenComplaintDetails(ModelSearchComplaint dataObject)
        {
            List<ModelSearchComplaint> lstComplaintSource = new List<ModelSearchComplaint>();
            ModelSearchComplaint objBlank = new ModelSearchComplaint();
            //objBlank.Value = "0";
            //objBlank.Text = "Select Source";
            //lstComplaintSource.Insert(0, objBlank);
            SqlParameter[] param ={
                new SqlParameter("@complaint_no",dataObject.COMPLAINT_NO),
                    new SqlParameter("@kno",dataObject.KNO),
                    new SqlParameter("@mobile_no",dataObject.MOBILE_NO),

                    };

            DataSet ds = SqlHelper.ExecuteDataset(HelperClass.Connection, CommandType.StoredProcedure, "GetReopenComplaintDetails", param);

            // lstComplaintSource = DataTableToList<ModelSearchComplaint>(ds.Tables[0]).ToList();

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objBlank = new ModelSearchComplaint();
                objBlank.KNO = (dr.ItemArray[0].ToString());
                objBlank.NAME = dr.ItemArray[1].ToString();
                objBlank.COMPLAINT_DATE = dr.ItemArray[2].ToString().Substring(0, 10);
                objBlank.COMPLAINT_NO = Convert.ToInt64(dr.ItemArray[3].ToString());
                objBlank.OFFICE_NAME = dr.ItemArray[4].ToString();
                objBlank.ADDRESS = dr.ItemArray[5].ToString();
                objBlank.COMPLAINT_TYPE = dr.ItemArray[6].ToString();
                objBlank.SOURCE_NAME = dr.ItemArray[7].ToString();
                objBlank.COMPLAINT_status = dr.ItemArray[8].ToString();
                objBlank.ASSIGNED_TO = dr.ItemArray[9].ToString();
                objBlank.MOBILE_NO = dr.ItemArray[10].ToString();
                objBlank.LANDLINE_NO = dr.ItemArray[11].ToString();
                objBlank.REJECTION_CAUSE = dr.ItemArray[12].ToString();
                lstComplaintSource.Add(objBlank);
            }
            return lstComplaintSource;
        }
        public static int ReopenComplaint(Int64 s, string remark, int userID)
        {
            int retStatus = 0;
            string retMsg = String.Empty; ;
            SqlParameter parmretStatus = new SqlParameter();
            parmretStatus.ParameterName = "@retStatus";
            parmretStatus.DbType = DbType.Int32;
            parmretStatus.Size = 8;
            parmretStatus.Direction = ParameterDirection.Output;

            SqlParameter parmretMsg = new SqlParameter();
            parmretMsg.ParameterName = "@retMsg";
            parmretMsg.DbType = DbType.String;
            parmretMsg.Size = 8;
            parmretMsg.Direction = ParameterDirection.Output;
            SqlParameter[] param ={
                new SqlParameter("@COMPLAINT_NO",s),
                new SqlParameter("@remark",remark),
                new SqlParameter("@UserID",userID),
                    parmretStatus,parmretMsg};


            try
            {
                SqlHelper.ExecuteNonQuery(HelperClass.Connection, CommandType.StoredProcedure, "Complaint_Repoen", param);

                if (param[3].Value != DBNull.Value)// status
                    retStatus = Convert.ToInt32(param[3].Value);
                else
                    retStatus = 0;
            }
            catch (Exception ex)
            {
                retStatus = -1;
            }



            return retStatus;

        }
        public static string ChangePassword(ModelUser modelUser)
        {

            string retStatus = "0";
            string retMsg = String.Empty; ;
            ModelUser obj = new ModelUser();
            obj = modelUser;

            SqlParameter parmretStatus = new SqlParameter();
            parmretStatus.ParameterName = "@retStatus";
            parmretStatus.DbType = DbType.Int32;
            parmretStatus.Size = 8;
            parmretStatus.Direction = ParameterDirection.Output;

            SqlParameter parmretMsg = new SqlParameter();
            parmretMsg.ParameterName = "@retMsg";
            parmretMsg.DbType = DbType.String;
            parmretMsg.Size = 8;
            parmretMsg.Direction = ParameterDirection.Output;

            string msg = Utility.EncryptText(modelUser.Password);
            //string msg = modelUser.Password;
            SqlParameter[] param ={
                new SqlParameter("@USER_ID",modelUser.User_id),
                    new SqlParameter("@PASSWORD",msg),
                    parmretStatus,parmretMsg};
            if (modelUser.Password.ToUpper() == modelUser.Confirm_Password.ToUpper())
            {

                try
                {
                    SqlHelper.ExecuteNonQuery(HelperClass.Connection, CommandType.StoredProcedure, "ChangePassword", param);

                    if (param[3].Value != DBNull.Value)// status
                        retStatus = "Password Changed Successfully";
                    else
                        retStatus = "Password Not Changed";
                }
                catch (Exception ex)
                {
                    retStatus = "Password Not Changed";
                }
            }
            else
            {
                retStatus = "New And Confirm Password not match";
            }



            return retStatus;

        }
        public static DataSet ShowComplaintDetails(string id)
        {
            DataSet ds = new DataSet();
            SqlParameter[] param ={
                new SqlParameter("@Complaint_no",id)};
            try
            {
                //SqlHelper.ExecuteNonQuery(HelperClass.Connection, CommandType.StoredProcedure, "ChangePassword", param);

                ds = SqlHelper.ExecuteDataset(HelperClass.Connection, CommandType.StoredProcedure, "ShowComplaint_Detail", param);

            }
            catch (Exception ex)
            {
                ds = new DataSet();
            }
            return ds;
        }
        public static async Task<int> CloseComplaint_Save(ModelCloseComplaint modelRemark, int UserID)
        {
            int retStatus = 0;
            int retStatus1 = 0;
            string retMsg = String.Empty; ;
            ModelCloseComplaint obj = new ModelCloseComplaint();
            obj = modelRemark;

            SqlParameter parmretStatus = new SqlParameter();
            parmretStatus.ParameterName = "@retStatus";
            parmretStatus.DbType = DbType.Int32;
            parmretStatus.Size = 8;
            parmretStatus.Direction = ParameterDirection.Output;

            SqlParameter parmretMsg = new SqlParameter();
            parmretMsg.ParameterName = "@retMsg";
            parmretMsg.DbType = DbType.String;
            parmretMsg.Size = 8;
            parmretMsg.Direction = ParameterDirection.Output;
            string abnoramility = "";

            if (modelRemark.AbnormalityCollectionId == 1)
                abnoramility = "Yes";
            else
                abnoramility = "NO";
            SqlParameter[] param ={
                new SqlParameter("@complaint_no",modelRemark.ComplaintNo),
                    new SqlParameter("@OUTAGE_TYPE",modelRemark.OutageTypeCollectionId1),
                    new SqlParameter("@OUTAGE_SUB_TYPE",0),
                    new SqlParameter("@OUTAGE_CAUSE",0),
                    new SqlParameter("@STATUS_BEFORE_RECTIFICATION",modelRemark.StatusBeforeRectification),
                    new SqlParameter("@STATUS_AFTER_RECTIFICATION",modelRemark.StatusAfterRectification),
                    new SqlParameter("@USER_ID",UserID),
                    new SqlParameter("@METER_NO",modelRemark.MeterNo),
                    new SqlParameter("@READING",modelRemark.Reading),
                    new SqlParameter("@TYPE",modelRemark.Type),
                    new SqlParameter("@ANYABNORMATITY",abnoramility),
                    new SqlParameter("@CONFIRM_BY_CONSUMER",modelRemark.IsConfirmByCustomer),
                    new SqlParameter("@REMARK",modelRemark.Remarks),
                    parmretStatus,parmretMsg};


            try
            {
                SqlHelper.ExecuteNonQuery(HelperClass.Connection, CommandType.StoredProcedure, "Save_Complaint_Close", param);

                if (param[13].Value != DBNull.Value)// status
                    retStatus = Convert.ToInt32(param[13].Value);
                else
                    retStatus = 0;
                if (retStatus > 0)
                {
                    SqlParameter[] param1 ={
                    new SqlParameter("@COMPLAINT_NO",modelRemark.ComplaintNo.ToString()),
                    new SqlParameter("@Outage_TYPE_ID",modelRemark.OutageTypeCollectionId1)};
                    string KNO = "";
                    string OutageType = "";
                    DataSet ds = SqlHelper.ExecuteDataset(HelperClass.Connection, CommandType.StoredProcedure, "GET_KNO_COMPLAINT_OUTAGE_TYPE", param1);
                    //Bind Complaint generic list using dataRow     
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        KNO = Convert.ToString(dr["KNO"]);
                        OutageType = Convert.ToString(dr["OUTAGE_TYPE"]);
                    }
                    ModelComplaintSendStatusToCMS modelComplaintSendToCMS = new ModelComplaintSendStatusToCMS();
                    modelComplaintSendToCMS.compl_number = modelRemark.ComplaintNo.ToString();
                    modelComplaintSendToCMS.compl_status = "Closed";
                    modelComplaintSendToCMS.compl_action_reason = OutageType;
                    modelComplaintSendToCMS.compl_action_description = modelRemark.Remarks;
                    TextSmsAPI textSmsAPI1 = new TextSmsAPI();
                    string response1 = await textSmsAPI1.SendComplaintStatusToCMS(modelComplaintSendToCMS);
                }


            }
            catch (Exception ex)
            {
                retStatus = -1;
            }
            //try
            //{
            //    COMPLAINT cOMPLAINT = Repository.GetMobileEmail(Convert.ToInt64(modelRemark.ComplaintNo));

            //    ModelSmsAPISendSMS modelSmsAPIOWN = new ModelSmsAPISendSMS();
            //    TextSmsAPI textSmsAPI1 = new TextSmsAPI();
            //    modelSmsAPIOWN.id = "0";
            //    modelSmsAPIOWN.to = cOMPLAINT.MOBILE_NO.ToString();
            //    modelSmsAPIOWN.smsText = "प्रिय उपभोक्ता, शिकायत क्रमांक " + cOMPLAINT.COMPLAINT_NO + " बंद की जा रही है। जोधपुर डिस्कॉम।";
            //    modelSmsAPIOWN.templateid = "1307160688875923092";
            //    string response1 = await textSmsAPI1.RegisterComplaintSendSMSWeb(modelSmsAPIOWN);


            //    log.Information(cOMPLAINT.MOBILE_NO.ToString());
            //    //ModelSmsAPI modelSmsAPI = new ModelSmsAPI();
            //    //TextSmsAPI textSmsAPI = new TextSmsAPI();
            //    //modelSmsAPI.To = cOMPLAINT.MOBILE_NO.ToString();
            //    //modelSmsAPI.Smstemplete = "1307160688875923092";
            //    //modelSmsAPI.Smstext = "प्रिय उपभोक्ता, शिकायत क्रमांक " + cOMPLAINT.COMPLAINT_NO + " बंद की जा रही है। जोधपुर डिस्कॉम।";
            //    //string response = await textSmsAPI.RegisterComplaintSMSEncode(modelSmsAPI);
            //    //cOMPLAINT.SMS = modelSmsAPI.Smstext;
            //    //log.Information(response.ToString());

            //    ////if (response.Contains("avvnlalt"))
            //    ////{
            //    //PUSH_SMS_DETAIL_Consumer(cOMPLAINT, response);
            //}
            //catch(Exception ex) 
            //{ 
            //    retStatus1 = -1;
            //}


            return retStatus;

        }
        #endregion

        #region Close Complaint
        public static List<SelectListItem> GetOutageType()
        {
            List<SelectListItem> lstComplaintSource = new List<SelectListItem>();
            SelectListItem objBlank = new SelectListItem();
            DataSet ds = SqlHelper.ExecuteDataset(HelperClass.Connection, CommandType.StoredProcedure, "GetOutageType");

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objBlank = new SelectListItem();
                objBlank.Value = dr.ItemArray[0].ToString();
                objBlank.Text = dr.ItemArray[1].ToString();
                lstComplaintSource.Add(objBlank);
            }
            return lstComplaintSource;
        }
        public static List<SelectListItem> GetOutageTypeSub(int OutageTypeId)
        {
            List<SelectListItem> lstComplaintSource = new List<SelectListItem>();
            SelectListItem objBlank = new SelectListItem();
            SqlParameter[] param ={
                new SqlParameter("@OUTAGE_TYPE_ID",OutageTypeId)};
            DataSet ds = SqlHelper.ExecuteDataset(HelperClass.Connection, CommandType.StoredProcedure, "GetSubOutageType", param);

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objBlank = new SelectListItem();
                objBlank.Value = dr.ItemArray[0].ToString();
                objBlank.Text = dr.ItemArray[1].ToString();
                lstComplaintSource.Add(objBlank);
            }
            return lstComplaintSource;
        }
        public static List<SelectListItem> GetOutageCause(int OutageTypeId)
        {
            List<SelectListItem> lstComplaintSource = new List<SelectListItem>();
            SelectListItem objBlank = new SelectListItem();
            SqlParameter[] param ={
                new SqlParameter("@OUTAGE_TYPE_ID",OutageTypeId)};
            DataSet ds = SqlHelper.ExecuteDataset(HelperClass.Connection, CommandType.StoredProcedure, "GetOutageCause", param);

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objBlank = new SelectListItem();
                objBlank.Value = dr.ItemArray[0].ToString();
                objBlank.Text = dr.ItemArray[1].ToString();
                lstComplaintSource.Add(objBlank);
            }
            return lstComplaintSource;
        }

        #endregion


        #region reports

        public static List<ModelRawComplaintReport> ReportRawComplaintData(ModelReport dataObject)
        {
            SqlParameter parmretTotalRecords = new SqlParameter();
            parmretTotalRecords.ParameterName = "@TOTALRECORDS";
            parmretTotalRecords.DbType = DbType.Int32;
            parmretTotalRecords.Size = 8;
            parmretTotalRecords.Direction = ParameterDirection.Output;

            int TotalRec = 0;

            List<ModelRawComplaintReport> lstReportdata = new List<ModelRawComplaintReport>();
            ModelRawComplaintReport objData = new ModelRawComplaintReport();
            SqlParameter[] param ={
                    new SqlParameter("@START_DATE",dataObject.fromdate),
                    new SqlParameter("@END_DATE",dataObject.todate),
                    new SqlParameter("@OFFICE_ID",dataObject.OfficeCode),
                    new SqlParameter("@Complaint_source",dataObject.ComplaintSource),
                    new SqlParameter("@Complaint_Type",dataObject.ComplaintType),
                    new SqlParameter("@STARTROWINDEX",dataObject.start),
                    new SqlParameter("@MAXIMUMROWS",dataObject.length),parmretTotalRecords};

            log.Debug(" RAW_COMPLAINT IP " + HelperClass.GetIPHelper() + " Proc Start Time :  " + DateTime.Now.ToString());
            DataSet ds = SqlHelper.ExecuteDataset(HelperClass.Connection, CommandType.StoredProcedure, "RAW_COMPLAINT", param);
            log.Debug(" RAW_COMPLAINT IP " + HelperClass.GetIPHelper() + " Proc End Time :  " + DateTime.Now.ToString());
            if (param[3].Value != DBNull.Value)// status
                TotalRec = Convert.ToInt32(param[7].Value);
            else
                TotalRec = 0;

            if (ds.Tables.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    objData = new ModelRawComplaintReport();
                    objData.SDO_CODE = Convert.ToString(dr.ItemArray[1].ToString());
                    objData.SubDivisionName = Convert.ToString(dr.ItemArray[2].ToString());
                    objData.COMPLAINT_NO = Convert.ToString(dr.ItemArray[0].ToString());
                    objData.AreaCode = Convert.ToString(dr.ItemArray[14].ToString());
                    objData.Name = Convert.ToString(dr.ItemArray[4].ToString());
                    objData.FatherName = Convert.ToString(dr.ItemArray[5].ToString());
                    objData.Address = Convert.ToString(dr.ItemArray[6].ToString());
                    objData.AlternateNo = Convert.ToString(dr.ItemArray[8].ToString());
                    objData.MobileNo = Convert.ToString(dr.ItemArray[7].ToString());
                    objData.KNO = Convert.ToString(dr.ItemArray[3].ToString());
                    objData.ComplaintType = Convert.ToString(dr.ItemArray[9].ToString());
                    objData.ComplaintDate = Convert.ToString(dr.ItemArray[11].ToString());
                    objData.SubComplaintType = Convert.ToString(dr.ItemArray[10].ToString());
                    objData.ClosedDate = Convert.ToString(dr.ItemArray[12].ToString());
                    objData.Duration = Convert.ToString(dr.ItemArray[13].ToString());
                    objData.CurrentStatus = Convert.ToString(dr.ItemArray[15].ToString());
                    objData.SOURCE_NAME = Convert.ToString(dr.ItemArray[16].ToString());
                    objData.CreatedUserID = Convert.ToString(dr.ItemArray[17].ToString());
                    objData.ClosedUserID = Convert.ToString(dr.ItemArray[18].ToString());
                    objData.Circle = Convert.ToString(dr.ItemArray[19].ToString());
                    objData.Division = Convert.ToString(dr.ItemArray[20].ToString());
                    objData.Total = TotalRec;
                    lstReportdata.Add(objData);
                }
            }
            return lstReportdata;
        }

        public static List<ModelRawComplaintReport> ReportRawComplaintData()
        {
            ModelReport dataObject = new ModelReport();
            List<ModelRawComplaintReport> lstReportdata = new List<ModelRawComplaintReport>();
            ModelRawComplaintReport objData = new ModelRawComplaintReport();
            SqlParameter[] param ={
                    new SqlParameter("@START_DATE",dataObject.fromdate),
                    new SqlParameter("@END_DATE",dataObject.todate),
                    new SqlParameter("@OFFICE_ID",dataObject.OfficeCode),
                    new SqlParameter("@Complaint_source",dataObject.ComplaintSource),
                    new SqlParameter("@Complaint_Type",dataObject.ComplaintType)

            };
            DataSet ds = SqlHelper.ExecuteDataset(HelperClass.Connection, CommandType.StoredProcedure, "RAW_COMPLAINT", param);
            if (ds.Tables.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    objData = new ModelRawComplaintReport();
                    objData.SDO_CODE = Convert.ToString(dr.ItemArray[1].ToString());
                    objData.SubDivisionName = Convert.ToString(dr.ItemArray[2].ToString());
                    objData.COMPLAINT_NO = Convert.ToString(dr.ItemArray[0].ToString());
                    objData.AreaCode = Convert.ToString(dr.ItemArray[14].ToString());
                    objData.Name = Convert.ToString(dr.ItemArray[4].ToString());
                    objData.FatherName = Convert.ToString(dr.ItemArray[5].ToString());
                    objData.Address = Convert.ToString(dr.ItemArray[6].ToString());
                    objData.AlternateNo = Convert.ToString(dr.ItemArray[8].ToString());
                    objData.MobileNo = Convert.ToString(dr.ItemArray[7].ToString());
                    objData.KNO = Convert.ToString(dr.ItemArray[3].ToString());
                    objData.ComplaintType = Convert.ToString(dr.ItemArray[9].ToString());
                    objData.ComplaintDate = Convert.ToString(dr.ItemArray[11].ToString());
                    objData.SubComplaintType = Convert.ToString(dr.ItemArray[10].ToString());
                    objData.ClosedDate = Convert.ToString(dr.ItemArray[12].ToString());
                    objData.Duration = Convert.ToString(dr.ItemArray[13].ToString());
                    objData.CurrentStatus = Convert.ToString(dr.ItemArray[15].ToString());
                    objData.SOURCE_NAME = Convert.ToString(dr.ItemArray[16].ToString());
                    objData.CreatedUserID = Convert.ToString(dr.ItemArray[17].ToString());
                    objData.ClosedUserID = Convert.ToString(dr.ItemArray[18].ToString());
                    lstReportdata.Add(objData);
                }
            }
            return lstReportdata;
        }

        public static List<ModelQueryBuilderReport> ReportQueryBuilder(string sql, string OfficeID)
        {
            List<ModelQueryBuilderReport> lstReportdata = new List<ModelQueryBuilderReport>();
            ModelQueryBuilderReport objData = new ModelQueryBuilderReport();
            SqlParameter[] param ={
                    new SqlParameter("@OFFICE_ID",OfficeID),
                     new SqlParameter("@Where_Clause",sql)

            };
            DataSet ds = SqlHelper.ExecuteDataset(HelperClass.Connection, CommandType.StoredProcedure, "Query_Builder", param);
            if (ds.Tables.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    objData = new ModelQueryBuilderReport();
                    objData.KNO = Convert.ToString(dr.ItemArray[0]);
                    objData.Consumer_name = Convert.ToString(dr.ItemArray[1]);
                    objData.ComplaintDate = Convert.ToString(dr.ItemArray[2].ToString());
                    objData.Duration = Convert.ToString(dr.ItemArray[3].ToString());
                    objData.Complaint_no = Convert.ToString(dr.ItemArray[4].ToString());
                    objData.office_name = Convert.ToString(dr.ItemArray[5].ToString());
                    objData.Address = Convert.ToString(dr.ItemArray[6].ToString());
                    objData.Complaint_Type = Convert.ToString(dr.ItemArray[7].ToString());
                    objData.Sub_Complaint_Type = Convert.ToString(dr.ItemArray[8].ToString());
                    objData.Complaint_Source = Convert.ToString(dr.ItemArray[9].ToString());
                    objData.Complaint_Status = Convert.ToString(dr.ItemArray[10].ToString());

                    lstReportdata.Add(objData);
                }
            }
            return lstReportdata;
        }



        public static List<ModelRandomizerReport> ReportRandomizerData(ModelReport dataObject)
        {


            List<ModelRandomizerReport> lstReportdata = new List<ModelRandomizerReport>();
            ModelRandomizerReport objData = new ModelRandomizerReport();

            SqlParameter parmretTotalRecords = new SqlParameter();
            parmretTotalRecords.ParameterName = "@TOTALRECORDS";
            parmretTotalRecords.DbType = DbType.Int32;
            parmretTotalRecords.Size = 8;
            parmretTotalRecords.Direction = ParameterDirection.Output;

            int TotalRec = 0;

            SqlParameter[] param ={
                    new SqlParameter("@FROM_DATE",dataObject.fromdate),
                     new SqlParameter("@TO_DATE",dataObject.todate),
                     new SqlParameter("@COUNT",string.IsNullOrEmpty( dataObject.count) ? "0": dataObject.count),
                     new SqlParameter("@STARTROWINDEX",dataObject.start),
                    new SqlParameter("@MAXIMUMROWS",dataObject.length),parmretTotalRecords};


            DataSet ds = SqlHelper.ExecuteDataset(HelperClass.Connection, CommandType.StoredProcedure, "RANDOMIZER", param);
            if (param[5].Value != DBNull.Value)// status
                TotalRec = Convert.ToInt32(param[5].Value);
            else
                TotalRec = 0;
            if (ds.Tables.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    objData = new ModelRandomizerReport();
                    objData.KNO = Convert.ToString(dr.ItemArray[1].ToString());
                    objData.OFFICE_CODE = Convert.ToString(dr.ItemArray[2]);
                    objData.SDOCode = Convert.ToString(dr.ItemArray[3]);


                    objData.CONSUMER_NAME = Convert.ToString(dr.ItemArray[4].ToString());
                    objData.MOBILE_NO = Convert.ToString(dr.ItemArray[5].ToString());
                    objData.TIME_STAMP = Convert.ToString(dr.ItemArray[6].ToString());
                    objData.CLOSE_DATE = Convert.ToString(dr.ItemArray[7].ToString());
                    objData.DURATION = Convert.ToString(dr.ItemArray[8].ToString());
                    objData.COMPLAINT_NO = Convert.ToString(dr.ItemArray[9].ToString());
                    objData.COMPLAINT_TYPE = Convert.ToString(dr.ItemArray[10].ToString());
                    objData.SUB_COMPLAINT_TYPE = Convert.ToString(dr.ItemArray[11].ToString());
                    objData.COMPLAINT_SOURCE = Convert.ToString(dr.ItemArray[12].ToString());
                    objData.REMARKS = Convert.ToString(dr.ItemArray[13].ToString());
                    objData.Total = TotalRec;
                    lstReportdata.Add(objData);
                }
            }
            return lstReportdata;
        }
        #endregion

        #region Dashboard
        public static ModelDashboard GetDashBoardData(String OFFICE_ID)
        {
            ModelDashboard dashboard = new ModelDashboard();
            List<ComplaintSummaryGraph> lstComplaintSummary = new List<ComplaintSummaryGraph>();
            List<CircleWiseComplaintSummary> lstComplaintSummary1 = new List<CircleWiseComplaintSummary>();
            ComplaintSummaryGraph objBlank = null;
            CircleWiseComplaintSummary objBlank1 = null;

            SqlParameter[] param = { new SqlParameter("@OFFICE_ID", OFFICE_ID) };
            DataSet ds = SqlHelper.ExecuteDataset(HelperClass.Connection, CommandType.StoredProcedure, "DASHBOARD_new", param);

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objBlank = new ComplaintSummaryGraph();
                objBlank.Month = dr.ItemArray[0].ToString();
                objBlank.TotalComplaint = dr.ItemArray[1].ToString();
                objBlank.ResolveComplaint = dr.ItemArray[2].ToString();
                lstComplaintSummary.Add(objBlank);
            }
            dashboard.ComplaintSummaries = lstComplaintSummary;

            foreach (DataRow dr in ds.Tables[1].Rows)
            {
                dashboard.Hares_TOTAL_COMPLAINTS_TILL_DATE = dr.ItemArray[0].ToString();
                dashboard.Hares_TOTAL_COMPLAINTS_RESOLVED_TILL_DATE = dr.ItemArray[1].ToString();
                dashboard.Hares_TOTAL_COMPLAINTS_FOR_CURRENT_MONTH = dr.ItemArray[2].ToString();
                dashboard.Hares_TOTAL_COMPLAINTS_RESOLVED_IN_CURRENT_MONTH = dr.ItemArray[3].ToString();
            }

            foreach (DataRow dr in ds.Tables[2].Rows)
            {
                dashboard.CurrentMonthTotalComplaint = dr.ItemArray[0].ToString();
                dashboard.CurrentMonthResolvedComplaint = dr.ItemArray[1].ToString();
                dashboard.PreviousMonthTotalComplaint = dr.ItemArray[2].ToString();
                dashboard.PreviousMonthResolvedComplaint = dr.ItemArray[3].ToString();
            }

            foreach (DataRow dr in ds.Tables[3].Rows)
            {
                objBlank1 = new CircleWiseComplaintSummary();
                objBlank1.CircleName = dr.ItemArray[0].ToString();
                objBlank1.TotalComplaint = dr.ItemArray[1].ToString();
                objBlank1.TotalPendingComplaints = dr.ItemArray[2].ToString();
                objBlank1.TotalReopenComplaint = dr.ItemArray[3].ToString();
                objBlank1.TotalResolvedComplaints = dr.ItemArray[4].ToString();
                lstComplaintSummary1.Add(objBlank1);
            }
            dashboard.CircleWiseComplaintSummaryData = lstComplaintSummary1;
            return dashboard;
        }
        #endregion


        #region saveApiCalls

        public static void SaveLogin(ModelUser modelUser)
        {
            SqlParameter LOGIN_TIME = new SqlParameter();
            LOGIN_TIME.ParameterName = "@LOGIN_TIME";
            LOGIN_TIME.DbType = DbType.DateTime;
            LOGIN_TIME.Size = 8;
            LOGIN_TIME.Direction = ParameterDirection.Input;
            LOGIN_TIME.Value = DateTime.Now;

            SqlParameter LOGOUT_TIME = new SqlParameter();
            LOGOUT_TIME.ParameterName = "@LOGOUT_TIME";
            LOGOUT_TIME.DbType = DbType.DateTime;
            LOGOUT_TIME.Size = 8;
            LOGOUT_TIME.Direction = ParameterDirection.Input;
            LOGOUT_TIME.Value = DateTime.Now;

            SqlParameter BREAK_TIME = new SqlParameter();
            BREAK_TIME.ParameterName = "@BREAK_TIME";
            BREAK_TIME.DbType = DbType.DateTime;
            BREAK_TIME.Size = 8;
            BREAK_TIME.Direction = ParameterDirection.Input;
            BREAK_TIME.Value = DateTime.Now;

            SqlParameter BREAK_END_TIME = new SqlParameter();
            BREAK_END_TIME.ParameterName = "@BREAK_END_TIME";
            BREAK_END_TIME.DbType = DbType.DateTime;
            BREAK_END_TIME.Size = 8;
            BREAK_END_TIME.Direction = ParameterDirection.Input;
            BREAK_END_TIME.Value = DateTime.Now;






            SqlParameter[] param ={
                  new SqlParameter("@AGENT_ID",modelUser.User_Name),
                    new SqlParameter("@AGENT_TYPE",modelUser.agent_type),LOGIN_TIME,LOGOUT_TIME,BREAK_TIME,BREAK_END_TIME,

                  new SqlParameter("@PROCESS_TYPE","IN"),

                                       };
            DataSet ds = SqlHelper.ExecuteDataset(HelperClass.Connection, CommandType.StoredProcedure, "AGENT_LOGIN", param);


        }

        #endregion

        #region Vaibhav
        public static int SaveCall(string agent_id, string phone, string agent_type, DateTime callStart, string callType, string callStatus, string Transfer_From, string Processes)
        {
            SqlParameter[] param =
            {
                new SqlParameter("@AGENT_ID",agent_id),
                new SqlParameter("@CALLER_NUMBER",phone),
                new SqlParameter("@AGENT_TYPE",agent_type),
                new SqlParameter("@CALL_START_TIME",callStart),
                new SqlParameter("@CALL_END_TIME",DateTime.Now),
                new SqlParameter("@CALL_HOLD_TIME",DateTime.Now),
                new SqlParameter("@CALL_PICKUP_TIME",DateTime.Now),
                new SqlParameter("@CALL_TYPE",callType),
                new SqlParameter("@CALL_STATUS",callStatus),
                new SqlParameter("@TRANSFER_FROM",Transfer_From),
                new SqlParameter("@PROCESS",Processes) };
            SqlHelper.ExecuteDataset(HelperClass.Connection, CommandType.StoredProcedure, "OUTBOUND_CALL_DETAIL", param);
            return 1;
        }
        public static int HoldCall(string agent_id, string phone, string agent_type, DateTime callHold, string callType, string Transfer_From, string Processes)
        {
            SqlParameter[] param =
            {
                new SqlParameter("@AGENT_ID",agent_id),
                new SqlParameter("@CALLER_NUMBER",phone),
                new SqlParameter("@AGENT_TYPE",agent_type),
                new SqlParameter("@CALL_START_TIME",DateTime.Now),
                new SqlParameter("@CALL_END_TIME",DateTime.Now),
                new SqlParameter("@CALL_HOLD_TIME",callHold),
                new SqlParameter("@CALL_PICKUP_TIME",DateTime.Now),
                new SqlParameter("@CALL_TYPE",callType),
                new SqlParameter("@CALL_STATUS","Hold"),
                new SqlParameter("@TRANSFER_FROM",Transfer_From),
                new SqlParameter("@PROCESS",Processes) };
            SqlHelper.ExecuteDataset(HelperClass.Connection, CommandType.StoredProcedure, "OUTBOUND_CALL_DETAIL", param);
            return 1;
        }
        public static int PickCall(string agent_id, string phone, string agent_type, DateTime callpick, string callType, string Transfer_From, string Processes)
        {
            SqlParameter[] param =
            {
                new SqlParameter("@AGENT_ID",agent_id),
                new SqlParameter("@CALLER_NUMBER",phone),
                new SqlParameter("@AGENT_TYPE",agent_type),
                new SqlParameter("@CALL_START_TIME",DateTime.Now),
                new SqlParameter("@CALL_END_TIME",DateTime.Now),
                new SqlParameter("@CALL_HOLD_TIME",DateTime.Now),
                new SqlParameter("@CALL_PICKUP_TIME",callpick),
                new SqlParameter("@CALL_TYPE",callType),
                new SqlParameter("@CALL_STATUS","Hold"),
                new SqlParameter("@TRANSFER_FROM",Transfer_From),
                new SqlParameter("@PROCESS",Processes) };
            SqlHelper.ExecuteDataset(HelperClass.Connection, CommandType.StoredProcedure, "OUTBOUND_CALL_DETAIL", param);
            return 1;
        }
        public static int HangCall(string agent_id, string phone, string agent_type, DateTime callHang, string callType, string Transfer_From, string Processes)
        {
            SqlParameter[] param =
            {
                new SqlParameter("@AGENT_ID",agent_id),
                new SqlParameter("@CALLER_NUMBER",phone),
                new SqlParameter("@AGENT_TYPE",agent_type),
                new SqlParameter("@CALL_START_TIME",DateTime.Now),
                new SqlParameter("@CALL_END_TIME",callHang),
                new SqlParameter("@CALL_HOLD_TIME",DateTime.Now),
                new SqlParameter("@CALL_PICKUP_TIME",DateTime.Now),
                new SqlParameter("@CALL_TYPE",callType),
                new SqlParameter("@CALL_STATUS","Hold"),
                new SqlParameter("@TRANSFER_FROM",Transfer_From),
                new SqlParameter("@PROCESS",Processes) };
            SqlHelper.ExecuteDataset(HelperClass.Connection, CommandType.StoredProcedure, "OUTBOUND_CALL_DETAIL", param);
            return 1;
        }

        public static void AgentLogin(string agent_id, string agent_type, DateTime LoginTime, DateTime logOutTime, DateTime BreakINTime, DateTime BreakOutTime, string Processes)
        {
            SqlParameter[] param =
            {
                new SqlParameter("@AGENT_ID",agent_id),
                new SqlParameter("@AGENT_TYPE",agent_type),
                new SqlParameter("@LOGIN_TIME",LoginTime),
                new SqlParameter("@LOGOUT_TIME",logOutTime),
                new SqlParameter("@BREAK_TIME",BreakINTime),
                new SqlParameter("@BREAK_END_TIME",BreakOutTime),
                new SqlParameter("@PROCESS_TYPE",Processes) };
            SqlHelper.ExecuteDataset(HelperClass.Connection, CommandType.StoredProcedure, "AGENT_LOGIN", param);
        }
        #endregion

        #region ExceptionsLog
        public static List<ModelExceptionLogger> GetExceptions(ModelReport dataObject)
        {
            List<ModelExceptionLogger> lstExceptions = new List<ModelExceptionLogger>();
            ModelExceptionLogger objBlank = new ModelExceptionLogger();
            SqlParameter[] param ={
                    new SqlParameter("@FromDate",dataObject.fromdate),
                     new SqlParameter("@ToDate",dataObject.todate)};
            DataSet ds = SqlHelper.ExecuteDataset(HelperClass.Connection, CommandType.StoredProcedure, "GetExceptions", param);

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objBlank = new ModelExceptionLogger();
                objBlank.ExceptionId = Convert.ToInt32(dr.ItemArray[0]);
                objBlank.ExceptionMessage = Convert.ToString(dr.ItemArray[1].ToString());
                objBlank.ControllerName = Convert.ToString(dr.ItemArray[2].ToString());
                objBlank.ActionName = Convert.ToString(dr.ItemArray[3].ToString());
                objBlank.ExceptionStackTrack = Convert.ToString(dr.ItemArray[4].ToString());
                objBlank.ExceptionLogTime = Convert.ToString(dr.ItemArray[5].ToString());
                lstExceptions.Add(objBlank);
            }
            return lstExceptions;
        }

        public static void SaveUnhandeledExceptions(ModelExceptionLogger dataObject)
        {

            SqlParameter[] param ={
                    new SqlParameter("@ExceptionMessage",dataObject.ExceptionMessage),
                     new SqlParameter("@ControllerName",dataObject.ControllerName),
                     new SqlParameter("@ActionName",dataObject.ActionName),
                     new SqlParameter("@ExceptionStackTrack",dataObject.ExceptionStackTrack)
            };


            DataSet ds = SqlHelper.ExecuteDataset(HelperClass.Connection, CommandType.StoredProcedure, "SaveUnhandeledExceptions", param);


        }
        #endregion


        public static int PUSH_SMS_DETAIL_Consumer(COMPLAINT modelRemark, string response)
        {
            int retStatus = 0;
            string retMsg = String.Empty; ;
            COMPLAINT obj = new COMPLAINT();
            obj = modelRemark;



            SqlParameter[] param ={

                new SqlParameter("@PHONE_NO",modelRemark.MOBILE_NO),
                    new SqlParameter("@TEXT_MEESAGE",modelRemark.SMS),
                    new SqlParameter("@DELIVERY_RESPONSE",response),
                    new SqlParameter("@REMARK","SMS SENT")};


            try
            {
                SqlHelper.ExecuteNonQuery(HelperClass.Connection, CommandType.StoredProcedure, "PUSH_SMS_DETAIL", param);
            }
            catch (Exception ex)
            {
                retStatus = -1;
            }

            return retStatus;

        }

        public static int PUSH_SMS_DETAIL_Consumer1(string mobile_no, string smstext, string response)
        {
            int retStatus = 0;
            string retMsg = String.Empty; ;
            COMPLAINT obj = new COMPLAINT();



            SqlParameter[] param ={

                new SqlParameter("@PHONE_NO",mobile_no),
                    new SqlParameter("@TEXT_MEESAGE",smstext),
                    new SqlParameter("@DELIVERY_RESPONSE",response),
                    new SqlParameter("@REMARK","SMS SENT")};


            try
            {
                SqlHelper.ExecuteNonQuery(HelperClass.Connection, CommandType.StoredProcedure, "PUSH_SMS_DETAIL", param);
            }
            catch (Exception ex)
            {
                retStatus = -1;
            }

            return retStatus;

        }

        public static int PUSH_SMS_DETAIL_FRT(COMPLAINT modelRemark, string response)
        {
            int retStatus = 0;
            string retMsg = String.Empty; ;
            COMPLAINT obj = new COMPLAINT();
            obj = modelRemark;



            SqlParameter[] param ={

                new SqlParameter("@PHONE_NO",modelRemark.Assign_FRTMobile),
                    new SqlParameter("@TEXT_MEESAGE",modelRemark.SMS_FRT),
                    new SqlParameter("@DELIVERY_RESPONSE",response),
                    new SqlParameter("@REMARK","SMS SENT")};


            try
            {
                SqlHelper.ExecuteNonQuery(HelperClass.Connection, CommandType.StoredProcedure, "PUSH_SMS_DETAIL", param);
            }
            catch (Exception ex)
            {
                retStatus = -1;
            }

            return retStatus;

        }


        public static List<COMPLAINT> GetConsumerDetails(string searchParm)
        {
            List<COMPLAINT> obj = new List<COMPLAINT>();
            DataSet ds = new DataSet();
            SqlParameter[] param = { new SqlParameter("@searchParm", searchParm) };
            try
            {
                ds = SqlHelper.ExecuteDataset(HelperClass.Connection, CommandType.StoredProcedure, "GetConsumerDetails", param);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    obj.Add(
                        new COMPLAINT
                        {

                            NAME = Convert.ToString(dr["NAME"]),
                            ADDRESS1 = Convert.ToString(dr["ADDRESS1"]),
                            ADDRESS2 = Convert.ToString(dr["ADDRESS2"]),
                            ADDRESS3 = Convert.ToString(dr["ADDRESS3"]),
                            KNO = Convert.ToString(dr["KNO"]),
                            MOBILE_NO = Convert.ToString(dr["MOBILE_NO"]),
                            OFFICE_CODE_ID = 3103310,
                        }
                        );
                }

            }
            catch (Exception ex)
            {
                return obj;
            }
            return obj;
        }



        #region ShowHaresmentComplaintDetails
        public static List<ModelDashboardHaresment> ShowHaresmentComplaintDetails(string compstatus, string cmonth)
        {
            List<ModelDashboardHaresment> modelDashboardHaresments = new List<ModelDashboardHaresment>();
            ModelDashboardHaresment objBlank = new ModelDashboardHaresment();
            SqlParameter[] param ={
                new SqlParameter("@COMPLAINT_MONTH",cmonth),
                new SqlParameter("@COMPLAINT_STATUS",compstatus)
            };

            DataSet ds = SqlHelper.ExecuteDataset(HelperClass.Connection, CommandType.StoredProcedure, "EXPORT_HARASSMENT", param);

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objBlank = new ModelDashboardHaresment();
                objBlank.COMPLAINT_NO = Convert.ToString(dr.ItemArray[0].ToString());
                objBlank.KNO = Convert.ToString(dr.ItemArray[1].ToString());
                objBlank.COMPLAINT_DATE = Convert.ToString(dr.ItemArray[2].ToString());
                objBlank.COMPLAINT_TYPE = Convert.ToString(dr.ItemArray[3].ToString());
                objBlank.NAME = Convert.ToString(dr.ItemArray[4].ToString());
                objBlank.ADDRESS = Convert.ToString(dr.ItemArray[5]);
                objBlank.MOBILE_NO = Convert.ToString(dr.ItemArray[6].ToString());
                objBlank.STATUS = Convert.ToString(dr.ItemArray[7].ToString());
                modelDashboardHaresments.Add(objBlank);
            }
            return modelDashboardHaresments;
        }
        #endregion


        #region GET_CC_MOBILE_NO
        public static string GET_CC_MOBILE_NO(string sdoCode)
        {
            string retMsg = String.Empty; ;
            SqlParameter[] param = { new SqlParameter("@SDO_CODE", sdoCode) };
            try
            {
                DataSet ds = new DataSet();
                ds = SqlHelper.ExecuteDataset(HelperClass.Connection, CommandType.StoredProcedure, "GET_CC_MOBILE_NO", param);
                if (ds.Tables.Count > 0)
                {
                    retMsg = ds.Tables[0].Rows[0][0].ToString();
                }
            }
            catch (Exception ex)
            {

            }

            return retMsg;

        }
        #endregion

        #region GET_COMPLAINT_DETAIL
        public static DataSet GET_COMPLAINT_DETAIL(Int64 Complaint_NO)
        {
            string retMsg = String.Empty;

            DataSet ds = new DataSet();
            SqlParameter[] param = { new SqlParameter("@Complaint_NO", Complaint_NO) };
            try
            {
                //DataSet ds = new DataSet();
                ds = SqlHelper.ExecuteDataset(HelperClass.Connection, CommandType.StoredProcedure, "GET_COMPLAINT_DETAIL_BY_COMPLAINT_NO", param);
                //if (ds.Tables.Count > 0)
                //{
                //    retMsg = ds.Tables[0].Rows[0][0].ToString();
                //}
            }
            catch (Exception ex)
            {

            }

            return ds;

        }
        #endregion

        #region New Connection
        public static List<SelectListItem> GetNewConnectionSteps()
        {
            List<SelectListItem> lstComplaintSource = new List<SelectListItem>();
            SelectListItem objBlank = new SelectListItem();
            DataSet ds = SqlHelper.ExecuteDataset(HelperClass.Connection, CommandType.StoredProcedure, "NEW_CONNECTION_STEP");

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objBlank = new SelectListItem();
                objBlank.Value = dr.ItemArray[0].ToString();
                objBlank.Text = dr.ItemArray[1].ToString();
                lstComplaintSource.Add(objBlank);
            }
            return lstComplaintSource;
        }

        public static List<ModelComplaintStepsGrid> GetNewConnectionStepsGrid(Int64 ComplaintNo)
        {
            SqlParameter[] param = { new SqlParameter("@COMPLAINT_NO", ComplaintNo) };
            DataSet ds = SqlHelper.ExecuteDataset(HelperClass.Connection, CommandType.StoredProcedure, "NEW_CONNECTION_STEP_DETAIL", param);
            var newConnList = ds.Tables[0].AsEnumerable()
                .Select(dataRow => new ModelComplaintStepsGrid
                {
                    COMPLAINT_NO = dataRow.Field<Int64>("COMPLAINT_NO"),
                    STEP_NAME = dataRow.Field<string>("STEP_NAME"),
                    DS_NDS = dataRow.Field<string>("DS_NDS")
                }).ToList();

            return newConnList;
        }

        public static async Task<int> SaveNewConnection(ModelNewConnection dataObject)
        {
            try
            {
                SqlParameter[] param ={new SqlParameter("@COMPLAINT_NO",dataObject.ComplaintNo),
                new SqlParameter("@STEP_ID",dataObject.NewConnectionStepId),
                new SqlParameter("@USER_ID",dataObject.userId),
                new SqlParameter("@DS_NDS",dataObject.rdoDsOrNds)};
                DataSet ds = await SqlHelper.ExecuteDatasetAsync(HelperClass.Connection, CommandType.StoredProcedure, "NEW_CONNECTION_STEP_CHANGE", param);
                return 1;
            }
            catch
            {
                return -1;
            }
        }


        #endregion


        #region OTP
        public static string GenerateOtp(string mobileNo)
        {
            try
            {
                SqlParameter[] param = { new SqlParameter("@RegMobileNo", mobileNo) };
                DataSet ds = SqlHelper.ExecuteDataset(HelperClass.Connection, CommandType.StoredProcedure, "GenerateOTP", param);
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        return Convert.ToString(ds.Tables[0].Rows[0][0]);
                    }
                    else
                    {
                        return string.Empty;
                    }
                }
                return string.Empty;
            }
            catch
            {

                return string.Empty;
            }
        }

        public static string ValidateOTP(string mobileNo, string userOtp)
        {
            try
            {
                SqlParameter[] param = { new SqlParameter("@RegMobileNo", mobileNo), new SqlParameter("@userOtp", userOtp) };
                DataSet ds = SqlHelper.ExecuteDataset(HelperClass.Connection, CommandType.StoredProcedure, "ValidateOTP", param);
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        return Convert.ToString(ds.Tables[0].Rows[0][0]);
                    }
                    else
                    {
                        return "0";
                    }
                }
                return "0";
            }
            catch
            {

                return "0";
            }
        }
        #endregion
        //GetOfficeVillageWise
        //GetAllOfficeDetails


        public static List<ModelVillage> GetVillageMaster()
        {
            List<ModelVillage> lstRoles = new List<ModelVillage>();
            ModelVillage objBlank = new ModelVillage();
            objBlank.Id = 0;
            objBlank.Name = "Select Village";
            lstRoles.Insert(0, objBlank);

            DataSet ds = SqlHelper.ExecuteDataset(HelperClass.Connection, CommandType.StoredProcedure, "GetVillageMaster");

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objBlank = new ModelVillage();
                objBlank.Name = dr.ItemArray[1].ToString();
                objBlank.Id = Convert.ToInt32(dr.ItemArray[0].ToString());
                lstRoles.Add(objBlank);
            }
            return lstRoles;
        }


        public static List<ModelVillage> GetOfficeVillageWise(string villageName)
        {
            List<ModelVillage> lstRoles = new List<ModelVillage>();
            ModelVillage objBlank = new ModelVillage();
            SqlParameter[] param = { new SqlParameter("@VillageName", villageName) };
            DataSet ds = SqlHelper.ExecuteDataset(HelperClass.Connection, CommandType.StoredProcedure, "GetOfficeVillageWise", param);

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objBlank = new ModelVillage();
                objBlank.Name = dr.ItemArray[3].ToString();
                objBlank.Id = Convert.ToInt32(dr.ItemArray[2].ToString());
                lstRoles.Add(objBlank);
            }
            return lstRoles;
        }



        public static ModelPaymentInfo GetPaymentBillInfoFromCMS(ModelBillingRequest modelBillingRequest)
        {
            ModelPaymentInfo modelPaymentInfo = new ModelPaymentInfo();
            string retMsg = String.Empty; ;
            try
            {
                TextSmsAPI textSmsAPI1 = new TextSmsAPI();
                modelPaymentInfo = textSmsAPI1.GetPaymentBillInfoFromCMSAPI(modelBillingRequest);
            }
            catch (Exception ex)
            {
                log.Information("GetPaymentBillInfoFromCMS :  " + ex.Message);
                return modelPaymentInfo;
            }
            return modelPaymentInfo;
        }

        public static List<ModelReportSdoLogin> GetSdoLoginTime(ModelBillingRequest modelBillingRequest)
        {
            ModelReportSdoLogin modelLoginData = null;
            List<ModelReportSdoLogin> lstmodelPaymentInfo = new List<ModelReportSdoLogin>();

            string retMsg = String.Empty;
            SqlParameter[] param = { new SqlParameter("@Date1", modelBillingRequest.cons_no) };
            DataSet ds = SqlHelper.ExecuteDataset(HelperClass.Connection, CommandType.StoredProcedure, "GetSdoLoginTime", param);

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                modelLoginData = new ModelReportSdoLogin();
                modelLoginData.OFFICE_ID = dr.ItemArray[0].ToString();
                modelLoginData.OFFICE_NAME = Convert.ToString(dr.ItemArray[1].ToString());
                modelLoginData.LOGIN_TIME = Convert.ToString(dr.ItemArray[2].ToString());
                modelLoginData.Pending_Complaint_Count = Convert.ToString(dr.ItemArray[3].ToString());
                lstmodelPaymentInfo.Add(modelLoginData);
            }
            return lstmodelPaymentInfo;
        }

        #region RUIDP
        public static ModelAdvice GetConsumerDetailForAdviceRUIDP(string kno)
        {
            ModelAdvice modelAdvice = new ModelAdvice();
            SqlParameter[] param ={
                    new SqlParameter("@Kno",kno)
                    };

            DataSet ds = SqlHelper.ExecuteDataset(HelperClass.Connection, CommandType.StoredProcedure, "GetConsumerDetailForAdvice", param);
            //Bind Complaint generic list using dataRow     

            List<ComplaintHistory> complaintHistoryList = new List<ComplaintHistory>(); ;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                ComplaintHistory complaintHistory = new ComplaintHistory();

                complaintHistory.name = Convert.ToString(dr["Name"]);
                complaintHistory.Kno = Convert.ToString(dr["KNO"]);
                complaintHistory.MeterNo = Convert.ToString(dr["MeterNo"]);
                complaintHistory.Address = Convert.ToString(dr["Address"]);
                complaintHistory.PrevReadingdate = Convert.ToString(dr["PRD"]);
                complaintHistory.CurrentReadingdate = Convert.ToString(dr["CRD"]);
                complaintHistory.PrevReading = Convert.ToString(dr["PR"]);
                complaintHistoryList.Add(complaintHistory);
            }

            modelAdvice.complaintHistories = complaintHistoryList;
            return (modelAdvice);
        }

        public static ModelStatus SaveAdvice(ModelAdvice modelAdvice)
        {
            ModelStatus modelStatus = new ModelStatus();
            if (modelAdvice.Order_Type == 1 || modelAdvice.Order_Type == 2)
            {
                modelStatus = Save_MCO_RCO_Advice(modelAdvice);
            }
            if (modelAdvice.Order_Type == 3 || modelAdvice.Order_Type == 4)
            {
                modelStatus = Save_DCO_PDCO_Advice(modelAdvice);
            }
            return modelStatus;
        }

        public static ModelStatus SavePayment(DataTable dataTable)
        {
            ModelStatus modelStatuis = new ModelStatus();

            SqlParameter[] param ={
                  new SqlParameter("@MyDataTable",dataTable)};
            SqlHelper.ExecuteNonQuery(HelperClass.Connection, CommandType.StoredProcedure, "InsertFromDataTable", param);

            int statusRet = 1;
            modelStatuis.StatusId = Convert.ToString(statusRet);
            modelStatuis.StatusMessage = "Success";
            return modelStatuis;
        }


        public static ModelStatus Save_MCO_RCO_Advice(ModelAdvice modelAdvice)
        {
            ModelStatus modelStatuis = new ModelStatus();
            SqlParameter parmretStatus = new SqlParameter();
            parmretStatus.ParameterName = "@RetStatus";
            parmretStatus.DbType = DbType.Int32;
            parmretStatus.Size = 8;
            parmretStatus.Direction = ParameterDirection.Output;

            SqlParameter parmretMsg = new SqlParameter();
            parmretMsg.ParameterName = "@RetMsg";
            parmretMsg.DbType = DbType.String;
            parmretMsg.Size = 500;
            parmretMsg.Direction = ParameterDirection.Output;

            SqlParameter[] param ={
                  new SqlParameter("@Kno",modelAdvice.Kno ),
                    new SqlParameter("@OrderDate",modelAdvice.OrderDate1),
                    new SqlParameter("@Order_Type",modelAdvice.Order_Type),
                    new SqlParameter("@Amount",modelAdvice.Amount),
                    new SqlParameter("@Receipt_no",modelAdvice.Receipt_no),
                    new SqlParameter("@Receipt_Date",modelAdvice.Receipt_Date),
                    new SqlParameter("@NewMeterNo",modelAdvice.New_Meter_No),
                    new SqlParameter("@oldMeterCurrReading",modelAdvice.Old_Meter_Current_reading),
                    new SqlParameter("@newMeterPrevReading",modelAdvice.Current_Meter_Previous_reading),
                    new SqlParameter("@newMeterCurrReading",modelAdvice.Current_Meter_Current_reading)
                    ,parmretStatus,parmretMsg};
            SqlHelper.ExecuteNonQuery(HelperClass.Connection, CommandType.StoredProcedure, "MCO_RCO", param);

            int statusRet = (int)parmretStatus.Value;
            modelStatuis.StatusId = Convert.ToString(statusRet);
            modelStatuis.StatusMessage = (string)parmretMsg.Value;
            return modelStatuis;
        }


        public static ModelStatus Save_Payment(ModelPayment modelPayment)
        {
            ModelStatus modelStatuis = new ModelStatus();
            SqlParameter parmretStatus = new SqlParameter();
            parmretStatus.ParameterName = "@retStatus";
            parmretStatus.DbType = DbType.Int32;
            parmretStatus.Size = 8;
            parmretStatus.Direction = ParameterDirection.Output;

            SqlParameter parmretMsg = new SqlParameter();
            parmretMsg.ParameterName = "@retmsg";
            parmretMsg.DbType = DbType.String;
            parmretMsg.Size = 500;
            parmretMsg.Direction = ParameterDirection.Output;

            SqlParameter[] param ={
                  new SqlParameter("@KNO",modelPayment.Kno ),
                    new SqlParameter("@PaymentDate",modelPayment.Payment_Date),
                    new SqlParameter("@PaidAmount",modelPayment.Payment_Amount),
                    new SqlParameter("@Transaction_no",modelPayment.Transaction_No),
                    new SqlParameter("@PaymentMode",modelPayment.SelectedPaymentMode)
                    ,parmretStatus,parmretMsg};
            SqlHelper.ExecuteNonQuery(HelperClass.Connection, CommandType.StoredProcedure, "PushPayment", param);

            int statusRet = (int)parmretStatus.Value;
            modelStatuis.StatusId = Convert.ToString(statusRet);
            modelStatuis.StatusMessage = (string)parmretMsg.Value;
            return modelStatuis;
        }

        //public static ModelStatus Save_PaymentBulk(ExcelData modelPayment)
        //{
        //    ModelStatus modelStatuis = new ModelStatus();
        //    SqlParameter parmretStatus = new SqlParameter();
        //    parmretStatus.ParameterName = "@retStatus";
        //    parmretStatus.DbType = DbType.Int32;
        //    parmretStatus.Size = 8;
        //    parmretStatus.Direction = ParameterDirection.Output;

        //    SqlParameter parmretMsg = new SqlParameter();
        //    parmretMsg.ParameterName = "@retmsg";
        //    parmretMsg.DbType = DbType.String;
        //    parmretMsg.Size = 500;
        //    parmretMsg.Direction = ParameterDirection.Output;

        //    SqlParameter[] param ={
        //          new SqlParameter("@KNO",modelPayment.Kno ),
        //            new SqlParameter("@PaymentDate",modelPayment.Payment_Date),
        //            new SqlParameter("@PaidAmount",modelPayment.Payment_Amount),
        //            new SqlParameter("@Transaction_no",modelPayment.Transaction_No),
        //            new SqlParameter("@PaymentMode",modelPayment.SelectedPaymentMode)
        //            ,parmretStatus,parmretMsg};
        //    SqlHelper.ExecuteNonQuery(HelperClass.Connection, CommandType.StoredProcedure, "PushPayment", param);

        //    int statusRet = (int)parmretStatus.Value;
        //    modelStatuis.StatusId = Convert.ToString(statusRet);
        //    modelStatuis.StatusMessage = (string)parmretMsg.Value;
        //    return modelStatuis;
        //}

        public static ModelStatus Save_Sundry(ModelSundry modelSundry)
        {
            ModelStatus modelStatuis = new ModelStatus();
            SqlParameter parmretStatus = new SqlParameter();
            parmretStatus.ParameterName = "@retStatus";
            parmretStatus.DbType = DbType.Int32;
            parmretStatus.Size = 8;
            parmretStatus.Direction = ParameterDirection.Output;

            SqlParameter parmretMsg = new SqlParameter();
            parmretMsg.ParameterName = "@retmsg";
            parmretMsg.DbType = DbType.String;
            parmretMsg.Size = 500;
            parmretMsg.Direction = ParameterDirection.Output;

            SqlParameter[] param ={
                  new SqlParameter("@KNO",modelSundry.Kno ),
                    new SqlParameter("@SundrySide",modelSundry.SelectedsundrySide),
                    new SqlParameter("@SundryAmount",modelSundry.Sundry_Amount),
                    new SqlParameter("@Remark",modelSundry.Remark),
                    new SqlParameter("@SundryType",'M'),
                    new SqlParameter("@SundryDate",modelSundry.Sundry_Date)
                    ,parmretStatus,parmretMsg};
            SqlHelper.ExecuteNonQuery(HelperClass.Connection, CommandType.StoredProcedure, "PushSundry", param);

            int statusRet = (int)parmretStatus.Value;
            modelStatuis.StatusId = Convert.ToString(statusRet);
            modelStatuis.StatusMessage = (string)parmretMsg.Value;
            return modelStatuis;
        }

        public static ModelStatus Save_DCO_PDCO_Advice(ModelAdvice modelAdvice)
        {
            ModelStatus modelStatuis = new ModelStatus();
            SqlParameter parmretStatus = new SqlParameter();
            parmretStatus.ParameterName = "@RetStatus";
            parmretStatus.DbType = DbType.Int32;
            parmretStatus.Size = 8;
            parmretStatus.Direction = ParameterDirection.Output;

            SqlParameter parmretMsg = new SqlParameter();
            parmretMsg.ParameterName = "@RetMsg";
            parmretMsg.DbType = DbType.String;
            parmretMsg.Size = 500;
            parmretMsg.Direction = ParameterDirection.Output;

            SqlParameter[] param ={
                  new SqlParameter("@Kno",modelAdvice.Kno ),
                    new SqlParameter("@OrderDate",modelAdvice.OrderDate2),
                    new SqlParameter("@Order_Type",modelAdvice.Order_Type),
                    new SqlParameter("@MeterCurrReading",modelAdvice.Meter_Final_reading)
                    ,parmretStatus,parmretMsg};
            DataSet ds = SqlHelper.ExecuteDataset(HelperClass.Connection, CommandType.StoredProcedure, "DCO_PDCO", param);
            int statusRet = (int)parmretStatus.Value;
            modelStatuis.StatusId = Convert.ToString(statusRet);
            modelStatuis.StatusMessage = (string)parmretMsg.Value;
            return modelStatuis;
        }

        #region SaveNewConnnection
        public static ModelStatus SaveNewConnnectionRUIDP(ModelNewConnectionRUIDP modelNewConnectionRUIDP)
        {
            ModelStatus modelStatuis = new ModelStatus();
            SqlParameter parmretStatus = new SqlParameter();
            parmretStatus.ParameterName = "@RetStatus";
            parmretStatus.DbType = DbType.Int32;
            parmretStatus.Size = 8;
            parmretStatus.Direction = ParameterDirection.Output;

            SqlParameter parmretMsg = new SqlParameter();
            parmretMsg.ParameterName = "@RetMsg";
            parmretMsg.DbType = DbType.String;
            parmretMsg.Size = 500;
            parmretMsg.Direction = ParameterDirection.Output;

            SqlParameter[] param ={
                  new SqlParameter("@OrderDate",modelNewConnectionRUIDP.OrderDate),
                  new SqlParameter("@Order_Type",modelNewConnectionRUIDP.Order_Type),
new SqlParameter("@Amount",modelNewConnectionRUIDP.Amount),
new SqlParameter("@Sno",modelNewConnectionRUIDP.Sno),
new SqlParameter("@ApplicantName",modelNewConnectionRUIDP.ApplicantName),
new SqlParameter("@RegNo",modelNewConnectionRUIDP.RegNo),
new SqlParameter("@Prev_acc_no",modelNewConnectionRUIDP.Prev_acc_no),
new SqlParameter("@father_name",modelNewConnectionRUIDP.father_name),
new SqlParameter("@Adhaar_no",modelNewConnectionRUIDP.Adhaar_no),
new SqlParameter("@Bhamasha_no",modelNewConnectionRUIDP.Bhamasha_no),
new SqlParameter("@WaterConnAdd",modelNewConnectionRUIDP.WaterConnAdd),
new SqlParameter("@Neighbour_bill_ac",modelNewConnectionRUIDP.Neighbour_bill_ac),
new SqlParameter("@SewerageCode",modelNewConnectionRUIDP.SewerageCode),
new SqlParameter("@oldMetercurr",modelNewConnectionRUIDP.oldMetercurr),
new SqlParameter("@PlotArea",modelNewConnectionRUIDP.PlotArea),
new SqlParameter("@BuildingArea",modelNewConnectionRUIDP.BuildingArea),
new SqlParameter("@BuildingType",modelNewConnectionRUIDP.BuildingType),
new SqlParameter("@AppStats",modelNewConnectionRUIDP.AppStats),
new SqlParameter("@Address",modelNewConnectionRUIDP.Address),
new SqlParameter("@OffAddress",modelNewConnectionRUIDP.OffAddress),
new SqlParameter("@ConnectionPurpose",modelNewConnectionRUIDP.ConnectionPurpose),
new SqlParameter("@ServiceLineType",modelNewConnectionRUIDP.ServiceLineType),
new SqlParameter("@WaterConnType",modelNewConnectionRUIDP.WaterConnType),
new SqlParameter("@DocumentSubmitted",modelNewConnectionRUIDP.DocumentSubmitted),
new SqlParameter("@PipeDist",modelNewConnectionRUIDP.PipeDist),
new SqlParameter("@RoadRepairAmm",modelNewConnectionRUIDP.RoadRepairAmm),
new SqlParameter("@Receipt_no",modelNewConnectionRUIDP.Receipt_no),
new SqlParameter("@Receipt_date",modelNewConnectionRUIDP.Receipt_date),
new SqlParameter("@WaterConnecionSecDep",modelNewConnectionRUIDP.WaterConnecionSecDep),
new SqlParameter("@OtherDep",modelNewConnectionRUIDP.OtherDep),
new SqlParameter("@TotalDep",modelNewConnectionRUIDP.TotalDep),
new SqlParameter("@Connection_date",modelNewConnectionRUIDP.Connection_date),
new SqlParameter("@JE_Card_No",modelNewConnectionRUIDP.JE_Card_No),
new SqlParameter("@Commission_date",modelNewConnectionRUIDP.Commission_date),
new SqlParameter("@R2_Register_no",modelNewConnectionRUIDP.R2_Register_no),
new SqlParameter("@AllotedAcc_no",modelNewConnectionRUIDP.AllotedAcc_no),
new SqlParameter("@Service_no",modelNewConnectionRUIDP.Service_no)
                    ,parmretStatus,parmretMsg};
            SqlHelper.ExecuteNonQuery(HelperClass.Connection, CommandType.StoredProcedure, "NewConnection", param);

            int statusRet = (int)parmretStatus.Value;
            modelStatuis.StatusId = Convert.ToString(statusRet);
            modelStatuis.StatusMessage = (string)parmretMsg.Value;
            return modelStatuis;
        }

        #endregion

        #region BIlling
        public static ModelBilling GetConsumerDetailForBillingRUIDP(string kno)
        {
            ModelBilling modelAdvice = new ModelBilling();
            SqlParameter[] param ={
                    new SqlParameter("@Kno",kno)
                    };

            DataSet ds = SqlHelper.ExecuteDataset(HelperClass.Connection, CommandType.StoredProcedure, "GetConsumerDetailForBilling", param);
            //Bind Complaint generic list using dataRow     

            List<BillingHistory> complaintHistoryList = new List<BillingHistory>(); ;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                BillingHistory complaintHistory = new BillingHistory();

                complaintHistory.name = Convert.ToString(dr["Name"]);
                complaintHistory.Kno = Convert.ToString(dr["KNO"]);
                complaintHistory.Address = Convert.ToString(dr["Address"]);
                complaintHistory.Service_Status = Convert.ToString(dr["Service_Status"]);
                complaintHistory.Last_Reading = Convert.ToString(dr["PR"]);
                complaintHistory.Last_Reading_Date = Convert.ToString(dr["PRD"]);
                complaintHistory.Account_No = Convert.ToString(dr["AccountNo"]);
                complaintHistory.Binder_No = Convert.ToString(dr["Binder_No"]);

                complaintHistoryList.Add(complaintHistory);
            }

            modelAdvice.billingHistories = complaintHistoryList;
            return (modelAdvice);
        }
        #endregion
        public static ModelStatus Save_Billing(ModelBilling modelAdvice)
        {
            ModelStatus modelStatuis = new ModelStatus();
            SqlParameter parmretStatus = new SqlParameter();
            parmretStatus.ParameterName = "@RetStatus";
            parmretStatus.DbType = DbType.Int32;
            parmretStatus.Size = 8;
            parmretStatus.Direction = ParameterDirection.Output;

            SqlParameter parmretMsg = new SqlParameter();
            parmretMsg.ParameterName = "@RetMsg";
            parmretMsg.DbType = DbType.String;
            parmretMsg.Size = 500;
            parmretMsg.Direction = ParameterDirection.Output;
            SqlParameter[] param ={
                  new SqlParameter("@KNO", modelAdvice.Kno ),
                    new SqlParameter("@CurrReadingDate", modelAdvice.Current_Reading_Date),
                    new SqlParameter("@currReading", modelAdvice.Current_Reading),
                    new SqlParameter("@billMonth", modelAdvice.BIll_Month),
                    new SqlParameter("@billYear", modelAdvice.BIll_Year)
                    ,parmretStatus,parmretMsg};
            DataSet ds = SqlHelper.ExecuteDataset(HelperClass.Connection, CommandType.StoredProcedure, "sp_SaveBilling", param);
            int statusRet = (int)parmretStatus.Value;
            modelStatuis.StatusId = Convert.ToString(statusRet);
            modelStatuis.StatusMessage = (string)parmretMsg.Value;
            return modelStatuis;
        }


        #endregion

        #region BillingPrint
        public static List<ModelZone> GetZoneList()
        {
            List<ModelZone> obj = new List<ModelZone>();
            //SqlParameter[] param ={
            //        new SqlParameter("@OFFICE_ID",OFFICE_ID)
            //                           };
            DataSet ds = SqlHelper.ExecuteDataset(HelperClass.Connection, CommandType.StoredProcedure, "GetZones");
            //ds.Tables[0].Rows.RemoveAt(0);
            //Bind Complaint generic list using dataRow     
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                obj.Add(
                    new ModelZone
                    {
                        ZoneId = Convert.ToString(dr["Id"]),
                        ZoneName = Convert.ToString(dr["Name"]),
                    }
                    );
            }
            return (obj);
        }

        public static List<ModelChokdi> GetChokdiList()
        {
            List<ModelChokdi> obj = new List<ModelChokdi>();
            //SqlParameter[] param ={
            //        new SqlParameter("@OFFICE_ID",OFFICE_ID)
            //                           };
            DataSet ds = SqlHelper.ExecuteDataset(HelperClass.Connection, CommandType.StoredProcedure, "GetChokdi");
            //ds.Tables[0].Rows.RemoveAt(0);
            //Bind Complaint generic list using dataRow     
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                obj.Add(
                    new ModelChokdi
                    {
                        Chokdi = Convert.ToString(dr["Chokdi"]),
                    }
                    );
            }
            return (obj);
        }

        

        public static List<PaymentModes> GetPaymentModes()
        {
            List<PaymentModes> obj = new List<PaymentModes>();
            //SqlParameter[] param ={
            //        new SqlParameter("@OFFICE_ID",OFFICE_ID)
            //                           };
            DataSet ds = SqlHelper.ExecuteDataset(HelperClass.Connection, CommandType.StoredProcedure, "GetPaymentModes");
            //ds.Tables[0].Rows.RemoveAt(0);
            //Bind Complaint generic list using dataRow     
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                obj.Add(
                    new PaymentModes
                    {
                        ID = Convert.ToInt32(dr["ID"]),
                        PaymentMode = Convert.ToString(dr["PaymentMode"]),
                    }
                    );
            }
            return (obj);
        }

        public static List<SundrySide> GetSundrySides()
        {
            return new List<SundrySide>
            {
                new SundrySide { ID = 1, sundrySide = "DR" },
                new SundrySide { ID = 2, sundrySide = "CR" }
            };
        }

        public static List<ModelDma> GetDmaById(string Id)
        {
            List<ModelDma> obj = new List<ModelDma>();
            SqlParameter[] param ={
                    new SqlParameter("@ZoneID",Id)
                                       };
            DataSet ds = SqlHelper.ExecuteDataset(HelperClass.Connection, CommandType.StoredProcedure, "GetDMA", param);
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                obj.Add(
                    new ModelDma
                    {
                        Id = Convert.ToString(dr["DMAID"]),
                        Name = Convert.ToString(dr["Name"]),
                    }
                    );
            }
            return (obj);

        }

        public static List<ModelBinder> GetBinderNo(string zoneId, string dmaId)
        {
            List<ModelBinder> obj = new List<ModelBinder>();
            SqlParameter[] param = { new SqlParameter("@ZoneID", zoneId), new SqlParameter("@DMAID", dmaId) };

            DataSet ds = SqlHelper.ExecuteDataset(HelperClass.Connection, CommandType.StoredProcedure, "GetBinderNo", param);
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                obj.Add(
                    new ModelBinder
                    {
                        Id = Convert.ToString(dr["Binder_No"]),
                        Name = Convert.ToString(dr["Binder_No"]),
                    }
                    );
            }
            return (obj);
        }

        public static List<ModelBinder> GetBinderNoByChokdi(string ChokdiId)
        {
            List<ModelBinder> obj = new List<ModelBinder>();
            SqlParameter[] param = { new SqlParameter("@chokdi", ChokdiId) };

            DataSet ds = SqlHelper.ExecuteDataset(HelperClass.Connection, CommandType.StoredProcedure, "GetChokdiBinderNo", param);
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                obj.Add(
                    new ModelBinder
                    {
                        Id = Convert.ToString(dr["Binder_No"]),
                        Name = Convert.ToString(dr["Binder_No"]),
                    }
                    );
            }
            return (obj);
        }

        public static List<ModelBinder> GetBinderNoByChokdiForLedger(string ChokdiId)
        {
            List<ModelBinder> obj = new List<ModelBinder>();
            SqlParameter[] param = { new SqlParameter("@chokdi", ChokdiId) };

            DataSet ds = SqlHelper.ExecuteDataset(HelperClass.Connection, CommandType.StoredProcedure, "GetChokdiBinderNoForLedger", param);
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                obj.Add(
                    new ModelBinder
                    {
                        Id = Convert.ToString(dr["Binder_No"]),
                        Name = Convert.ToString(dr["Binder_No"]),
                    }
                    );
            }
            return (obj);
        }

        public static List<ModelBillingPrint> GetConsumerBillForPrint(string strchokdiId, string strbinderNo, string strbm, string strby)
        {
            ModelBillingPrint modelAdvice = new ModelBillingPrint();
            SqlParameter[] param ={
                    //new SqlParameter("@Zone",strzoneId),
                    // new SqlParameter("@DMA",strdmaId),
                    new SqlParameter("@Chokdi",strchokdiId),
                      new SqlParameter("@BinderNo",strbinderNo),
                       new SqlParameter("@BillMonth",strbm),
                        new SqlParameter("@billYear",strby)
                    };

            DataSet ds = SqlHelper.ExecuteDataset(HelperClass.Connection, CommandType.StoredProcedure, "Bill_Detail", param);
            //Bind Complaint generic list using dataRow     

            List<ModelBillingPrint> complaintHistoryList = new List<ModelBillingPrint>(); ;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                ModelBillingPrint complaintHistory = new ModelBillingPrint();
                complaintHistory.KNO = Convert.ToString(dr["KNO"]);
                complaintHistory.Chokdi = Convert.ToString(dr["Chokdi"]);
                complaintHistory.Name = Convert.ToString(dr["Name"]);
                complaintHistory.Address = Convert.ToString(dr["Address"]);
                complaintHistory.Account_No = Convert.ToString(dr["Account_No"]);
                complaintHistory.Binder_No = Convert.ToString(dr["Binder_No"]);
                complaintHistory.Office_Code = Convert.ToString(dr["Office_Code"]);
                complaintHistory.Category = Convert.ToString(dr["Category"]);
                complaintHistory.Meter_No = Convert.ToString(dr["Meter_No"]);
                complaintHistory.Previous_Reading_Date = Convert.ToString(dr["Previous_Reading_Date"]);
                complaintHistory.Bill_Month = Convert.ToString(dr["Bill_Month"]);
                complaintHistory.Bill_Year = Convert.ToString(dr["Bill_Year"]);
                complaintHistory.Current_Reading_Date = Convert.ToString(dr["Current_Reading_Date"]);
                complaintHistory.Bill_Date = Convert.ToString(dr["Bill_Date"]);
                complaintHistory.Due_Date_Cash = Convert.ToString(dr["Due_Date_Cash"]);
                complaintHistory.Due_Date_Cheque = Convert.ToString(dr["Due_Date_Cheque"]);
                complaintHistory.Current_Reading = Convert.ToString(dr["Current_Reading"]);
                complaintHistory.Precious_Reading = Convert.ToString(dr["Precious_Reading"]);
                complaintHistory.Net_Consumption = Convert.ToString(dr["Net_Consumption"]);
                complaintHistory.Pending_Amount = Convert.ToString(dr["Pending_Amount"]);
                complaintHistory.Service_No = Convert.ToString(dr["Service_No"]);
                complaintHistory.Meter_Result = Convert.ToString(dr["Meter_Result"]);
                complaintHistory.Adjusted_Consumption_Amount_Water = Convert.ToString(dr["Adjusted_Consumption_Amount_Water"]);
                complaintHistory.Adjusted_Consumption_Amount_Sewerage = Convert.ToString(dr["Adjusted_Consumption_Amount_Sewerage"]);
                complaintHistory.Pure_Consumption_Amount_Water = Convert.ToString(dr["Pure_Consumption_Amount_Water"]);
                complaintHistory.Pure_Consumption_Amount_Sewerage = Convert.ToString(dr["Pure_Consumption_Amount_Sewerage"]);
                complaintHistory.Excess_Sewerage = Convert.ToString(dr["Excess_Sewerage"]);
                complaintHistory.Government_Rebate = Convert.ToString(dr["Government_Rebate"]);
                complaintHistory.Water_Amount = Convert.ToString(dr["Water_Amount"]);
                complaintHistory.Sewerage_Amount = Convert.ToString(dr["Sewerage_Amount"]);
                complaintHistory.Meter_Service_Charge = Convert.ToString(dr["Meter_Service_Charge"]);
                complaintHistory.Infrastructure_Development_Surcharge = Convert.ToString(dr["Infrastructure_Development_Surcharge"]);
                complaintHistory.Fix_Charge = Convert.ToString(dr["Fix_Charge"]);
                complaintHistory.Pending_Amount = Convert.ToString(dr["Pending_Amount"]);
                complaintHistory.LPS = Convert.ToString(dr["LPS"]);
                complaintHistory.Other = Convert.ToString(dr["Other"]);
                complaintHistory.Interest = Convert.ToString(dr["Interest"]);
                complaintHistory.Amount_On_Due_Date = Convert.ToString(dr["Amount_On_Due_Date"]);
                complaintHistory.Amount_After_Due_Date = Convert.ToString(dr["Amount_After_Due_Date"]);
                complaintHistory.Payment_Date = Convert.ToString(dr["Payment_Date"]);
                complaintHistoryList.Add(complaintHistory);
            }
            return (complaintHistoryList);
        }


        public static DataTable GetLedgerForPrint(string strchokdiId, string strbinderNo, string strbm, string strby)
        {
            SqlParameter[] param ={
                    //new SqlParameter("@Zone",strzoneId),
                    // new SqlParameter("@DMA",strdmaId),
                    new SqlParameter("@Chokdi",strchokdiId),
                      new SqlParameter("@binder_no",strbinderNo),
                       new SqlParameter("@Bill_Month",strbm),
                        new SqlParameter("@Bill_Year",strby)
                    };

            DataSet ds = SqlHelper.ExecuteDataset(HelperClass.Connection, CommandType.StoredProcedure, "GetLedger", param);
            //Bind Complaint generic list using dataRow     
            DataTable dt = ds.Tables[0];
            
            return (dt);
        }

        public static DataTable GetPaymentForPrint(string payment_date, string ID)
        {
            SqlParameter[] param ={
                    //new SqlParameter("@Zone",strzoneId),
                    // new SqlParameter("@DMA",strdmaId),
                    new SqlParameter("@Date",payment_date),
                      new SqlParameter("@PaymentMode",ID)
                    };

            DataSet ds = SqlHelper.ExecuteDataset(HelperClass.Connection, CommandType.StoredProcedure, "GetPayment", param);
            //Bind Complaint generic list using dataRow     
            DataTable dt = ds.Tables[0];

            return (dt);
        }

        #endregion
    }
}


