using ShardaMotorsWebAPI.App_Data.BAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShardaMotorsWebAPI.App_Data.DAL
{
    public class DAL_EmailTemplate:DALBase
    {
        public List<BAL_EmailTemplate> SelectEmailTempByTypeId(int _typeid)
        {
            //if 0 then select all else by id
            try
            {
                List<BAL_EmailTemplate> UserList = new List<BAL_EmailTemplate>();
                AddParameter("@typeId", SqlDbType.Int, _typeid);
                DataSet ds = ExecuteForData("Sp_Select_TemplateType");
                foreach (DataRow users in ds.Tables[0].Rows)
                {
                    BAL_EmailTemplate emailtemp = new BAL_EmailTemplate();
                    emailtemp.temp_id = Convert.ToInt32(users["temp_id"]);
                    emailtemp.email_sent_to = Convert.ToString(users["email_sent_to"]);
                    emailtemp.subject = Convert.ToString(users["subject"]);
                    emailtemp.type = Convert.ToInt32(users["type"]);
                    emailtemp.content = Convert.ToString(users["content"]);
                    emailtemp.is_deleted = Convert.ToBoolean(users["is_deleted"]);
                    UserList.Add(emailtemp);
                }
                return UserList;
            }
            catch (Exception ex)
            {
                return null;
            }
            finally { _dataConnection.Close(); }
        }
    }
}
