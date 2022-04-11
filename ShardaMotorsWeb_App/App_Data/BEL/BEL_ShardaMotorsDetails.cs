using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ShardaMotorsWeb_App.App_Data.BEL
{
    public class BEL_ShardaMotorsDetails
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
        public string Machine_ID { get; set; }
    }
}
