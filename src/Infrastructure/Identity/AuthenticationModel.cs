using ElarabyCA.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ElarabyCA.Infrastructure.Identity
{
    public class AuthenticationModel : IAuthenticationModel
    {
        public List<string> Roles { get; set; }
        public List<string> Claims { get; set; }
        public string Name { get; set; }
        public string Picture { get; set; }
    }
}
