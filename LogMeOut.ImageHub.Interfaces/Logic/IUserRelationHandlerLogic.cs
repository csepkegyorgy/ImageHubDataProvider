namespace LogMeOut.ImageHub.Interfaces.Logic
{
    using LogMeOut.ImageHub.Interfaces.Logic.TransportObjects;
    using System;

    public interface IUserRelationHandlerLogic
    {
        void CreateUserFollowRequestRelation(Guid requesterUserId, Guid targetUserId);

        void RejectFollowRequestForUser(Guid rejecterUserId, Guid targetUserId);

        void AcceptFollowRequestForUser(Guid accepterUserId, Guid targetUserId);

        void UnfollowUsers(Guid unfollowerUserId, Guid targetUserId);

        GetUserRelationForUserResponse GetUserRelationForUser(Guid requesterUserId, Guid targetUserId);
    }
}
