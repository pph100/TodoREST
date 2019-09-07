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

            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MTQwMDMzQDMxMzcyZTMyMmUzMGxHdmw1WlBHMTRtWkJzeklpRDYyVDF4cWw3REtyaVNTSHhnWHEwNysySEU9;MTQwMDM0QDMxMzcyZTMyMmUzME5mWnNFWDlpRU15UVYvZkRPY3piYzRLWVBYU3AxOHM4VTRXL3lhaHIyaEE9;MTQwMDM1QDMxMzcyZTMyMmUzMEJxOERlcmhkWkU2SmZMRU4ySDhVaG93UTZrQi9WTURNQkl5WHk0ZnNINGc9;MTQwMDM2QDMxMzcyZTMyMmUzMGNtMTdsMk5TS2NYaFF0VytZYUtna1hBY2RubWFlTkFMQWgzSzNlUTF0dGM9;MTQwMDM3QDMxMzcyZTMyMmUzMFlCSXBjRnBML2ZSMjFmcFNOZ2hudGg5L1ZYOEdWdTBubzR0SWF4MVM4ZG89;MTQwMDM4QDMxMzcyZTMyMmUzMEhSMHpVcHNzbW5lTGJDQTFCaUdPbU9aMG8vZlhDNUxtamc2Z2I5bkYyUE09;MTQwMDM5QDMxMzcyZTMyMmUzMFdrZTl0NzQvQXNPQ3h0ZUx5dU5CV0hQdWVseXBuNXlta0drcE1rTy9Oanc9;MTQwMDQwQDMxMzcyZTMyMmUzMGZiR1Bmb1F0bytvUHVzVFA0YWMwelkyRXZoejFUbDY4OWtvd080UDR4dmc9;MTQwMDQxQDMxMzcyZTMyMmUzMGw4eVR4SkNrbXZTaXh2bjBTN0RYM2prVWFWdDNNazJtdkhzTitLdlc2cHc9;MTQwMDQyQDMxMzcyZTMyMmUzMFlCSXBjRnBML2ZSMjFmcFNOZ2hudGg5L1ZYOEdWdTBubzR0SWF4MVM4ZG89");

            Syncfusion.SfChart.XForms.iOS.Renderers.SfChartRenderer.Init();
            // App.Speech = new Speech();
            Log.Listeners.Add(new DelegateLogListener((c, m) => Debug.WriteLine(m, c)));
            LoadApplication(new App());

            return base.FinishedLaunching(app, options);
        }
    }
}

