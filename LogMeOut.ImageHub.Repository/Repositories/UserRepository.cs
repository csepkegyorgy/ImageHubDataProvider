namespace LogMeOut.ImageHub.Repository.Repositories
{
    using LogMeOut.ImageHub.Interfaces.Repository;

    public class UserRepository : BaseRepository, IUserRepository
    {
        private ImageHubRepository imageHubRepository;

        public UserRepository(ImageHubRepository imageHubRepository)
            : base(imageHubRepository)
        {
            this.imageHubRepository = imageHubRepository;
        }
    }
}