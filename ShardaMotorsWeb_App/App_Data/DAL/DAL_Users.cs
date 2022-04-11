using ShardaMotorsWeb_App.App_Data.BAL;
using ShardaMotorsWeb_App.App_Data.BEL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShardaMotorsWeb_App.App_Data.DAL
{
   public class DAL_Users: DALBase
    {
        public List<BAL_Users> SelectUserById(int id)
        {
            //if 0 then select all else by id
            try
            {
                List<BAL_Users> UserList = new List<BAL_Users>();
                AddParameter("@Id", SqlDbType.Int, id);
                DataSet ds = ExecuteForData("Sp_Select_Users");
                foreach (DataRow users in ds.Tables[0].Rows)
                {
                    BAL_Users userDetail = new BAL_Users();
                    userDetail.Id = Convert.ToInt32(users["Id"]);
                    userDetail.UserName = Convert.ToString(users["UserName"]);
                    userDetail.Password = Convert.ToString(users["Password"]);
                   
                    UserList.Add(userDetail);
                }
                return UserList;
            }
            catch (Exception ex)
            {
                return null;
            }
            finally { _dataConnection.Close(); }
        }

        public bool ChangePassword(BEL_Users _BelUsers)
        {

            try
            {
                AddParameter("@Password", SqlDbType.VarChar, _BelUsers.Password);
                AddParameter("@Id", SqlDbType.VarChar, _BelUsers.Id);

                ExecuteScalar("Sp_Update_ChangePassword");

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
    }
}
