namespace LogMeOut.ImageHub.BusinessLogic.Logic
{
    using LogMeOut.ImageHub.Interfaces;
    using LogMeOut.ImageHub.Interfaces.Exceptions;
    using LogMeOut.ImageHub.Interfaces.Logic;
    using LogMeOut.ImageHub.Interfaces.Logic.TransportObjects;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net;
    using System.Threading;

    public class FtpDownloaderLogic : IFtpDownloaderLogic
    {
        public FtpDownloadResponse<byte[]> DownloadImage(FtpDownloadRequest request)
        {
            if (request.FtpInfo == null)
            {
                throw new InvalidFtpConnectionInformationException(request.FtpInfo);
            }

            string uri = $"ftp://{request.FtpInfo.Host}:{request.FtpInfo.Port}/{request.FileName}";

            FtpWebRequest ftpWebRequest = (FtpWebRequest)FtpWebRequest.Create(uri);
            ftpWebRequest.Credentials = new NetworkCredential(request.FtpInfo.UserName, request.FtpInfo.Password);
            ftpWebRequest.KeepAlive = false;
            ftpWebRequest.UseBinary = true;
            ftpWebRequest.EnableSsl = request.FtpInfo.EnableSsl;
            ftpWebRequest.Method = WebRequestMethods.Ftp.DownloadFile;

            byte[] fileContent = null;
            try
            {
                using (Stream stream = GetStream(ftpWebRequest, request.FileName))
                {
                    List<byte> fileContentList = new List<byte>();

                    byte[] buffer = new byte[102400];
                    int bytes = 0;
                    do
                    {
                        bytes = stream.Read(buffer, 0, buffer.Length);
                        fileContentList.AddRange(buffer);
                    } while (bytes != 0);

                    fileContent = fileContentList.ToArray();
                }
            }
            catch (Exception e)
            {
                if (e is WebException)
                {
                    HandleFatalWebException(e as WebException);
                }
            }

            return new FtpDownloadResponse<byte[]>()
            {
                FileContent = fileContent,
                FileName = request.FileName,
                SuccessfulRequest = true
            };
        }

        public FtpDownloadResponse<string> DownloadText(FtpDownloadRequest request)
        {
            throw new NotImplementedException();
        }

        private Stream GetStream(FtpWebRequest ftpWebRequest, string fileName)
        {
            int tries = AppConfig.FileAccessTries;
            Stream stream = null;

            while (tries > 0 && stream == null)
            {
                try
                {
                    stream = ftpWebRequest.GetResponse().GetResponseStream();
                }
                catch (Exception e)
                {
                    if ((e is WebException) &&
                       (((e as WebException).Response as FtpWebResponse).StatusCode == FtpStatusCode.ActionNotTakenFileUnavailableOrBusy))
                    {
                        tries--;
                        if (tries == 0)
                        {
                            throw new FtpFileAccessExceededTriesException(fileName);
                        }
                        Thread.Sleep(AppConfig.FileAccessSleepTime);
                    }
                    else
                    {
                        throw;
                    }
                }

            }
            return stream;
        }

        private void HandleFatalWebException(WebException e)
        {
            switch ((e.Response as FtpWebResponse).StatusCode)
            {
                case FtpStatusCode.ActionNotTakenFileUnavailable:
                    throw new InvalidFtpFileAccessException(e);
                default:
                    throw new UnknownException(e);
            }
        }
    }
}
