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
using System.Reflection;

namespace TodoREST
{
    public class CryptoService : ICryptoService
    {
        HttpClient cryptoClient;
        HttpClient assetClient;
        HttpClient comodityClient;

        public List<Asset> Assets;
        public Asset _asset;

        public List<Comodity> Comodities;
        public Comodity _comodity;

        public List<CryptoItem> CryptoItems { get; private set; }

        public string TotalValue { get; set; }

        // public CryptoItem _item { get; private set; }

        public CryptoService()
        {
            cryptoClient = new HttpClient();
            cryptoClient.MaxResponseContentBufferSize = 256000;

            assetClient = new HttpClient();
            assetClient.MaxResponseContentBufferSize = 256000;

            comodityClient = new HttpClient();
            comodityClient.MaxResponseContentBufferSize = 256000;

            CryptoItems = new List<CryptoItem>();
            // _item = new CryptoItem();

            Assets = new List<Asset>();
            _asset = new Asset();

            Comodities = new List<Comodity>();
            _comodity = new Comodity();

            TotalValue = "";
        }


        public async Task<List<CryptoItem>> RefreshData()
        {
            if (Assets == null || Assets.Count < 1)
            {
                Assets = await this.RefreshAssets();
                App._debug("CryptoService:RefreshData()", "RefreshAssets() called");
            }

            if (Comodities == null || Comodities.Count < 1)
            {
                Comodities = await this.RefreshComodities();
                App._debug("CryptoService:RefreshData()", "RefreshComodities() called");
            }

            if (CryptoItems == null || CryptoItems.Count < 1)
            {
                CryptoItems = await this.RefreshDataAsync();
                App._debug("CryptoService:RefreshData()", "RefreshDataAsync() called");
            }

            App._debug("CryptoService:RefreshData()", "function ended");
            return CryptoItems;
        }


