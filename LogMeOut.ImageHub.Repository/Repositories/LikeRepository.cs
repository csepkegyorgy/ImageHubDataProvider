namespace LogMeOut.ImageHub.Repository.Repositories
{
    using LogMeOut.ImageHub.Repository.Interfaces;

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