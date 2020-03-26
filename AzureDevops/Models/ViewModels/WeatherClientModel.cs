using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AzureDevops.Models.ViewModels
{
    public class WeatherClientModel
    {
      
        public string Name { get; set; }
        public string Country { get; set; }
        public string Region { get; set; }
        public int? TempureMorning { get; set; }
        public int? TempureNight { get; set; }
        public string WeatherCondition { get; set; }
        public int DayOfWeek { get; set; }
        public int? Rain { get; set; }
        public int? WindSpeed { get; set; }
        public DateTime? Date { get; set; }

        public int WeatherId { get; set; }
    }
}
