using ElarabyCA.Application.Common.Mappings;
using ElarabyCA.Domain.Entities;

namespace ElarabyCA.Application.TodoLists.Queries.ExportTodos
{
    public class TodoItemRecord : IMapFrom<TodoItem>
    {
        public string Title { get; set; }

        public bool Done { get; set; }
    }
}
