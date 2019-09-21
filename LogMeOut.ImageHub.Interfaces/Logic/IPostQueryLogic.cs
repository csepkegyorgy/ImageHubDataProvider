namespace LogMeOut.ImageHub.Interfaces.Logic
{
    using LogMeOut.ImageHub.Interfaces.Logic.TransportObjects;

    public interface IPostQueryLogic
    {
        GetUserFeedBatchResponse GetUserFeedBatch(GetUserFeedBatchRequest request);
    }
}