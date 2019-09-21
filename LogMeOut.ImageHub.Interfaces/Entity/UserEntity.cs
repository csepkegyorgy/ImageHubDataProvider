namespace LogMeOut.ImageHub.Interfaces.Entity
{
    using System;

    public class UserEntity
    {
        public Guid UserId { get; set; }

        public string ProfileImageId { get; set; }

        public string Name { get; set; }
    }
}