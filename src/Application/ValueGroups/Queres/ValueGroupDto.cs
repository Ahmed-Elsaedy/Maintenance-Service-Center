using ElarabyCA.Application.Common.Mappings;
using ElarabyCA.Domain.Entities;

namespace ElarabyCA.Application.ValueGroups.Queres
{
    public class ValueGroupDto : IMapFrom<ValueGroup>
    {
        public int ValueGroupId { get; set; }
        public string Value { get; set; }
        public string Group { get; set; }
    }
}
