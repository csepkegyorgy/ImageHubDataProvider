namespace LogMeOut.ImageHub.BusinessLogic.Logic
{
    using LogMeOut.ImageHub.Interfaces;
    using LogMeOut.ImageHub.Interfaces.Exceptions;
    using LogMeOut.ImageHub.Interfaces.Logic;
    using LogMeOut.ImageHub.Interfaces.Logic.TransportObjects;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net;
    using System.Threading;
    using System.Linq;

    public class AuthenticationLogic : IAuthenticationLogic
    {
        public AuthenticateUserResponse AuthenticateUser(AuthenticateUserRequest request)
        {
            Interfaces.Entity.UserEntity user = DemoDataGenerator.Users.SingleOrDefault(u => u.FacebookUserId == request.FacebookUserId);

            if (user == null)
            {
                DemoDataGenerator.AddUser(new Interfaces.Entity.UserEntity()
                {
                    Email = request.Email,
                    FacebookUserId = request.FacebookUserId,
                    Name = request.UserName,
                    UserId = Guid.NewGuid(),
                    ProfileImageId = "testimage.jpg"
                });

                user = DemoDataGenerator.Users.Single(u => u.FacebookUserId == request.FacebookUserId);
            }

            return new AuthenticateUserResponse()
            {
                UserId = user.UserId,
                ProfileIconId = user.ProfileImageId,
                Email = user.Email,
                FacebookUserId = user.FacebookUserId,
                Name = user.Name
            };
        }
    }
}