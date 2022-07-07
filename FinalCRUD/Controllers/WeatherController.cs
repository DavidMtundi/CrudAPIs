using BusinessLogic.Models.WeatherModel;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace FinalCRUD.Controllers
{
    [ApiController]
    ///Weathercontroller is a controller that displays data on the nugget
    public class WeatherController : Controller
    {
        //  public static string key = "6819076af36a4b7d8d5125139220607";
        [HttpGet("GetWeather")]
        public ActionResult GetWeatherData(string names, string key)
        {
            string weatherUrl = $"http://api.weatherapi.com/v1/current.json?key={key}&q={names}&aqi=no";
            using (var client = new HttpClient())
            {
                //client.BaseAddress = new Uri(WeatherUrl);
                var result = client.GetAsync(weatherUrl);
                var response = result.Result.Content.ReadAsStringAsync().Result;
                var weatherdata = JsonConvert.DeserializeObject<WeatherModel>(response);
                // var response = JsonConvert.DeserializeObject<WeatherModel>(result.Result.ToString());
                return weatherdata != null ? Ok(weatherdata) : BadRequest("bad request");
            }
            Console.WriteLine("Enter your name");
            var name = Console.ReadLine();

            Console.WriteLine($"Your name is {name.ToString()}");
            Console.ReadKey();
        }



    }
}
