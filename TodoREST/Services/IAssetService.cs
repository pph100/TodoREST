using System.Collections.Generic;
using System.Threading.Tasks;

namespace TodoREST
{
    public interface IAssetService
    {
        Task<List<Asset>> RefreshDataAsync();
        Task<List<Asset>> RefreshData();
        Task<Asset> FindAssetByTicker(string tag);
    }
}