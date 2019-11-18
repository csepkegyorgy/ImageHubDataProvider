namespace LogMeOut.ImageHub.BusinessLogic.Logic.Base
{
    using LogMeOut.ImageHub.Interfaces;
    using LogMeOut.ImageHub.Interfaces.Util;
    using LogMeOut.ImageHub.Repository.Interfaces;

    public class BaseLogicDependency : IBaseLogicDependency
    {
        public IImageHubRepository Repository { get; }

        public FtpConnectionInformation FtpConnectionInformation { get; }

        public BaseLogicDependency(IImageHubRepository repository)
        {
            this.Repository = repository;
            this.FtpConnectionInformation = AppConfig.GetFtpConnectionInformation();
        }
    }
}
