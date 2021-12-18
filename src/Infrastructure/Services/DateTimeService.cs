using ElarabyCA.Application.Common.Interfaces;
using System;

namespace ElarabyCA.Infrastructure.Services
{
    public class DateTimeService : IDateTime
    {
        public DateTime Now => DateTime.Now;
    }
}
