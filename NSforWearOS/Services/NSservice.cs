using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;
using System.IO;
using  NSforWearOS.Models.Departures;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace NSforWearOS.Services
{
    public static class NSservice
    {
        private static HttpClient client;
        static NSservice()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://gateway.apiportal.ns.nl/");
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "38aecdd9f0664f64aa5b96a79790cff9");
        }

        private static HttpRequestMessage CreateRequest(string uri)
        {
            var request = new HttpRequestMessage(HttpMethod.Get,uri);
            return request;
        }
        public static async Task<Departures> GetDepartures(string StationCode, int maxJourneys = 40)
        {
            var request = CreateRequest($"https://gateway.apiportal.ns.nl/reisinformatie-api/api/v2/departures?station={StationCode}");
            var response = await client.SendAsync(request);

            response.EnsureSuccessStatusCode();

            string s = await response.Content.ReadAsStringAsync();

            var serializer = new JsonSerializer();
            using (var sr = new StringReader(s))
            using (var jsonTextReader = new JsonTextReader(sr))
            {
                try
                {

                return serializer.Deserialize<NSforWearOS.Models.Departures.Root>(jsonTextReader).payload;
                }
                catch(Exception exe)
                {

                    return null;
                }
            }
        }
    }
}