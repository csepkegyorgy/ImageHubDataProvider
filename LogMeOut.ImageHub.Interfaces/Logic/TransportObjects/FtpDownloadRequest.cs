namespace LogMeOut.ImageHub.Interfaces.Logic.TransportObjects
{
    using LogMeOut.ImageHub.Interfaces.Util;

    public class FtpDownloadRequest
    {
        public string FileName { get; set; }

        public string FileExtension { get; set; }

        public FtpConnectionInformation FtpInfo { get; set; }
    }
}
