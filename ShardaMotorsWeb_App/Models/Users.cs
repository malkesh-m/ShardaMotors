using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShardaMotorsWeb_App.Models
{
    public class Users
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Confirm_Password { get; set; }
    }
}