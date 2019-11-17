namespace LogMeOut.ImageHub.BusinessLogic.Logic.Base
{
    using LogMeOut.ImageHub.Interfaces.Repository;
    using LogMeOut.ImageHub.Interfaces.Util;
    using System.IO;
    using System.Net;

    public abstract class BaseLogic
    {
        protected IBaseLogicDependency BaseLogicDependency;

        protected IImageHubRepository Repository => BaseLogicDependency.Repository;

        protected FtpConnectionInformation FtpInfo => BaseLogicDependency.FtpConnectionInformation;

        public BaseLogic(IBaseLogicDependency baseLogicDependency)
        {
            this.BaseLogicDependency = baseLogicDependency;
        }

        protected void UploadImage(byte[] fileContent, string uri)
        {
            FtpWebRequest ftpWebRequest = (FtpWebRequest)FtpWebRequest.Create(uri);
            ftpWebRequest.Credentials = new NetworkCredential(FtpInfo.UserName, FtpInfo.Password);
            ftpWebRequest.KeepAlive = false;
            ftpWebRequest.UseBinary = true;
            ftpWebRequest.EnableSsl = FtpInfo.EnableSsl;
            ftpWebRequest.Method = WebRequestMethods.Ftp.UploadFile;

            using (Stream stream = ftpWebRequest.GetRequestStream())
            {
                stream.Write(fileContent, 0, fileContent.Length);
            }
        }

        protected byte[] DownloadImageFromPublicUrl(string url)
        {
            byte[] fileContent;

            using (WebClient webClient = new WebClient())
            {
                fileContent = webClient.DownloadData(url);
            }

            return fileContent;
        }
    }
}
