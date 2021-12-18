using ElarabyCA.Application.Common.Models;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ElarabyCA.Application.Common.Interfaces
{
    public interface IIdentityService
    {
        Task<string> GetUserNameAsync(string userId);

        Task<(Result Result, string UserId)> CreateUserAsync(string userName, string password);

        Task<Result> DeleteUserAsync(string userId);

        Task<IAuthenticationModel> GetAuthenticationModel(string userId);
    }
}
