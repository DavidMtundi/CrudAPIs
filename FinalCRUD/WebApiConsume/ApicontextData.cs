using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace FinalCRUD.WebApiConsume
{
    public class ApisContextData : IWebapi
    {
        public HttpClient client;
        public ApisContextData()
        {
            client = new HttpClient();
        }

        public async Task<List<Telcos>> GetallTelcosc()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:64195/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            List<Telcos> telcos = null;
            HttpResponseMessage response = await client.GetAsync("");
            if (response.IsSuccessStatusCode)
            {
                telcos = await Task.Run(() => response.Content.ReadAsAsync<List<Telcos>>());
            }
            return telcos;
        }
        public Telcos AddTelcos(Telcos telcos)
        {
            Telcos telcoss = new Telcos();
            string data = JsonConvert.SerializeObject(telcos);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage msg = client.PostAsync(client.BaseAddress, content).Result;
            if (msg.IsSuccessStatusCode)
            {
                return telcos;
            }
            return telcoss;


        }

    }
}
