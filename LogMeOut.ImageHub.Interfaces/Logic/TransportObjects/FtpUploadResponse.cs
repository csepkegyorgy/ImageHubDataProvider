namespace LogMeOut.ImageHub.Interfaces.Logic.TransportObjects
{
    public class FtpUploadResponse
    {
        public string FileName { get; set; }

        public string FileExtension { get; set; }

        public bool SuccessfulRequest { get; set; }
    }
}
