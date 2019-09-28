namespace LogMeOut.ImageHub.Interfaces.Logic.TransportObjects
{
    using System;

    public class AuthenticateUserResponse
    {
        public Guid UserId { get; set; }

        public string ProfileIconId { get; set; }

        public string Name { get; set; }

        public string FacebookUserId { get; set; }

        public string Email { get; set; }

        public bool SuccessfulLogin
        {
            get
            {
                return (!string.IsNullOrWhiteSpace(this.Email) &&
                        !string.IsNullOrWhiteSpace(this.FacebookUserId) &&
                        !string.IsNullOrWhiteSpace(this.ProfileIconId) &&
                        !string.IsNullOrWhiteSpace(this.Name) &&
                        (this.UserId == null || this.UserId != Guid.Empty));
            }
        }
    }
}
