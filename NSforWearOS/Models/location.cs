// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace NSforWearOS.Models.Location
{
    public class Extra
    {
    }

    public class Link
    {
        public string uri { get; set; }
    }



    public class Location
    {
        public string name { get; set; }
        public string stationCode { get; set; }
        public double lat { get; set; }
        public double lng { get; set; }
        public string open { get; set; }
        public Link link { get; set; }
        public Extra extra { get; set; }
        public List<object> infoImages { get; set; }
        public List<object> apps { get; set; }
        public List<Site> sites { get; set; }
        public List<object> extraInfo { get; set; }
        public NearbyMeLocationId nearbyMeLocationId { get; set; }
        public string code { get; set; }

        [JsonProperty("namen")]
        public Names names { get; set; }
        public double naderenRadius { get; set; }
        public bool heeftFaciliteiten { get; set; }
        public bool heeftVertrektijden { get; set; }
        public bool heeftReisassistentie { get; set; }
        public string stationType { get; set; }
        public string land { get; set; }
        public List<string> synoniemen { get; set; }
        public string UICCode { get; set; }
        public string EVACode { get; set; }
    }


    public class Names
    {
        public string middel { get; set; }
        public string kort { get; set; }
        public string lang { get; set; }
    }

    public class NearbyMeLocationId
    {
        public string value { get; set; }
        public string type { get; set; }
    }

    public class LocationCollection
    {
        public string type { get; set; }
        public string name { get; set; }
        public List<object> identifiers { get; set; }
        public List<Location> locations { get; set; }
        public string open { get; set; }
        public List<object> keywords { get; set; }
        public bool stationBound { get; set; }
        public List<object> advertImages { get; set; }
        public List<object> infoImages { get; set; }
    }

    public class Site
    {
        public string qualifier { get; set; }
        public string url { get; set; }
    }


}