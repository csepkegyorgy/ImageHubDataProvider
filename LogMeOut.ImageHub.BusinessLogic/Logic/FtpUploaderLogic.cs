namespace LogMeOut.ImageHub.BusinessLogic.Logic
{
    using LogMeOut.ImageHub.Interfaces.Logic;
    using LogMeOut.ImageHub.Interfaces.Logic.TransportObjects;
    using System.IO;
    using System.Net;

    public class FtpUploaderLogic : IFtpUploaderLogic
    {
        public FtpUploadResponse UploadImage(FtpUploadRequest<byte[]> request)
        {
            string resultFileName = request.FileName + "." + request.FileExtension;
            string uri = $"ftp://{request.FtpInfo.Host}:{request.FtpInfo.Port}/";
            if (string.IsNullOrWhiteSpace(request.Folder))
            {
                uri += resultFileName;
            }
            else
            {
                uri += $"{request.Folder}/{resultFileName}";
            }

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
                FileName = resultFileName
            };
        }

        public FtpUploadResponse UploadText(FtpUploadRequest<string> request)
        {
            throw new System.NotImplementedException();
        }
    }
}
