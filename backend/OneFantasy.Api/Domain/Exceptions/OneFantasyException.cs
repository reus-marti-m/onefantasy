using System;

namespace OneFantasy.Api.Domain.Exceptions
{
    public class OneFantasyException : Exception
    {

        public int StatusCode { get; }
        public string Title { get; }

        protected OneFantasyException(string title, string message, int statusCode)
            : base(message)
        {
            Title = title;
            StatusCode = statusCode;
        }

    }
}
