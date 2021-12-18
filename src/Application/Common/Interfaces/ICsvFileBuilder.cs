using ElarabyCA.Application.TodoLists.Queries.ExportTodos;
using System.Collections.Generic;

namespace ElarabyCA.Application.Common.Interfaces
{
    public interface ICsvFileBuilder
    {
        byte[] BuildTodoItemsFile(IEnumerable<TodoItemRecord> records);
    }
}
