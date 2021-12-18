using System.Collections.Generic;
using System.Threading.Tasks;

namespace ElarabyCA.Application.Common.Interfaces
{
    public interface ICurrentUserService
    {
        string UserId { get; }
    }
}
