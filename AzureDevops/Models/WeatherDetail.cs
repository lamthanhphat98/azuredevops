using System;
using System.Collections.Generic;

namespace AzureDevops.Models
{
    public partial class WeatherDetail
    {
        public int Id { get; set; }
        public int WeatherId { get; set; }
        public string DayOfWeek { get; set; }
        public int? TempureMorning { get; set; }
        public int? Rain { get; set; }
        public int? WindSpeed { get; set; }
        public DateTime? Date { get; set; }
        public string Name { get; set; }
        public int? TempureNight { get; set; }

        public virtual Weather Weather { get; set; }
    }
}
