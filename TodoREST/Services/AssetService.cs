using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xamarin.Forms;
using System.Net;
using System.Web;

namespace TodoREST
{
    public class CryptoService : ICryptoService
    {
        HttpClient cryptoClient;

        public List<CryptoItem> Items { get; private set; }

        public CryptoItem _item { get; private set; }

        public CryptoService()
        {
            cryptoClient = new HttpClient();
            cryptoClient.MaxResponseContentBufferSize = 256000;
            // cryptoClient.DefaultRequestHeaders.Authorization = new AuthenticatierValue("Apikey", Constants.CryptoCompareAPIKey);
            Items = new List<CryptoItem>();
            _item = new CryptoItem();
        }

        public async Task<List<CryptoItem>> RefreshData()
        {
            if (Items == null || Items.Count < 1)
            {
                Items = await this.RefreshDataAsync();
            }
            return Items;

        }

        public async Task<List<CryptoItem>> RefreshDataAsync()
        {
            // Items = new List<CryptoItem>();
            // _item = null;
            string[] _ccyPairs = {
                "BTC-EUR",
                "LTC-EUR",
                "BCH-EUR",
                "IOTA-EUR",
                "XRP-EUR",
                "ETH-EUR",
                "BSV-EUR",
                "TRX-EUR",
                "SWFTC-EUR",
                "DOGE-EUR",
                "GNT-EUR",
                "BCN-EUR"
             };

            foreach (var _ccyPair in _ccyPairs)
            {
                var uri = new Uri(string.Format(Constants.CN_BaseURL, _ccyPair));

                try
                {
                    var response = await cryptoClient.GetAsync(uri);
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        _item = JsonConvert.DeserializeObject<CryptoItem>(content);

                        if (_item.ticker != null && _item.ticker.@base != "")
                        {
                            string MyDate = DateTime.Now.ToString("dd.MM.yy HH:mm:ss");
                            _item.DttmLastUpdated = MyDate;
                            _item.ticker.cryptoCode = _item.ticker.@base;
                            _item.stock = "3.041";
                            _item.value = (Double.Parse(_item.stock) * Double.Parse(_item.ticker.price)).ToString();
                            Items.Add(_item);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(@"               ERROR {0}", ex.Message);
                    Debug.WriteLine(@"Unsuccessful connect to URI {0} at {1}", uri, System.DateTime.Now);
                    await App._mainPage.DisplayAlert(
                        "Connection Issue",
                        "Connection to  " + uri + "@" + System.DateTime.Now + " revealed: " + ex.Message,
                        "OK"
                    );
                }
            }

            return Items;
        }

    }

}

