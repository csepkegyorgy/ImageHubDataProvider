namespace LogMeOut.ImageHub.BusinessLogic.Logic
{
    using LogMeOut.ImageHub.BusinessLogic.Logic.Base;
    using LogMeOut.ImageHub.Interfaces.Entity;
    using LogMeOut.ImageHub.Interfaces.Logic;
    using LogMeOut.ImageHub.Interfaces.Logic.TransportObjects;
    using LogMeOut.ImageHub.Repository.Models;
    using System.Collections.Generic;
    using System.Linq;

    public class UserSearcherLogic : BaseLogic, IUserSearcherLogic
    {
        public UserSearcherLogic(IBaseLogicDependency baseLogicDependency)
            : base(baseLogicDependency)
        {
        }

        public UserSearchResult SearchUsersByPartialUserName(string partialUserName)
        {
            if (!string.IsNullOrWhiteSpace(partialUserName))
            {
                List<User> result = Repository.UserRepository.SearchByUserName(partialUserName);

                return new UserSearchResult()
                {
                    Users = result.Select(x => ToUserEntity(x)).ToList(),
                    Success = true
                };
            }
            else
            {
                return new UserSearchResult()
                {
                    Users = new List<UserEntity>(),
                    Success = false
                };
            }
        }

        public UserEntity ToUserEntity(User user)
        {
            return new UserEntity()
            {
                Email = user.Email,
                FacebookUserId = user.FacebookUserId,
                Name = user.Name,
                ProfileImageId = user.ProfileImageId,
                UserId = user.Id
            };
        }
    }
}
