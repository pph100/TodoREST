using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace TodoREST
{
    public class TodoService : ITodoService
    {
        HttpClient client;

        public List<TodoItem> Items { get; private set; }


        public TodoService()
        {
            // var _timeoutSeconds = 10; 
            // var authData = string.Format("{0}:{1}", Constants.Username, Constants.Password);
            // var authHeaderValue = Convert.ToBase64String(Encoding.UTF8.GetBytes(authData));

            client = new HttpClient();
            // client.Timeout = TimeSpan.FromSeconds(_timeoutSeconds);

            client.MaxResponseContentBufferSize = 256000;
            // client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authHeaderValue);
        }


        public async Task<List<TodoItem>> RefreshDataAsync()
        {
            Items = new List<TodoItem>();

            // var uri = new Uri(string.Format(Constants.RestUrl, string.Empty));
            var uri = new Uri(string.Format(Constants.RestUrlOrdered));

            try
            {
                var response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    Items = JsonConvert.DeserializeObject<List<TodoItem>>(content);

                    foreach (var i in Items)
                    {
                        if (i.DueDate == null)
                        {
                            CultureInfo MyCultureInfo = new CultureInfo("de-DE");
                            string MyDate = DateTime.Now.ToString("dd.MM.yyyy");
                            string MyTime = "15:00:00";
                            i.DueDate = DateTime.Parse(MyDate + " " + MyTime, MyCultureInfo);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // var _page = new Page();
                Debug.WriteLine(@"               ERROR {0}", ex.Message);
                Debug.WriteLine(@"Unsuccessful connect to URI {0} at {1}", uri, System.DateTime.Now);
                await App._mainPage.DisplayAlert(
                    "Connection Issue",
                    "Connection to  " + uri + "@" + System.DateTime.Now + " revealed: " + ex.Message,
                    "OK"
                );
            }

            return Items;
        }


        async Task ITodoService.SaveTodoItemAsync(TodoItem item, bool isNewItem = false)
        {
            var uri = new Uri(string.Format(Constants.RestUrl, string.Empty));

            // reformat before save
            try
            {
                /*
                if (item.Due != null && item.Due.Length > 10)
                {
                    // DateTime MyDateTime = DateTime.Parse(item.Due, new CultureInfo("de-DE"));
                    // item.Due = MyDateTime.ToString("MM/dd/yyyy");
                    String temp = item.Due.Substring(1, 10);
                    item.Due = temp;
                }

                if (item.Due == null)
                {
                    // string MyDateTime = DateTime.Now.ToString("MM/dd/yyyy");
                    MyDateTime = DateTime.Now.ToString("dd.MM.yyyy");
                    if (MyDateTime.Length > 10)
                    {
                        item.Due = MyDateTime.Substring(1, 10);
                        CultureInfo MyCultureInfo = new CultureInfo("de-DE");
                        item.DueDate = DateTime.Parse(item.Due, MyCultureInfo);
                    }
                    else
                    {
                        item.Due = MyDateTime;
                    }
                }
                */

                if (item.DueDate == null)
                {
                    CultureInfo MyCultureInfo = new CultureInfo("de-DE");
                    string MyDate = DateTime.Now.ToString("dd.MM.yyyy");
                    string MyTime = "15:00:00";     // hack w/Mitternacht
                    item.DueDate = DateTime.Parse(MyDate + " " + MyTime, MyCultureInfo);
                }


                //if(item.DueDate.ToString("HH:mm") == "00:00" )
                if ((item.DueDate.Kind == DateTimeKind.Utc)
                    || (item.DueDate.Kind == DateTimeKind.Unspecified)
                    || (item.DueDate.Kind == DateTimeKind.Local))
                {
                    CultureInfo MyCultureInfo = new CultureInfo("de-DE");
                    string MyDate = item.DueDate.ToString("dd.MM.yyyy");
                    string MyTime = "15:00:00";
                    item.DueDate = DateTime.Parse(MyDate + " " + MyTime, MyCultureInfo);
                    DateTime.SpecifyKind(item.DueDate, DateTimeKind.Local);
                }

                var json = JsonConvert.SerializeObject(item);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = null;
                if (isNewItem)
                {
                    response = await client.PostAsync(uri, content);
                }
                else
                {
                    response = await client.PutAsync(uri, content);
                }

                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine(@"               TodoItem successfully saved.");
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"               ERROR {0}", ex.Message);
                await App._mainPage.DisplayAlert(
                    "Connection Issue",
                    "Connection to  " + uri + "@" + System.DateTime.Now + " revealed: " + ex.Message,
                    "OK"
                );
            }
        }


        public async Task DeleteTodoItemAsync(string id)
        {
            var uri = new Uri(string.Format(Constants.RestUrl, id));

            try
            {
                var response = await client.DeleteAsync(uri);

                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine(@"               TodoItem successfully deleted.");
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"               ERROR {0}", ex.Message);
                await App._mainPage.DisplayAlert(
                    "Connection Issue",
                    "Connection to  " + uri + "@" + System.DateTime.Now + " revealed: " + ex.Message,
                    "OK"
                );
            }
        }
    }
}
