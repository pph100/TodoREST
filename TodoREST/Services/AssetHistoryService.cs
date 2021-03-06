﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xamarin.Forms;
using System.Net;
using System.Web;
using System.Globalization;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;

namespace TodoREST
{
    public class AssetHistoryService : INotifyPropertyChanged, IAssetHistoryService
    {
        HttpClient assetHistoryClient;

        string assetHistoryTicker;

        private ObservableCollection<AssetHistory> __assetHistory;

        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<AssetHistory> _AssetHistory
        {
            get => __assetHistory;
            private set
            {
                if (__assetHistory != value)
                {
                    __assetHistory = value;
                    NotifyPropertyChanged("_AssetHistory");
                }
            }
        }

        public List<ObservableCollection<AssetHistory>> _AssetHistories { get; private set; }

        public ObservableCollection<AssetTotalHistory> _AssetTotalHistory { get; private set; }

        // neu
        private double maximum;

        public double Maximum
        {
            get { return maximum; }
            set
            {
                if (maximum != value)
                {
                    maximum = value;
                    // PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Maximum"));
                    NotifyPropertyChanged("Maximum");
                }

            }
        }


        public AssetHistoryService()
        {
            assetHistoryClient = new HttpClient();
            assetHistoryClient.MaxResponseContentBufferSize = 256000;
            assetHistoryTicker = "";
            _AssetHistory = new ObservableCollection<AssetHistory>();
            _AssetHistories = new List<ObservableCollection<AssetHistory>>();
        }

        public bool setJustAssetTicker(string newTicker)
        {
            if (newTicker == null || newTicker == "")
            {
                return false;
            }

            if (!newTicker.Equals(assetHistoryTicker))
            {
                assetHistoryTicker = newTicker;
                return true;
            }
            return false;
        }


        public async Task<ObservableCollection<AssetHistory>> getAssetHistory()
        {
            // return _AssetHistory;
            return await RefreshData();
        }


        public async Task<ObservableCollection<AssetHistory>> setAssetHistoryTicker(string newTicker)
        {
            if (newTicker == null || newTicker == "")
            {
                return null;
            }

            if (!newTicker.Equals(assetHistoryTicker))
            {
                assetHistoryTicker = newTicker;
                _AssetHistory = await this.RefreshData();
            }
            return _AssetHistory;
        }


        public async Task<ObservableCollection<AssetHistory>> setAssetHistoryTicker()
        {
            _AssetHistory = await this.RefreshData();
            return _AssetHistory;
        }


        public async Task<ObservableCollection<AssetHistory>> RefreshData()
        {
            if (assetHistoryTicker == null || assetHistoryTicker == "")
            {
                return (ObservableCollection<AssetHistory>)null;
            }

            // aktuelle Liste ist noch leer
            if (_AssetHistories == null || _AssetHistories.Count < 1)
            {
                _AssetHistory = await this.RefreshDataAsync();
                return (_AssetHistory);
            }
            else
            {
                // prüfen, ob aktuelle Liste bereits einen Eintrag für den aktuellen Ticker enthält
                foreach (var _assetHistoryList in _AssetHistories)
                {
                    if ((_assetHistoryList.Count != 0) && (_assetHistoryList[0] != null) && _assetHistoryList[0].AssetTicker.Equals(assetHistoryTicker))
                    {
                        // ja, es gibt schon einen Eintrag mit dem Ticker in der Liste
                        _AssetHistory = _assetHistoryList;
                        return (_AssetHistory);
                    }
                }
            }
            // nein, Liste muss erweitert werden
            return (_AssetHistory = await this.RefreshDataAsync());
        }


        public async Task<ObservableCollection<AssetHistory>> RefreshDataAsync()
        {
            if (assetHistoryTicker == null || assetHistoryTicker == "")
            {
                return (ObservableCollection<AssetHistory>)null;
            }

            var uri = new Uri(string.Format(Constants.AssetHistoryUrl, assetHistoryTicker));

            try
            {
                var response = await assetHistoryClient.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    _AssetHistory = JsonConvert.DeserializeObject<ObservableCollection<AssetHistory>>(content);
                    if(_AssetHistory.Count == 0)
                    {
                        return (ObservableCollection<AssetHistory>)null;
                    }
                    _AssetHistories.Add(_AssetHistory);
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

            return _AssetHistory;
        }


        protected void NotifyPropertyChanged(String propertyName = null)
        {
            var propertyChanged = this.PropertyChanged;
            if (propertyChanged != null)
            // if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }


        public async Task<ObservableCollection<AssetTotalHistory>> getAssetTotalHistory()
        {
            var uri = new Uri(string.Format(Constants.AssetTotalHistoryUrl));

            try
            {
                var response = await assetHistoryClient.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    _AssetTotalHistory = JsonConvert.DeserializeObject<ObservableCollection<AssetTotalHistory>>(content);
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
            return _AssetTotalHistory;
        }
    }

}

