using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using METAR_Web_App.Controllers.METAR;
using METAR_Web_App.Models;

namespace METAR_Web_App.Controllers
{
    public class AppController : Controller
    {
        public WeatherService weatherService = new WeatherService();

        public async Task<IActionResult> ICAO(string ICAO)
        {
            if (ICAO != null)
            {
                WeatherInformation weatherInformation = await weatherService.getInformation(ICAO);

                return View(weatherInformation);
            }
            else
            {
                return View();
            }   
        }

    }
}
