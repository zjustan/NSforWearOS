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
using NSforWearOS.Models;
using  NSforWearOS.Models.Departures;
using  NSforWearOS.Models.journeys;
using  NSforWearOS.Models.trips;
using Newtonsoft.Json;
using System.Threading.Tasks;
using NSforWearOS.Models.Location;

namespace NSforWearOS.Services
{
    public static class NSservice
    {
        private static HttpClient client;
        private static TripAdvices LastAdvices;
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

            return await deserialize<Departures>(response);

            
        }

        public static async Task<Journey> GetJourney(Int32 Train )
        {
            var request = CreateRequest($"https://gateway.apiportal.ns.nl/reisinformatie-api/api/v2/journey?train={Train}");
            var response = await client.SendAsync(request);

            response.EnsureSuccessStatusCode();

            return await deserialize<Journey>(response);
        }

        public static async Task<TripAdvices> GetTripAdvices(string fromStationCode, string ToStationCode)
        {
            var request = CreateRequest($"https://gateway.apiportal.ns.nl/reisinformatie-api/api/v3/trips?fromStation={fromStationCode}&toStation={ToStationCode}");
            var response = await client.SendAsync(request);

            response.EnsureSuccessStatusCode();

            LastAdvices = await deserializeAsType<TripAdvices>(response);
            return LastAdvices;
        } 

        public static Trip GetChachedTrip(int idx)
        {
            return LastAdvices.trips.Find(x => x.idx == idx);
        }


        public static async Task<List<LocationCollection>> GetPlaces(string? Query = null)
        {
            var request = CreateRequest($"https://gateway.apiportal.ns.nl/places-api/v2/places?type=stationV2&q={Query}&limit=99999999");
            var response = await client.SendAsync(request);

            response.EnsureSuccessStatusCode();

            return await deserialize<List<LocationCollection>>(response);
        }

        public static async Task<List<LocationCollection>> GetClosestStation(string Lat, string lng)
        {
            var request = CreateRequest($"https://gateway.apiportal.ns.nl/places-api/v2/places?type=stationV2&lat={Lat}&lng={lng}&radius=99999");
            var response = await client.SendAsync(request);

            response.EnsureSuccessStatusCode();

            return await deserialize<List<LocationCollection>>(response);
        }


        private static async Task<T> deserialize<T>(HttpResponseMessage response)
        {
            string s = await response.Content.ReadAsStringAsync();

            var serializer = new JsonSerializer();
            using (var sr = new StringReader(s))
            using (var jsonTextReader = new JsonTextReader(sr))
            {
                try
                {
                    Task<T> deserialized = new Task<T>(() => serializer.Deserialize<Root<T>>(jsonTextReader).payload);
                    deserialized.Start();
                    return  await deserialized ;
                }
                catch (Exception exe)
                {

                    return default(T);
                }
            }
        }

        private static async Task<T> deserializeAsType<T>(HttpResponseMessage response)
        {
            string s = await response.Content.ReadAsStringAsync();

            var serializer = new JsonSerializer();
            using (var sr = new StringReader(s))
            using (var jsonTextReader = new JsonTextReader(sr))
            {
                try
                {
                    Task<T> deserialized = new Task<T>(() => serializer.Deserialize<T>(jsonTextReader));
                    deserialized.Start();
                    return await deserialized;
                }
                catch (Exception exe)
                {

                    return default(T);
                }
            }
        }
    }
}