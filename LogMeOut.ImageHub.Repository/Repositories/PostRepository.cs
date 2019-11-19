namespace LogMeOut.ImageHub.Repository.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using LogMeOut.ImageHub.Repository.Interfaces;
    using LogMeOut.ImageHub.Repository.Models;
    using Microsoft.EntityFrameworkCore;

    public class PostRepository : BaseRepository, IPostRepository
    {
        private ImageHubRepository imageHubRepository;

        public PostRepository(ImageHubRepository imageHubRepository)
            : base(imageHubRepository)
        {
            this.imageHubRepository = imageHubRepository;
        }

        public void AddPost(Post post)
        {
            Context.Post.Add(post);
            Context.SaveChanges();
        }

        public List<Post> ListFeedPostsForUser(Guid userId, Guid loggedInUserId, int take, Guid? lastPostId)
        {
            List<Guid> userPostScope = Context.UserRelation
                 .Where(x => x.RelationType == UserRelationType.Follow)
                 .Where(x => x.User.Id == userId)
                 .Select(x => x.TargetUser.Id)
                 .ToList();
            userPostScope.Add(userId);

            var result = Context.Post
                .Include(x => x.Poster)
                .AsEnumerable()
                .Where(x => userPostScope.Contains(x.Poster.Id))
                .OrderByDescending(x => x.Date)
                .ToList();

            return result;
        }

        //public List<Post> ListFeedPostsForUser(Guid userId, Guid loggedInUserId, int take, Guid? lastPostId)
        //{
        //    List<Guid> userPostScope = Context.UserRelation
        //        .Where(x => x.User.Id == userId)
        //        .Select(x => x.TargetUser.Id)
        //        .ToList();
        //    userPostScope.Add(userId);

        //    var result = Context.Post
        //        .Include(x => x.Poster)
        //        .AsEnumerable()
        //        .Where(x => userPostScope.Contains(x.Poster.Id))
        //        .OrderByDescending(x => x.Date)
        //        .ToList();

        //    if (lastPostId.HasValue)
        //    {
        //        int cursor = result.FindIndex(x => x.PostId == lastPostId.Value);
        //        if (cursor > 0)
        //        {
        //            result = result.Skip(cursor).ToList();
        //        }
        //    }

        //    result = result.Take(take).ToList();

        //    foreach (var item in result)
        //    {
        //        item.Likes = Context.Like.Include(x => x.User).Where(x => x.Post.PostId == item.PostId).ToList();
        //        item.Comments = Context.Comment.Where(x => x.Post.PostId == item.PostId).ToList();
        //    }

        //    return result;
        //}

        public List<Post> ListPostsByUserId(Guid userId)
        {
            var result = Context.Post
                .Include(x => x.Poster)
                .Where(x => x.Poster.Id == userId)
                .ToList();

            foreach (var item in result)
            {
                item.Likes = Context.Like.Include(x => x.User).Where(x => x.Post.PostId == item.PostId).ToList();
                item.Comments = Context.Comment.Where(x => x.Post.PostId == item.PostId).ToList();
            }

            return result;
        }
    }
}