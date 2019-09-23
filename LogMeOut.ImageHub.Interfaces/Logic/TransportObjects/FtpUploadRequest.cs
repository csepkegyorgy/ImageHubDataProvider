namespace LogMeOut.ImageHub.Interfaces.Logic.TransportObjects
{
    using LogMeOut.ImageHub.Interfaces.Util;

    public class FtpUploadRequest<T>
    {
        public string FileName { get; set; }

        public string FileExtension { get; set; }

        public string Folder { get; set; }

        public T FileContent { get; set; }

        public FtpConnectionInformation FtpInfo { get; set; }
    }
}
