namespace LogMeOut.ImageHub.Interfaces.Exceptions
{
    using System;

    public class UnknownException : Exception
    {
        public Exception OriginalException { get; set; }

        public UnknownException(Exception exception)
            : base($"An unexpected error has happened.")
        {
            this.OriginalException = exception;
        }
    }
}
