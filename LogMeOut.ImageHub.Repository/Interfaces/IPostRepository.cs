namespace LogMeOut.ImageHub.Repository.Interfaces
{
    using LogMeOut.ImageHub.Repository.Models;
    using System;
    using System.Collections.Generic;

    public interface IPostRepository
    {
        void AddPost(Post post);

        List<Post> ListPostsByUserId(Guid userId);
        List<Post> ListFeedPostsForUser(Guid userId, Guid loggedInUserId, int take, Guid? lastPostId);
    }
}