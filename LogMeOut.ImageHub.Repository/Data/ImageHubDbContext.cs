namespace LogMeOut.ImageHub.Repository.Data
{
    using LogMeOut.ImageHub.Repository.Models;
    using Microsoft.EntityFrameworkCore;

    public class ImageHubDbContext : DbContext
    {
        public DbSet<User> User { get; set; }

        public DbSet<Post> Post { get; set; }

        public DbSet<Comment> Comment { get; set; }

        public DbSet<Like> Like { get; set; }

        public ImageHubDbContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlServer("Server=tcp:imagehubdbprovider.database.windows.net,1433;Initial Catalog=imagehubdb;Persist Security Info=False;User ID=imagehubdbadmin;Password=Imagehubdb1234;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .HasMany(x => x.Comments)
                .WithOne(x => x.Commenter)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<User>()
                .HasMany(x => x.Posts)
                .WithOne(x => x.Poster)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<User>()
                .HasMany(x => x.Likes)
                .WithOne(x => x.User)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Post>()
                .HasMany(x => x.Likes)
                .WithOne(x => x.Post)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Post>()
                .HasMany(x => x.Comments)
                .WithOne(x => x.Post)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
