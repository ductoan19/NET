using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace Model
{
    public class BaseRequest
    {
        [Required]
        public string Location { get; set; }
        public string Lang { get; set; }
        public Nullable<int> Hour { get; set; }
        public Nullable<bool> AirQualityIncluded { get; set; }
    }
    public class ForecastRequest : BaseRequest
    {
        public Nullable<int> Days { get; set; }
        /// <summary>
        /// should be on (0 to 10 days) from today
        /// </summary>
        public Nullable<DateTime> Date { get; set; }
    }
    public class CurrentRequest : BaseRequest
    {
    }



    public class OnDateRequest : BaseRequest
    {
        /// <summary>
        /// should be on or after 1st Jan, 2010 or on (14 to 300 days) after today
        /// </summary>
        public Nullable<DateTime> Date { get; set; }
    }



    public enum RequestMethod
    {
        Current, Forecast, Future, History
    }
}