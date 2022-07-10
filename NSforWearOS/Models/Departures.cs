// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
using NSforWearOS.Models.product;
using NSforWearOS.Models.stations;
using System;
using System.Collections.Generic;
namespace NSforWearOS.Models.Departures
{

    public class Departure
    {
        public string direction { get; set; }
        public string name { get; set; }
        public DateTime plannedDateTime { get; set; }
        public int plannedTimeZoneOffset { get; set; }
        public DateTime actualDateTime { get; set; }
        public int actualTimeZoneOffset { get; set; }
        public string plannedTrack { get; set; }
        public Product product { get; set; }
        public string trainCategory { get; set; }
        public bool cancelled { get; set; }
        public List<MinimalStation> routeStations { get; set; }
        public List<object> messages { get; set; }
        public string departureStatus { get; set; }
    }

    public class Departures : IPayload
    {
        public string source { get; set; }
        public List<Departure> departures { get; set; }
    }
}