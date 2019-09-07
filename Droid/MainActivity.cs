using Android.App;
using Android.Content.PM;
using Android.OS;

namespace TodoREST.Droid
{
    [Activity(Label = "TodoREST.Droid", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        internal static MainActivity Instance { get; private set; }

        [System.Obsolete]
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MTQwMDMzQDMxMzcyZTMyMmUzMGxHdmw1WlBHMTRtWkJzeklpRDYyVDF4cWw3REtyaVNTSHhnWHEwNysySEU9;MTQwMDM0QDMxMzcyZTMyMmUzME5mWnNFWDlpRU15UVYvZkRPY3piYzRLWVBYU3AxOHM4VTRXL3lhaHIyaEE9;MTQwMDM1QDMxMzcyZTMyMmUzMEJxOERlcmhkWkU2SmZMRU4ySDhVaG93UTZrQi9WTURNQkl5WHk0ZnNINGc9;MTQwMDM2QDMxMzcyZTMyMmUzMGNtMTdsMk5TS2NYaFF0VytZYUtna1hBY2RubWFlTkFMQWgzSzNlUTF0dGM9;MTQwMDM3QDMxMzcyZTMyMmUzMFlCSXBjRnBML2ZSMjFmcFNOZ2hudGg5L1ZYOEdWdTBubzR0SWF4MVM4ZG89;MTQwMDM4QDMxMzcyZTMyMmUzMEhSMHpVcHNzbW5lTGJDQTFCaUdPbU9aMG8vZlhDNUxtamc2Z2I5bkYyUE09;MTQwMDM5QDMxMzcyZTMyMmUzMFdrZTl0NzQvQXNPQ3h0ZUx5dU5CV0hQdWVseXBuNXlta0drcE1rTy9Oanc9;MTQwMDQwQDMxMzcyZTMyMmUzMGZiR1Bmb1F0bytvUHVzVFA0YWMwelkyRXZoejFUbDY4OWtvd080UDR4dmc9;MTQwMDQxQDMxMzcyZTMyMmUzMGw4eVR4SkNrbXZTaXh2bjBTN0RYM2prVWFWdDNNazJtdkhzTitLdlc2cHc9;MTQwMDQyQDMxMzcyZTMyMmUzMFlCSXBjRnBML2ZSMjFmcFNOZ2hudGg5L1ZYOEdWdTBubzR0SWF4MVM4ZG89");

            base.OnCreate(bundle);
            Instance = this;
            global::Xamarin.Forms.Forms.Init(this, bundle);

            // App.Speech = new Speech();
            LoadApplication(new App());
        }
    }
}

