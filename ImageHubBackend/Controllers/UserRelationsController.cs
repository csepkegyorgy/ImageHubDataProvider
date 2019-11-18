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
        [HttpPost]
        public IActionResult Post(Guid sourceUser, Guid targetUser, string type) // type = { followRequest, followAccept, followReject, unfollow }
        {
            if (sourceUser != null && targetUser != null && !string.IsNullOrWhiteSpace(type) && sourceUser != Guid.Empty && targetUser != Guid.Empty)
            {
                if (type == "followRequest")
                {
                    UserRelationHandlerLogic.CreateUserFollowRequestRelation(sourceUser, targetUser);
                }
                else if (type == "followAccept")
                {
                    UserRelationHandlerLogic.AcceptFollowRequestForUser(sourceUser, targetUser);
                }
                else if (type == "followReject")
                {
                    UserRelationHandlerLogic.RejectFollowRequestForUser(sourceUser, targetUser);
                }
                else if (type == "unfollow")
                {
                    UserRelationHandlerLogic.UnfollowUsers(sourceUser, targetUser);
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
