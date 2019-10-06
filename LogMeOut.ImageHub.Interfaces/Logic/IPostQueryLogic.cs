namespace LogMeOut.ImageHub.Interfaces.Logic
{
    using LogMeOut.ImageHub.Interfaces.Logic.TransportObjects;

    public interface IPostQueryLogic
    {
        PostsBatchResponse GetUserFeedBatch(PostsBatchRequest request);

        PostsBatchResponse LoadUserPosts(PostsBatchRequest request);
    }
}