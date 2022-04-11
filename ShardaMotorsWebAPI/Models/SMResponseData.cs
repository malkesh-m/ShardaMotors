using ShardaMotorsWebAPI.App_Data.BAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShardaMotorsWebAPI.Models
{
    public class SMResponseData
    {
        public List<BAL_ShardaMotorsDetails> data { get; set; }
        public string Message { get; set; }

        public SMResponseData(string message, List<BAL_ShardaMotorsDetails> data)//bool status, 
        {
            data = data.ToList();
            Message = message;
        }
    }

}