using ShardaMotorsWebAPI.App_Data.BEL;
using ShardaMotorsWebAPI.App_Data.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShardaMotorsWebAPI.App_Data.BAL
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
        public string Machine_ID { get; set; }
        public string Location_Path { get; set; }
        public List<BAL_SMTPSettings> _LstSMTPSettings { get; set; }
        DAL_SMTPSettings _DalSMTP = new DAL_SMTPSettings();
        public bool InsertSMTPDetails(BEL_SMTPSettings smtp)
        {
            return _DalSMTP.InsertSMTPDetails(smtp);
        }
        public bool UpdateSMTPDetails(BEL_SMTPSettings smtp)
        {
            return _DalSMTP.UpdateSMTPDetails(smtp);
        }
        public List<BAL_SMTPSettings> SelectsmtpDtlsById()
        {
            _LstSMTPSettings = _DalSMTP.SelectsmtpDtlsById().ToList();
            return _LstSMTPSettings;
        }
    }
}
