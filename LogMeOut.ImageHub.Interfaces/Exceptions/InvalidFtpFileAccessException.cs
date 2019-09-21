namespace LogMeOut.ImageHub.Interfaces.Exceptions
{
    using System;

    public class InvalidFtpFileAccessException : Exception
    {
        public string FileName { get; set; }

        public InvalidFtpFileAccessException(string fileName)
            : base($"No resource exists corresponding the provided resource identifier [{fileName}].")
        {
            this.FileName = fileName;
        }
    }
}
