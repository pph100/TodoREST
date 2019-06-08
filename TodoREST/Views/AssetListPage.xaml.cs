using System;
using System.Globalization;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace TodoREST
{
    public partial class CryptoListPage : ContentPage
    {

        public CryptoListPage()
        {
            InitializeComponent();

            listView.RefreshCommand = new Command(async () =>
            {
                await RefreshDataAsync();
                listView.IsRefreshing = false;
            });
        }


        private async Task RefreshData()
        {
            var cryptoList = await App.CryptoItemManager.Refresh();
            listView.ItemsSource = cryptoList;
        }


        private async Task RefreshDataAsync()
        {
            var cryptoList = await App.CryptoItemManager.RefreshAsync();
            listView.ItemsSource = cryptoList;
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
            await RefreshData();
        }


        public async void OnCancel(object sender, EventArgs e)
        {
            await RefreshData();
        }

    }
}