namespace LogMeOut.ImageHub.BusinessLogic.Logic
{
    using LogMeOut.ImageHub.Interfaces.Logic;
    using LogMeOut.ImageHub.Interfaces.Logic.TransportObjects;
    using System;
    using LogMeOut.ImageHub.Repository.Models;
    using LogMeOut.ImageHub.BusinessLogic.Logic.Base;
    using System.Net;
    using System.IO;

    public class AuthenticationLogic : BaseLogic, IAuthenticationLogic
    {
        public AuthenticationLogic(IBaseLogicDependency baseLogicDependency)
            : base(baseLogicDependency)
        {
        }

        private string UploadImageToFtpByUrlAndGetImageId(AuthenticateUserRequest request, Guid userId)
        {
            string resultFileName = GetFileNameFromUserAndDateForUploadedImage(userId) + ".jpg";
            string uri = $"ftp://{FtpInfo.Host}:{FtpInfo.Port}/";
            uri += $"profileicons/{resultFileName}";

            var fileContent = base.DownloadImageFromPublicUrl(request.ProfilePictureUrl);
            base.UploadImage(fileContent, uri);

            return resultFileName;
        }

        public AuthenticateUserResponse AuthenticateUser(AuthenticateUserRequest request)
        {
            User user = Repository.UserRepository.GetUserByFacebookUserId(request.FacebookUserId);

            if (user == null)
            {
                Guid userId = Guid.NewGuid();
                string imageId = UploadImageToFtpByUrlAndGetImageId(request, userId);

                user = new User()
                {
                    ProfileImageId = imageId,
                    Id = userId,
                    FacebookUserId = request.FacebookUserId,
                    Email = request.Email,
                    Name = request.UserName
                };
                Repository.UserRepository.AddUser(user);
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

        private string GetFileNameFromUserAndDateForUploadedImage(Guid userId)
        {
            string resultFileName = "";

            resultFileName += userId.ToString() + "_";

            DateTime now = DateTime.Now;
            resultFileName +=
                now.Year.ToString() +
                FormatDateElement(now.Month.ToString()) +
                FormatDateElement(now.Day.ToString()) +
                FormatDateElement(now.Hour.ToString()) +
                FormatDateElement(now.Minute.ToString()) +
                FormatDateElement(now.Second.ToString());

            return resultFileName;
        }

        private string FormatDateElement(string dateElement)
        {
            if (dateElement.Length == 1)
            {
                dateElement = "0" + dateElement;
            }
            return dateElement;
        }
    }
}