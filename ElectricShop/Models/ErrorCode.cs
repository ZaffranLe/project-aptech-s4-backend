using System;
using System.Collections.Generic;

namespace ElectricShop.Models
{
    public class RequestErrorCode : IDisposable
    {
        public RequestErrorCode(bool isSuccess, string errorCode = null, string  errorMess = null)
        {
            IsSuccess = isSuccess;
            ErrorCode = errorCode;
            ErrorMsg = errorMess;
            ListDataResult = new List<object>();
        }
        public bool IsSuccess { get; set; }
        public string ErrorCode { get; set; }
        public string ErrorMsg { get; set; }
        public List<object> ListDataResult;

        public void Dispose()
        {
        }
    }
}