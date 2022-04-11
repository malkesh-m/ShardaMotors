using ShardaMotorsWebAPI.App_Data.BAL;
using ShardaMotorsWebAPI.App_Data.BEL;
using ShardaMotorsWebAPI.Model;
using ShardaMotorsWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ShardaMotorsWebAPI.Controllers
{
    public class ShardaMotorsAPIController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }

        [Route("~/api/Login")]
        [HttpGet]
        public Response Login(string _userName, string _Password)//Login user
        {
            Response Response = null;
            try
            {
                BAL_Users _balUsers = new BAL_Users();
                _balUsers._lstLoginUser = _balUsers.SelectUserById(1);
                if (_userName != null && _userName != "" && _Password != null && _Password != "")
                {
                    if (_balUsers._lstLoginUser != null)
                    {
                        foreach (var item in _balUsers._lstLoginUser)
                        {
                            if (_userName == item.UserName && _Password == item.Password)
                            {
                                Response = new Response("Login Successfully..!");//, _balUsers._lstLoginUser
                            }
                            else
                            {
                                Response = new Response("Login Failed..!");
                            }
                        }
                    }
                    else
                    {
                        Response = new Response("Login Failed..!");
                    }
                }
                else
                {
                    Response = new Response("Login Failed..!");
                }

                return Response;
            }
            catch (Exception ex)
            {
                Response = new Response("Something goes wrong, Please try again later.");
            }
            return Response;
        }

        [Route("~/api/SMTPSettingUser")]
        [HttpGet]
        public ResponseData SMTPSettingUser()//Get SMTP details
        {
            ResponseData ResponseData = null;
            try
            {
                BAL_SMTPSettings _balSmtpUser = new BAL_SMTPSettings();
                _balSmtpUser._LstSMTPSettings = _balSmtpUser.SelectsmtpDtlsById(1);
                //var datalst = from s in _balSmtpUser.SelectsmtpDtlsById(1).ToList().Distinct() select new { s.Id, s.SMTP_UserName, s.Sender_Name, s.Sender_EmailId, s.SMTP_Host, s.SMTP_Password, s.SMTP_PortNumber };
                if (_balSmtpUser._LstSMTPSettings.Count() != 0)
                {
                    ResponseData = new ResponseData("Success.", _balSmtpUser._LstSMTPSettings);
                    ResponseData.data = _balSmtpUser._LstSMTPSettings;
                    return ResponseData;
                }
                else
                {
                    ResponseData = new ResponseData("Data not found.", null);
                }
            }
            catch (Exception ex)
            {
                ResponseData = new ResponseData("Something goes wrong, Please try again later.", null);
            }
            return ResponseData;
        }
        [Route("~/api/ShardaMotorsDetails")]
        [HttpGet]
        public SMResponseData ShardaMotorsDetails()//Get SMTP details
        {
            SMResponseData ResponseData = null;
            try
            {
                BAL_ShardaMotorsDetails _balSMdetails = new BAL_ShardaMotorsDetails();
                _balSMdetails._LstShardaMotorsDtls = _balSMdetails.SelectSMDetails();
                if (_balSMdetails._LstShardaMotorsDtls.Count() != 0)
                {
                    ResponseData = new SMResponseData("Success.", _balSMdetails._LstShardaMotorsDtls);
                    ResponseData.data = _balSMdetails._LstShardaMotorsDtls;
                    return ResponseData;
                }
                else
                {
                    ResponseData = new SMResponseData("Data not found.", null);
                }
            }
            catch (Exception ex)
            {
                ResponseData = new SMResponseData("Something goes wrong, Please try again later.", null);
            }
            return ResponseData;
        }
        [Route("~/api/SaveSMTPSetting")]
        [HttpPost]
        public Response SaveSMTPSetting(string _SenderName, string _SenderEmailId, string _SMTPUserName, string _SMTPPassword, string _SMTPHost, int _SMTPPortNumber)
        {
            Response Response = null;

            try
            {
                BEL_SMTPSettings _BelSMTPSettings = new BEL_SMTPSettings();
                BAL_SMTPSettings _BalSMTPSettings = new BAL_SMTPSettings();
                // Initialization.
                _BelSMTPSettings.SMTP_UserName = _SMTPUserName;
                _BelSMTPSettings.Sender_Name = _SenderName;
                _BelSMTPSettings.Sender_EmailId = _SenderEmailId;
                _BelSMTPSettings.SMTP_Password = _SMTPPassword;
                _BelSMTPSettings.SMTP_Host = _SMTPHost;
                _BelSMTPSettings.SMTP_PortNumber = Convert.ToInt32(_SMTPPortNumber);

                _BalSMTPSettings._LstSMTPSettings = _BalSMTPSettings.SelectsmtpDtlsById(1);


                if (_BalSMTPSettings._LstSMTPSettings == null && _BalSMTPSettings.Id != 1)
                {
                    _BelSMTPSettings.Id = 1;
                    bool IsSave = false;
                    IsSave = _BalSMTPSettings.InsertSMTPDetails(_BelSMTPSettings);
                    Response = new Response("SMTP Details Added Successfully!");
                }
                else
                {
                    bool IsUpdate = false;
                    IsUpdate = _BalSMTPSettings.UpdateSMTPDetails(_BelSMTPSettings);
                    Response = new Response("SMTP Details Updated Successfully!");
                }

            }
            catch (Exception ex)
            {
                Response = new Response("Something goes wrong, Please try again later.");
            }
            return Response;
        }

        [Route("~/api/SaveSMDetails")]
        [HttpPost]
        public Response SaveSMDetails(DataTable csvFileData)
        {
            Response Response = null;

            try
            {
                BEL_ShardaMotorsDetails _Bel_SMDetails = new BEL_ShardaMotorsDetails();
                BAL_ShardaMotorsDetails _BalSMTPSettings = new BAL_ShardaMotorsDetails();
                // Initialization.
                bool IsDelete = false;
                if(csvFileData!=null)
                {
                    IsDelete = _BalSMTPSettings.DeleteSMTPDetails();
                }
                List<BEL_ShardaMotorsDetails> _LstSMDetail = new List<BEL_ShardaMotorsDetails>();
                for (int i = 0; i < csvFileData.Rows.Count; i++)
                {
                    BEL_ShardaMotorsDetails _SMDetails = new BEL_ShardaMotorsDetails();
                    //_SMDetails.Id = Convert.ToInt32(csvFileData.Rows[i]["Id"]);
                    _SMDetails.Time = Convert.ToString(csvFileData.Rows[i]["Time"]);
                    _SMDetails.Part_Name = Convert.ToString(csvFileData.Rows[i]["Part_Name"]);
                    _SMDetails.DPF = Convert.ToString(csvFileData.Rows[i]["DPF"]);
                    _SMDetails.LNT = Convert.ToString(csvFileData.Rows[i]["LNT"]);
                    _SMDetails.Serial = Convert.ToString(csvFileData.Rows[i]["Serial"]);
                    _SMDetails.DPF_Cat = Convert.ToString(csvFileData.Rows[i]["DPF_Cat"]);
                    _SMDetails.LNT_Cat = Convert.ToString(csvFileData.Rows[i]["LNT_Cat"]);
                    _SMDetails.DPF_Weight = Convert.ToString(csvFileData.Rows[i]["DPF_Weight"]);
                    _SMDetails.LNT_Weight = Convert.ToString(csvFileData.Rows[i]["LNT_Weight"]);
                    _SMDetails.DPF_BW = Convert.ToString(csvFileData.Rows[i]["DPF_BW"]);
                    _SMDetails.LNT_BW = Convert.ToString(csvFileData.Rows[i]["LNT_BW"]);
                    _SMDetails.DPF_Out_dia = Convert.ToString(csvFileData.Rows[i]["DPF_Out_dia"]);
                    _SMDetails.LNT_Out_dia = Convert.ToString(csvFileData.Rows[i]["LNT_Out_dia"]);
                    _SMDetails.DPF_Sizing_Data = Convert.ToString(csvFileData.Rows[i]["DPF_Sizing_Data"]);
                    _SMDetails.LNT_Sizing_Data = Convert.ToString(csvFileData.Rows[i]["LNT_Sizing_Data"]);
                    _SMDetails.DPF_Target_GBD = Convert.ToString(csvFileData.Rows[i]["DPF_Target_GBD"]);
                    _SMDetails.LNT_Target_GBD = Convert.ToString(csvFileData.Rows[i]["LNT_Target_GBD"]);
                    _SMDetails.DPF_Gap = Convert.ToString(csvFileData.Rows[i]["DPF_Gap"]);
                    _SMDetails.LNT_Gap = Convert.ToString(csvFileData.Rows[i]["LNT_Gap"]);
                    _LstSMDetail.Add(_SMDetails);
                }
                BAL_ShardaMotorsDetails _BalSMDetails = new BAL_ShardaMotorsDetails();
                foreach (var items in _LstSMDetail)
                {
                    BAL_ShardaMotorsDetails _SMDetails = new BAL_ShardaMotorsDetails();
                    //_SMDetails.Id = items.Id;
                    _SMDetails.Time = items.Time;
                    _SMDetails.DPF = items.DPF;
                    _SMDetails.LNT = items.LNT;
                    _SMDetails.Part_Name = items.Part_Name;
                    _SMDetails.Serial = items.Serial;
                    _SMDetails.DPF_Cat = items.DPF_Cat;
                    _SMDetails.LNT_Cat = items.LNT_Cat;
                    _SMDetails.DPF_Weight = items.DPF_Weight;
                    _SMDetails.LNT_Weight = items.LNT_Weight;
                    _SMDetails.DPF_BW = items.DPF_BW;
                    _SMDetails.LNT_BW = items.LNT_BW;
                    _SMDetails.DPF_Out_dia = items.DPF_Out_dia;
                    _SMDetails.LNT_Out_dia = items.LNT_Out_dia;
                    _SMDetails.DPF_Sizing_Data = items.DPF_Sizing_Data;
                    _SMDetails.LNT_Sizing_Data = items.LNT_Sizing_Data;
                    _SMDetails.DPF_Target_GBD = items.DPF_Target_GBD;
                    _SMDetails.LNT_Target_GBD = items.LNT_Target_GBD;
                    _SMDetails.DPF_Gap = items.DPF_Gap;
                    _SMDetails.LNT_Gap = items.LNT_Gap;
                    bool IsSave = false;
                    IsSave = _BalSMDetails.InsertSMTPDetails(_SMDetails);
                    Response = new Response("Sharda Motors Detail's Added Successfully!");

                }
            }
            catch (Exception ex)
            {
                Response = new Response("Something goes wrong, Please try again later.");
            }
            return Response;
        }
        [Route("~/api/EmailTemplateId")]
        [HttpGet]
        public EmailTempResponse EmailTemplateId()//Get SMTP details
        {
            EmailTempResponse ResponseData = null;
            try
            {
                BAL_EmailTemplate _balEmailTemp = new BAL_EmailTemplate();
                _balEmailTemp.EmailTempLst = _balEmailTemp.SelectEmailTempByTypeId(15);
                if (_balEmailTemp.EmailTempLst.Count() != 0)
                {
                    ResponseData = new EmailTempResponse("Success.", _balEmailTemp.EmailTempLst);
                    ResponseData.data = _balEmailTemp.EmailTempLst;
                    return ResponseData;
                }
                else
                {
                    ResponseData = new EmailTempResponse("Data not found.", null);
                }
            }
            catch (Exception ex)
            {
                ResponseData = new EmailTempResponse("Something goes wrong, Please try again later.", null);
            }
            return ResponseData;
        }
    }
}