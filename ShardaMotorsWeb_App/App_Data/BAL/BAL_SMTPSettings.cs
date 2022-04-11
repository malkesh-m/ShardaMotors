using ShardaMotorsWeb_App.App_Data.BEL;
using ShardaMotorsWeb_App.App_Data.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShardaMotorsWeb_App.App_Data.BAL
{
    public class BAL_SMTPSettings
    {
        public int Id { get; set; }
        public string Sender_Name { get; set; }
        public string Sender_EmailId { get; set; }
        public string SMTP_UserName { get; set; }
        public string SMTP_Password { get; set; }
        public string SMTP_Host { get; set; }
        public int SMTP_PortNumber { get; set; }

        public BAL_SMTPSettings _LstSMTPSettings { get; set; }
        DAL_SMTPSettings _DalSMTP = new DAL_SMTPSettings();
        DataTable dt = new DataTable();

        public bool InsertSMTPDetails(BEL_SMTPSettings smtp)
        {
            return _DalSMTP.InsertSMTPDetails(smtp);
        }
        public bool UpdateSMTPDetails(BEL_SMTPSettings smtp)
        {
            return _DalSMTP.UpdateSMTPDetails(smtp);
        }
        public BAL_SMTPSettings SelectsmtpDtlsById(int id)
        {
            _LstSMTPSettings = _DalSMTP.SelectsmtpDtlsById(id).FirstOrDefault();
            return _LstSMTPSettings;
        }
        public DataTable SelectsmtpDtlsById()
        {
            dt = _DalSMTP.SelectsmtpDtlsById();
            return dt;
        }
    }
}
