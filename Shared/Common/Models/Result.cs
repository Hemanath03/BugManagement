using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Common.Models
{
    public class Result
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public string[]? Errors { get; set; }
        public Result()
        {
        }
        public Result(bool succeeded, IEnumerable<string> errors)
        {
            Success = succeeded;
            Errors = errors.ToArray();
            Message = errors?.First();
        }
        public Result(bool success, string message)
        {
            Success = success;
            Message = message;
            Errors = Array.Empty<string>();
        }
        public static Result OnSuccess()
        {
            return new Result(true, Array.Empty<string>());
        }
        public static Result OnSuccess(string message)
        {
            return new Result(true, message);
        }

        public static Result OnFailure(IEnumerable<string> errors)
        {
            return new Result(false, errors);
        }
        public static Result OnFailure(string message)
        {
            return new Result(false, message);
        }
    }
}
