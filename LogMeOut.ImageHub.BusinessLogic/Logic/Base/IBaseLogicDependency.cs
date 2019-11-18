namespace LogMeOut.ImageHub.BusinessLogic.Logic.Base
{
    using LogMeOut.ImageHub.Interfaces.Util;
    using LogMeOut.ImageHub.Repository.Interfaces;

    public interface IBaseLogicDependency
    {
        IImageHubRepository Repository { get; }

        FtpConnectionInformation FtpConnectionInformation { get; }
    }
}
