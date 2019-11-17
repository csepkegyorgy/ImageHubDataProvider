namespace LogMeOut.ImageHub.BusinessLogic.Logic
{
    using LogMeOut.ImageHub.BusinessLogic.Logic.Base;
    using LogMeOut.ImageHub.Interfaces.Logic;
    using LogMeOut.ImageHub.Interfaces.Logic.TransportObjects;
    using System.IO;
    using System.Net;

    public class FtpUploaderLogic : BaseLogic, IFtpUploaderLogic
    {
        public FtpUploaderLogic(IBaseLogicDependency baseLogicDependency)
            : base(baseLogicDependency)
        {
        }

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

            base.UploadImage(request.FileContent, uri);

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
