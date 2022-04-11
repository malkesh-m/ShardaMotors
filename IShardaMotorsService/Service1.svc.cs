using ShardaMotors_App.App_Data.BAL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace IShardaMotorsService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {

        private LoginUser _loginUser = null;
        private List<LoginUser> customers = null;

        string connectionString = ConfigurationManager.ConnectionStrings["PropertyControldb"].ConnectionString;

        //public List<LoginUser> GetCustomers()
        //{
        //    _loginUser = new LoginUser();
        //    BAL_Users _balUsers = new BAL_Users();
        //    _balUsers._lstLoginUser = _balUsers.SelectUserById(1);
        //    customers = new List<LoginUser>();
        //    int id = 1;
        //    using (var cnn = new SqlConnection(connectionString))
        //    {
        //        using (var cmd = new SqlCommand(
        //        "Select * From" +
        //        "FROM admin_smmcs.Tbl_Users Where Id=" + id + "", cnn))
        //        {
        //            cnn.Open();
        //            using (SqlDataReader CustomersReader =
        //            cmd.ExecuteReader())
        //            {
        //                while (CustomersReader.Read())
        //                {
        //                    _loginUser.Id = _balUsers.Id;
        //                    _loginUser.User_Name = _balUsers.UserName;
        //                    _loginUser.Password = _balUsers.Password;
        //                    customers.Add(_loginUser);
        //                }
        //            }
        //        }
        //    }
        //    return customers;
        //}
        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }

        public List<LoginUser> GetLoginUser()
        {
            _loginUser = new LoginUser();
            BAL_Users _balUsers = new BAL_Users();
            _balUsers._lstLoginUser = _balUsers.SelectUserById(1);
            customers = new List<LoginUser>();
            int id = 1;
            using (var cnn = new SqlConnection(connectionString))
            {
                using (var cmd = new SqlCommand(
                "Select * " +
                "FROM admin_smmcs.Tbl_Users Where Id=" + id + "", cnn))
                {
                    cnn.Open();
                    using (SqlDataReader CustomersReader =
                    cmd.ExecuteReader())
                    {
                        while (CustomersReader.Read())
                        {
                            _loginUser.Id = _balUsers.Id;
                            _loginUser.User_Name = _balUsers.UserName;
                            _loginUser.Password = _balUsers.Password;
                            customers.Add(_loginUser);
                        }
                    }
                }
            }
            return customers;
        }
    }
}
