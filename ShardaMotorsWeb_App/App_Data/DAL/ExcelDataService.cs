using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShardaMotorsWeb_App.App_Data.DAL
{
    public class ShardaMotorsDetails
    {
        public int Id { get; set; }
        public string Time { get; set; }
        public string Part_Name { get; set; }
        public string DPF { get; set; }
        public string LNT { get; set; }
        public string Serial { get; set; }
        public double DPF_Cat { get; set; }
        public double LNT_Cat { get; set; }
        public double DPF_Weight { get; set; }
        public double LNT_Weight { get; set; }
        public double DPF_BW { get; set; }
        public double LNT_BW { get; set; }
        public double DPF_Out_dia { get; set; }
        public double LNT_Out_dia { get; set; }
        public double DPF_Sizing_Data { get; set; }
        public double LNT_Sizing_Data { get; set; }
        public double DPF_Target_GBD { get; set; }
        public double LNT_Target_GBD { get; set; }
        public double DPF_Gap { get; set; }
        public double LNT_Gap { get; set; }
    }
    public class ExcelDataService
    {
        OleDbCommand Cmd;
        OleDbConnection Conn;
        public static string path = @"C:\src\RedirectApplication\RedirectApplication\301s.xlsx";
        public static string connStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path + ";Extended Properties=Excel 12.0;";
        public ExcelDataService()
        {
            //SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings.Get("PropertyControldb"));
            //string ExcelFilePath = @"H:\\SchoolManagement.xlsx";//bin\ExcelData
            string ExcelFilePath = AppDomain.CurrentDomain.BaseDirectory + "ExcelData\\ShardaMotorsDetails.csv";
            string excelConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + ExcelFilePath + ";Extended Properties=Excel 12.0;Persist Security Info=True";
            Conn = new OleDbConnection(excelConnectionString);
        }

        /// <summary>  
        /// Method to Get All the Records from Excel  
        /// </summary>  
        /// <returns></returns>  
        public async Task<ObservableCollection<ShardaMotorsDetails>> ReadRecordFromEXCELAsync()
        {           
            ObservableCollection<ShardaMotorsDetails> Students = new ObservableCollection<ShardaMotorsDetails>();
            await Conn.OpenAsync();
            Cmd = new OleDbCommand();
            Cmd.Connection = Conn;
            Cmd.CommandText = "Select * from [ShardaMotorsDetails$]";
            
            var Reader = await Cmd.ExecuteReaderAsync();
            while (Reader.Read())
            {
                Students.Add(new ShardaMotorsDetails()
                {
                    Id = Convert.ToInt32(Reader["Id"]),
                    Time = Reader["Time"].ToString(),
                    Part_Name = Reader["Part_Name"].ToString(),
                    DPF = Reader["DPF"].ToString(),
                    LNT = Reader["LNT"].ToString(),
                    Serial = Reader["Serial"].ToString(),
                    DPF_Cat =Convert.ToDouble(Reader["DPF_Cat"]),
                    LNT_Cat = Convert.ToDouble(Reader["LNT_Cat"]),
                    DPF_Weight = Convert.ToDouble(Reader["DPF_Weight"]),
                    LNT_Weight = Convert.ToDouble(Reader["LNT_Weight"]),
                    DPF_BW =Convert.ToDouble(Reader["DPF_BW"]),
                    LNT_BW = Convert.ToDouble(Reader["LNT_BW"]),
                    DPF_Out_dia = Convert.ToDouble(Reader["DPF_Out_dia"]),
                    LNT_Out_dia = Convert.ToDouble(Reader["LNT_Out_dia"]),
                    DPF_Sizing_Data = Convert.ToDouble(Reader["DPF_Sizing_Data"]),
                    LNT_Sizing_Data = Convert.ToDouble(Reader["LNT_Sizing_Data"]),
                    DPF_Target_GBD = Convert.ToDouble(Reader["DPF_Target_GBD"]),
                    LNT_Target_GBD = Convert.ToDouble(Reader["LNT_Target_GBD"]),
                    DPF_Gap = Convert.ToDouble(Reader["DPF_Gap"]),
                    LNT_Gap = Convert.ToDouble(Reader["LNT_Gap"])

                });
            }
            Reader.Close();
            Conn.Close();
            return Students;
        }

        /// <summary>  
        /// Method to Insert Record in the Excel  
        /// S1. If the EmpNo =0, then the Operation is Skipped.  
        /// S2. If the Student is already exist, then it is taken for Update  
        /// </summary>  
        /// <param name="Emp"></param>  
        public async Task<bool> ManageExcelRecordsAsync(ShardaMotorsDetails stud)
        {
            bool IsSave = false;
            if (stud.Id != 0)
            {
                await Conn.OpenAsync();
                Cmd = new OleDbCommand();
                Cmd.Connection = Conn;
                Cmd.Parameters.AddWithValue("@Id", stud.Id);
                Cmd.Parameters.AddWithValue("@Time", stud.Time);
                Cmd.Parameters.AddWithValue("@Part_Name", stud.Part_Name);
                Cmd.Parameters.AddWithValue("@DPF", stud.DPF);
                Cmd.Parameters.AddWithValue("@LNT", stud.LNT);
                Cmd.Parameters.AddWithValue("@Serial", stud.Serial);
                Cmd.Parameters.AddWithValue("@DPF_Cat", stud.DPF_Cat);
                Cmd.Parameters.AddWithValue("@LNT_Cat", stud.LNT_Cat);
                Cmd.Parameters.AddWithValue("@DPF_Weight", stud.DPF_Weight);
                Cmd.Parameters.AddWithValue("@LNT_Weight", stud.LNT_Weight);
                Cmd.Parameters.AddWithValue("@DPF_BW", stud.DPF_BW);
                Cmd.Parameters.AddWithValue("@LNT_BW", stud.LNT_BW);
                Cmd.Parameters.AddWithValue("@DPF_Out_dia", stud.DPF_Out_dia);
                Cmd.Parameters.AddWithValue("@LNT_Out_dia", stud.LNT_Out_dia);
                Cmd.Parameters.AddWithValue("@DPF_Sizing_Data", stud.DPF_Sizing_Data);
                Cmd.Parameters.AddWithValue("@LNT_Sizing_Data", stud.LNT_Sizing_Data);
                Cmd.Parameters.AddWithValue("@DPF_Target_GBD", stud.DPF_Target_GBD);
                Cmd.Parameters.AddWithValue("@LNT_Target_GBD", stud.LNT_Target_GBD);
                Cmd.Parameters.AddWithValue("@DPF_Gap", stud.DPF_Gap);
                Cmd.Parameters.AddWithValue("@LNT_Gap", stud.LNT_Gap);

                if (!IsStudentRecordExistAsync(stud).Result)
                {
                    Cmd.CommandText = "Insert into [ShardaMotorsDetails$] values (@Id,@Time,@Part_Name,@DPF,@LNT,@Serial,@DPF_Cat,@LNT_Cat,@DPF_Weight,@LNT_Weight,@DPF_BW,@LNT_BW,@DPF_Out_dia,@LNT_Out_dia,@DPF_Sizing_Data,@LNT_Sizing_Data,@DPF_Target_GBD,@LNT_Target_GBD,@DPF_Gap,@LNT_Gap)";
                }
                else
                {
                    Cmd.CommandText = "Update [ShardaMotorsDetails$] set Id=@Id,Time=@Time,Part_Name=@Part_Name,DPF=@DPF,LNT=@LNT,Serial=@Serial,DPF_Cat=@DPF_Cat,LNT_Cat=@LNT_Cat,DPF_Weight=@DPF_Weight,LNT_Weight=@LNT_Weight,DPF_BW=@DPF_BW,LNT_BW=@LNT_BW,DPF_Out_dia=@DPF_Out_dia,LNT_Out_dia=@LNT_Out_dia,DPF_Sizing_Data=@DPF_Sizing_Data,LNT_Sizing_Data=@LNT_Sizing_Data,DPF_Target_GBD=@DPF_Target_GBD,LNT_Target_GBD=@LNT_Target_GBD,DPF_Gap=@DPF_Gap,LNT_Gap=@LNT_Gap where Id=@Id";

                }
                int result = await Cmd.ExecuteNonQueryAsync();
                if (result > 0)
                {
                    IsSave = true;
                }
                Conn.Close();
            }
            return IsSave;

        }
        /// <summary>  
        /// The method to check if the record is already available   
        /// in the workgroup  
        /// </summary>  
        /// <param name="emp"></param>  
        /// <returns></returns>  
        private async Task<bool> IsStudentRecordExistAsync(ShardaMotorsDetails stud)
        {
            bool IsRecordExist = false;
            Cmd.CommandText = "Select * from [Sheet1$] where Id=@Id";
            var Reader = await Cmd.ExecuteReaderAsync();
            if (Reader.HasRows)
            {
                IsRecordExist = true;
            }

            Reader.Close();
            return IsRecordExist;
        }
    }
}