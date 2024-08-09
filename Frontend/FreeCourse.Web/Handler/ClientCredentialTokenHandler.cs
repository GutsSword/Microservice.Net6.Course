using FreeCourse.Web.Exceptions;
using FreeCourse.Web.Services.Concrete;
using FreeCourse.Web.Services.Interfaces;
using System.Net.Http.Headers;

namespace FreeCourse.Web.Handler
{
    public class ClientCredentialTokenHandler : DelegatingHandler
    {

        private readonly IClientCredentialTokenService clientCredentialTokenService;

        public ClientCredentialTokenHandler(IClientCredentialTokenService clientCredentialTokenService)
        {
            this.clientCredentialTokenService = clientCredentialTokenService;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var getToken = await clientCredentialTokenService.GetToken();
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", getToken);

            var response = await base.SendAsync(request, cancellationToken);

            if(response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                throw new UnAuthorizeException();
            }

            return response;
        }
    }
}
