using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace TodoREST
{
    public class App : Application
    {
        public static TodoItemManager TodoManager { get; private set; }
        public static PersonManager PersonManager { get; private set; }
        public static CryptoItemManager CryptoItemManager { get; private set; }
        public static AssetManager AssetManager { get; private set; }
        public static AssetHistoryManager AssetHistoryManager { get; private set; }

        public static TabPage _mainPage = new TabPage();
        // public static TabPage _mainPage;

        [System.Obsolete]
        public App()
        {
            // Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("YOUR LICENSE KEY");
            // LogWarningsToApplicationOutput = true;
            TodoManager = new TodoItemManager(new TodoService());
            PersonManager = new PersonManager(new PersonService());
            CryptoItemManager = new CryptoItemManager(new CryptoService());
            AssetManager = new AssetManager(new AssetService());
            AssetHistoryManager = new AssetHistoryManager(new AssetHistoryService());

            MainPage = new NavigationPage(_mainPage);
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
