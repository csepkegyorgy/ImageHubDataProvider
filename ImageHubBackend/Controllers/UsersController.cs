﻿namespace LogMeOut.ImageHub.DataProvider.Controllers
{
    using LogMeOut.ImageHub.Interfaces.Logic;
    using LogMeOut.ImageHub.Interfaces.Logic.TransportObjects;
    using Microsoft.AspNetCore.Cors;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    [ApiController]
    public class UsersController
    {
        private IUserSearcherLogic UserSearcherLogic;

        public UsersController(IUserSearcherLogic userSearcherLogic)
            : base()
        {
            this.UserSearcherLogic = userSearcherLogic;
        }

        [EnableCors("MyCorsPolicy")]
        [HttpGet]
        public IActionResult Get(string partialUserName)
        {
            if (!string.IsNullOrWhiteSpace(partialUserName))
            {
                UserSearchResult logicResult = UserSearcherLogic.SearchUsersByPartialUserName(partialUserName.ToLower());
                return new JsonResult(logicResult);
            }
            else
            {
                return new JsonResult(new { success = false, errors = new string[] { "No search string was provided." } });
            }
        }
    }
}
