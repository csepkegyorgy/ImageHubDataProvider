namespace LogMeOut.ImageHub.Interfaces.Repository
{
    using LogMeOut.ImageHub.Repository.Models;
    using System;

    public interface IUserRepository
    {
        User GetUserById(Guid id);

        User GetUserByFacebookUserId(string facebookUserId);

        void AddUser(User user);
    }
}