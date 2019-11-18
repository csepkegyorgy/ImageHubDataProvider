namespace LogMeOut.ImageHub.Repository.Interfaces
{
    using LogMeOut.ImageHub.Repository.Models;
    using System;

    public interface IUserRelationRepository
    {
        void AddUserRelation(UserRelation relation);

        UserRelation GetUserRelationForUserByUser(Guid userId, Guid targetUserId);

        void RemoveUserRelation(UserRelation relation);
    }
}
