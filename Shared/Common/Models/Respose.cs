using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Common.Models
{
    public static class Response
    {
        public static Response<T> Fail<T>(string? message, T? data = default) =>
            new Response<T>(false, message, data);
        public static Response<T> Ok<T>(T data, string? message = null) =>
            new Response<T>(true, message, data);
        public static Response<T> Error<T>(string message, string[] errors) =>
           new Response<T>(message, errors);
        public static Response<T> Error<T>(string message, string error) =>
           new Response<T>(message, new string[] { error });
        public static Response<T> Error<T>(string message, Exception exception) =>
           new Response<T>(message, new string[] { exception.Message, exception.InnerException?.Message });
    }
    public class Response<T>
    {
        public Response(bool success, string? message, T? data)
        {
            Success = success;
            Message = message;
            Data = data;
        }

        public Response(string message, string[] errors)
        {
            Message = message;
            Errors = errors;
            Success = false;
        }

        public bool Success { get; set; }
        public string? Message { get; set; }
        public T? Data { get; set; }
        public string[]? Errors { get; set; }
    }
}
