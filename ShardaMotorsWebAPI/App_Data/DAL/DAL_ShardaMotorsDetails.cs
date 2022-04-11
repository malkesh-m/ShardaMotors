using ShardaMotorsWebAPI.App_Data.BAL;
using ShardaMotorsWebAPI.App_Data.BEL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShardaMotorsWebAPI.App_Data.DAL
{
    public class DAL_ShardaMotorsDetails : DALBase
    {
        public DataTable SelectsmtpDtlsById()
        {
            DataTable SMTPList = new DataTable();
            DataSet ds = new DataSet();
            try
            {
                ds = ExecuteForData("Sp_Select_ShardaMotorsDetails");
                SMTPList = ds.Tables[0];
            }
            catch (Exception ex)
            {
            }
            finally { _dataConnection.Close(); }
            return SMTPList;
        }

        public bool InsertSMDetails(BAL_ShardaMotorsDetails _BelSMdetails)
        {

            try
            {
                //AddParameter("@Id", SqlDbType.Int, _BelSMdetails.Id);
                AddParameter("@Time", SqlDbType.VarChar, _BelSMdetails.Time);
                AddParameter("@Part_Name", SqlDbType.VarChar, _BelSMdetails.Part_Name);
                AddParameter("@DPF", SqlDbType.VarChar, _BelSMdetails.DPF);
                AddParameter("@LNT", SqlDbType.VarChar, _BelSMdetails.LNT);
                AddParameter("@Serial", SqlDbType.VarChar, _BelSMdetails.Serial);
                AddParameter("@DPF_Cat", SqlDbType.VarChar, _BelSMdetails.DPF_Cat);
                AddParameter("@LNT_Cat", SqlDbType.VarChar, _BelSMdetails.LNT_Cat);
                AddParameter("@DPF_Weight", SqlDbType.VarChar, _BelSMdetails.DPF_Weight);
                AddParameter("@LNT_Weight", SqlDbType.VarChar, _BelSMdetails.LNT_Weight);
                AddParameter("@DPF_BW", SqlDbType.VarChar, _BelSMdetails.DPF_BW);
                AddParameter("@LNT_BW", SqlDbType.VarChar, _BelSMdetails.LNT_BW);
                AddParameter("@DPF_Out_dia", SqlDbType.VarChar, _BelSMdetails.DPF_Out_dia);
                AddParameter("@LNT_Out_dia", SqlDbType.VarChar, _BelSMdetails.LNT_Out_dia);
                AddParameter("@DPF_Sizing_Data", SqlDbType.VarChar, _BelSMdetails.DPF_Sizing_Data);
                AddParameter("@LNT_Sizing_Data", SqlDbType.VarChar, _BelSMdetails.LNT_Sizing_Data);
                AddParameter("@DPF_Target_GBD", SqlDbType.VarChar, _BelSMdetails.DPF_Target_GBD);
                AddParameter("@LNT_Target_GBD", SqlDbType.VarChar, _BelSMdetails.LNT_Target_GBD);
                AddParameter("@DPF_Gap", SqlDbType.VarChar, _BelSMdetails.DPF_Gap);
                AddParameter("@LNT_Gap", SqlDbType.VarChar, _BelSMdetails.LNT_Gap);
                AddParameter("@Updated_DateTime", SqlDbType.DateTime, _BelSMdetails.Updated_DateTime);
                AddParameter("@Machine_ID", SqlDbType.VarChar, _BelSMdetails.Machine_ID);

                ExecuteScalar("Sp_Insert_ShardaMotorsDetails");

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
        
        public void InsertSMMCSCsvBulk(DataTable dtSMMCSDetails)
        {
            try
            {
                //var allocationData = new SqlParameter();
                //allocationData.Direction = ParameterDirection.Input;
                //allocationData.TypeName = "Type_ShardaMotorsDetails";
                //allocationData.Value = dtSMMCSDetails;
                AddParameter("@p_SMMCSCsvDetails", SqlDbType.Structured, dtSMMCSDetails);
                ExecuteScalar("Sp_Insert_ShardaMotorsCsvBulk");
            }
            catch (Exception ex)
            {

            }
            finally
            {
                _dataConnection.Close();
            }
        }
        public void InsertSMMCSBulk(DataTable dtSMMCSDetails)
        {
            try
            {
                //varS allocationData = new SqlParameter();
                //allocationData.Direction = ParameterDirection.Input;
                //allocationData.TypeName = "Type_ShardaMotorsDetails";
                //allocationData.Value = dtSMMCSDetails;
                AddParameter("@p_SMMCSDetailss", SqlDbType.Structured, dtSMMCSDetails);
                ExecuteScalar("Sp_Insert_ShardaMotorsBulk");
            }
            catch (Exception ex)
            {

            }
            finally
            {
                _dataConnection.Close();
            }
        }
        public bool DeleteSMDetails(string _UpdatedDate)
        {

            try
            {
                AddParameter("@UpdatedDate", SqlDbType.VarChar, _UpdatedDate);
                ExecuteScalar("Sp_Delete_ShardaMotorsDetails");

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
        public List<BAL_ShardaMotorsDetails> SelectSMDetails()
        {
            List<BAL_ShardaMotorsDetails> SMDetails = new List<BAL_ShardaMotorsDetails>();
            try
            {
                DataSet ds = ExecuteForData("Sp_Select_ShardaMotorsDetails");
                foreach (DataRow users in ds.Tables[0].Rows)
                {
                    BAL_ShardaMotorsDetails smtpDetails = new BAL_ShardaMotorsDetails();
                    smtpDetails.Id = Convert.ToInt32(users["Id"]);
                    smtpDetails.Time = Convert.ToString(users["Time"]);
                    smtpDetails.Part_Name = Convert.ToString(users["Part_Name"]);
                    smtpDetails.DPF = Convert.ToString(users["DPF"]);
                    smtpDetails.LNT = Convert.ToString(users["LNT"]);
                    smtpDetails.Serial = Convert.ToString(users["Serial"]);
                    smtpDetails.DPF_Cat = Convert.ToString(users["DPF_Cat"]);
                    smtpDetails.LNT_Cat = Convert.ToString(users["LNT_Cat"]);
                    smtpDetails.DPF_Weight = Convert.ToString(users["DPF_Weight"]);
                    smtpDetails.LNT_Weight = Convert.ToString(users["LNT_Weight"]);
                    smtpDetails.DPF_BW = Convert.ToString(users["DPF_BW"]);
                    smtpDetails.LNT_BW = Convert.ToString(users["LNT_BW"]);
                    smtpDetails.DPF_Out_dia = Convert.ToString(users["DPF_Out_dia"]);
                    smtpDetails.LNT_Out_dia = Convert.ToString(users["LNT_Out_dia"]);
                    smtpDetails.DPF_Sizing_Data = Convert.ToString(users["DPF_Sizing_Data"]);
                    smtpDetails.LNT_Sizing_Data = Convert.ToString(users["LNT_Sizing_Data"]);
                    smtpDetails.DPF_Target_GBD = Convert.ToString(users["DPF_Target_GBD"]);
                    smtpDetails.LNT_Target_GBD = Convert.ToString(users["LNT_Target_GBD"]);
                    smtpDetails.DPF_Gap = Convert.ToString(users["DPF_Gap"]);
                    smtpDetails.LNT_Gap = Convert.ToString(users["LNT_Gap"]);
                    smtpDetails.Updated_DateTime = Convert.ToDateTime(users["Updated_DateTime"]);                   
                    smtpDetails.Machine_ID = Convert.ToString(users["Machine_ID"]);

                    SMDetails.Add(smtpDetails);
                }

            }
            catch (Exception ex)
            {
            }
            finally { _dataConnection.Close(); }
            return SMDetails.ToList();
        }
        public List<BAL_ShardaMotorsDetails> SelectSMDetailsDateWise(DateTime _UpdatedDate)
        {
            List<BAL_ShardaMotorsDetails> SMDetails = new List<BAL_ShardaMotorsDetails>();
            try
            {
                AddParameter("@UpdatedDateTime", SqlDbType.DateTime, _UpdatedDate);
                DataSet ds = ExecuteForData("Sp_Select_ShardaMotorsDetailsDateWise");
                foreach (DataRow users in ds.Tables[0].Rows)
                {
                    BAL_ShardaMotorsDetails smtpDetails = new BAL_ShardaMotorsDetails();
                    smtpDetails.Id = Convert.ToInt32(users["Id"]);
                    smtpDetails.Time = Convert.ToString(users["Time"]);
                    smtpDetails.Part_Name = Convert.ToString(users["Part_Name"]);
                    smtpDetails.DPF = Convert.ToString(users["DPF"]);
                    smtpDetails.LNT = Convert.ToString(users["LNT"]);
                    smtpDetails.Serial = Convert.ToString(users["Serial"]);
                    smtpDetails.DPF_Cat = Convert.ToString(users["DPF_Cat"]);
                    smtpDetails.LNT_Cat = Convert.ToString(users["LNT_Cat"]);
                    smtpDetails.DPF_Weight = Convert.ToString(users["DPF_Weight"]);
                    smtpDetails.LNT_Weight = Convert.ToString(users["LNT_Weight"]);
                    smtpDetails.DPF_BW = Convert.ToString(users["DPF_BW"]);
                    smtpDetails.LNT_BW = Convert.ToString(users["LNT_BW"]);
                    smtpDetails.DPF_Out_dia = Convert.ToString(users["DPF_Out_dia"]);
                    smtpDetails.LNT_Out_dia = Convert.ToString(users["LNT_Out_dia"]);
                    smtpDetails.DPF_Sizing_Data = Convert.ToString(users["DPF_Sizing_Data"]);
                    smtpDetails.LNT_Sizing_Data = Convert.ToString(users["LNT_Sizing_Data"]);
                    smtpDetails.DPF_Target_GBD = Convert.ToString(users["DPF_Target_GBD"]);
                    smtpDetails.LNT_Target_GBD = Convert.ToString(users["LNT_Target_GBD"]);
                    smtpDetails.DPF_Gap = Convert.ToString(users["DPF_Gap"]);
                    smtpDetails.LNT_Gap = Convert.ToString(users["LNT_Gap"]);
                    smtpDetails.Updated_DateTime = Convert.ToDateTime(users["Updated_DateTime"]);
                    smtpDetails.Machine_ID = Convert.ToString(users["Machine_ID"]);

                    SMDetails.Add(smtpDetails);
                }

            }
            catch (Exception ex)
            {
            }
            finally { _dataConnection.Close(); }
            return SMDetails.ToList();
        }
    }
}
