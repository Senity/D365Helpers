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
        

        public static string GetAuthenticationHeader(bool useWebAppAuthentication = false)
        {
            return OAuthHelper.GetAuthenticationHeader(ClientConfig.Default, useWebAppAuthentication);
        }

        /// <summary>
        /// Retrieves an authentication header from the service.
        /// </summary>
        /// <returns>The authentication header for the Web API call.</returns>
        public static string GetAuthenticationHeader(ClientConfig config, bool useWebAppAuthentication = false)
        {            
            string aadTenant = config.ActiveDirectoryTenant;
            string aadClientAppId = config.ActiveDirectoryClientAppId;
            string aadClientAppSecret = config.ActiveDirectoryClientAppSecret;
            string aadResource = config.ActiveDirectoryResource;

            AuthenticationContext authenticationContext = new AuthenticationContext(aadTenant, false);
            AuthenticationResult authenticationResult;

            if (useWebAppAuthentication)
            {
                if (string.IsNullOrEmpty(aadClientAppSecret))
                {
                    throw new Exception("Failed OAuth by empty application secret. Fill ClientConfig first.");
                }

                try
                {
                    // OAuth through application by application id and application secret.
                    var creadential = new ClientCredential(aadClientAppId, aadClientAppSecret);
                    authenticationResult = authenticationContext.AcquireTokenAsync(aadResource, creadential).Result;
                }
                catch (Exception ex)
                {
                    throw new Exception("Failed to authenticate with Azure Active Directory by application.", ex);
                }
            }
            else
            {
                // OAuth through username and password.
                string username = config.UserName;
                string password = config.Password;

                if (string.IsNullOrEmpty(password))
                {
                    throw new Exception("Failed OAuth by empty password. Fill ClientConfig first.");
                }

                try
                {
                    // Get token object
                    var userCredential = new UserPasswordCredential(username, password); ;
                    authenticationResult = authenticationContext.AcquireTokenAsync(aadResource, aadClientAppId, userCredential).Result;
                }
                catch (Exception ex)
                {
                    throw new Exception("Failed to authenticate with Azure Active Directory by the credential.", ex);
                }
            }

            // Create and get JWT token
            return authenticationResult.CreateAuthorizationHeader();
        }
    }
}
