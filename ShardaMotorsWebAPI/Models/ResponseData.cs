using ShardaMotorsWebAPI.App_Data.BAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShardaMotorsWebAPI.Model
{
    public class ResponseData
    {
        public List<BAL_SMTPSettings> data { get; set; }
        public string Message { get; set; }

        public ResponseData(string message, List<BAL_SMTPSettings> data)//bool status, 
        {           
            data = data.ToList();
            Message = message;
        }
    }
}
