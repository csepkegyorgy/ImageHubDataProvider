namespace LogMeOut.ImageHub.Repository.Repositories
{
    using LogMeOut.ImageHub.Interfaces.Repository;
    using LogMeOut.ImageHub.Repository.Data;
    using System;

    public class ImageHubRepository : IImageHubRepository, IDisposable
    {
        public ImageHubDbContext DbContext => DbContextLazy.Value;

        public IUserRepository UserRepository => UserRepositoryLazy.Value;

        public IPostRepository PostRepository => PostRepositoryLazy.Value;

        public ICommentRepository CommentRepository => CommentRepositoryLazy.Value;

        public ILikeRepository LikeRepository => LikeRepositoryLazy.Value;

        private Lazy<ImageHubDbContext> DbContextLazy;
        private Lazy<IUserRepository> UserRepositoryLazy;
        private Lazy<IPostRepository> PostRepositoryLazy;
        private Lazy<ICommentRepository> CommentRepositoryLazy;
        private Lazy<ILikeRepository> LikeRepositoryLazy;

        public ImageHubRepository()
        {
            DbContextLazy = new Lazy<ImageHubDbContext>(() => new ImageHubDbContext());
            UserRepositoryLazy = new Lazy<IUserRepository>(() => new UserRepository(this));
            PostRepositoryLazy = new Lazy<IPostRepository>(() => new PostRepository(this));
            CommentRepositoryLazy = new Lazy<ICommentRepository>(() => new CommentRepository(this));
            LikeRepositoryLazy = new Lazy<ILikeRepository>(() => new LikeRepository(this));
        }

        ~ImageHubRepository()
        {
            this.Dispose();
        }

        public void SaveChanges()
        {
            DbContext.SaveChanges();
        }

        public void Dispose()
        {
            if(DbContextLazy != null && DbContextLazy.Value != null)
                DbContextLazy.Value.Dispose();
            DbContextLazy = null;
        }

        public void RestartContext()
        {
            if (DbContextLazy != null)
            {
                DbContextLazy.Value.Dispose();
                DbContextLazy = null;
            }

            DbContextLazy = new Lazy<ImageHubDbContext>(() => new ImageHubDbContext());
        }
    }
}