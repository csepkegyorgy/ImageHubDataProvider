namespace LogMeOut.ImageHub.Repository.Repositories
{
    using LogMeOut.ImageHub.Interfaces.Repository;
    using LogMeOut.ImageHub.Repository.Models;
    using System.Linq;

    public class UserRepository : BaseRepository, IUserRepository
    {
        private ImageHubRepository imageHubRepository;

        public UserRepository(ImageHubRepository imageHubRepository)
            : base(imageHubRepository)
        {
            this.imageHubRepository = imageHubRepository;
        }

        public void AddUser(User user)
        {
            Context.User.Add(user);
            Context.SaveChanges();
        }

        public User GetUserByFacebookUserId(string facebookUserId)
        {
            return Context.User.SingleOrDefault(x => x.FacebookUserId == facebookUserId);
        }
    }
}