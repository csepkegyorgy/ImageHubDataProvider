namespace LogMeOut.ImageHub.Interfaces.Logic.TransportObjects
{
    using System;

    public class SubmitPostRequest
    {
        public Guid UserId { get; set; }

        public string ImageId { get; set; }

        public string Description { get; set; }
    }
}
