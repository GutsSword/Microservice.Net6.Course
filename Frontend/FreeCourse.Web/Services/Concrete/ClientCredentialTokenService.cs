using FreeCourse.Web.Models;
using FreeCourse.Web.Services.Interfaces;
using IdentityModel.AspNetCore.AccessTokenManagement;
using IdentityModel.Client;
using Microsoft.Extensions.Options;
using System.Net.Http;

namespace FreeCourse.Web.Services.Concrete
{
    public class ClientCredentialTokenService : IClientCredentialTokenService
    {
        private readonly ServiceApiSettings serviceApiSettings;
        private readonly ClientSettings clientSettings;
        private readonly IClientAccessTokenCache clientAccessTokenCache;   // Gelen tokeni cachlemek için.
        private readonly HttpClient httpClient;

        public ClientCredentialTokenService(IOptions<ServiceApiSettings> serviceApiSettings, IOptions<ClientSettings> clientSettings, HttpClient httpClient, IClientAccessTokenCache clientAccessTokenCache)
        {
            this.serviceApiSettings = serviceApiSettings.Value;
            this.clientSettings = clientSettings.Value;
            this.httpClient = httpClient;
            this.clientAccessTokenCache = clientAccessTokenCache;
        }

        public async Task<string> GetToken()
        {
            var currentToken = await clientAccessTokenCache.GetAsync("WebClientToken",default);

            if(currentToken is not null)
            {
                return currentToken.AccessToken;
            }
             
            var discoveryEndpoints = await httpClient.GetDiscoveryDocumentAsync(new DiscoveryDocumentRequest
            {
                Address = serviceApiSettings.IdentityBaseUri,
                Policy = new DiscoveryPolicy
                {
                    RequireHttps = false,
                },
            });

            if (discoveryEndpoints.IsError)
            {
                throw discoveryEndpoints.Exception;
            }

            var clientCredentialTokenRequest = new ClientCredentialsTokenRequest
            {
                ClientId = clientSettings.WebClient.ClientId,
                ClientSecret = clientSettings.WebClient.ClientSecret,
                Address = discoveryEndpoints.TokenEndpoint
            };

            var newToken = await httpClient.RequestClientCredentialsTokenAsync(clientCredentialTokenRequest);

            if(newToken.IsError)
            {
                throw newToken.Exception;
            }

            await clientAccessTokenCache.SetAsync("WebClientToken", newToken.AccessToken,newToken.ExpiresIn,default);

            return newToken.AccessToken;
        }
    }
}
