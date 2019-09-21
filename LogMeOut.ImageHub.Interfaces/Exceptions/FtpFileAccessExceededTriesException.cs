namespace LogMeOut.ImageHub.Interfaces.Exceptions
{
    using System;

    public class FtpFileAccessExceededTriesException : Exception
    {
        public FtpFileAccessExceededTriesException(string fileName)
            : base($"{fileName} was busy or unavailable and exceeded access tries threshold.")
        {
        }
    }
}
