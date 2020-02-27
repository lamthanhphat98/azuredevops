using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace AzureDevops.Models
{
    public partial class Weather
    {
        public Weather()
        {
            WeatherDetail = new HashSet<WeatherDetail>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public string Region { get; set; }
        public int? Tempure { get; set; }
        public string WeatherCondition { get; set; }
        public DateTime? Date { get; set; }
        [JsonIgnore]
        public virtual ICollection<WeatherDetail> WeatherDetail { get; set; }
    }
}
