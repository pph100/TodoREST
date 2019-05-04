using System.Collections.Generic;
using System.Threading.Tasks;

namespace TodoREST
{
    public interface IRestService
    {
        Task<List<TodoItem>> RefreshDataAsync();

        Task SaveTodoItemAsync(TodoItem item, bool isNewItem);

        Task DeleteTodoItemAsync(string id);
    }

    public interface IPersonService
    {
        Task<List<PersonItem>> RefreshDataAsync();

        Task SavePersonItemAsync(PersonItem item, bool isNewItem);

        Task DeletePersonItemAsync(string id);
    }
}
