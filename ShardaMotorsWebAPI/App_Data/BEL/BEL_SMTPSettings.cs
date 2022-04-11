using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShardaMotorsWebAPI.App_Data.BEL
{
   public class BEL_SMTPSettings
    {
        public int Id { get; set; }
        public string Sender_Name { get; set; }
        public string Sender_EmailId { get; set; }
        public string SMTP_UserName { get; set; }
        public string SMTP_Password { get; set; }
        public string SMTP_Host { get; set; }
        public string Machine_ID { get; set; }
        public string Location_Path { get; set; }
        public int SMTP_PortNumber { get; set; }
    }
}
