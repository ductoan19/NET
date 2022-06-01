using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using Web.Models;
using Microsoft.Extensions.Configuration;

namespace Web.Controllers
{
    public class WeatherController : Controller
    {
        static HttpClient client = new HttpClient();
        public CurrentWeatherVm model = new CurrentWeatherVm();
        private string BaseUrl;
        private string APIKey;
        private readonly IConfiguration _configuration;

        public WeatherController(IConfiguration configuration)
        {
            _configuration = configuration;
            BaseUrl = _configuration["WeatherAPI:BaseUrl"];
            APIKey = _configuration["WeatherAPI:APIKey"];
        }
        public async Task<IActionResult> Index()
        {
            string forecastUri = BaseUrl + _configuration["WeatherAPI:Forecast"] + APIKey + "&q=Hanoi&days=3";
            HttpResponseMessage res = await client.GetAsync(forecastUri);
            var resultString = await res.Content.ReadAsStringAsync();
            model = JsonSerializer.Deserialize<CurrentWeatherVm>(resultString);
            //dynamic model2 = JsonSerializer.Deserialize<dynamic>(resultString);
            return View(model);
        }
    }
}
