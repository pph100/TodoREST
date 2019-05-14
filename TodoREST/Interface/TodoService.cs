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
            var authData = string.Format("{0}:{1}", Constants.Username, Constants.Password);
            var authHeaderValue = Convert.ToBase64String(Encoding.UTF8.GetBytes(authData));

            client = new HttpClient();
            // client.Timeout = TimeSpan.FromSeconds(_timeoutSeconds);

            client.MaxResponseContentBufferSize = 256000;
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authHeaderValue);
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

                    foreach(var i in Items)
                    {
                        if (i.Due != null && i.Due.Length > 10)
                        {
                            // CultureInfo MyCultureInfo = new CultureInfo("de-DE");
                            // DateTime MyDateTime = DateTime.Parse(i.Due, MyCultureInfo);

                            // DateTime MyDateTime = DateTime.Parse(i.Due);
                            // i.Due = MyDateTime.ToString("MM/dd/yyyy");
                            String temp = i.Due.Substring(1, 10);
                            i.Due = temp;
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


        public async Task SaveTodoItemAsync(TodoItem item, bool isNewItem = false)
        {
            var uri = new Uri(string.Format(Constants.RestUrl, string.Empty));

            // reformat before save
            try { 
                if(item.Due != null && item.Due.Length > 10)
                {
                    // DateTime MyDateTime = DateTime.Parse(item.Due, new CultureInfo("de-DE"));
                    // item.Due = MyDateTime.ToString("MM/dd/yyyy");
                    string temp = item.Due.Substring(1, 10);
                    item.Due = temp;
                }

                if(item.Due == null)
                {
                    string MyDateTime = DateTime.Now.ToString("MM/dd/yyyy");
                    if (MyDateTime.Length > 10)
                        item.Due = MyDateTime.Substring(1, 10);
                    else
                        item.Due = MyDateTime;
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
