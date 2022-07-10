using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NSforWearOS.Models.product;

namespace NSforWearOS.Models.stations
{
    public class MinimalStation
    {
        public string uicCode { get; set; }
        public string mediumName { get; set; }
    }

    public class Station : MinimalStation
    {
        [JsonProperty("uicCode")]
        public string Code { get => uicCode; set => uicCode = value; }
        public string name { get => mediumName ; set => mediumName = value; }
        public double lng { get; set; }
        public double lat { get; set; }
        public string countryCode { get; set; }
    }
    public class Stop
    {
        public string id { get; set; }
        public Station stop { get; set; }
        public List<string> previousStopId { get; set; }
        public List<string> nextStopId { get; set; }
        public string destination { get; set; }
        public string status { get; set; }
        public List<Arrival> arrivals { get; set; }
        public List<Departure> departures { get; set; }
        public Stock actualStock { get; set; }
        public Stock plannedStock { get; set; }
        public List<object> platformFeatures { get; set; }
        public List<object> coachCrowdForecast { get; set; }

        public class Arrival
        {
            public Product product { get; set; }
            public Station origin { get; set; }
            public Station destination { get; set; }
            public DateTime plannedTime { get; set; }
            public DateTime actualTime { get; set; }
            public int delayInSeconds { get; set; }
            public string plannedTrack { get; set; }
            public string actualTrack { get; set; }
            public bool cancelled { get; set; }
            public double punctuality { get; set; }
            public string crowdForecast { get; set; }
            public List<string> stockIdentifiers { get; set; }
        }

        public class Departure
        {
            public Product product { get; set; }
            public stations.Station origin { get; set; }
            public stations.Station destination { get; set; }
            public DateTime plannedTime { get; set; }
            public DateTime actualTime { get; set; }
            public int delayInSeconds { get; set; }
            public string plannedTrack { get; set; }
            public string actualTrack { get; set; }
            public bool cancelled { get; set; }
            public string crowdForecast { get; set; }
            public List<string> stockIdentifiers { get; set; }
        }
    }

}