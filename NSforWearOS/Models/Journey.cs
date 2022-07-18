using NSforWearOS.Models.stations;
using System.Collections.Generic;

namespace NSforWearOS.Models.journeys
{
    public class Journey : NSforWearOS.Models.IPayload
    {
        public List<object> notes { get; set; }
        public List<string> productNumbers { get; set; }
        public List<Stop> stops { get; set; }
        public bool allowCrowdReporting { get; set; }
        public string source { get; set; }
    }
}