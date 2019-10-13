namespace LogMeOut.ImageHub.BusinessLogic.Logic
{
    using LogMeOut.ImageHub.Interfaces;
    using LogMeOut.ImageHub.Interfaces.Exceptions;
    using LogMeOut.ImageHub.Interfaces.Logic;
    using LogMeOut.ImageHub.Interfaces.Logic.TransportObjects;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net;
    using System.Threading;
    using System.Linq;
    using LogMeOut.ImageHub.Interfaces.Entity;

    public class PostSubmitterLogic : IPostSubmitterLogic
    {
        public SubmitPostResponse SubmitPost(SubmitPostRequest request)
        {
            UserEntity user = DemoDataGenerator.Users.SingleOrDefault(u => u.UserId == request.UserId);

            PostEntity post = new PostEntity()
            {
                Date = DateTime.Now,
                ImageId = request.ImageId,
                HubtasticCount = 0,
                IsHubbedByCurrentUser = false,
                PostDescription = request.Description,
                PosterId = user.UserId,
                PosterName = user.Name,
                PosterProfileIconId = user.ProfileImageId,
                PostId = Guid.NewGuid()
            };
            DemoDataGenerator.AddPost(post);

            return new SubmitPostResponse()
            {

            };
        }
    }
}