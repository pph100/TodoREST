using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace TodoREST
{
    public partial class CryptoListPage : ContentPage
    {

        enum ViewSortOrder : int
        {
            unsorted = 0,
            sortByName = 1,
            sortByStock = 2,
            sortByPrice = 3,
            sortByValue = 4
        };

        bool sortDescending = true;

        ViewSortOrder sortOrder = ViewSortOrder.unsorted;

        public CryptoListPage()
        {
            InitializeComponent();
            sortOrder = ViewSortOrder.sortByValue;
            sortDescending = true;

            listView.RefreshCommand = new Command(async () =>
            {
                await RefreshDataAsync();
                listView.IsRefreshing = false;
            });

        }


        private async Task _refresh(List<CryptoItem> cryptoList)
        {
            var totalValue = App.CryptoItemManager.getTotalValue();
            var newlist = cryptoList;

            if (sortDescending)
            {
                switch (sortOrder)
                {
                    case ViewSortOrder.sortByName:
                        newlist = newlist.OrderByDescending(x => x.cryptoName).ToList();
                        break;
                    case ViewSortOrder.sortByPrice:
                        newlist = newlist.OrderByDescending(x => x.priceAsDouble).ToList();
                        break;
                    case ViewSortOrder.sortByStock:
                        newlist = newlist.OrderByDescending(x => x.stockAsDouble).ToList();
                        break;
                    case ViewSortOrder.sortByValue:
                        newlist = newlist.OrderByDescending(x => x.valueAsDouble).ToList();
                        break;
                    default:
                        break;
                }
            }
            else
            {
                switch (sortOrder)
                {
                    case ViewSortOrder.sortByName:
                        newlist = newlist.OrderBy(x => x.cryptoName).ToList();
                        break;
                    case ViewSortOrder.sortByPrice:
                        newlist = newlist.OrderBy(x => x.priceAsDouble).ToList();
                        break;
                    case ViewSortOrder.sortByStock:
                        newlist = newlist.OrderBy(x => x.stock).ToList();
                        break;
                    case ViewSortOrder.sortByValue:
                        newlist = newlist.OrderBy(x => x.valueAsDouble).ToList();
                        break;
                    default:
                        break;
                }

            }
            listView.ItemsSource = newlist;
            xTotal.Text = totalValue;
            xSum.Text = "Total:";
            await App.CryptoItemManager.SaveAssetValues(newlist);
        }


        private async Task RefreshData()
        {
            var cryptoList = await App.CryptoItemManager.Refresh();
            await this._refresh(cryptoList);
        }


        private async Task RefreshDataAsync()
        {
            var cryptoList = await App.CryptoItemManager.RefreshAsync();
            await this._refresh(cryptoList);
        }


        void OnAssetSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var cryptoItem = e.SelectedItem as CryptoItem;
            var asset = App.CryptoItemManager.FindAssetByTicker(cryptoItem.ticker.cryptoCode);
            var assetPage = new AssetPage();

            assetPage.BindingContext = asset;
            Navigation.PushAsync(assetPage);
        }


        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await RefreshDataAsync();
        }


        public async void OnCancel(object sender, EventArgs e)
        {
            await RefreshData();
        }


        private async void NameTapped(object sender, EventArgs e)
        {
            // var _label = sender as Label;
            switch (sortOrder)
            {
                case ViewSortOrder.sortByName:
                    sortDescending = !sortDescending;
                    await this.RefreshData();
                    /*
                    if (sortDescending)
                    {
                        _label.Text += " ▾";
                    }
                    else
                    {
                        _label.Text += " ▴";
                    }
                    */
                    break;

                default:
                    sortDescending = true;
                    sortOrder = ViewSortOrder.sortByName;
                    await this.RefreshData();
                    break;
            }
        }


        private async void PriceTapped(object sender, EventArgs e)
        {
            switch (sortOrder)
            {
                case ViewSortOrder.sortByPrice:
                    sortDescending = !sortDescending;
                    await this.RefreshData();
                    break;

                default:
                    sortDescending = true;
                    sortOrder = ViewSortOrder.sortByPrice;
                    await this.RefreshData();
                    break;
            }
        }


        private async void StockTapped(object sender, EventArgs e)
        {
            switch (sortOrder)
            {
                case ViewSortOrder.sortByStock:
                    sortDescending = !sortDescending;
                    await this.RefreshData();
                    break;

                default:
                    sortDescending = true;
                    sortOrder = ViewSortOrder.sortByStock;
                    await this.RefreshData();
                    break;
            }
        }


        private async void ValueTapped(object sender, EventArgs e)
        {
            switch (sortOrder)
            {
                case ViewSortOrder.sortByValue:
                    sortDescending = !sortDescending;
                    await this.RefreshData();
                    break;

                default:
                    sortDescending = true;
                    sortOrder = ViewSortOrder.sortByValue;
                    await this.RefreshData();
                    break;
            }
        }

    }
}