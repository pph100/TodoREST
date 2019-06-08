using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TodoREST
{
    public class CryptoItemManager
    {
        ICryptoService cryptoService;

        public CryptoItemManager(ICryptoService service)
        {
            cryptoService = service;
        }

        public Task<List<CryptoItem>> Refresh()
        {
            return cryptoService.RefreshData();
        }

        public Task<List<CryptoItem>> RefreshAsync()
        {
            return cryptoService.RefreshDataAsync();
        }

    }
}
