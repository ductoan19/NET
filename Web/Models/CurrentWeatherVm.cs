using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Models
{
    public class ConditionVm
    {
        public string text { get; set; }
        public string icon { get; set; }
        public int cod { get; set; }
    }
    public class LocationVm
    {
        public string name { get; set; }
        public string region { get; set; }
        public string country { get; set; }
        public float lat { get; set; }
        public float lon { get; set; }
        public string tz_id { get; set; }
        public int localtime_epoch { get; set; }
        public string localtime { get; set; }
    }

    public class CurrentVm
    {
        public int is_day { get; set; }
        public float temp_c { get; set; }
        public float feelslike_c { get; set; }
        public float wind_kph { get; set; }
        public float wind_degree { get; set; }
        public float vis_km { get; set; }
        public float uv { get; set; }
        public float gust_kph { get; set; }
        public ConditionVm condition { get; set; }
    }

    public class DayVm
    {
        public float maxtemp_c { get; set; }
        public float mintemp_c { get; set; }
        public float avgtemp_c { get; set; }
        public ConditionVm condition { get; set; }
        public float uv { get; set; }

    }

    public class AstroVm
    {
        public string sunrise               { get; set; }
        public string sunset                { get; set; }
        public string moonrise              { get; set; }
        public string moonset               { get; set; }
        public string moon_phase            { get; set; }
        public string moon_illumination     { get; set; }
    }
    public class HourVm
    {
        public ConditionVm condition { get; set; }
        public string time { get; set; }
        public float temp_c { get; set; }
        public float feelslike_c { get; set; }
        public float wind_kph { get; set; }
        public float wind_degree { get; set; }
        public float vis_km { get; set; }
        public float uv { get; set; }

    }
    public class ForecastdayVm
    {
        public string date { get; set; }
        public DayVm day { get; set; }
        public AstroVm astro { get; set; }
        public HourVm[] hour { get; set; }
    }
    public class ForecastVm
    {
        public ForecastdayVm[] forecastday { get; set; }
    }

    public class CurrentWeatherVm
    {
        public LocationVm location { get; set; }
        public CurrentVm current { get; set; }
        public ForecastVm forecast { get; set; }
    }
}
