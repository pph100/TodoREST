using System.Collections.Generic;
using System.Threading.Tasks;

namespace TodoREST
{
    public interface IPersonService
    {
        Task<List<PersonItem>> RefreshDataAsync();

        Task SavePersonItemAsync(PersonItem item, bool isNewItem);

        Task DeletePersonItemAsync(string id);
    }
}
