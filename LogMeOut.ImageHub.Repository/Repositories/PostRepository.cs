namespace LogMeOut.ImageHub.Repository.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using LogMeOut.ImageHub.Interfaces.Repository;
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

        public List<Post> ListPostsByUserId(Guid userId)
        {
            return Context.Post
                .Include(x => x.Likes)
                .Include(x => x.Likes.Select(y => y.User))
                .Include(x => x.Comments)
                .Include(x => x.Poster)
                .Where(x => x.Poster.Id == userId)
                .ToList();
        }
    }
}