

using ShardaMotorsWebAPI.App_Data.BAL;
using ShardaMotorsWebAPI.App_Data.BEL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShardaMotorsWebAPI.App_Data.DAL
{
   public class DAL_SMTPSettings: DALBase
    {
        public bool InsertSMTPDetails(BEL_SMTPSettings _Belsmtp)
        {

            try
            {
                //AddParameter("@Id", SqlDbType.Int, _Belsmtp.Id);
                AddParameter("@Sender_Name", SqlDbType.VarChar, _Belsmtp.Sender_Name);
                AddParameter("@Sender_EmailId", SqlDbType.VarChar, _Belsmtp.Sender_EmailId);
                AddParameter("@SMTP_Host", SqlDbType.VarChar, _Belsmtp.SMTP_Host);
                AddParameter("@SMTP_Password", SqlDbType.VarChar, _Belsmtp.SMTP_Password);
                AddParameter("@SMTP_UserName", SqlDbType.VarChar, _Belsmtp.SMTP_UserName);
                AddParameter("@SMTP_PortNumber", SqlDbType.VarChar, _Belsmtp.SMTP_PortNumber);
                AddParameter("@Machine_ID", SqlDbType.VarChar, _Belsmtp.Machine_ID);
                AddParameter("@Location_Path", SqlDbType.VarChar, _Belsmtp.Location_Path);
                 ExecuteScalar("Sp_Insert_SmtpDetails");

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                _dataConnection.Close();
            }
        }
        public bool UpdateSMTPDetails(BEL_SMTPSettings _Belsmtp)
        {

            try
            {
                AddParameter("@Id", SqlDbType.Int, _Belsmtp.Id);
                AddParameter("@Sender_Name", SqlDbType.VarChar, _Belsmtp.Sender_Name);
                AddParameter("@Sender_EmailId", SqlDbType.VarChar, _Belsmtp.Sender_EmailId);
                AddParameter("@SMTP_Host", SqlDbType.VarChar, _Belsmtp.SMTP_Host);
                AddParameter("@SMTP_Password", SqlDbType.VarChar, _Belsmtp.SMTP_Password);
                AddParameter("@SMTP_UserName", SqlDbType.VarChar, _Belsmtp.SMTP_UserName);
                AddParameter("@SMTP_PortNumber", SqlDbType.VarChar, _Belsmtp.SMTP_PortNumber);
                AddParameter("@Machine_ID", SqlDbType.VarChar, _Belsmtp.Machine_ID);
                AddParameter("@Location_Path", SqlDbType.VarChar, _Belsmtp.Location_Path);
                ExecuteScalar("Sp_Update_SmtpDetails");
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                _dataConnection.Close();
            }
        }
        public List<BAL_SMTPSettings> SelectsmtpDtlsById()//int id
        {
            List<BAL_SMTPSettings> SMTPList = new List<BAL_SMTPSettings>();
            try
            {
                //AddParameter("@Id", SqlDbType.Int, id);
                DataSet ds = ExecuteForData("Sp_Select_SMTPDetails");
                foreach (DataRow users in ds.Tables[0].Rows)
                {
                    BAL_SMTPSettings smtpDetails = new BAL_SMTPSettings();
                    smtpDetails.Id = Convert.ToInt32(users["Id"]);
                    smtpDetails.SMTP_UserName = Convert.ToString(users["SMTP_UserName"]);
                    smtpDetails.Sender_Name = Convert.ToString(users["Sender_Name"]);
                    smtpDetails.Sender_EmailId = Convert.ToString(users["Sender_EmailId"]);
                    smtpDetails.SMTP_Host = Convert.ToString(users["SMTP_Host"]);
                    smtpDetails.SMTP_Password = Convert.ToString(users["SMTP_Password"]);
                    smtpDetails.SMTP_PortNumber = Convert.ToInt32(users["SMTP_PortNumber"]);
                    smtpDetails.Machine_ID = Convert.ToString(users["Machine_ID"]);
                    smtpDetails.Location_Path = Convert.ToString(users["Location_Path"]);
                    SMTPList.Add(smtpDetails);
                }

            }
            catch (Exception ex)
            {
            }
            finally { _dataConnection.Close(); }
            return SMTPList.ToList();
        }
    }
}
