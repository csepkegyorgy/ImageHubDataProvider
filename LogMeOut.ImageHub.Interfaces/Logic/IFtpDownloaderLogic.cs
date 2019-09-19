namespace LogMeOut.ImageHub.Interfaces.Logic
{
    using LogMeOut.ImageHub.Interfaces.Logic.TransportObjects;

    public interface IFtpDownloaderLogic
    {
        FtpDownloadResponse<byte[]> DownloadImage(FtpDownloadRequest request);

        FtpDownloadResponse<string> DownloadText(FtpDownloadRequest request);
    }
}
