using ShardaMotorsWebAPI.App_Data.BAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShardaMotorsWebAPI.Models
{
    public class EmailTempResponse
    {
        public List<BAL_EmailTemplate> data { get; set; }
        public string Message { get; set; }

        public EmailTempResponse(string message, List<BAL_EmailTemplate> data)//bool status, 
        {
            data = data.ToList();
            Message = message;
        }
    }
}