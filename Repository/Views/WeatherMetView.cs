using System.Collections.Generic;

namespace mvc_decea.Repository.Views
{
    public class WeatherMetView
    {
        public IList<string> loc { get; set; }
        public IList<string> metar { get; set; }
        public IList<string> taf { get; set; }
    } 
}