namespace LogMeOut.ImageHub.BusinessLogic.Query
{
    using LogMeOut.ImageHub.Interfaces.Entity;
    using LogMeOut.ImageHub.Interfaces.Logic;
    using LogMeOut.ImageHub.Interfaces.Logic.TransportObjects;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class PostQueryLogic : IPostQueryLogic
    {
        public PostsBatchResponse GetUserFeedBatch(PostsBatchRequest request)
        {
            List<PostEntity> allPosts = DemoDataGenerator.Posts.OrderByDescending(p => p.Date).ToList();

            int cursor = 0;
            if (request.LastPostId != null)
            {
                cursor = allPosts.FindIndex(p => p.PostId == request.LastPostId.Value);
            }

            List<PostEntity> skippedPosts = allPosts.Skip(cursor).ToList();
            List<PostEntity> resultPosts = skippedPosts.Take(request.Take).ToList();
            bool hasMoreLoad = false;
            if (allPosts.Count - request.Take > 0)
            {
                hasMoreLoad = true;
            }

            return new PostsBatchResponse()
            {
                Posts = resultPosts,
                HasMoreLoad = hasMoreLoad
            };
        }

        public PostsBatchResponse LoadUserPosts(PostsBatchRequest request)
        {
            List<PostEntity> allPosts = DemoDataGenerator.Posts.Where(p => p.PosterId == request.UserId).OrderByDescending(p => p.Date).ToList();

            int cursor = 0;
            if (request.LastPostId != null)
            {
                cursor = allPosts.FindIndex(p => p.PostId == request.LastPostId.Value);
            }

            List<PostEntity> skippedPosts = allPosts.Skip(cursor).ToList();
            List<PostEntity> resultPosts = skippedPosts.Take(request.Take).ToList();
            bool hasMoreLoad = false;
            if (allPosts.Count - request.Take > 0)
            {
                hasMoreLoad = true;
            }

            return new PostsBatchResponse()
            {
                Posts = resultPosts,
                HasMoreLoad = hasMoreLoad
            };
        }
    }
}
