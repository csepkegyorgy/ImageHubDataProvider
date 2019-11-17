using LogMeOut.ImageHub.Interfaces;
using LogMeOut.ImageHub.Interfaces.Repository;
using LogMeOut.ImageHub.Interfaces.Util;

namespace LogMeOut.ImageHub.BusinessLogic.Logic.Base
{
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
