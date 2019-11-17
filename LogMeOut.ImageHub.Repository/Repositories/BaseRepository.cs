namespace LogMeOut.ImageHub.Repository.Repositories
{
    using LogMeOut.ImageHub.Repository.Data;

    public class BaseRepository
    {
        protected ImageHubRepository Repository { get; private set; }

        protected ImageHubDbContext Context => Repository.DbContext;

        public BaseRepository(ImageHubRepository repository)
        {
            this.Repository = repository;
        }
    }
}