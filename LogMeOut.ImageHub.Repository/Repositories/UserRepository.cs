namespace LogMeOut.ImageHub.Repository.Repositories
{
    using LogMeOut.ImageHub.Repository.Interfaces;
    using LogMeOut.ImageHub.Repository.Models;
    using System;
    using System.Collections.Generic;
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

        public User GetUserById(Guid id)
        {
            return Context.User.SingleOrDefault(x => x.Id == id);
        }

        public List<User> SearchByUserName(string partialUserName)
        {
            return Context.User
                .Where(x => x.Name.Contains(partialUserName))
                .ToList();
        }
    }
}