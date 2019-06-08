using System.Collections.Generic;
using System.Threading.Tasks;

namespace TodoREST
{
    public interface ICryptoService
    {
        Task<List<CryptoItem>> RefreshDataAsync();
        Task<List<CryptoItem>> RefreshData();
    }
}