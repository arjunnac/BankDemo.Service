using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankDemo.Common
{
    public class ResponseMessage
    {
        public ResponseMessage(bool status, string msg)
        {
            this.Status = status;
            this.Message = msg;
        }

        public bool Status { get; }
        public string Message { get; }
    }
}
