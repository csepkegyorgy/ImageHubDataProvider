namespace LogMeOut.ImageHub.BusinessLogic.Logic.Base
{
    using LogMeOut.ImageHub.Interfaces.Repository;
    using LogMeOut.ImageHub.Interfaces.Util;

    public interface IBaseLogicDependency
    {
        IImageHubRepository Repository { get; }

        FtpConnectionInformation FtpConnectionInformation { get; }
    }
}
