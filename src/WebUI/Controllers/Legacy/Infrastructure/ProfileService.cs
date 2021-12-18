using ElarabyCA.Infrastructure.Identity;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ElarabyCore3.Web.Infrastructure
{
    public class ProfileService_OLD : IProfileService
    {
        protected UserManager<ApplicationUser> _userManager;

        public ProfileService_OLD(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var user = await _userManager.GetUserAsync(context.Subject);
            var userClaims = await _userManager.GetClaimsAsync(user);
            var userRoles = await _userManager.GetRolesAsync(user);
            context.IssuedClaims.Add(new Claim("roles", string.Join(';', userRoles)));

            var displayNameClaim = userClaims.SingleOrDefault(x => x.Type == ClaimTypes.Name);
            if (displayNameClaim != null)
                context.IssuedClaims.Add(new Claim("name", displayNameClaim.Value));

            var logins = await _userManager.GetLoginsAsync(user);
            var facebookLogin = logins.FirstOrDefault(x => x.LoginProvider == "Facebook");
            if (facebookLogin != null)
                context.IssuedClaims.Add(new Claim("picture",
                    $"https://graph.facebook.com/{facebookLogin.ProviderKey}/picture?type=small"));
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            var user = await _userManager.GetUserAsync(context.Subject);
            context.IsActive = (user != null);
        }
    }
}
