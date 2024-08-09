using FreeCourse.Web.Models;
using FreeCourse.Web.Services.Interfaces;

namespace FreeCourse.Web.Services.Concrete
{
    
    public class UserService : IUserService
    {
        private readonly HttpClient _httpClient;
        private readonly IIdentityService _identityService;

        public UserService(HttpClient httpClient, IIdentityService identityService)
        {
            _httpClient = httpClient;
            _identityService = identityService;
        }

        public async Task<UserViewModel> GetUser()
        {
            return await _httpClient.GetFromJsonAsync<UserViewModel>("/api/user/getuser");
        }
    }
}
