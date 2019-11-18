namespace LogMeOut.ImageHub.Repository.Interfaces
{
    using LogMeOut.ImageHub.Repository.Interfaces;
    using System;

    public interface IImageHubRepository : IDisposable
    {
        IUserRepository UserRepository { get; }

        IPostRepository PostRepository { get; }

        ICommentRepository CommentRepository { get; }

        ILikeRepository LikeRepository { get; }

        IUserRelationRepository UserRelationRepository { get; }

        void SaveChanges();
    }
}