namespace TodoREST
{
    public static class Constants
    {
        // URL of REST service
        // public static string RestUrl = "https://developer.xamarin.com:8081/api/todoitems/{0}";
        // public static string RestUrl     = "http://192.168.178.51:3000/api/todo_entry/{0}";

        public static string BaseURL = "http://home.ulip.org";
        public static string UrlPathOrdered = "/api/todo_entry?_sort=Done,-Urgent,Duedate,Author";
        public static string UrlPathUnOrdered = "/api/todo_entry/{0}";
        public static string UrlPort = ":3000";
        public static string RestUrlOrdered = BaseURL + UrlPort + UrlPathOrdered;
        public static string RestUrlUnOrdered = BaseURL + UrlPort + UrlPathUnOrdered;
        public static string RestUrl = RestUrlUnOrdered;

        // public static string RestUrlOrdered = "http://home.ulip.org:3000/api/todo_entry?_sort=Done,-Urgent,Duedate,Author";
        // public static string RestUrl = "http://home.ulip.org:3000/api/todo_entry/{0}";
        // public static string RestUrlUnOrdered = "http://home.ulip.org:3000/api/todo_entry/{0}";
        public static string Username = "Xamarin";
        public static string Password = "Pa$$w0rd";

        public static string PersonUrl = "http://home.ulip.org:3000/api/person/{0}";
    }
}
