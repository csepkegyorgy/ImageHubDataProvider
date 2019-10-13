namespace LogMeOut.ImageHub.Interfaces.Logic
{
    using LogMeOut.ImageHub.Interfaces.Logic.TransportObjects;

    public interface IPostSubmitterLogic
    {
        SubmitPostResponse SubmitPost(SubmitPostRequest request);
    }
}
