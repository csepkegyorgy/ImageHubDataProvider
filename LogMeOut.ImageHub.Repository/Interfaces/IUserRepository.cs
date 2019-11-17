namespace LogMeOut.ImageHub.Interfaces.Repository
{
    using LogMeOut.ImageHub.Repository.Models;

    public interface IUserRepository
    {
        User GetUserByFacebookUserId(string facebookUserId);

        void AddUser(User user);
    }
}