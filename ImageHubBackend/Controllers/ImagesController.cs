namespace LogMeOut.ImageHub.DataProvider.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using LogMeOut.ImageHub.Interfaces;
    using LogMeOut.ImageHub.Interfaces.Exceptions;
    using LogMeOut.ImageHub.Interfaces.Logic;
    using LogMeOut.ImageHub.Interfaces.Logic.TransportObjects;
    using LogMeOut.ImageHub.Interfaces.Util;
    using Microsoft.AspNetCore.Cors;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private IFtpDownloaderLogic FtpDownloaderLogic;
        
        public ImagesController(IFtpDownloaderLogic ftpDownloaderLogic)
            : base()
        {
            this.FtpDownloaderLogic = ftpDownloaderLogic;
        }

        [EnableCors("MyCorsPolicy")]
        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            FtpDownloadRequest request = new FtpDownloadRequest()
            {
                FileName = id,
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
                string errorText = CreateErrorTextForException(e);

                return new JsonResult(new { error = errorText, fileName = id });
            }
        }

        private string CreateErrorTextForException(Exception exception)
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