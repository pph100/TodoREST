using AppKit;
using Foundation;
using Xamarin.Forms;
using TodoREST;
using Xamarin.Forms.Platform.MacOS;

namespace TodoREST.macOS
{
    [Register("AppDelegate")]
    public partial class AppDelegate : FormsApplicationDelegate
    // public partial class AppDelegate : global::Xamarin.Forms.Platform.MacOS.FormsApplicationDelegate
    {

        NSWindow window;

        public override NSWindow MainWindow
        {
            get
            {
                return window;
            }
        }

        public AppDelegate()
        {
            var style = NSWindowStyle.Closable | NSWindowStyle.Resizable | NSWindowStyle.Miniaturizable | NSWindowStyle.Titled;
            var rect = new CoreGraphics.CGRect(100, 100, 1024, 768);
            window = new NSWindow(rect, style, NSBackingStore.Buffered, false);
            window.Title = "CryptoApp";
            window.TitleVisibility = NSWindowTitleVisibility.Hidden;
        }

        [System.Obsolete]
        public override void DidFinishLaunching(NSNotification notification)
        {
            // Forms.Init();
            global::Xamarin.Forms.Forms.Init();

            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MTc5MDc4QDMxMzcyZTMzMmUzMGsyVHN0bmhYRXdDWGV6b3NxUDNxNHVTNTRkdHlpdllJNUNrU3Z6cTlhYzQ9;MTc5MDc5QDMxMzcyZTMzMmUzMGFhRXZFUkpERU9PclBZTDRha1V4eE5PZ3lackNOWndWUTZEK2I5WHRobG89;MTc5MDgwQDMxMzcyZTMzMmUzMFhsbE9JOEFSMW4vZE5nWVVHUHp5VVhWVDAvSDd1NENvTWJDQXVqb25Relk9;MTc5MDgxQDMxMzcyZTMzMmUzMEg4cXQ1UU9yQU5TNFlvTkY2TCthQlRGcitubzBXWmhlWW5leURyR09pb2M9;MTc5MDgyQDMxMzcyZTMzMmUzMEY0VHplMVFUR0tVaUpkQldpdTZlcytKaFR4djZEdDRsWk9sWE9vMVZOL2s9;MTc5MDgzQDMxMzcyZTMzMmUzMGdwMzIwWXBVbWJvb29aNGEzc0wrOE5yMVlOUGFQU2RiUWZKRDR0YXlVQU09;MTc5MDg0QDMxMzcyZTMzMmUzMEpGYUxsQ1RTWnJKVi96SVBrU00yY2FGQ3Y3YVV2QnMrSXhNZkNUcTdzTUU9;MTc5MDg1QDMxMzcyZTMzMmUzMGxkRWdBUDFNTWxnYmF3MkRFcmF1Z2NOK0V3VTFna2hpM2F2eWtCbGZnVm89;MTc5MDg2QDMxMzcyZTMzMmUzMEZOa0x5N2pLUVdsOFRHOEVucER6WExCczdYZkhZN2xOOWFYTy9oTW1IaW89;MTc5MDg3QDMxMzcyZTMzMmUzMEY0VHplMVFUR0tVaUpkQldpdTZlcytKaFR4djZEdDRsWk9sWE9vMVZOL2s9;NT8mJyc2IWhiZH1gfWN9YmdoYmF8YGJ8ampqanNiYmlmamlmanMDHmgjNiM2OzITJDYxfTc2");

            Syncfusion.SfChart.XForms.MacOS.SfChartRenderer.Init();

            LoadApplication(new App());
            base.DidFinishLaunching(notification);
        }

        public override void WillTerminate(NSNotification notification)
        {
            // Insert code here to tear down your application
        }
    }
}