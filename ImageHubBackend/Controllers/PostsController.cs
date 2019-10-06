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
    public class PostsController : ControllerBase
    {
        private IPostQueryLogic PostQueryLogic;
        
        public PostsController(IPostQueryLogic postQueryLogic)
            : base()
        {
            this.PostQueryLogic = postQueryLogic;
        }

        [EnableCors("MyCorsPolicy")]
        [HttpGet("listposts")]
        public IActionResult Get(Guid userId, int take, string type, Guid? lastPostId)
        {
            PostsBatchRequest request = new PostsBatchRequest()
            {
                UserId = userId,
                Take = take,
                LastPostId = lastPostId
            };

            PostsBatchResponse response = null;
            try
            {
                if (type == "userfeed")
                {
                    response = PostQueryLogic.GetUserFeedBatch(request);
                }
                else if (type == "usersite")
                {
                    response = PostQueryLogic.LoadUserPosts(request);
                }
                else
                {
                    string errorText = $"Invalid request type";
                    return new JsonResult(new { error = errorText, userId = userId });
                }
                return new JsonResult(response);
            }
            catch (Exception)
            {
                string errorText = $"Internal server error";
                return new JsonResult(new { error = errorText, userId = userId });
            }
        }
    }
}