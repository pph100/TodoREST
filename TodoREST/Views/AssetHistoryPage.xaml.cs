using System;
using System.Collections.Generic;
using Xamarin.Essentials;
using Xamarin.Forms;
using Syncfusion.SfChart.XForms;
using System.Globalization;
using System.Collections.ObjectModel;

namespace TodoREST
{
    public partial class AssetHistoryPage : ContentPage
    {

        enum CallToBeMade
        {
            noCall = 0,
            callSingleValue = 1,
            callTotalValue = 2,
            callTotalSum = 3
        };

        CallToBeMade callType = CallToBeMade.noCall;
        // private bool singleValue = true; // true: display chart with single Values per item per day
        // false: display chart with Total Values per item per day

        public AssetHistoryPage()
        {
            InitializeComponent();
            callType = CallToBeMade.callSingleValue;
        }

        public AssetHistoryPage(bool SingleOrTotal)
        {
            InitializeComponent();
            if (SingleOrTotal == true)
            {
                callType = CallToBeMade.callSingleValue;
            }
            else
            {
                callType = CallToBeMade.callTotalValue;
            }

        }

        public AssetHistoryPage(int CallType)
        {
            InitializeComponent();
            callType = (CallToBeMade)CallType;
        }

        protected async override void OnAppearing()
        {
            // var newItemsSource = await App.AssetHistoryManager.getAssetHistory();
            ObservableCollection<AssetTotalHistory> newItemsTotalSource; // = await App.AssetHistoryManager.getAssetHistory();
            ObservableCollection<AssetHistory> newItemsSource;

            // myLineSeries.ItemsSource = newItemsSource;

            myLineSeries.ItemsSource = null;

            switch (callType)
            {
                case CallToBeMade.callSingleValue:
                    newItemsSource = await App.AssetHistoryManager.getAssetHistory();
                    myLineSeries.ItemsSource = newItemsSource;
                    mySfChart.Title.Text = "Value History for " + newItemsSource[0].AssetName;
                    myLineSeries.YBindingPath = "daily_avg";
                    myLineSeries.XBindingPath = "DT_DMY";
                    break;

                case CallToBeMade.callTotalValue:
                    newItemsSource = await App.AssetHistoryManager.getAssetHistory();
                    myLineSeries.ItemsSource = newItemsSource;
                    mySfChart.Title.Text = "Total value History for " + newItemsSource[0].AssetName;
                    myLineSeries.YBindingPath = "daily_value";
                    myLineSeries.XBindingPath = "DT_DMY";
                    break;

                case CallToBeMade.callTotalSum:
                    newItemsTotalSource = await App.AssetHistoryManager.getAssetTotalHistory();
                    myLineSeries.ItemsSource = newItemsTotalSource;
                    mySfChart.Title.Text = "Total overall Value History";
                    myLineSeries.YBindingPath = "daily_total";
                    myLineSeries.XBindingPath = "DT_DMY";
                    break;
            }

            base.OnAppearing();
        }

        /*
        private void YAxis_LabelCreated(object sender, Syncfusion.SfChart.XForms.ChartAxisLabelEventArgs e)
        {
            double value = Convert.ToDouble(e.LabelContent);
            e.LabelContent = value.ToString("#,###.##", new CultureInfo("en-US"));
        }
        */
    }
}
