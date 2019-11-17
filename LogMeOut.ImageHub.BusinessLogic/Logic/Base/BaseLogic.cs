namespace LogMeOut.ImageHub.BusinessLogic.Logic.Base
{
    using LogMeOut.ImageHub.Interfaces.Repository;

    public class BaseLogic
    {
        protected IBaseLogicDependency BaseLogicDependency;

        protected IImageHubRepository Repository => BaseLogicDependency.Repository;

        public BaseLogic(IBaseLogicDependency baseLogicDependency)
        {
            this.BaseLogicDependency = baseLogicDependency;
        }
    }
}