        // diese funktion liefert richtige werte!!
        public async Task<List<CryptoItem>> RefreshDataAsync()
        {
            /*
            Assets = await this.RefreshAssetsAsync();
            Comodities = await this.RefreshComoditiesAsync();
            */

            if (Assets == null || Assets.Count < 1)
            {
                Assets = await this.RefreshAssets();
                App._debug("CryptoService:RefreshDataAsync()", "RefreshAssets() called");
            }

            if (Comodities == null || Comodities.Count < 1)
            {
                Comodities = await this.RefreshComodities();
                App._debug("CryptoService:RefreshDataAsync()", "RefreshComodities() called");
            }

            // funktioniert das???
            CryptoItems = new List<CryptoItem>();

            TotalValue = "";
            double _total = 0.0f;

            foreach (var asset in Assets)
            {
                if (asset.IncludeInList)
                {
                    if (asset.AssetClass == "Crypto")
                    {
                        var uri = new Uri(string.Format(Constants.CN_BaseURL, asset.SearchString));

                        try
                        {
                            double EPSILON = 0.01;
                            var response = await cryptoClient.GetAsync(uri);
                            if (response.IsSuccessStatusCode)
                            {
                                var content = await response.Content.ReadAsStringAsync();
                                var _item = JsonConvert.DeserializeObject<CryptoItem>(content);

                                if (_item.ticker != null && _item.ticker.@base != "")
                                {
                                    string MyDate = DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss");
                                    // _item.DttmLastUpdated = MyDate;
                                    _item.ticker.cryptoCode = _item.ticker.@base;
                                    _item.stock = (Double.Parse(asset.AssetStock, new CultureInfo("en-US"))).ToString(new CultureInfo("de-DE"));
                                    _item.stockAsDouble = (Double.Parse(asset.AssetStock, new CultureInfo("en-US")));
                                    _item.cryptoName = asset.AssetName;
                                    _item.searchString = asset.SearchString;

                                    var prettyPrice = (Double.Parse(_item.ticker.price, new CultureInfo("en-US")));
                                    _item.prettyPrice = prettyPrice > 2.0 ? prettyPrice.ToString("C2", new CultureInfo("de-DE")) : prettyPrice.ToString("C4", new CultureInfo("de-DE"));
                                    _item.priceAsDouble = (Double.Parse(_item.ticker.price, new CultureInfo("en-US")));
                                    _item.lastPrice = (Double.Parse(asset.AssetValue, new CultureInfo("en-US")));
                                    _item.increased = _item.priceAsDouble > _item.lastPrice ? true : false;
                                    // _item.decreased = (_item.priceAsDouble < _item.lastPrice);
                                    _item.decreased = _item.priceAsDouble < _item.lastPrice ? true : false;
                                    _item.stayedFlat = ((Math.Abs(_item.priceAsDouble - _item.lastPrice) < EPSILON) || (Math.Abs(_item.lastPrice) < 0.1));
                                    _item.lastPrice = _item.priceAsDouble;

                                    var prettyValue = (Double.Parse(asset.AssetStock, new CultureInfo("en-US")) * Double.Parse(_item.ticker.price, new CultureInfo("en-US"))).ToString("C2", new CultureInfo("de-DE"));
                                    _item.value = prettyValue;
                                    _item.valueAsDouble = _item.priceAsDouble * _item.stockAsDouble;
                                    _total += (Double.Parse(asset.AssetStock, new CultureInfo("en-US")) * Double.Parse(_item.ticker.price, new CultureInfo("en-US")));
                                    TotalValue = _total.ToString("C2", new CultureInfo("de-DE"));
                                    CryptoItems.Add(_item);

                                    asset.AssetValue = _item.ticker.price;
                                    asset.prettyValue = _item.prettyPrice;
                                    asset.AssetValueDttm = MyDate;
                                    // asset.AssetStock = _item.stock;
                                }
                            }
                            // TODO: Else-Zweig!!!
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

                    // Add Gold and Silver
                    if (asset.AssetClass == "Comodity")
                    {

                        //
                        // Hier weitermachen: 
                        //
                        // 1. search actual asset in comodities
                        // 2. 
                        //

                        foreach (var _c in Comodities)
                        {
                            double EPSILON = 0.1;

                            if (_c.category == asset.AssetName)
                            {
                                var _item = new CryptoItem();
                                _item.ticker = new TickerItem();
                                string MyDate = DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss");

                                _item.ticker.cryptoCode = asset.AssetTicker;
                                _item.ticker.price = _c.price_per_gram;
                                _item.stock = (Double.Parse(asset.AssetStock, new CultureInfo("en-US"))).ToString(new CultureInfo("de-DE"));
                                _item.stockAsDouble = (Double.Parse(asset.AssetStock, new CultureInfo("en-US")));
                                _item.cryptoName = asset.AssetName;
                                _item.searchString = asset.SearchString;

                                var prettyPrice = (Double.Parse(_item.ticker.price, new CultureInfo("de-DE")));
                                _item.prettyPrice = prettyPrice > 2.0 ? prettyPrice.ToString("C2", new CultureInfo("de-DE")) : prettyPrice.ToString("C4", new CultureInfo("de-DE"));
                                _item.priceAsDouble = (Double.Parse(_item.ticker.price, new CultureInfo("de-DE")));
                                _item.lastPrice = (asset.AssetValue == null) ? (Double)0.0f : (Double.Parse(asset.AssetValue, new CultureInfo("de-DE")));

                                _item.increased = _item.priceAsDouble > _item.lastPrice ? true : false;
                                // _item.decreased = (_item.priceAsDouble < _item.lastPrice);
                                _item.decreased = _item.priceAsDouble < _item.lastPrice ? true : false;
                                _item.stayedFlat = ((Math.Abs(_item.priceAsDouble - _item.lastPrice) < EPSILON) || (Math.Abs(_item.lastPrice) < 0.001));

                                _item.lastPrice = _item.priceAsDouble;

                                var prettyValue = (Double.Parse(asset.AssetStock, new CultureInfo("en-US")) * Double.Parse(_item.ticker.price, new CultureInfo("de-DE"))).ToString("C2", new CultureInfo("de-DE"));
                                _item.value = prettyValue;
                                _item.valueAsDouble = _item.priceAsDouble * _item.stockAsDouble;
                                _total += (Double.Parse(asset.AssetStock, new CultureInfo("en-US")) * Double.Parse(_item.ticker.price, new CultureInfo("de-DE")));
                                TotalValue = _total.ToString("C2", new CultureInfo("de-DE"));
                                CryptoItems.Add(_item);

                                asset.AssetValue = _item.ticker.price;
                                asset.prettyValue = _item.prettyPrice;
                                asset.AssetValueDttm = MyDate;

                            }
                        }

                    }

                    // 
                    // remove "await" for test purposes
                    // await this.SaveAssetAsync(asset);
                    try
                    {
                        App._debug("CryptoService:RefreshDataAsync()", "NEW/NO ASYNC: about to call SaveAssetAsync(" + asset.AssetTicker + ")");
                        SaveAssetAsync(asset).ConfigureAwait(false);
                        App._debug("CryptoService:RefreshDataAsync()", "NEW/NO ASYNC: call to SaveAssetAsync(" + asset.AssetTicker + ") has ended.");
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine(@"               ERROR {0}", ex.Message);
                        Debug.WriteLine(@"Unsuccessful call to save data in DB @{0}", System.DateTime.Now);
                        await App._mainPage.DisplayAlert(
                            "Issue with saving data in DB",
                            "Attempt to save Data in DB for Asset " + asset.AssetTicker + " @" + System.DateTime.Now + " revealed: " + ex.Message,
                            "OK"
                        );
                    }
                }
                // on purpose no else clause: for items that are not included in list, no action is defined.
            }

            App._debug("CryptoService:RefreshDataAsync()", "function ended");
            return CryptoItems;
        }


        // hier sind zur Laufzeit beim erstn Call falsche Werte!
        public async Task<List<Asset>> RefreshAssets()
        {
            if (Assets == null || Assets.Count < 1)
            {
                App._debug("CryptoService:RefreshAssets()", "function called, about to call this.RefreshAssetsAsync()");
                Assets = await this.RefreshAssetsAsync();
            }
            return Assets;
        }


        public async Task<List<Asset>> RefreshAssetsAsync()
        {
            // achtung: diese funktion holt sich die daten aus der datenbank, nicht vom externen Provider!!!!
            var uri = new Uri(string.Format(Constants.AssetUrl, string.Empty));

            App._debug("CryptoService:RefreshAssetsAsync()", "function called");
            try
            {
                App._debug("CryptoService:RefreshAssetsAsync()", "REST call starting");
                var response = await assetClient.GetAsync(uri);
                App._debug("CryptoService:RefreshAssetsAsync()", "REST call returned");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    Assets = JsonConvert.DeserializeObject<List<Asset>>(content);
                    App._debug("CryptoService:RefreshAssetsAsync()", "Assets data structure built and parsed");

                    foreach (var asset in Assets)
                    {
                        if (asset.AssetValue != null && asset.AssetValue != "")
                        {
                            string myDate = DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss");
                            asset.AssetValueDttm = myDate;
#if DEBUG
                            Debug.WriteLine("Asset {0} with value {1} set to date {2}", asset.AssetName, asset.AssetValue, myDate);
#endif
                        }
                        else
                        {
                            Debug.WriteLine("Asset {0} has no value: '{1}'", asset.AssetName, asset.AssetValue);
                        }
                    }
                }
                else
                {
                    App._debug("CryptoService:RefreshAssetsAsync()", "responsecode <> success: " + response.StatusCode.ToString());
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
            App._debug("CryptoService:RefreshAssetsAsync()", "function ended");
            return Assets;
        }


        public async Task<List<Comodity>> RefreshComodities()
        {
            if (Comodities == null || Comodities.Count < 1)
            {
                Comodities = await this.RefreshComoditiesAsync();
            }
            return Comodities;
        }


        public async Task<List<Comodity>> RefreshComoditiesAsync()
        {

            // Gold: XAU
            var uri = new Uri(string.Format(Constants.GoldUrl, string.Empty));
            try
            {
                var response = await comodityClient.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    Comodities = JsonConvert.DeserializeObject<List<Comodity>>(content);
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

            // Silber: XAG
            uri = new Uri(string.Format(Constants.SilberUrl, string.Empty));
            try
            {
                var response = await comodityClient.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var _comodities = JsonConvert.DeserializeObject<List<Comodity>>(content);
                    foreach (var _c in _comodities)
                    {
                        Comodities.Add(_c);
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
            // todo: hier speichern in DB

            return Comodities;
        }


        public async Task SaveAssetValues(List<CryptoItem> cryptoList)
        {
            foreach (var item in cryptoList)
            {
                var asset = FindAssetByTicker(item.ticker.cryptoCode);
                if (asset != null)
                {
                    App._debug("CryptoService:SaveAssetValues()", "about to call SaveAssetAsync(" + asset.AssetTicker + ")");
                    await this.SaveAssetAsync(asset);
                }
            }
        }


        public async Task SaveAssetAsync(Asset item, bool isNewItem = false)
        {
            App._debug("CryptoService:SaveAssetAsync(" + item.AssetTicker + ")", "function started");
            var uri = new Uri(string.Format(Constants.AssetUrl, string.Empty));

            if (item != null)
            {
                try
                {
                    Double result = (Double)0.0f;
                    string MyDate = DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss");
                    item.DttmLastUpdated = MyDate;
                    Double.TryParse(item.AssetStock, NumberStyles.AllowDecimalPoint, new CultureInfo("en-US"), out result);
                    var resultStr = result.ToString();
                    if (resultStr == null || resultStr == "0")
                    {
                        Double.TryParse(item.AssetStock, NumberStyles.AllowDecimalPoint, new CultureInfo("de-DE"), out result);
                        resultStr = result.ToString();
                        if (resultStr != null && resultStr != "0")
                        {
                            item.AssetStock = result.ToString(new CultureInfo("en-US"));
                        }

                    }
                    var json = JsonConvert.SerializeObject(item);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

#if DEBUG
                    Debug.WriteLine(@"Information: asset " + item.AssetTicker + " will NOT be saved with value " + item.prettyValue + " in database when debugging is active.");
#else
                    Debug.WriteLine(@"Information: asset " + item.AssetTicker + " going to be saved with value " + item.prettyValue + " in database:");
                    HttpResponseMessage response = null;
                    if (isNewItem)
                    {
                        response = await cryptoClient.PostAsync(uri, content);
                    }
                    else
                    {
                        response = await cryptoClient.PutAsync(uri, content);
                    }
                    if(!response.IsSuccessStatusCode) {
                        Debug.WriteLine(@"Response code for save " + item.AssetTicker + " in DB <> success: " + response.StatusCode.ToString());
                    }
                    else {
                        Debug.WriteLine(@"Save in DB finished for asset " + item.AssetTicker);
                    }
                    // TODO: handle response
#endif
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(@"               ERROR in function SaveAssetAsync: {0}", ex.Message);
                    await App._mainPage.DisplayAlert(
                        "Connection Issue",
                        "Connection to  " + uri + "@" + System.DateTime.Now + " revealed: " + ex.Message,
                        "OK"
                    );
                }
                App._debug("CryptoService:SaveAssetAsync()", "function ended");
            }
        }


        public Asset FindAssetByTicker(string tag)
        {
            if (Assets == null || Assets.Count < 1)
            {
                Debug.WriteLine(@"FindAssetByTicker: static Asset list seems to be empty");
                return null;
            }

            foreach (var asset in Assets)
            {
                if (asset.AssetTicker.Equals(tag))
                {
                    _asset = asset;
                    return _asset;
                }

            }
            return (Asset)null;
        }


        public string GetAssetPriceByTicker(string ticker)
        {
            if (Assets == null || Assets.Count < 1)
            {
                Debug.WriteLine(@"FindAssetByTicker: static Asset list seems to be empty");
                return null;
            }

            foreach (var asset in Assets)
            {
                if (asset.AssetTicker.Equals(ticker))
                {
                    _asset = asset;
                    return _asset.AssetValue;
                }
            }
            return (string)null;
        }



        public string getTotalValue()
        {
            return TotalValue;
        }


        public int getNumberOfCryptoItems()
        {
            return CryptoItems.Count;
        }


        public int getNumberOfAssetItems()
        {
            return Assets.Count;
        }


    }

}

