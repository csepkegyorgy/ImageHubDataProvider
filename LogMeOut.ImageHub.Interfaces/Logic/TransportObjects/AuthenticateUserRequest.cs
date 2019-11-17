namespace LogMeOut.ImageHub.Interfaces.Logic.TransportObjects
{
    public class AuthenticateUserRequest
    {
        public string FacebookUserId { get; set; }

        public string Email { get; set; }

        public string UserName { get; set; }

        public string ProfilePictureUrl { get; set; }
    }
}
