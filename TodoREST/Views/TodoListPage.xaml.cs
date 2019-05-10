using System;
using System.Globalization;
using Xamarin.Forms;

namespace TodoREST
{
    public partial class TodoListPage : ContentPage
    {
        public TodoListPage()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            var todoList = await App.TodoManager.GetTasksAsync();
            listView.ItemsSource = todoList;
        }

        void OnAddItemClicked(object sender, EventArgs e)
        {
            var todoItem = new TodoItem()
            {
                ID = Guid.NewGuid().ToString(),
                DttmCreated = System.DateTime.Now.ToString("d", CultureInfo.CreateSpecificCulture("de-DE")),
                Due = System.DateTime.Now.ToString("d", CultureInfo.CreateSpecificCulture("de-DE"))
            };
            var todoPage = new TodoItemPage(true);
            todoPage.BindingContext = todoItem;
            Navigation.PushAsync(todoPage);
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