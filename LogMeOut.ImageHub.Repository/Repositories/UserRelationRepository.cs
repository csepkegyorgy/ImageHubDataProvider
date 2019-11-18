namespace LogMeOut.ImageHub.Repository.Repositories
{
    using System;
    using System.Linq;
    using LogMeOut.ImageHub.Repository.Interfaces;
    using LogMeOut.ImageHub.Repository.Models;

    public class UserRelationRepository : BaseRepository, IUserRelationRepository
    {
        private ImageHubRepository imageHubRepository;

        public UserRelationRepository(ImageHubRepository imageHubRepository)
            : base(imageHubRepository)
        {
            this.imageHubRepository = imageHubRepository;
        }

        public void AddUserRelation(UserRelation relation)
        {
            Context.UserRelation.Add(relation);
            Context.SaveChanges();
        }

        public UserRelation GetUserRelationForUserByUser(Guid userId, Guid targetUserId)
        {
            return Context.UserRelation
                .SingleOrDefault(x => x.User.Id == userId && x.TargetUser.Id == targetUserId);
        }

        public void RemoveUserRelation(UserRelation relation)
        {
            Context.Remove(relation);
            Context.SaveChanges();
        }
    }
}