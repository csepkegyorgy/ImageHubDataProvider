namespace LogMeOut.ImageHub.BusinessLogic.Logic
{
    using System;
    using LogMeOut.ImageHub.BusinessLogic.Logic.Base;
    using LogMeOut.ImageHub.Interfaces.Logic;
    using LogMeOut.ImageHub.Interfaces.Logic.TransportObjects;
    using LogMeOut.ImageHub.Repository.Models;

    public class UserRelationHandlerLogic : BaseLogic, IUserRelationHandlerLogic
    {
        public UserRelationHandlerLogic(IBaseLogicDependency baseLogicDependency)
            : base(baseLogicDependency)
        {
        }

        public void AcceptFollowRequestForUser(Guid accepterUserId, Guid targetUserId)
        {
            var accepterUser = Repository.UserRepository.GetUserById(accepterUserId);
            var targetUser = Repository.UserRepository.GetUserById(targetUserId);

            if (accepterUser != null && targetUser != null)
            {
                var accepterUserRelation = Repository.UserRelationRepository.GetUserRelationForUserByUser(accepterUserId, targetUserId);
                var targetUserRelation = Repository.UserRelationRepository.GetUserRelationForUserByUser(targetUserId, accepterUserId);

                if (accepterUserRelation != null && targetUserRelation != null)
                {
                    accepterUserRelation.RelationType = UserRelationType.Follow;
                    targetUserRelation.RelationType = UserRelationType.Follow;
                    Repository.SaveChanges();
                }
            }
        }

        public void CreateUserFollowRequestRelation(Guid requesterUserId, Guid targetUserId)
        {
            var requesterUser = Repository.UserRepository.GetUserById(requesterUserId);
            var targetUser = Repository.UserRepository.GetUserById(targetUserId);

            if (requesterUser != null && targetUser != null)
            {
                var requesterUserRelation = new UserRelation()
                {
                    Id = Guid.NewGuid(),
                    RelationType = UserRelationType.FollowRequestOutgoing,
                    TargetUser = targetUser,
                    User = requesterUser
                };
                var targetUserRelation = new UserRelation()
                {
                    Id = Guid.NewGuid(),
                    RelationType = UserRelationType.FollowRequestIncoming,
                    TargetUser = targetUser,
                    User = requesterUser
                };

                Repository.UserRelationRepository.AddUserRelation(requesterUserRelation);
                Repository.UserRelationRepository.AddUserRelation(targetUserRelation);
            }
        }

        public void RejectFollowRequestForUser(Guid rejecterUserId, Guid targetUserId)
        {
            var rejecterUser = Repository.UserRepository.GetUserById(rejecterUserId);
            var targetUser = Repository.UserRepository.GetUserById(targetUserId);

            if (rejecterUser != null && targetUser != null)
            {
                var rejecterUserRelation = Repository.UserRelationRepository.GetUserRelationForUserByUser(rejecterUserId, targetUserId);
                var targetUserRelation = Repository.UserRelationRepository.GetUserRelationForUserByUser(targetUserId, rejecterUserId);

                if (rejecterUserRelation != null && targetUserRelation != null)
                {
                    Repository.UserRelationRepository.RemoveUserRelation(rejecterUserRelation);
                    Repository.UserRelationRepository.RemoveUserRelation(targetUserRelation);
                }
            }
        }

        public void UnfollowUsers(Guid unfollowerUserId, Guid targetUserId)
        {
            var unfollowerUser = Repository.UserRepository.GetUserById(unfollowerUserId);
            var targetUser = Repository.UserRepository.GetUserById(targetUserId);

            if (unfollowerUser != null && targetUser != null)
            {
                var unfollowerUserRelation = Repository.UserRelationRepository.GetUserRelationForUserByUser(unfollowerUserId, targetUserId);
                var targetUserRelation = Repository.UserRelationRepository.GetUserRelationForUserByUser(targetUserId, unfollowerUserId);

                if (unfollowerUserRelation != null && targetUserRelation != null)
                {
                    Repository.UserRelationRepository.RemoveUserRelation(unfollowerUserRelation);
                    Repository.UserRelationRepository.RemoveUserRelation(targetUserRelation);
                }
            }
        }

        GetUserRelationForUserResponse IUserRelationHandlerLogic.GetUserRelationForUser(Guid requesterUserId, Guid targetUserId)
        {
            UserRelation userRelation = Repository.UserRelationRepository.GetUserRelationForUserByUser(requesterUserId, targetUserId);

            string relation = "none";
            if (userRelation != null)
            {
                if (userRelation.RelationType == UserRelationType.Follow)
                {
                    relation = "following";
                }
                else if (userRelation.RelationType == UserRelationType.FollowRequestIncoming)
                {
                    relation = "incomingpending";
                }
                else if (userRelation.RelationType == UserRelationType.FollowRequestIncoming)
                {
                    relation = "outgoingpending";
                }
            }

            return new GetUserRelationForUserResponse()
            {
                Relation = relation,
                Success = true,
                UserId = requesterUserId
            };
        }
    }
}
