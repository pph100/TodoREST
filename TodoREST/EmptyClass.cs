﻿using System;
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
            Assets = await this.RefreshAssets();
            Comodities = await this.RefreshComodities();

            if (CryptoItems == null || CryptoItems.Count < 1)
            {
                CryptoItems = await this.RefreshDataAsync();
            }

            return CryptoItems;
        }


        public async Task<List<CryptoItem>> RefreshDataAsync()
        {
            Assets = await this.RefreshAssetsAsync();
            Comodities = await this.RefreshComoditiesAsync();

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
                                    _item.stayedFlat = ((_item.priceAsDouble == _item.lastPrice) || (Math.Abs(_item.lastPrice) < 0.1));
                                    _item.decreased = (_item.priceAsDouble < _item.lastPrice);
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
                        // 2. update asset with latest price (bid)
                        // 3. update CryptoItems List
                        //
                        var uri = new Uri(string.Format(Constants.CN_BaseURL, asset.SearchString));

                        try
                        {
                            var response = await cryptoClient.GetAsync(uri);
                            if (response.IsSuccessStatusCode)
                            {
                                var content = await response.Content.ReadAsStringAsync();
                                var _item = JsonConvert.DeserializeObject<CryptoItem>(content);

                                if (_item.ticker != null && _item.ticker.@base != "")
                                {
                                    string MyDate = DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss");
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
                                    _item.stayedFlat = ((_item.priceAsDouble == _item.lastPrice) || (Math.Abs(_item.lastPrice) < 0.1));
                                    _item.decreased = (_item.priceAsDouble < _item.lastPrice);
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
                }
            }

            return CryptoItems;
        }


        public async Task<List<Asset>> RefreshAssets()
        {
            if (Assets == null || Assets.Count < 1)
            {
                Assets = await this.RefreshAssetsAsync();
            }
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


        public async Task<List<Asset>> RefreshAssetsAsync()
        {
            var uri = new Uri(string.Format(Constants.AssetUrl, string.Empty));

            try
            {
                var response = await assetClient.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    Assets = JsonConvert.DeserializeObject<List<Asset>>(content);
                    foreach (var asset in Assets)
                    {
                        if (asset.AssetValue != null && asset.AssetValue != "")
                        {
                            string myDate = DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss");
                            asset.AssetValueDttm = myDate;
                            Debug.WriteLine("Asset {0} with value {1} set to date {2}", asset.AssetName, asset.AssetValue, myDate);
                        }
                        else
                        {
                            Debug.WriteLine("Asset {0} seems to have no value: '{1}'!!!", asset.AssetName, asset.AssetValue);
                        }
                    }
                }
                // TODO: handle unsuccessful response
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
            return Assets;
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

            return Comodities;
        }


        public async Task<List<Asset>> RefreshComoditiesAsyncTEMP()
        {
            var uri = new Uri(string.Format(Constants.GoldUrl, string.Empty));

            try
            {
                var response = await assetClient.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    Assets = JsonConvert.DeserializeObject<List<Asset>>(content);
                    foreach (var asset in Assets)
                    {
                        if (asset.AssetValue != null && asset.AssetValue != "")
                        {
                            string myDate = DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss");
                            asset.AssetValueDttm = myDate;
                            Debug.WriteLine("Asset {0} with value {1} set to date {2}", asset.AssetName, asset.AssetValue, myDate);
                        }
                        else
                        {
                            Debug.WriteLine("Asset {0} seems to have no value: '{1}'!!!", asset.AssetName, asset.AssetValue);
                        }
                    }
                }
                // TODO: handle unsuccessful response
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
            return Assets;
        }


        public async Task SaveAssetAsync(Asset item, bool isNewItem = false)
        {
            var uri = new Uri(string.Format(Constants.AssetUrl, string.Empty));

            try
            {
                string MyDate = DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss");
                item.DttmLastUpdated = MyDate;
                var json = JsonConvert.SerializeObject(item);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = null;
                if (isNewItem)
                {
                    response = await cryptoClient.PostAsync(uri, content);
                }
                else
                {
                    response = await cryptoClient.PutAsync(uri, content);
                }
                // TODO: handle response
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"               ERROR {0}", ex.Message);
                await App._mainPage.DisplayAlert(
                    "Connection Issue",
                    "Connection to  " + uri + "@" + System.DateTime.Now + " revealed: " + ex.Message,
                    "OK"
                );
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


        public async Task SaveAssetValues(List<CryptoItem> cryptoList)
        {
            foreach (var item in cryptoList)
            {
                var asset = FindAssetByTicker(item.ticker.cryptoCode);
                await this.SaveAssetAsync(asset);
            }
        }
    }

}

