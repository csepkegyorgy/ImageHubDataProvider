namespace LogMeOut.ImageHub.BusinessLogic.Logic
{
    using LogMeOut.ImageHub.Interfaces.Logic;
    using LogMeOut.ImageHub.Interfaces.Logic.TransportObjects;
    using System.IO;
    using System.Net;

    class FtpUploaderLogic : IFtpUploaderLogic
    {
        public FtpUploadResponse UploadImage(FtpUploadRequest<byte[]> request)
        {
            string uri = $"ftp://{request.FtpInfo.Host}:{request.FtpInfo.Port}/{request.FileName}.{request.FileExtension}";

            FtpWebRequest ftpWebRequest = (FtpWebRequest)FtpWebRequest.Create(uri);
            ftpWebRequest.Credentials = new NetworkCredential(request.FtpInfo.UserName, request.FtpInfo.Password);
            ftpWebRequest.KeepAlive = false;
            ftpWebRequest.UseBinary = true;
            ftpWebRequest.EnableSsl = request.FtpInfo.EnableSsl;
            ftpWebRequest.Method = WebRequestMethods.Ftp.UploadFile;

            using (Stream stream = ftpWebRequest.GetRequestStream())
            {
                stream.Write(request.FileContent, 0, request.FileContent.Length);
            }

            return new FtpUploadResponse()
            {
                SuccessfulRequest = true
            };
        }

        public FtpUploadResponse UploadText(FtpUploadRequest<string> request)
        {
            throw new System.NotImplementedException();
        }
    }
}
