using System;
using System.Collections.Generic;
using Xamarin.Essentials;
using Xamarin.Forms;
using Syncfusion.SfChart.XForms;
using System.Globalization;
using System.Collections.ObjectModel;
using Syncfusion.Licensing.math;

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

        NumericalAxis secondaryAxis = null;

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
            ObservableCollection<AssetTotalHistory> newItemsTotalSource;
            ObservableCollection<AssetHistory> newItemsSource;

            secondaryAxis = new NumericalAxis();
            secondaryAxis.LabelCreated += SecondaryAxis_LabelCreated;       // set CallBack function, see below
            mySfChart.SecondaryAxis = secondaryAxis;                        // connect to visible Element

            myLineSeries.ItemsSource = null;

            switch (callType)
            {
                case CallToBeMade.callSingleValue:
                    secondaryAxis.ActualRangeChanged += SecondaryAxis_ActualRangeChanged;       // set CallBack function, see below
                    newItemsSource = await App.AssetHistoryManager.getAssetHistory();
                    myLineSeries.ItemsSource = newItemsSource;
                    mySfChart.Title.Text = "Value History for " + newItemsSource[0].AssetName;
                    myLineSeries.YBindingPath = "daily_avg";
                    myLineSeries.XBindingPath = "DT_DMY";
                    break;

                case CallToBeMade.callTotalValue:
                    secondaryAxis.ActualRangeChanged += SecondaryAxis_ActualRangeChanged;       // set CallBack function, see below
                    newItemsSource = await App.AssetHistoryManager.getAssetHistory();
                    myLineSeries.ItemsSource = newItemsSource;
                    mySfChart.Title.Text = "Total value History for " + newItemsSource[0].AssetName;
#if RELEASE
                    myLineSeries.YBindingPath = "daily_value_formatted";
#else
                    myLineSeries.YBindingPath = "daily_value";
#endif
                    myLineSeries.XBindingPath = "DT_DMY";
                    break;

                case CallToBeMade.callTotalSum:                                                 // here: no callback function, does not work
                    newItemsTotalSource = await App.AssetHistoryManager.getAssetTotalHistory();
                    myLineSeries.ItemsSource = newItemsTotalSource;
                    mySfChart.Title.Text = "Total overall Value History";
                    myLineSeries.YBindingPath = "daily_total";
                    myLineSeries.XBindingPath = "DT_DMY";
                    break;
            }

            base.OnAppearing();
        }


        private void SecondaryAxis_LabelCreated(object sender, Syncfusion.SfChart.XForms.ChartAxisLabelEventArgs e)
        {

            // TODO: determine safely and differentiate between German ("4,35" or "5.601,00") and US ("4.35" and "5,601.00") format 
#if RELEASE
            var yVal = Convert.ToDouble(e.LabelContent, new CultureInfo("de-DE"));
#else
            var yVal = Double.Parse(e.LabelContent, new CultureInfo("en-US"));
#endif
            if (yVal > 999.9)
                e.LabelContent = yVal.ToString("C0", new CultureInfo("de-DE"));
            else
                e.LabelContent = yVal > 2.0 ? yVal.ToString("C2", new CultureInfo("de-DE")) : yVal.ToString("C4", new CultureInfo("de-DE"));
        }


        private void SecondaryAxis_ActualRangeChanged(object sender, Syncfusion.SfChart.XForms.ActualRangeChangedEventArgs e)
        {
            var axis = sender as NumericalAxis;

            if ((double)e.ActualMaximum < 1.0)                                          // 4 decimal places
            {
                axis.Maximum = Math.Round(((double)e.ActualMaximum * 1.05), 4);      // add 5% on top
                axis.Minimum = Math.Round(((double)e.ActualMinimum / 1.02), 4);      // add 2% on bottom
            }

            if (((double)e.ActualMaximum >= 1.0) && ((double)e.ActualMaximum < 100.0))  // 2 decimal places
            {
                axis.Maximum = Math.Round(((double)e.ActualMaximum * 1.05), 2);      // add 5% on top
                axis.Minimum = Math.Round(((double)e.ActualMinimum / 1.02), 2);      // add 2% on bottom
            }


            if ((double)e.ActualMaximum >= 100.0)                                       // no decimal places
            {
                axis.Maximum = Math.Round(((double)e.ActualMaximum * 1.05), 0);      // add 5% on top
                axis.Minimum = Math.Round(((double)e.ActualMinimum / 1.02), 0);      // add 2% on bottom
            }

        }

    }
}
