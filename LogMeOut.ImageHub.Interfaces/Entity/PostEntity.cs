namespace LogMeOut.ImageHub.Interfaces.Entity
{
    using System;

    public class PostEntity
    {
        public Guid PostId { get; set; }
        public string ImageId { get; set; }
        public string PostDescription { get; set; }
        public DateTime Date { get; set; }

        public int HubtasticCount { get; set; }
        public bool IsHubbedByCurrentUser { get; set; }

        public string PosterName { get; set; }
        public Guid PosterId { get; set; }
        public string PosterProfileIconId { get; set; }

    }
}
