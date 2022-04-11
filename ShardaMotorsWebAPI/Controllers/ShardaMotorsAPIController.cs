using ShardaMotorsWebAPI.App_Data.BAL;
using ShardaMotorsWebAPI.App_Data.BEL;
using ShardaMotorsWebAPI.App_Data.DAL;
using ShardaMotorsWebAPI.Model;
using ShardaMotorsWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Management;
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
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody] string value)
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
                string machineID = GetProcessorId();
                _balSmtpUser._LstSMTPSettings = _balSmtpUser.SelectsmtpDtlsById().Where(m => m.Machine_ID == machineID).ToList();
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
                var PreviousDate = _balSMdetails.SelectSMDetails().LastOrDefault().Updated_DateTime;
                _balSMdetails._LstShardaMotorsDtls = _balSMdetails.SelectSMDetailsDateWise(PreviousDate).ToList();

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
        [Route("~/api/ShardaMotorsDetailsDateWise")]
        [HttpGet]
        public SMResponseData ShardaMotorsDetailsDateWise()//Get SMTP details
        {
            SMResponseData ResponseData = null;
            try
            {
                BAL_ShardaMotorsDetails _balSMdetails = new BAL_ShardaMotorsDetails();
                var PreviousDate = _balSMdetails.SelectSMDetails().LastOrDefault().Updated_DateTime;
                _balSMdetails._LstShardaMotorsDtls = _balSMdetails.SelectSMDetailsDateWise(PreviousDate).OrderByDescending(m => m.Id).ToList();//.Where(m => m.Updated_DateTime == PreviousDate).ToList();

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
        public Response SaveSMTPSetting(string _SenderName, string _SenderEmailId, string _SMTPUserName, string _SMTPPassword, string _SMTPHost, int _SMTPPortNumber, string _MachineID, string _LocationPath)
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
                _BelSMTPSettings.Machine_ID = _MachineID;
                _BelSMTPSettings.Location_Path = _LocationPath;
                _BalSMTPSettings._LstSMTPSettings = _BalSMTPSettings.SelectsmtpDtlsById().Where(m => m.Machine_ID == _MachineID).ToList();


                if (_BalSMTPSettings._LstSMTPSettings.Count == 0)
                {
                    bool IsSave = false;
                    IsSave = _BalSMTPSettings.InsertSMTPDetails(_BelSMTPSettings);
                    Response = new Response("SMTP Details Added Successfully!");
                }
                else
                {
                    foreach (var items in _BalSMTPSettings._LstSMTPSettings)
                    {
                        _BelSMTPSettings.Id = items.Id;
                    }

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
        public Response SaveSMDetails(DataTable csvFileData, string _UpdatedDate, string MachineID)
        {
            Response Response = null;

            try
            {
                BEL_ShardaMotorsDetails _Bel_SMDetails = new BEL_ShardaMotorsDetails();
                BAL_ShardaMotorsDetails _BalSMTPSettings = new BAL_ShardaMotorsDetails();

                if (csvFileData.Columns.Count == 11)
                {
                    csvFileData.Columns[0].ColumnName = "Time";
                    csvFileData.Columns[1].ColumnName = "Part_Name";
                    csvFileData.Columns[2].ColumnName = "DPF";
                    csvFileData.Columns[3].ColumnName = "LNT";
                    csvFileData.Columns[4].ColumnName = "Serial";
                    csvFileData.Columns[5].ColumnName = "DPF_Weight";
                    csvFileData.Columns[6].ColumnName = "DPF_Cat";
                    csvFileData.Columns[7].ColumnName = "DPF_Sizing_Data";
                    csvFileData.Columns[8].ColumnName = "LNT_Weight";
                    csvFileData.Columns[9].ColumnName = "LNT_Cat";
                    csvFileData.Columns[10].ColumnName = "LNT_Sizing_Data";
                    csvFileData.Columns.Add("Updated_DateTime", typeof(System.String));
                    csvFileData.Columns.Add("MachineID", typeof(System.String));
                    foreach (DataRow row in csvFileData.Rows)
                    {
                        //need to set value to NewColumn column
                        row["Updated_DateTime"] = _UpdatedDate;   // or set it to some other value
                        row["MachineID"] = GetProcessorId();
                    }

                    DAL_ShardaMotorsDetails _DalSMMCS = new DAL_ShardaMotorsDetails();
                    _DalSMMCS.InsertSMMCSCsvBulk(csvFileData);
                    Response = new Response("Sharda Motors Detail's Added Successfully!");
                }
                else
                {
                    csvFileData.Columns[0].ColumnName = "Time";
                    csvFileData.Columns[1].ColumnName = "Part_Name";
                    csvFileData.Columns[2].ColumnName = "DPF";
                    csvFileData.Columns[3].ColumnName = "LNT";
                    csvFileData.Columns[4].ColumnName = "Serial";
                    csvFileData.Columns[5].ColumnName = "DPF_Cat";
                    csvFileData.Columns[6].ColumnName = "LNT_Cat";
                    csvFileData.Columns[7].ColumnName = "DPF_Weight";
                    csvFileData.Columns[8].ColumnName = "LNT_Weight";
                    csvFileData.Columns[9].ColumnName = "DPF_BW";
                    csvFileData.Columns[10].ColumnName = "LNT_BW";
                    csvFileData.Columns[11].ColumnName = "DPF_Out_dia";
                    csvFileData.Columns[12].ColumnName = "LNT_Out_dia";
                    csvFileData.Columns[13].ColumnName = "DPF_Sizing_Data";
                    csvFileData.Columns[14].ColumnName = "LNT_Sizing_Data";
                    csvFileData.Columns[15].ColumnName = "DPF_Target_GBD";
                    csvFileData.Columns[16].ColumnName = "LNT_Target_GBD";
                    csvFileData.Columns[17].ColumnName = "DPF_Gap";
                    csvFileData.Columns[18].ColumnName = "LNT_Gap";
                    csvFileData.Columns.Add("Updated_DateTime", typeof(System.String));
                    csvFileData.Columns.Add("MachineID", typeof(System.String));
                    foreach (DataRow row in csvFileData.Rows)
                    {
                        //need to set value to NewColumn column
                        row["Updated_DateTime"] = _UpdatedDate;   // or set it to some other value
                        row["MachineID"] = MachineID;
                    }

                    DAL_ShardaMotorsDetails _DalSMMCS = new DAL_ShardaMotorsDetails();
                    _DalSMMCS.InsertSMMCSBulk(csvFileData);
                    Response = new Response("Sharda Motors Detail's Added Successfully!");
                }

            }
            catch (Exception ex)
            {
                Response = new Response("Something goes wrong, Please try again later.");
            }
            return Response;
        }
        public String GetProcessorId()
        {

            ManagementClass mc = new ManagementClass("win32_processor");
            ManagementObjectCollection moc = mc.GetInstances();
            String Id = String.Empty;
            foreach (ManagementObject mo in moc)
            {

                Id = mo.Properties["processorID"].Value.ToString();
                break;
            }
            return Id;

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