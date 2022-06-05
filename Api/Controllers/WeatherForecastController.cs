using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Model;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Dynamic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;



namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : BaseController
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly string BaseUrl;
        private readonly string APIKey;
        public WeatherForecastController(
        ILogger<WeatherForecastController> logger,
        IConfiguration configuration) : base(configuration)
        {
            _logger = logger;
            BaseUrl = _configuration["WeatherAPI:BaseUrl"];
            APIKey = _configuration["WeatherAPI:APIKey"];
        }



        /// <summary>
        /// Get weather data at current time
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("Current")]
        public async Task<IActionResult> Current([FromForm] CurrentRequest request)
        {
            if (!ModelState.IsValid) return BadRequest("Invalid request parameter");
            string Uri = BaseRequestUri(request, RequestMethod.Current);



            try
            {
                HttpResponseMessage res = await _http.GetAsync(Uri);
                var resultString = await res.Content.ReadAsStringAsync();
                var jsonSettings = new JsonSerializerSettings();
                jsonSettings.Converters.Add(new ExpandoObjectConverter());
                jsonSettings.Converters.Add(new StringEnumConverter());
                dynamic result = JsonConvert.DeserializeObject<ExpandoObject>(resultString, jsonSettings);
                return Ok(result);
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }



        /// <summary>
        /// Get weather forecase data for a range of day
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("Forecast")]
        public async Task<IActionResult> Forecast([FromForm] ForecastRequest request)
        {
            if (!ModelState.IsValid) return BadRequest("Invalid request parameter");
            string Uri = BaseRequestUri(request, RequestMethod.Forecast);
            if (request.Days != null) Uri += "&days=" + request.Days.ToString();
            if (request.Date != null) Uri += "&dt=" + request.Date.Value.ToString("yyyy-MM-dd");



            try
            {
                //Call 3rd-party API
                HttpResponseMessage res = await _http.GetAsync(Uri);
                var resultString = await res.Content.ReadAsStringAsync();



                //Deserialize json
                var jsonSettings = new JsonSerializerSettings();
                jsonSettings.Converters.Add(new ExpandoObjectConverter());
                jsonSettings.Converters.Add(new StringEnumConverter());
                dynamic result = JsonConvert.DeserializeObject<ExpandoObject>(resultString, jsonSettings);



                //Add average temperature for forecast days
                //Add to result.forecast.avgtemp_c (temporarily not using F degree)
                List<double> avgTemp = new List<double>();
                foreach (dynamic d in result.forecast.forecastday)
                {
                    double temp = d.day.avgtemp_c;
                    avgTemp.Add(temp);
                }
                result.forecast.avgtemp_c = Math.Round(avgTemp.Average(), 1);



                return Ok(result);
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }



        /// <summary>
        /// Get weather data for a specific day (in past or future)
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("OnDate")]
        public async Task<IActionResult> OnDate([FromForm] OnDateRequest request)
        {
            if (!ModelState.IsValid) return BadRequest("Invalid request parameter");



            RequestMethod method;
            if (request.Date < DateTime.Now.Date) method = RequestMethod.History;
            else method = RequestMethod.Future;
            string Uri = BaseRequestUri(request, method);
            if (request.Date != null) Uri += "&dt=" + request.Date.Value.ToString("yyyy-MM-dd");



            try
            {
                //Call 3rd-party API
                HttpResponseMessage res = await _http.GetAsync(Uri);
                var resultString = await res.Content.ReadAsStringAsync();



                //Deserialize json
                var jsonSettings = new JsonSerializerSettings();
                jsonSettings.Converters.Add(new ExpandoObjectConverter());
                jsonSettings.Converters.Add(new StringEnumConverter());
                dynamic result = JsonConvert.DeserializeObject<ExpandoObject>(resultString, jsonSettings);

                return Ok(result);
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }



        [NonAction]
        private string BaseRequestUri(object request, RequestMethod method)
        {
            var model = request as BaseRequest;
            string Uri = BaseUrl + method.ToString("g").ToLower() + ".json" + APIKey + "&q=" + model.Location;
            if (model.Hour != null) Uri += "&hour=" + model.Hour.ToString();
            if (model.AirQualityIncluded == true) Uri += "&aqi=yes";
            if (model.Lang != null) Uri += "&lang=" + model.Lang;
            return Uri;
        }
    }
}