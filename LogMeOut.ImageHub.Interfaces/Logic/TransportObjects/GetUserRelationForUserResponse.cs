namespace LogMeOut.ImageHub.Interfaces.Logic.TransportObjects
{
    using System;

    public class GetUserRelationForUserResponse
    {
        public string Relation { get; set; }

        public Guid UserId { get; set; }

        public bool Success { get; set; }
    }
}
