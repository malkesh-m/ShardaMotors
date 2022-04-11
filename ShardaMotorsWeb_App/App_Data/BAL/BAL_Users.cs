using ShardaMotorsWeb_App.App_Data.BEL;
using ShardaMotorsWeb_App.App_Data.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShardaMotorsWeb_App.App_Data.BAL
{
    public class BAL_Users
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        public List<BAL_Users> _lstLoginUser { get; set; }
        DAL_Users _DalUsers = new DAL_Users();
        public List<BAL_Users> SelectUserById(int id)
        {
            _lstLoginUser = _DalUsers.SelectUserById(id).ToList();
            return _lstLoginUser;
        }
        public bool ChangePassword(BEL_Users objBel)
        {
            return _DalUsers.ChangePassword(objBel);
        }
    }
}
