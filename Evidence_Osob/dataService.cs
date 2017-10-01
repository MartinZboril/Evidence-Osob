using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Evidence_Osob
{
    public class dataService
    {
        HttpClient client = new HttpClient();

        public async Task<ObservableCollection<Person>> GetPersonsListAsync()
        {
            var uri = new Uri(string.Format("http://martinzboril.cz/Evidence_Osob_API/API.php", string.Empty));
            var response = await client.GetAsync(uri);
            ObservableCollection<Person> users = new ObservableCollection<Person>();
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                users = JsonConvert.DeserializeObject<ObservableCollection<Person>>(content);
            }

            return users;
        }

        public async Task PostPerson(Person item)
        {
            var uri = new Uri(string.Format("http://martinzboril.cz/Evidence_Osob_API/API.php", string.Empty));

            var json = JsonConvert.SerializeObject(item);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PostAsync(uri, content);

            if (response.IsSuccessStatusCode)
            {

            }
        }

        public async Task DeletePersonAsync(Person item)
        {
            var uri = new Uri(string.Format("http://martinzboril.cz/Evidence_Osob_API/Delete.php", string.Empty));

            var json = JsonConvert.SerializeObject(item);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PostAsync(uri, content);

            if (response.IsSuccessStatusCode)
            {

            }

        }

        public async Task UpdatePersonsync(Person item)
        {
            var uri = new Uri(string.Format("http://martinzboril.cz/Evidence_Osob_API/Delete.php", string.Empty));

            var json = JsonConvert.SerializeObject(item);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PostAsync(uri, content);

            if (response.IsSuccessStatusCode)
            {

            }

        }
    }
}
