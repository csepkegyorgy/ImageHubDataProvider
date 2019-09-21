namespace LogMeOut.ImageHub.Interfaces.Exceptions
{
    using LogMeOut.ImageHub.Interfaces.Util;
    using System;

    public class InvalidFtpConnectionInformationException : Exception
    {
        public InvalidFtpConnectionInformationException(FtpConnectionInformation ftpInfo)
            : base(ftpInfo == null ? $"No Ftp connection information provide." : "Invalid Ftp connection information provided.")
        {
        }
    }
}
