using MealDeals.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MealDeals
{
    public class LocalAPICall
    {
        public async Task<List<Special>> GetLocal()
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:52346/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                try
                {
                    HttpResponseMessage response = await client.GetAsync("api/Specials");
                    response.EnsureSuccessStatusCode();
                    string data = await response.Content.ReadAsStringAsync();
                    var specials = JsonConvert.DeserializeObject<IEnumerable<Special>>(data).ToList();

                    return specials;
                }
                catch (Exception e)
                {
                    return null;
                }
            }
        }
        public async Task PostLocal(Special special)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:52346/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                try
                {
                    var stringContent = new StringContent(JsonConvert.SerializeObject(special), Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await client.PostAsync("api/Specials", stringContent).ConfigureAwait(false);
                    response.EnsureSuccessStatusCode();
                    string data = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    var jsonResults = JsonConvert.DeserializeObject<IEnumerable<Special>>(data).ToList();

                }
                catch (Exception e)
                {

                }
            }
        }
        public async void PutLocal(Special special)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:52346/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                try
                {
                    var newSpecial = new
                    {
                        id = special.Id,
                        NewSpecial = special
                    };
                    var stringContent = new StringContent(JsonConvert.SerializeObject(newSpecial), Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await client.PutAsync("api/Specials", stringContent);
                    response.EnsureSuccessStatusCode();
                    string data = await response.Content.ReadAsStringAsync();
                    var jsonResults = JsonConvert.DeserializeObject<IEnumerable<Special>>(data).ToList();

                }
                catch (Exception e)
                {

                }
            }
        }
    }
}