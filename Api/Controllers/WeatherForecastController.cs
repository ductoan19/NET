using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Model;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Dynamic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Microsoft.Extensions.Caching.Memory;

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
        IConfiguration configuration,
        IMemoryCache cache
        ) : base(configuration, cache)
        {
            _logger = logger;
            BaseUrl = _configuration["WeatherAPI:BaseUrl"];
            APIKey = _configuration["WeatherAPI:APIKey"];
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
            if (request.Days == null) request.Days = 1;
            if (request.Date == null) request.Date = DateTime.Today;
            
            try
            {
                //Deserialize json
                var jsonSettings = new JsonSerializerSettings();
                jsonSettings.Converters.Add(new ExpandoObjectConverter());
                jsonSettings.Converters.Add(new StringEnumConverter());
                string uri, key, resultString;
                dynamic tmpObj;
                dynamic result = new ExpandoObject();
                List<double> avgTemp = new List<double>();
                
                for (int d = 0; d < request.Days; d++)
                {
                    if(d != 0) request.Date = request.Date.Value.AddDays(1);
                    key = request.Location + "_" + request.Date.Value.ToString("yyyyMMdd");
                    if (!_cache.TryGetValue(key, out resultString))
                    {
                        //Call 3rd-party API
                        uri = BuildRequestUri(request, RequestMethod.Forecast);
                        HttpResponseMessage res = await _http.GetAsync(uri);
                        resultString = await res.Content.ReadAsStringAsync();
                        _cache.Set(key, resultString);
                    }
                    if (d == 0)
                    {
                        result = JsonConvert.DeserializeObject<ExpandoObject>(resultString, jsonSettings);
                    }
                    else
                    {
                        tmpObj = JsonConvert.DeserializeObject<ExpandoObject>(resultString, jsonSettings);
                        result.forecast.forecastday.Add(tmpObj.forecast.forecastday[0]);
                    }
                    avgTemp.Add(result.forecast.forecastday[0].day.avgtemp_c);
                }
                result.forecast.avgtemp_c = Math.Round(avgTemp.Average(), 1);

                return Ok(result);
            }
            catch (Exception ex) 
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message); 
            }
        }

        [NonAction]
        private string BuildRequestUri(object request, RequestMethod method)
        {
            var model = request as ForecastRequest;
            string Uri = BaseUrl + method.ToString("g").ToLower() + ".json" + APIKey + "&q=" + model.Location;
            //if (model.Days != null) Uri += "&days=" + model.Days.ToString();
            if (model.Date != null) Uri += "&dt=" + model.Date.Value.ToString("yyyy-MM-dd");
            if (model.AirQualityIncluded == true) Uri += "&aqi=yes";
            if (model.Lang != null) Uri += "&lang=" + model.Lang;
            return Uri;
        }
    }
}