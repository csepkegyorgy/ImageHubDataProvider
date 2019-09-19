namespace LogMeOut.ImageHub.DataProvider
{
    using LogMeOut.ImageHub.Interfaces.Util;

    public static class AppConfig
    {
        // FTP
        public const string Host = "imagehub.ftp.evennode.com";
        public const string UserName = "imagehub_d8c2b";
        public const string Password = "ftppass";
        public const int Port = 21;
        public const bool EnableSsl = false; 

        public static FtpConnectionInformation GetFtpConnectionInformation()
        {
            return new FtpConnectionInformation()
            {
                Host = Host,
                UserName = UserName,
                Password = Password,
                Port = Port,
                EnableSsl = EnableSsl
            };
        }
    }
}
