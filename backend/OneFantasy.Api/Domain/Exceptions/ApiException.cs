using System;

namespace OneFantasy.Api.Domain.Exceptions
{
    public abstract class ApiException : Exception
    {
        public int StatusCode { get; }
        public string Title { get; }

        protected ApiException(string title, string message, int statusCode)
            : base(message)
        {
            Title = title;
            StatusCode = statusCode;
        }
    }
}
