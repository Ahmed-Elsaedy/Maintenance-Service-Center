using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ElarabyCA.Application.Common.Interfaces;
using ElarabyCA.Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ElarabyCA.WebUI.Controllers
{
    [Authorize]
    public class CurrentUserController : ApiController
    {
        private ICurrentUserService _currentUserService;
        private UserManager<ApplicationUser> _userManager;
        public CurrentUserController(ICurrentUserService currentUserService,
            UserManager<ApplicationUser> userManager)
        {
            _currentUserService = currentUserService;
            _userManager = userManager;
        }

        [HttpGet("AuthenticationModel")]
        public ActionResult<AuthenticationModel> AuthenticationModel()
        {
            var user = _userManager.FindByIdAsync(_currentUserService.UserId).Result;
            var roles = _userManager.GetRolesAsync(user).Result;

            return new AuthenticationModel()
            {
                UserId = _currentUserService.UserId,
                Roles = roles.ToList()
            };
        }
    }

    public class AuthenticationModel
    {
        public string UserId { get; set; }
        public List<string> Roles { get; set; }
    }
}