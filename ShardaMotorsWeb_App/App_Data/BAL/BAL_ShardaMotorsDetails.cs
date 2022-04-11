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
    public class BAL_ShardaMotorsDetails
    {
        public int Id { get; set; }
        public DateTime Updated_DateTime { get; set; }
        public string Time { get; set; }
        public string Part_Name { get; set; }
        public string DPF { get; set; }
        public string LNT { get; set; }
        public string Serial { get; set; }
        public string DPF_Cat { get; set; }
        public string LNT_Cat { get; set; }
        public string DPF_Weight { get; set; }
        public string LNT_Weight { get; set; }
        public string DPF_BW { get; set; }
        public string LNT_BW { get; set; }
        public string DPF_Out_dia { get; set; }
        public string LNT_Out_dia { get; set; }
        public string DPF_Sizing_Data { get; set; }
        public string LNT_Sizing_Data { get; set; }
        public string DPF_Target_GBD { get; set; }
        public string LNT_Target_GBD { get; set; }
        public string DPF_Gap { get; set; }
        public string LNT_Gap { get; set; }
        //public string Updated_DateTime { get; set; }
        public string Machine_ID { get; set; }
        public int RowNumber { get; set; }
        public List<BAL_ShardaMotorsDetails> _LstShardaMotorsDtls { get; set; }
        DAL_ShardaMotorsDetails _DalSMDtls = new DAL_ShardaMotorsDetails();
        DataTable dt = new DataTable();
        public bool InsertSMTPDetails(BAL_ShardaMotorsDetails SMDtls)
        {
            return _DalSMDtls.InsertSMDetails(SMDtls);
        }
        public bool DeleteSMTPDetails()
        {
            return _DalSMDtls.DeleteSMTPDetails();
        }
        public DataTable SelectsmtpDtlsById()
        {
            dt = _DalSMDtls.SelectsmtpDtlsById();
            return dt;
        }
        public List<BAL_ShardaMotorsDetails> SelectSMDetails()
        {
            _LstShardaMotorsDtls = _DalSMDtls.SelectSMDetails();
            return _LstShardaMotorsDtls;
        }
        public BalShardaMotorsWithTotal SelectSMDetailsDateWise(DateTime _UpdatedDate)
        {
           var balShardaMotorsWithTotal = _DalSMDtls.SelectSMDetailsDateWise(_UpdatedDate);
            return balShardaMotorsWithTotal;
        }
        public BalShardaMotorsWithTotal SelectSMDetailsPageWise(int pageNumber,int Size, DateTime fromdate, DateTime todate)
        {
            var balShardaMotorsWithTotal = _DalSMDtls.SelectSMDetailsPageWise(pageNumber,Size, fromdate, todate);
            return balShardaMotorsWithTotal;
        }
        public List<BAL_ShardaMotorsDetails> SelectSMDetailsNew(int startRecord, int endRecord)
        {
            _LstShardaMotorsDtls = _DalSMDtls.SelectSMDetailsNew(startRecord, endRecord);
            return _LstShardaMotorsDtls;
        }
    }

    public class BalShardaMotorsWithTotal
    {
        public List<BAL_ShardaMotorsDetails> bal_ShardaMotorsDetails { get; set; }
        public int TotalRecord { get; set; }
    }


}
