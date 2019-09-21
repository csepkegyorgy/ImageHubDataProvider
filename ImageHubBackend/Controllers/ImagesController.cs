namespace LogMeOut.ImageHub.DataProvider.Controllers
{
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using LogMeOut.ImageHub.Interfaces;
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
            FtpDownloadResponse<byte[]> response = FtpDownloaderLogic.DownloadImage(request);
            
            if (response.SuccessfulRequest)
            {     
                return File(response.FileContent, "image/jpeg");
            }
            else
            {
                return NotFound();
            }
        }
    }
}