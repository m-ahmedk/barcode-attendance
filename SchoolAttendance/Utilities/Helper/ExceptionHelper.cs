using System;
using System.Diagnostics;

namespace SchoolAttendance.Utilities.Helper
{
    public static class ExceptionHelper
    {
        public static string GetExceptionDetails(Exception ex)
        {
            var st = new StackTrace(ex, true);
            var frame = st.GetFrame(0);
            int line = frame.GetFileLineNumber();
            string fileName = frame.GetFileName();
            string method = frame.GetMethod().Name;

            return $"Exception occurred in {fileName}, method: {method}, line: {line}. Message: {ex.Message}";
        }
    }
}