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
    using LogMeOut.ImageHub.BusinessLogic.Logic.Base;
    using LogMeOut.ImageHub.Repository.Models;

    public class PostSubmitterLogic : BaseLogic, IPostSubmitterLogic
    {
        public PostSubmitterLogic(IBaseLogicDependency baseLogicDependency)
            : base(baseLogicDependency)
        {
        }

        public SubmitPostResponse SubmitPost(SubmitPostRequest request)
        {
            User user = Repository.UserRepository.GetUserById(request.UserId);// DemoDataGenerator.Users.SingleOrDefault(u => u.UserId == request.UserId);

            if (user != null)
            {
                Post post = new Post()
                {
                    Date = DateTime.UtcNow,
                    ImageId = request.ImageId,
                    PostDescription = request.Description,
                    Poster = user,
                    PostId = Guid.NewGuid()
                };
                Repository.PostRepository.AddPost(post);
            }

            return new SubmitPostResponse()
            {
            };
        }
    }
}