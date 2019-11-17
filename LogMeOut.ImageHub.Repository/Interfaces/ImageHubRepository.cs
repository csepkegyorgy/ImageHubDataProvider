namespace LogMeOut.ImageHub.Interfaces.Repository
{
    using System;

    public interface IImageHubRepository : IDisposable
    {
        IUserRepository UserRepository { get; }

        IPostRepository PostRepository { get; }

        ICommentRepository CommentRepository { get; }

        ILikeRepository LikeRepository { get; }

        void SaveChanges();
    }
}