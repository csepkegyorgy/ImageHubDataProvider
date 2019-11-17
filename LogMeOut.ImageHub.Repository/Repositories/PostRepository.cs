namespace LogMeOut.ImageHub.Repository.Repositories
{
    using LogMeOut.ImageHub.Interfaces.Repository;
    using LogMeOut.ImageHub.Repository.Models;

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
    }
}