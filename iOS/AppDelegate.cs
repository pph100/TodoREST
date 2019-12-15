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

            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MTc5MDc4QDMxMzcyZTMzMmUzMGsyVHN0bmhYRXdDWGV6b3NxUDNxNHVTNTRkdHlpdllJNUNrU3Z6cTlhYzQ9;MTc5MDc5QDMxMzcyZTMzMmUzMGFhRXZFUkpERU9PclBZTDRha1V4eE5PZ3lackNOWndWUTZEK2I5WHRobG89;MTc5MDgwQDMxMzcyZTMzMmUzMFhsbE9JOEFSMW4vZE5nWVVHUHp5VVhWVDAvSDd1NENvTWJDQXVqb25Relk9;MTc5MDgxQDMxMzcyZTMzMmUzMEg4cXQ1UU9yQU5TNFlvTkY2TCthQlRGcitubzBXWmhlWW5leURyR09pb2M9;MTc5MDgyQDMxMzcyZTMzMmUzMEY0VHplMVFUR0tVaUpkQldpdTZlcytKaFR4djZEdDRsWk9sWE9vMVZOL2s9;MTc5MDgzQDMxMzcyZTMzMmUzMGdwMzIwWXBVbWJvb29aNGEzc0wrOE5yMVlOUGFQU2RiUWZKRDR0YXlVQU09;MTc5MDg0QDMxMzcyZTMzMmUzMEpGYUxsQ1RTWnJKVi96SVBrU00yY2FGQ3Y3YVV2QnMrSXhNZkNUcTdzTUU9;MTc5MDg1QDMxMzcyZTMzMmUzMGxkRWdBUDFNTWxnYmF3MkRFcmF1Z2NOK0V3VTFna2hpM2F2eWtCbGZnVm89;MTc5MDg2QDMxMzcyZTMzMmUzMEZOa0x5N2pLUVdsOFRHOEVucER6WExCczdYZkhZN2xOOWFYTy9oTW1IaW89;MTc5MDg3QDMxMzcyZTMzMmUzMEY0VHplMVFUR0tVaUpkQldpdTZlcytKaFR4djZEdDRsWk9sWE9vMVZOL2s9;NT8mJyc2IWhiZH1gfWN9YmdoYmF8YGJ8ampqanNiYmlmamlmanMDHmgjNiM2OzITJDYxfTc2");

            Syncfusion.SfChart.XForms.iOS.Renderers.SfChartRenderer.Init();
            // App.Speech = new Speech();
            Log.Listeners.Add(new DelegateLogListener((c, m) => Debug.WriteLine(m, c)));
            LoadApplication(new App());

            return base.FinishedLaunching(app, options);
        }
    }
}

