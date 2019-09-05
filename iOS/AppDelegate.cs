using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Foundation;
using UIKit;
using Xamarin.Forms.Internals;

namespace TodoREST.iOS
{
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        [Obsolete]
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();

            Syncfusion.SfChart.XForms.iOS.Renderers.SfChartRenderer.Init();
            // App.Speech = new Speech();
            Log.Listeners.Add(new DelegateLogListener((c, m) => Debug.WriteLine(m, c)));
            LoadApplication(new App());

            return base.FinishedLaunching(app, options);
        }
    }
}

