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
    public class AuthenticationController : ControllerBase
    {
        private IAuthenticationLogic AuthenticationLogic;

        public AuthenticationController(IAuthenticationLogic authenticationLogic)
            : base()
        {
            this.AuthenticationLogic = authenticationLogic;
        }

        [EnableCors("MyCorsPolicy")]
        [HttpGet("loginuser")]
        public IActionResult Get(string facebookUserId, string email, string name)
        {
            AuthenticateUserRequest request = new AuthenticateUserRequest()
            {
                FacebookUserId = facebookUserId,
                Email = email,
                UserName = name
            };

            try
            {
                AuthenticateUserResponse response = AuthenticationLogic.AuthenticateUser(request);
                return new JsonResult(response);
            }
            catch (Exception)
            {
                string errorText = $"Internal server error";
                return new JsonResult(new { errors = errorText });
            }
        }
    }
}