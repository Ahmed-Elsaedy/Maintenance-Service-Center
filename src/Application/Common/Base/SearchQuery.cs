using System;
using System.Collections.Generic;
using System.Text;

namespace ElarabyCA.Application.Common.Base
{
    public abstract class SearchQuery
    {
        public string Sort { get; set; }
        public int? PageIndex { get; set; }
        public int? PageLength { get; set; }
        public string Filter { get; set; }
    }
}
