namespace TodoREST
{
    public static class Constants
    {
        // URL of REST service
        // public static string RestUrl = "https://developer.xamarin.com:8081/api/todoitems/{0}";
        // public static string RestUrl     = "http://192.168.178.51:3000/api/todo_entry/{0}";

        public static string RestUrlOrdered = "http://home.ulip.org:3000/api/todo_entry?_sort=Done,-Urgent,Due,Author";
        public static string RestUrlUnOrdered = "http://home.ulip.org:3000/api/todo_entry/{0}";
        public static string RestUrl = "http://home.ulip.org:3000/api/todo_entry/{0}";
        public static string Username = "Xamarin";
        public static string Password = "Pa$$w0rd";

        public static string PersonUrl = "http://home.ulip.org:3000/api/person/{0}";
    }
}
