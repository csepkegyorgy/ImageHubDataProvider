namespace LogMeOut.ImageHub.BusinessLogic.Logic
{
    using LogMeOut.ImageHub.Interfaces.Logic;
    using LogMeOut.ImageHub.Interfaces.Logic.TransportObjects;
    using System;
    using LogMeOut.ImageHub.Repository.Models;
    using LogMeOut.ImageHub.BusinessLogic.Logic.Base;

    public class AuthenticationLogic : BaseLogic, IAuthenticationLogic
    {
        public AuthenticationLogic(IBaseLogicDependency baseLogicDependency)
            : base(baseLogicDependency)
        {
        }

        public AuthenticateUserResponse AuthenticateUser(AuthenticateUserRequest request)
        {
            User user = Repository.UserRepository.GetUserByFacebookUserId(request.FacebookUserId);

            if (user == null)
            {
                user = new User()
                {
                    ProfileImageId = "default.jpg",
                    Id = Guid.NewGuid(),
                    FacebookUserId = request.FacebookUserId,
                    Email = request.Email,
                    Name = request.UserName
                };
                Repository.SaveChanges();
            }

            return new AuthenticateUserResponse()
            {
                UserId = user.Id,
                ProfileIconId = user.ProfileImageId,
                Email = user.Email,
                FacebookUserId = user.FacebookUserId,
                Name = user.Name
            };
        }
    }
}