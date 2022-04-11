using ShardaMotorsWebAPI.App_Data.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShardaMotorsWebAPI.App_Data.BAL
{
    public class BAL_EmailTemplate
    {
        public int temp_id { get; set; }
        public string email_sent_to { get; set; }
        public string subject { get; set; }
        public string content { get; set; }
        public int type { get; set; }
        public bool is_deleted { get; set; }
        public List<BAL_EmailTemplate> EmailTempLst { get; set; }

        DAL_EmailTemplate DalemailTem = new DAL_EmailTemplate();
        public List<BAL_EmailTemplate> SelectEmailTempByTypeId(int typeId)
        {
            EmailTempLst = DalemailTem.SelectEmailTempByTypeId(typeId).ToList();
            return EmailTempLst;
        }
    }
}
