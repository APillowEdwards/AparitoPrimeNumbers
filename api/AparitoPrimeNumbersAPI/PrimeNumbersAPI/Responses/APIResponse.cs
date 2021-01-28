using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrimeNumbersAPI.Responses
{
    public class APIResponse<T>
    {
        public T Result { get; set; }
        public string Status { get; set; }
        public int Code { get; set; }
        public string Message { get; set; }

        private APIResponse(T result, string status, int code, string message)
        {
            Result = result;
            Status = status;
            Code = code;
            Message = message;
        }

        public static APIResponse<T> OkResponse(T result)
        {
            return OkResponse(result, "");
        }

        public static APIResponse<T> OkResponse(T result, string message)
        {
            return new APIResponse<T>(result, "Ok", 200, message);
        }

        public static APIResponse<T> BadRequestResponse(string message)
        {
            return new APIResponse<T>(default, "BadRequest", 400, message);
        }

        public static APIResponse<T> NotFoundResponse(string message)
        {
            return new APIResponse<T>(default, "Not Found", 404, message);
        }

        public static APIResponse<T> UnhandledExceptionResponse()
        {
            string message = "Unhandled Exception";

            return new APIResponse<T>(default, "Internal Server Error", 500, message);
        }
    }
}
