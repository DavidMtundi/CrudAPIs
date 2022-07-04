using FinalCRUD.WebApiConsume;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace FinalCRUD.Controllers
{
    [ApiController]
    [Route("TelcosController")]
    public class TelcosController : ControllerBase
    {
        private readonly HttpClientUtil httpClientUtil = new HttpClientUtil();
        private readonly HttpClient client = new HttpClient();
        private string basepath = "https://api-omnichannel-dev.azure-api.net/v1/datalookup/telcos";
        public TelcosController(
            //  HttpClientUtil ClientUtil
            )
        {
            //this.httpClientUtil = ClientUtil;
        }
        // [HttpGet]
        //  [Route("api/[TelcosController]")]
        [HttpGet(Name = "GetallTelcos")]

        public IActionResult GetAllTelcos()
        {
            var telcoss = httpClientUtil.GetJSON<Telcos>(path: basepath);
            //return Ok(webapi.GetallTelcosc());
            return Ok(telcoss);
            //return View();
        }
        [HttpGet(Name = "Fetch Telcos")]
        // [Route("api/[TelcosController]")]

        public async Task<ActionResult> FetchData()
        {
            List<Telcos>? telcos = new List<Telcos>();
            HttpResponseMessage httpResponse = await client.GetAsync(basepath);

            if (httpResponse.IsSuccessStatusCode)
            {
                var result = httpResponse.Content.ReadAsStringAsync().Result;
                telcos = JsonConvert.DeserializeObject<List<Telcos>>(result);
                return Ok(telcos!);
            }
            return BadRequest();
        }
    }
}
