namespace LogMeOut.ImageHub.DataProvider.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using LogMeOut.ImageHub.Interfaces;
    using LogMeOut.ImageHub.Interfaces.Exceptions;
    using LogMeOut.ImageHub.Interfaces.Logic;
    using LogMeOut.ImageHub.Interfaces.Logic.TransportObjects;
    using LogMeOut.ImageHub.Interfaces.Util;
    using Microsoft.AspNetCore.Cors;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private IFtpDownloaderLogic FtpDownloaderLogic;
        private IFtpUploaderLogic FtpUploaderLogic;

        public ImagesController(IFtpDownloaderLogic ftpDownloaderLogic, IFtpUploaderLogic ftpUploaderLogic)
            : base()
        {
            this.FtpDownloaderLogic = ftpDownloaderLogic;
            this.FtpUploaderLogic = ftpUploaderLogic;
        }

        [EnableCors("MyCorsPolicy")]
        [HttpGet]
        public IActionResult Get(string id, string type)
        {
            FtpDownloadRequest request = new FtpDownloadRequest()
            {
                FileName = id,
                Folder = ImageTypeFolderResolver[type == null ? "" : type],
                FtpInfo = AppConfig.GetFtpConnectionInformation()
            };

            FtpDownloadResponse<byte[]> response = null;
            try
            {
                response = FtpDownloaderLogic.DownloadImage(request);
                return File(response.FileContent, "image/jpeg");
            }
            catch (Exception e)
            {
                string errorText = CreateErrorTextForFileQueryException(e);
                return new JsonResult(new { error = errorText, fileName = id });
            }
        }

        [EnableCors("MyCorsPolicy")]
        [HttpPost]
        public IActionResult UploadImage([FromForm] IFormFile file, [FromForm] Guid userId, [FromForm] string type)
        {
            IEnumerable<string> errors = CheckFileUploadRequestForErrors(file, userId, type);
            if (errors.Any())
            {
                return new JsonResult(new { success = false, errors = errors });
            }

            FtpUploadRequest<byte[]> request = new FtpUploadRequest<byte[]>()
            {
                FileContent = ReadFileStreamForFileContent(file.OpenReadStream()),
                FileName = GetFileNameFromUserAndDateForUploadedImage(userId),
                Folder = ImageTypeFolderResolver[type],
                FileExtension = "jpg",
                FtpInfo = AppConfig.GetFtpConnectionInformation()
            };

            FtpUploadResponse response = null;
            try
            {
                response = FtpUploaderLogic.UploadImage(request);
                return new JsonResult(new { success = true, filename = response.FileName });
            }
            catch (Exception e)
            {
                return new JsonResult(new { success = false, errors = new string[] { "lazy error text" } });
            }
        }

        private Dictionary<string, string> ImageTypeFolderResolver = new Dictionary<string, string>()
        {
            { "post", "posts" },
            { "profile", "profileicons" },
            { "", "" }
        };

        private string GetFileNameFromUserAndDateForUploadedImage(Guid userId)
        {
            string resultFileName = "";

            resultFileName += userId.ToString() + "_";

            DateTime now = DateTime.Now;
            resultFileName +=
                now.Year.ToString() +
                FormatDateElement(now.Month.ToString()) +
                FormatDateElement(now.Day.ToString()) +
                FormatDateElement(now.Hour.ToString()) +
                FormatDateElement(now.Minute.ToString()) +
                FormatDateElement(now.Second.ToString());

            return resultFileName;
        }

        private string FormatDateElement(string dateElement)
        {
            if (dateElement.Length == 1)
            {
                dateElement = "0" + dateElement;
            }
            return dateElement;
        }

        private byte[] ReadFileStreamForFileContent(Stream fileStream)
        {
            List<byte> result = new List<byte>();

            int currentByte = 0;
            while (currentByte != -1)
            {
                currentByte = fileStream.ReadByte();
                if (currentByte != -1)
                {
                    result.Add((byte)currentByte);
                }
            }

            fileStream.Close();
            return result.ToArray();
        }

        private IEnumerable<string> CheckFileUploadRequestForErrors(IFormFile file, Guid userId, string type)
        {
            List<string> errors = new List<string>();

            if (file == null || file.Length == 0)
            {
                errors.Add("No file was found in the request.");
            }
            if (userId == null || userId == Guid.Empty)
            {
                errors.Add("Invalid or no userId provided.");
            }
            if (string.IsNullOrWhiteSpace(type))
            {
                errors.Add("No type provided.");
            }
            else
            {
                if (type != "post" && type != "profile")
                {
                    errors.Add("Invalid type provided. Uploading to the root folder of the ftp server is only possible through manual actions.");
                }
            }

            return errors;
        }

        private string CreateErrorTextForFileQueryException(Exception exception)
        {
            string errorText = null;

            if (exception is FtpFileAccessExceededTriesException)
            {
                errorText = $"Could not load image resource [{(exception as FtpFileAccessExceededTriesException).FileName}]. Resource was either unavailable or busy.";
            }
            else if (exception is UnknownException)
            {
                errorText = $"An unexpected error has happened.";
            }
            else if (exception is InvalidFtpFileAccessException)
            {
                errorText = $"The requested resource [{(exception as InvalidFtpFileAccessException).FileName}] does not exist on the storage server.";
            }
            else
            {
                errorText = $"Internal server error.";
            }

            return errorText;
        }
    }
}