using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TodoREST
{
    public class PersonService : IPersonService
    {
        HttpClient client;

        public List<PersonItem> Items { get; private set; }

        public PersonService()
        {
            // var authData = string.Format("{0}:{1}", Constants.Username, Constants.Password);
            // var authHeaderValue = Convert.ToBase64String(Encoding.UTF8.GetBytes(authData));

            client = new HttpClient();

            client.MaxResponseContentBufferSize = 256000;
            // client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authHeaderValue);
        }

        public async Task<List<PersonItem>> RefreshDataAsync()
        {
            Items = new List<PersonItem>();

            var uri = new Uri(string.Format(Constants.PersonUrl, string.Empty));

            try
            {
                // Debug.WriteLine(@"Trying to connect to URI {0} at {1}", uri, System.DateTime.Now);
                var response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    // Debug.WriteLine(@"Successful connect to URI {0} at {1}", uri, System.DateTime.Now);
                    var content = await response.Content.ReadAsStringAsync();
                    Items = JsonConvert.DeserializeObject<List<PersonItem>>(content);
                }
            }
            catch (Exception ex)
            {
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

        public async Task SavePersonItemAsync(PersonItem item, bool isNewItem = false)
        {
            var uri = new Uri(string.Format(Constants.PersonUrl, string.Empty));

            try
            {
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

                // if (response.IsSuccessStatusCode)
                // {
                    // Debug.WriteLine(@"               TodoItem successfully saved.");
                // }

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

        public async Task DeletePersonItemAsync(string id)
        {
            var uri = new Uri(string.Format(Constants.PersonUrl, id));

            try
            {
                var response = await client.DeleteAsync(uri);

                // if (response.IsSuccessStatusCode)
                // {
                    // Debug.WriteLine(@"               TodoItem successfully deleted.");
                // }

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
