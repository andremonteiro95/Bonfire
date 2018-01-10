using BonfireMobileApp.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BonfireMobileApp.Services
{
    class BonfireService
    {
        string baseAddress = "http://10.0.2.2:9607/BonfireService.svc/";

        public async Task<List<Content>> GetContentsByBeacon(string beaconuuid)
        {
            HttpClient httpClient = new HttpClient
            {
                BaseAddress = new Uri(baseAddress)
            };

            try
            { 
                var response = await httpClient.GetAsync("GetContentsByBeacon/" + beaconuuid);
                ;
            }
            catch (Exception e)
            {
                ;
            }

          //  var x = response.Content;

            return new List<Content>();
        }

    }
}
