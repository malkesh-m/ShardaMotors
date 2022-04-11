using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShardaMotorsWeb_App.App_Data.BEL
{
   public class BEL_SMTPSettings
    {
        public int Id { get; set; }
        public string Sender_Name { get; set; }
        public string Sender_EmailId { get; set; }
        public string SMTP_UserName { get; set; }
        public string SMTP_Password { get; set; }
        public string SMTP_Host { get; set; }
        public int SMTP_PortNumber { get; set; }
    }
}
