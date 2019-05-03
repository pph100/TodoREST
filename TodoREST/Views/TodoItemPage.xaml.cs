using System;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace TodoREST
{
    public partial class TodoItemPage : ContentPage
    {

        bool isNewItem;


        void Handle_CompletedByChanged(object sender, Xamarin.Forms.TextChangedEventArgs e)
        {
            if (completedByEntry.Text.Length > 0)
            {
                doneSwitch.IsToggled = true;
            }
            else
            {
                doneSwitch.IsToggled = false;
            }
        }

        void Handle_NotesChanged(object sender, Xamarin.Forms.TextChangedEventArgs e)
        {
            if (notesEntry.Text.Length > 0)
            {
                SaveButton.IsEnabled = true;
            }
            else
            {
                SaveButton.IsEnabled = false;
            }
        }

        public TodoItemPage(bool isNew = false)
        {
            InitializeComponent();
            isNewItem = isNew;
            if (isNew)
            {
                DeleteButton.IsEnabled = false;
            }
            else
            {
                DeleteButton.IsEnabled = true;
            }
        }

        async void OnSaveActivated(object sender, EventArgs e)
        {
            var todoItem = (TodoItem)BindingContext;

            todoItem.DttmLastUpdated = System.DateTime.Now.ToString();
            await App.TodoManager.SaveTaskAsync(todoItem, isNewItem);
            await Navigation.PopAsync();
        }

        async void OnDeleteActivated(object sender, EventArgs e)
        {
            var todoItem = (TodoItem)BindingContext;
            await App.TodoManager.DeleteTaskAsync(todoItem);
            await Navigation.PopAsync();
        }

        void OnCancelActivated(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

    }
}
