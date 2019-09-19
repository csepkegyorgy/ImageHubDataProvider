namespace LogMeOut.ImageHub.Interfaces.Logic
{
    using LogMeOut.ImageHub.Interfaces.Logic.TransportObjects;

    public interface IFtpUploaderLogic
    {
        FtpUploadResponse UploadImage(FtpUploadRequest<byte[]> request);

        FtpUploadResponse UploadText(FtpUploadRequest<string> request);
    }
}
