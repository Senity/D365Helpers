
namespace D365Helpers.Services
{
    class ClientConfig
    {
        public static ClientConfig Default { get { return ClientConfig.CloudAX; } }

        public static ClientConfig CloudAX = new ClientConfig()
        {

            //UriString = "https://devaos.cloudax.dynamics.com/",
            UriString = "",

            // Insert the correct username here for the actual test (If needed).
            UserName = "",
            // Insert the correct password here for the actual test (If needed).
            Password = "",

            //ActiveDirectoryResource = "https://devaos.cloudax.dynamics.com",
            ActiveDirectoryResource = "",
            //ActiveDirectoryTenant = "https://login.windows.net/DOMAIN.com",
            ActiveDirectoryTenant = "",
            //ActiveDirectoryClientAppId = "0831b191-b463-41f6-a86c-041272bdb340",
            ActiveDirectoryClientAppId = "",
            // Insert here the application secret when authenticate with AAD by the application
            //ActiveDirectoryClientAppSecret = "A/UcmIlmWWv7yhhV6/XokoQR582NQ/4/WUyuor0MbJQ=",
            ActiveDirectoryClientAppSecret = "",

            // Change TLS version of HTTP request from the client here
            // Ex: TLSVersion = "1.2"
            // Leave it empty if want to use the default version
            TLSVersion = "",
        };

        public string TLSVersion { get; set; }
        public string UriString { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ActiveDirectoryResource { get; set; }
        public string ActiveDirectoryTenant { get; set; }
        public string ActiveDirectoryClientAppId { get; set; }
        public string ActiveDirectoryClientAppSecret { get; set; }
    }
}
