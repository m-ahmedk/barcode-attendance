using System.Collections.Generic;

namespace SchoolAttendance.Data.Response
{
    public class AppResponse<T> where T : class
    {
        public bool? Success;
        public bool? HasError;
        public string? Message;
        public T? Data { get; set; }
        public IEnumerable<string>? Errors;
  
        public AppResponse()
        {
            HasError = false;
            Success = true;
        }

        public AppResponse(T? data, string? message = null)
        {
            Data = data;
            HasError = false;
            Success = true;
            Message = message;
            Errors = null;
        }

        public AppResponse(IEnumerable<string>? errors, bool success, string message = null)
        {
            Data = null;
            HasError = true;
            Success = success;
            Message = message;
            Errors = errors ?? new List<string>();
        }
    }
}
