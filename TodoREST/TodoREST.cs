using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace TodoREST
{
    public class App : Application
    {
        public static TodoItemManager TodoManager { get; private set; }
        public static PersonManager PersonManager { get; private set; }

        public static TabPage _mainPage = new TabPage();

        public App()
        {
            TodoManager = new TodoItemManager(new TodoService());
            // TodoManager = new TodoItemManager(new RestService());
            PersonManager = new PersonManager(new PersonService());

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

