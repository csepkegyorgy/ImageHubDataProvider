namespace LogMeOut.ImageHub.Interfaces.Exceptions
{
    using System;

    public class InvalidFtpFileAccessException : Exception
    {
        public Exception OriginalException { get; set; }

        public InvalidFtpFileAccessException(Exception exception)
            : base($"The provided information was invalid to access the resource.")
        {
            this.OriginalException = exception;
        }
    }
}
