namespace LogMeOut.ImageHub.Repository.Interfaces
{
    using LogMeOut.ImageHub.Repository.Models;
    using System;
    using System.Collections.Generic;

    public interface IUserRepository
    {
        User GetUserById(Guid id);

        User GetUserByFacebookUserId(string facebookUserId);

        List<User> SearchByUserName(string partialUserName);

        void AddUser(User user);
    }
}