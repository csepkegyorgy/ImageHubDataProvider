namespace LogMeOut.ImageHub.Repository.Repositories
{
    using LogMeOut.ImageHub.Interfaces.Repository;

    public class CommentRepository : BaseRepository, ICommentRepository
    {
        private ImageHubRepository imageHubRepository;

        public CommentRepository(ImageHubRepository imageHubRepository)
            : base (imageHubRepository)
        {
            this.imageHubRepository = imageHubRepository;
        }
    }
}