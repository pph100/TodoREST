using System.Collections.Generic;
using System.Threading.Tasks;

namespace TodoREST
{
    public interface IComodityService
    {
        Task<List<Comodity>> RefreshDataAsync();
        Task<List<Comodity>> RefreshData();
    }
}