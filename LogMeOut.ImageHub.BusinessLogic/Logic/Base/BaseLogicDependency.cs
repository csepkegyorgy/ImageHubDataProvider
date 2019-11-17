using LogMeOut.ImageHub.Interfaces.Repository;

namespace LogMeOut.ImageHub.BusinessLogic.Logic.Base
{
    public class BaseLogicDependency : IBaseLogicDependency
    {
        public IImageHubRepository Repository { get; }

        public BaseLogicDependency(IImageHubRepository repository)
        {
            this.Repository = repository;
        }
    }
}
