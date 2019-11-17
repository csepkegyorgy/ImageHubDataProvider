namespace LogMeOut.ImageHub.Repository.Repositories
{
    using LogMeOut.ImageHub.Interfaces.Repository;

    internal class LikeRepository : BaseRepository, ILikeRepository
    {
        private ImageHubRepository imageHubRepository;

        public LikeRepository(ImageHubRepository imageHubRepository)
            : base(imageHubRepository)
        {
            this.imageHubRepository = imageHubRepository;
        }
    }
}