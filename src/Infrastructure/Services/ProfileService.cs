using ElarabyCA.Application.Common.Interfaces;
using ElarabyCA.Infrastructure.Identity;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using System.Security.Claims;
 
using System.Threading.Tasks;

namespace ElarabyCA.WebUI.Services
{
    public class ProfileService : IProfileService
    {
        private readonly IIdentityService _IdentityService;
        private readonly UserManager<ApplicationUser> _userManager;

        public ProfileService(UserManager<ApplicationUser> userManager, IIdentityService identityService)
        {
            _userManager = userManager;
            _IdentityService = identityService;
        }

        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var user = await _userManager.GetUserAsync(context.Subject);
            var model = await _IdentityService.GetAuthenticationModel(user.Id);
            var auth_model = JsonConvert.SerializeObject(model);
         
            context.IssuedClaims.Add(new Claim("auth_model", auth_model));
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            var user = await _userManager.GetUserAsync(context.Subject);
            context.IsActive = (user != null);
        }
    }
}