using System;
using System.Collections.Generic;
using System.Text;

namespace Weather_Pocket.Models
{
    public class ForecastModel
    {
        public string City { get; set; }
        public string Date { get; set; }
        public string Image { get; set; }
        public string Temprature { get; set; }
        public string Description { get; set; }
        public string Humidity { get; set; }
        public string Wind { get; set; }
        public string Pressure { get; set; }
        public string Cloudness { get; set; }
    }
}
