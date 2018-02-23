using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System;

namespace D365Helpers.Services
{
    class OAuthHelper
    {
        /// <summary>
        /// The header to use for OAuth authentication.
        /// </summary>
        public const string OAuthHeader = "Authorization";

        /// <summary>
        /// Retrieves an authentication header from the service.
        /// </summary>
        /// <returns>The authentication header for the Web API call.</returns>
        public static string GetAuthenticationHeader(bool useWebAppAuthentication = false)
        {
            string aadTenant = ClientConfig.Default.ActiveDirectoryTenant;
            string aadClientAppId = ClientConfig.Default.ActiveDirectoryClientAppId;
            string aadClientAppSecret = ClientConfig.Default.ActiveDirectoryClientAppSecret;
            string aadResource = ClientConfig.Default.ActiveDirectoryResource;

            AuthenticationContext authenticationContext = new AuthenticationContext(aadTenant, false);
            AuthenticationResult authenticationResult;

            if (useWebAppAuthentication)
            {
                if (string.IsNullOrEmpty(aadClientAppSecret))
                {
                    throw new Exception("Failed OAuth by empty application secret. Fill CloudAX config first.");
                }

                try
                {
                    // OAuth through application by application id and application secret.
                    var creadential = new ClientCredential(aadClientAppId, aadClientAppSecret);
                    authenticationResult = authenticationContext.AcquireTokenAsync(aadResource, creadential).Result;
                }
                catch (Exception ex)
                {
                    throw new Exception("Failed to authenticate with Azure Active Directory by application.");
                }
            }
            else
            {
                // OAuth through username and password.
                string username = ClientConfig.Default.UserName;
                string password = ClientConfig.Default.Password;

                if (string.IsNullOrEmpty(password))
                {
                    throw new Exception("Failed OAuth by empty password. Fill CloudAX config first.");
                }

                try
                {
                    // Get token object
                    var userCredential = new UserPasswordCredential(username, password); ;
                    authenticationResult = authenticationContext.AcquireTokenAsync(aadResource, aadClientAppId, userCredential).Result;
                }
                catch (Exception ex)
                {
                    throw new Exception("Failed to authenticate with Azure Active Directory by the credential.");
                }
            }

            // Create and get JWT token
            return authenticationResult.CreateAuthorizationHeader();
        }
    }
}
