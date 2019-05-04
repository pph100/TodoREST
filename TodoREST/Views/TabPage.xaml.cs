using System;
using Xamarin.Forms;

namespace TodoREST
{
    public partial class TabPage : TabbedPage
    {
        int activeTabIndex = -1;

        public TabPage()
        {
            InitializeComponent();
        }

        void OnAddItemClicked(object sender, EventArgs e)
        {
            switch (activeTabIndex)
            {
                case 0:         // ToDo Tab ist aktiv
                    var todoItem = new TodoItem()
                    {
                        ID = Guid.NewGuid().ToString(),
                        DttmCreated = System.DateTime.Now.ToString()
                    };
                    var todoPage = new TodoItemPage(true);
                    todoPage.BindingContext = todoItem;
                    Navigation.PushAsync(todoPage);
                    break;

                case 1:
                    var personItem = new PersonItem()
                    {
                        ID = Guid.NewGuid().ToString(),
                        DttmCreated = System.DateTime.Now.ToString()
                    };
                    var personPage = new PersonPage(true);
                    personPage.BindingContext = personItem;
                    Navigation.PushAsync(personPage);
                    break;

                default:         // keine aktion bei Klick auf Plus bei "Über"
                    break;
            }
        }

        void Handle_Appearing_ToDoPage(object sender, System.EventArgs e)
        {
            activeTabIndex = 0;
        }

        void Handle_Appearing_PersonPage(object sender, System.EventArgs e)
        {
            activeTabIndex = 1;
        }

        void Handle_Appearing_OtherPage(object sender, System.EventArgs e)
        {
            activeTabIndex = -1;
        }

        void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var todoItem = e.SelectedItem as TodoItem;
            var todoPage = new TodoItemPage();

            todoPage.BindingContext = todoItem;
            Navigation.PushAsync(todoPage);
        }
    }
}