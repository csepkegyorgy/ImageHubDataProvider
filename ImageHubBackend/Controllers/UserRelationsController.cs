namespace LogMeOut.ImageHub.DataProvider.Controllers
{
    using LogMeOut.ImageHub.Interfaces.Logic;
    using Microsoft.AspNetCore.Cors;
    using Microsoft.AspNetCore.Mvc;
    using System;

    [Route("api/[controller]")]
    [ApiController]
    public class UserRelationsController : ControllerBase
    {
        private IUserRelationHandlerLogic UserRelationHandlerLogic;

        public UserRelationsController(IUserRelationHandlerLogic userRelationHandlerLogic, IFtpUploaderLogic ftpUploaderLogic)
            : base()
        {
            this.UserRelationHandlerLogic = userRelationHandlerLogic;
        }

        [EnableCors("MyCorsPolicy")]
        [HttpGet]
        public IActionResult Get(Guid userId, Guid targetUserId)
        {
            if (userId != null && targetUserId != null && userId != Guid.Empty && targetUserId != Guid.Empty)
            {
                var logicResponse = UserRelationHandlerLogic.GetUserRelationForUser(userId, targetUserId);
                return new JsonResult(logicResponse);
            }

            return new JsonResult(new { success = "false", errors = new string[] { "Invalid parameters provided." } });
        }

        [EnableCors("MyCorsPolicy")]
        [HttpPost]
        public IActionResult Post([FromBody] Guid userId, [FromBody] Guid targetUserId, [FromBody] string type) // type = { followRequest, followAccept, followReject, unfollow }
        {
            if (userId != null && targetUserId != null && !string.IsNullOrWhiteSpace(type) && userId != Guid.Empty && targetUserId != Guid.Empty)
            {
                if (type == "followRequest")
                {
                    UserRelationHandlerLogic.CreateUserFollowRequestRelation(userId, targetUserId);
                }
                else if (type == "followAccept")
                {
                    UserRelationHandlerLogic.AcceptFollowRequestForUser(userId, targetUserId);
                }
                else if (type == "followReject")
                {
                    UserRelationHandlerLogic.RejectFollowRequestForUser(userId, targetUserId);
                }
                else if (type == "unfollow")
                {
                    UserRelationHandlerLogic.UnfollowUsers(userId, targetUserId);
                }
                else
                {
                    return new JsonResult(new { success = "false", errors = new string[] { "Invalid type provided." } });
                }
                return new JsonResult(new { success = "true" });
            }
            return new JsonResult(new { success = "false", errors = new string[] { "Invalid or missing parameters provided." } });
        }
    }
}
