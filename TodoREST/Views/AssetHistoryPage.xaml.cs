using System;
using System.Collections.Generic;
using Xamarin.Essentials;
using Xamarin.Forms;
using Syncfusion.SfChart.XForms;
using System.Globalization;

namespace TodoREST
{
    public partial class AssetHistoryPage : ContentPage
    {
        private bool singleValue = true; // true: display chart with single Values per item per day
                                         // false: display chart with Total Values per item per day

        public AssetHistoryPage()
        {
            InitializeComponent();
            singleValue = true;
        }

        public AssetHistoryPage(bool SingleOrTotal)
        {
            InitializeComponent();
            singleValue = SingleOrTotal;
        }

        protected async override void OnAppearing()
        {
            var newItemsSource = App.AssetHistoryManager.getAssetHistory();

            myLineSeries.ItemsSource = newItemsSource;

            if (singleValue)
            {
                mySfChart.Title.Text = "Value History for " + newItemsSource[0].AssetName;
                myLineSeries.YBindingPath = "daily_avg";
            }
            else
            {
                mySfChart.Title.Text = "Total value History for " + newItemsSource[0].AssetName;
                myLineSeries.YBindingPath = "daily_value";
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
