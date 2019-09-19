namespace LogMeOut.ImageHub.BusinessLogic.Logic
{
    using LogMeOut.ImageHub.Interfaces.Logic;
    using LogMeOut.ImageHub.Interfaces.Logic.TransportObjects;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net;

    public class FtpDownloaderLogic : IFtpDownloaderLogic
    {
        public FtpDownloadResponse<byte[]> DownloadImage(FtpDownloadRequest request)
        {
            string uri = $"ftp://{request.FtpInfo.Host}:{request.FtpInfo.Port}/{request.FileName}.{request.FileExtension}";

            FtpWebRequest ftpWebRequest = (FtpWebRequest)FtpWebRequest.Create(uri);
            ftpWebRequest.Credentials = new NetworkCredential(request.FtpInfo.UserName, request.FtpInfo.Password);
            ftpWebRequest.KeepAlive = false;
            ftpWebRequest.UseBinary = true;
            ftpWebRequest.EnableSsl = request.FtpInfo.EnableSsl;
            ftpWebRequest.Method = WebRequestMethods.Ftp.DownloadFile;

            byte[] fileContent = null;
            using (Stream stream = ftpWebRequest.GetResponse().GetResponseStream())
            {
                List<byte> fileContentList = new List<byte>();

                byte[] buffer = new byte[102400];
                int bytes = 0;
                do
                {
                    bytes = stream.Read(buffer, 0, buffer.Length);
                    for (int i = 0; i < bytes; i++)
                    {
                        fileContentList.Add(buffer[i]);
                    }
                } while (bytes != 0);

                fileContent = fileContentList.ToArray();
            }

            return new FtpDownloadResponse<byte[]>()
            {
                FileContent = fileContent,
                FileName = request.FileName + "." + request.FileExtension,
                SuccessfulRequest = true
            };
        }

        public FtpDownloadResponse<string> DownloadText(FtpDownloadRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
