using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TodoREST
{
    public class PersonManager
    {
        IPersonService personService;

        public PersonManager(IPersonService service)
        {
            personService = service;
        }

        public Task<List<PersonItem>> GetTasksAsync()
        {
            return personService.RefreshDataAsync();
        }

        public Task SaveTaskAsync(PersonItem item, bool isNewItem = false)
        {
            return personService.SavePersonItemAsync(item, isNewItem);
        }

        public Task DeleteTaskAsync(PersonItem item)
        {
            return personService.DeletePersonItemAsync(item.ID);
        }
    }
}
