using System.Net.Http.Headers;

namespace FinalCRUD.WebApiConsume
{
    public class ApisContextData : IWebapi
    {
        public ApisContextData()
        {

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


    }
}
