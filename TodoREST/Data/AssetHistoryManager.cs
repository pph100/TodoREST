using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace TodoREST
{
    public class AssetHistoryManager
    {
        IAssetHistoryService assetHistoryService;


        public AssetHistoryManager(IAssetHistoryService service)
        {
            assetHistoryService = service;
        }


        public Task<ObservableCollection<AssetHistory>> Refresh()
        {
            return assetHistoryService.RefreshData();
        }


        public Task<ObservableCollection<AssetHistory>> RefreshAsync()
        {
            return assetHistoryService.RefreshDataAsync();
        }



        public ObservableCollection<AssetHistory> getAssetHistory()
        {
            return assetHistoryService.getAssetHistory();
        }


        public Task<ObservableCollection<AssetHistory>> setAssetHistoryTicker(string newTicker)
        {
            return assetHistoryService.setAssetHistoryTicker(newTicker);
        }


        public Task<ObservableCollection<AssetHistory>> setAssetHistoryTicker()
        {
            return assetHistoryService.setAssetHistoryTicker();
        }


        public bool setJustAssetTicker(string newTicker)
        {
            return assetHistoryService.setJustAssetTicker(newTicker);
        }
    }
}
