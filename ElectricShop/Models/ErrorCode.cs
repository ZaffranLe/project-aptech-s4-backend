using System;

namespace ElectricShop.Models
{
    public class TokenErrorCode : IDisposable
    {
        public string error { get; set; }
        public string error_description { get; set; }

        public void Dispose()
        {
        }
    }

    public class RequestErrorCode : IDisposable
    {
        public bool IsSuccess { get; set; }
        public int ErrorCode { get; set; }
        public string ErrorMsg { get; set; }
        public int SeqResult { get; set; }

        public void Dispose()
        {
        }
    }
}