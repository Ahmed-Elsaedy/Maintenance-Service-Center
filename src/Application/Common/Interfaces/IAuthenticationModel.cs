using System;
using System.Collections.Generic;
using System.Text;

namespace ElarabyCA.Application.Common.Interfaces
{
    public interface IAuthenticationModel
    {
        List<string> Roles { get; set; }
        List<string> Claims { get; set; }
        string Name { get; set; }
        string Picture { get; set; }
    }
}
