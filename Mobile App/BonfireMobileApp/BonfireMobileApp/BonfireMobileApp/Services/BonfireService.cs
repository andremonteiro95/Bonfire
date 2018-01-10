using BonfireMobileApp.Entities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
        string baseAddress = "http://bonfirewebservice.azurewebsites.net/BonfireService.svc/";

        public async Task<List<Content>> GetContentsByBeacon(string beaconuuid)
        {
            HttpClient httpClient = new HttpClient
            {
                BaseAddress = new Uri(baseAddress)
            };

            List<Content> result = new List<Content>();

            try
            { 
                var response = await httpClient.GetAsync("GetContentsByBeacon/" + beaconuuid);
                var obj = response.Content.ReadAsStringAsync().Result;
                JObject json = JObject.Parse(obj);
                IList<JToken> tokens = json["Result"].Children().ToList();
                foreach(JToken token in tokens)
                {
                    Content content = token.ToObject<Content>();
                    result.Add(content);
                }
            }
            catch (Exception e)
            {
                return null;
            }

            return result;
        }

    }
}
