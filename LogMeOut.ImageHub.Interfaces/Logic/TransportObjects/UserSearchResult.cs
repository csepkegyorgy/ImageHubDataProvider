namespace LogMeOut.ImageHub.Interfaces.Logic.TransportObjects
{
    using LogMeOut.ImageHub.Interfaces.Entity;
    using System;
    using System.Collections.Generic;

    public class UserSearchResult
    {
        public List<UserEntity> Users { get; set; }

        public bool Success { get; set; }
    }
}
