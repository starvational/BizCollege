using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BizCollegeMvc.Models
{
    public class JsonOperationResult
    {
        public ErrorCodes ErrorCode { get; set; }
        public String Message { get; set; }
    } 

    public enum ErrorCodes
    { 
        Success, 
        InvalidData, 
        Fail

    }
}