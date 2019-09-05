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

            // Syncfusion.SfChart.XForms.MacOS.SfChartRenderer.Init();

            LoadApplication(new App());
            base.DidFinishLaunching(notification);
        }

        public override void WillTerminate(NSNotification notification)
        {
            // Insert code here to tear down your application
        }
    }
}