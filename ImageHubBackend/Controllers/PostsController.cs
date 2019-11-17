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
        private IPostSubmitterLogic PostSubmitterLogic;

        public PostsController(IPostQueryLogic postQueryLogic, IPostSubmitterLogic postSubmitterLogic)
            : base()
        {
            this.PostQueryLogic = postQueryLogic;
            this.PostSubmitterLogic = postSubmitterLogic;
        }

        [EnableCors("MyCorsPolicy")]
        [HttpGet("listposts")]
        public IActionResult Get(Guid userId, Guid loggedInUserId, int take, string type, Guid? lastPostId)
        {
            PostsBatchRequest request = new PostsBatchRequest()
            {
                UserId = userId,
                Take = take,
                LastPostId = lastPostId,
                LoggedInUserId = loggedInUserId
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

        [EnableCors("MyCorsPolicy")]
        [HttpPost]
        public IActionResult Post([FromForm] Guid userId, [FromForm] string imageId, [FromForm] string description)
        {
            SubmitPostResponse response = null;
            try
            {
                response = this.PostSubmitterLogic.SubmitPost(new SubmitPostRequest()
                {
                    UserId = userId,
                    Description = description,
                    ImageId = imageId
                });
                return new JsonResult(new { success = true });
            }
            catch (Exception e)
            {
                return new JsonResult(new { success = false, errors = new string[] { "lazy error text" } });
            }
        }
    }
}