namespace LogMeOut.ImageHub.Interfaces.Logic
{
    using LogMeOut.ImageHub.Interfaces.Logic.TransportObjects;

    public interface IUserSearcherLogic
    {
        UserSearchResult SearchUsersByPartialUserName(string partialUserName);
    }
}
