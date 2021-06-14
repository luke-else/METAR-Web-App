using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using libmetar.Services;
using METAR_Web_App.Models;

namespace METAR_Web_App.Controllers.METAR
{
    public class WeatherService
    {

        private MetarService metarService { get; set; } = new MetarService();
        private List<string> METAR { get; set; } = new List<string>();
        private TafService tafService { get; set; } = new TafService();
        private List<string> TAF { get; set; } = new List<string>();
        private string ICAO { get; set; }
        private bool isValid { get; set; } = false;

        public async Task<WeatherInformation> getInformation(string ICAO)
        {

            WeatherInformation weatherInformation = new WeatherInformation();

            weatherInformation.ICAO = ICAO.Trim();

            if (setICAO(weatherInformation.ICAO) == true)
            {
                //METAR
                var METAR = getMETAR();
                //TAF
                var TAF = getTAF();

                weatherInformation.METAR = await METAR;
                weatherInformation.TAF = await TAF;

            }
            else
            {
                weatherInformation.ICAOError = true;
            }


            return weatherInformation;

        }

        //* ------------------Setter Functions ---------------------------*
        private bool setICAO(string ICAO)
        {

            if (ICAO.Length == 4)
            {
                this.ICAO = ICAO.ToUpper();
                isValid = true;
            }
            else
            {
                isValid = false;
            }

            return isValid;

        }
        //*--------------------------------------------------------------*


        //*-------------------- App Functions ---------------------------*
        private async Task<List<string>> getMETAR()
        {

            METAR.Clear();

            try
            {
                var downloadedMETAR = await metarService.GetRawAsync(ICAO);

                METAR.Add(downloadedMETAR.Raw);

                return METAR;
            }
            catch (Exception)
            {
                return null;
            }

            

        }

        private async Task<List<string>> getTAF()
        {

            TAF.Clear();

            //Try statement to collect the data from the server

            try
            {
                var downloadedTAF = await tafService.GetRawAsync(ICAO);

                foreach (var line in downloadedTAF.RawSplit)
                {
                    TAF.Add(line);
                }

                return TAF;
            }
            catch (Exception)
            {
                return null;
            }
        }

        //*--------------------------------------------------------------*
    }

}

