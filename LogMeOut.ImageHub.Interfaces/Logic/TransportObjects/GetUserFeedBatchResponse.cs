namespace LogMeOut.ImageHub.Interfaces.Logic.TransportObjects
{
    using LogMeOut.ImageHub.Interfaces.Entity;
    using System.Collections.Generic;

    public class GetUserFeedBatchResponse
    {
        public IEnumerable<PostEntity> Posts { get; set; }

        public bool HasMoreLoad { get; set; }
    }
}
