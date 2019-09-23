namespace LogMeOut.ImageHub.Interfaces.Entity
{
    using System;

    public class CommentEntity
    {
        public string Text { get; set; }
        public DateTime Date { get; set; }

        public string CommenterName { get; set; }
        public Guid CommenterId { get; set; }
        public string CommenterProfileIconId { get; set; }

        public Guid PostId { get; set; }
    }
}
