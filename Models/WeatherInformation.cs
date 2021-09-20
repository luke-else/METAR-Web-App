using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using libmetar.Services;

namespace METAR_Web_App.Models
{
    public class WeatherInformation
    {
        public List<string> METAR { get; set; } = new List<string>();
        public List<string> TAF { get; set; } = new List<string>();
        public string ICAO { get; set; }
        public bool ICAOError { get; set; } = false;
    }
}
