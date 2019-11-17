namespace LogMeOut.ImageHub.BusinessLogic.Logic.Base
{
    using LogMeOut.ImageHub.Interfaces.Repository;

    public interface IBaseLogicDependency
    {
        IImageHubRepository Repository { get; }
    }
}
