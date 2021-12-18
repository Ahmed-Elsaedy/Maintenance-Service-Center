using System;
using System.Collections.Generic;
using System.Text;

namespace ElarabyCA.Application.Common.Base
{
    public abstract class SearchQueryViewModel<T> where T : class
    {
        public List<T> Data { get; set; }
        public int TotalRecords { get; set; }
    }
}
