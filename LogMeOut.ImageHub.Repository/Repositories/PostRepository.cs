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
            var result = Context.Post
                //.Include(x => x.Likes)
                //.Include(x => x.Likes.Select(y => y.User))
                //.Include(x => x.Comments)
                .Include(x => x.Poster)
                .Where(x => x.Poster.Id == userId)
                .ToList();

            //foreach (var item in result)
            //{
            //    Context.Entry(item).Collection(x => x.Comments);
            //    Context.Entry(item).Collection(x => x.Likes);
            //    foreach (var like in item.Likes)
            //    {
            //        Context.Entry(like).Reference(x => x.User);
            //    }
            //}
            foreach (var item in result)
            {
                item.Likes = Context.Like.Include(x => x.User).Where(x => x.Post.PostId == item.PostId).ToList();
                item.Comments = Context.Comment.Where(x => x.Post.PostId == item.PostId).ToList();
            }

            return result;
        }
    }
}