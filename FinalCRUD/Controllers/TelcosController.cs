using BusinessLogic.Models;
using FinalCRUD.WebApiConsume;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Shared;
using System.Security.Claims;
using System.Text;

namespace FinalCRUD.Controllers
{
    [ApiController]
    [Route("TelcosController")]
    public class TelcosController : ControllerBase
    {
        private readonly HttpClient client;
        private static string basepathmineadd = "http://localhost:55117/api";
        private static string basepath = "https://api-omnichannel-dev.azure-api.net/v1/datalookup/telcos";
        public Uri baseaddress = new Uri(basepath);
        private readonly HttpClientUtil _httpclientutil;
        public TelcosController()
        {
            client = new HttpClient();
            client.BaseAddress = baseaddress;
            _httpclientutil = new HttpClientUtil();
        }

        [HttpGet]
        [Route("Authenticate")]
        public ActionResult AuthenticatedUser()
        {
            if ((ClaimsIdentity)User.Identity != null)
            {
                var identity = (ClaimsIdentity)User.Identity;
                return Ok($"Hello, user logged in is :{identity.Name}");
            }
            return Ok("No user LOgged in");
        }

        [HttpGet(Name = "Fetch Telcos")]
        public async Task<ActionResult> FetchData()
        {
            List<Telcos>? telcos = new List<Telcos>();
            HttpResponseMessage httpResponse = await client.GetAsync(client.BaseAddress);

            if (httpResponse.IsSuccessStatusCode)
            {
                var result = httpResponse.Content.ReadAsStringAsync().Result;
                telcos = JsonConvert.DeserializeObject<List<Telcos>>(result);
                return Ok(telcos!);
            }
            return BadRequest();
        }
        [HttpGet("{name}")]
        public ActionResult GetByName(String name)
        {
            List<Telcos> telcosList = new List<Telcos>();
            HttpResponseMessage responseMessage = client.GetAsync(client.BaseAddress).Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                var data = responseMessage.Content.ReadAsStringAsync().Result;

                telcosList = JsonConvert.DeserializeObject<List<Telcos>>(data);
                foreach (Telcos telcos in telcosList)
                {
                    if (telcos.telco.ToString().ToLower().Equals(name.ToLower()))
                    {
                        return Ok(telcos);
                    }
                }
                // return Ok(telcosList);
            }
            return NotFound("No element found");

        }
        [HttpGet("Getby/{id}")]
        public ActionResult GetById(int id)
        {
            List<Telcos>? telcos = new List<Telcos>();
            HttpResponseMessage msg = client.GetAsync(client.BaseAddress).Result;
            if (msg.IsSuccessStatusCode)
            {
                var data = msg.Content.ReadAsStringAsync().Result;

                //deserialize converts the json to a llist of telcos
                telcos = JsonConvert.DeserializeObject<List<Telcos>>(data);
                Telcos? telc = telcos.FirstOrDefault((telco) => telco.id == id);
                if (telc != null)
                {
                    return Ok(telc!);
                }

            }
            return NotFound($"The element with an id of {id} is not found");
        }
        [HttpPost("Add")]
        public ActionResult AddTelcos(Telcos t)
        {
            //convert to json first
            //confirm first if the url, is valid
            List<Telcos>? telcos = new List<Telcos>();

            HttpResponseMessage msg = client.GetAsync(client.BaseAddress).Result;
            String urlread = "https://localhost:7285/api/Employees";

            if (msg.IsSuccessStatusCode)
            {
                var dat = msg.Content.ReadAsStringAsync().Result;
                telcos = JsonConvert.DeserializeObject<List<Telcos>>(dat);
                int id = telcos.Last().id;

                t.id = id + 1;

                //convert the telcos value to a json

                var data = JsonConvert.SerializeObject(t);
                //get the new httpcontent
                HttpContent content = new StringContent(data, encoding: System.Text.Encoding.UTF32, "application/json");
                client.PostAsync(urlread, content);
                return Ok(data);
            }
            return BadRequest("Could not add the Telcos");

        }
        [HttpPost("AddEmployee")]
        public ActionResult AddEmployee(Employee t)
        {
            String urlread = "https://localhost:7285/api/Employees";

            //convert the telcos value to a json
            var data = JsonConvert.SerializeObject(t);
            //get the new httpcontent
            HttpContent content = new StringContent(data, encoding: Encoding.UTF32, "application/json");
            var result = client.PostAsync(urlread, content).Result.Content.ReadAsStringAsync().Result;
            Console.WriteLine(result.ToString());
            return result == null ? BadRequest("Could not add this employee") : Ok(result);
        }
        [HttpPost("AddEmployeeUpdatedFrom Request")]
        public async Task<IActionResult> GetDetails([FromBody] Employee model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(model);
            }
            Dictionary<string, string> header = new Dictionary<string, string>();
            var url = "https://localhost:7285/api/Employees";
            header.Add("Content-Type", "application/json");
            var response = await _httpclientutil.PostJSONAsync<Employee>(url, model, headers: header);
            if (response == null)
            {
                return BadRequest();
            }
            return Ok(response);
        }


        //[HttpPost(Name = "Add Telcos")]
        //[Route("api/[TelcosController]/{telc}")]
        //public Telcos CreateTelcos(Telcos telc)
        //{
        //    return webapi.AddTelcos(telc);
        //}
    }
}
