
using IdentityModel.Client;

namespace FreeCourse.Gateway.DelegateHandlers
{
    public class TokenExchangeDelegateHandler : DelegatingHandler
    {
        private readonly HttpClient httpClient;
        private readonly IConfiguration configuration;
        private string accessToken;

        public TokenExchangeDelegateHandler(HttpClient httpClient, IConfiguration configuration)
        {
            this.httpClient = httpClient;
            this.configuration = configuration;
        }

        private async Task<string> GetToken(string requestToken)
        {
            if (!string.IsNullOrEmpty(accessToken))
            {
                return accessToken;
            }

            var discoveryEndpoints = await httpClient.GetDiscoveryDocumentAsync(new DiscoveryDocumentRequest
            {
                Address = configuration["IdentityServerUrl"],
                Policy = new DiscoveryPolicy
                {
                    RequireHttps = false,
                },
            });

            if (discoveryEndpoints.IsError)
            {
                throw discoveryEndpoints.Exception;
            }

            TokenExchangeTokenRequest tokenExchangeTokenRequest = new TokenExchangeTokenRequest()
            {
                Address = discoveryEndpoints.TokenEndpoint,
                ClientId = configuration["ClientId"],
                ClientSecret = configuration["ClientSecret"],
                GrantType = configuration["GrantType"],
                SubjectToken = requestToken,
                SubjectTokenType = "urn:ietf:params:oauth:token-type:access-token",
                Scope = "openid FakePaymentFullPermission DiscountFullPermission",
            };

            var tokenResponse = await httpClient.RequestTokenExchangeTokenAsync(tokenExchangeTokenRequest);

            if (tokenResponse.IsError)
            {
                throw tokenResponse.Exception;
            }

            accessToken = tokenResponse.AccessToken;

            return accessToken;

        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var requestToken = request.Headers.Authorization.Parameter;
            var newToken = await GetToken(requestToken);

            request.SetBearerToken(newToken);


            return await base.SendAsync(request, cancellationToken);
        }
    }
}
