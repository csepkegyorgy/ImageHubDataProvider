﻿namespace LogMeOut.ImageHub.Interfaces.Repository
{
    using LogMeOut.ImageHub.Repository.Models;
    using System;
    using System.Collections.Generic;

    public interface IPostRepository
    {
        void AddPost(Post post);

        List<Post> ListPostsByUserId(Guid userId);
    }
}