using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace Model
{
    public class ForecastRequest
    {
        [Required]
        public string Location { get; set; }

        /// <summary>
        /// should be on (0 to 10 days) from today
        /// </summary>
        public Nullable<DateTime> Date { get; set; }
        public string Lang { get; set; }
        public Nullable<bool> AirQualityIncluded { get; set; }
        public Nullable<int> Days { get; set; }
    }

    public enum RequestMethod
    {
        Current, Forecast, Future, History
    }
}