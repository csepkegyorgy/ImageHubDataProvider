namespace LogMeOut.ImageHub.Repository.Repositories
{
    using LogMeOut.ImageHub.Interfaces.Repository;

    public class PostRepository : BaseRepository, IPostRepository
    {
        private ImageHubRepository imageHubRepository;

        public PostRepository(ImageHubRepository imageHubRepository)
            : base(imageHubRepository)
        {
            this.imageHubRepository = imageHubRepository;
        }
    }
}