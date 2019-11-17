namespace LogMeOut.ImageHub.Interfaces.Logic.TransportObjects
{
    using System;

    public class PostsBatchRequest
    {
        public Guid LoggedInUserId { get; set; }

        public Guid UserId { get; set; }

        public int Take { get; set; }

        public Guid? LastPostId { get; set; }
    }
}