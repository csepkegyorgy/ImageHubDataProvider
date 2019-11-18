namespace LogMeOut.ImageHub.BusinessLogic.Query
{
    using LogMeOut.ImageHub.BusinessLogic.Logic.Base;
    using LogMeOut.ImageHub.Interfaces.Entity;
    using LogMeOut.ImageHub.Interfaces.Logic;
    using LogMeOut.ImageHub.Interfaces.Logic.TransportObjects;
    using LogMeOut.ImageHub.Repository.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class PostQueryLogic : BaseLogic, IPostQueryLogic
    {
        public PostQueryLogic(IBaseLogicDependency baseLogicDependency)
            : base(baseLogicDependency)
        {
        }

        public PostsBatchResponse GetUserFeedBatch(PostsBatchRequest request)
        {
            //List<PostEntity> allPosts = DemoDataGenerator.Posts.OrderByDescending(p => p.Date).ToList();
            var posts = Repository.PostRepository.ListFeedPostsForUser(request.UserId, request.LoggedInUserId, request.Take, request.LastPostId);

            int cursor = 0;
            if (request.LastPostId != null)
            {
                cursor = posts.FindIndex(p => p.PostId == request.LastPostId.Value);
            }

            List<Post> skippedPosts = posts.Skip(cursor).ToList();
            List<Post> resultPosts = skippedPosts.Take(request.Take).ToList();
            bool hasMoreLoad = false;
            if (posts.Count - request.Take > 0)
            {
                hasMoreLoad = true;
            }

            return new PostsBatchResponse()
            {
                Posts = resultPosts.Select(x => ToPostEntity(x, request.LoggedInUserId)),
                HasMoreLoad = hasMoreLoad
            };
        }

        private PostEntity ToPostEntity(Post item, Guid loggedInUserId)
        {
            return new PostEntity()
            {
                Date = item.Date,
                HubtasticCount = item.Likes.Count,
                ImageId = item.ImageId,
                PostDescription = item.PostDescription,
                IsHubbedByCurrentUser = item.Likes.Any(x => x.User.Id == loggedInUserId),
                PosterId = item.Poster.Id,
                PosterName = item.Poster.Name,
                PosterProfileIconId = item.Poster.ProfileImageId,
                PostId = item.PostId
            };
        }

        public PostsBatchResponse LoadUserPosts(PostsBatchRequest request)
        {
            //List<PostEntity> allPosts = DemoDataGenerator.Posts.Where(p => p.PosterId == request.UserId).OrderByDescending(p => p.Date).ToList();
            List<Post> posts = Repository.PostRepository.ListPostsByUserId(request.UserId).OrderByDescending(p => p.Date).ToList();

            int cursor = 0;
            if (request.LastPostId != null)
            {
                cursor = posts.FindIndex(p => p.PostId == request.LastPostId.Value);
            }

            List<Post> skippedPosts = posts.Skip(cursor).ToList();
            List<Post> resultPosts = skippedPosts.Take(request.Take).ToList();
            bool hasMoreLoad = false;
            if (posts.Count - request.Take > 0)
            {
                hasMoreLoad = true;
            }

            List<PostEntity> resultPostsConverted = new List<PostEntity>();
            foreach (var item in resultPosts)
            {
                resultPostsConverted.Add(ToPostEntity(item, request.LoggedInUserId));
            }

            return new PostsBatchResponse()
            {
                Posts = resultPostsConverted,
                HasMoreLoad = hasMoreLoad
            };
        }
    }
}
