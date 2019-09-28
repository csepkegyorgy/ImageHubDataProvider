namespace LogMeOut.ImageHub.Interfaces.Logic
{
    using LogMeOut.ImageHub.Interfaces.Logic.TransportObjects;

    public interface IAuthenticationLogic
    {
        AuthenticateUserResponse AuthenticateUser(AuthenticateUserRequest request);
    }
}