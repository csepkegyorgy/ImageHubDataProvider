namespace LogMeOut.ImageHub.Interfaces.Logic.TransportObjects
{
    public class FtpDownloadResponse<T>
    {
        public string FileName { get; set; }

        public T FileContent { get; set; }
    }
}
