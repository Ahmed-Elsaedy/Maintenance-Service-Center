using ElarabyCA.Application.Common.Mappings;
using ElarabyCA.Domain.Entities;
using System.Collections.Generic;

namespace ElarabyCA.Application.TodoLists.Queries.GetTodos
{
    public class TodoListDto : IMapFrom<TodoList>
    {
        public TodoListDto()
        {
            Items = new List<TodoItemDto>();
        }

        public int Id { get; set; }

        public string Title { get; set; }

        public IList<TodoItemDto> Items { get; set; }
    }
}
