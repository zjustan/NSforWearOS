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

namespace NSforWearOS.Models.trips
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class Availability
    {
        public bool seats { get; set; }
        public int numberOfSeats { get; set; }
        public bool bicycle { get; set; }
        public int numberOfBicyclePlaces { get; set; }
    }

    public class Destination
    {
        public string name { get; set; }
        public double lng { get; set; }
        public double lat { get; set; }
        public string countryCode { get; set; }
        public string uicCode { get; set; }
        public string type { get; set; }
        public int plannedTimeZoneOffset { get; set; }
        public DateTime plannedDateTime { get; set; }
        public int actualTimeZoneOffset { get; set; }
        public DateTime actualDateTime { get; set; }
        public string plannedTrack { get; set; }
        public string actualTrack { get; set; }
        public string exitSide { get; set; }
        public string checkinStatus { get; set; }
        public List<object> notes { get; set; }
        public int varCode { get; set; }
    }

    public class Fare
    {
        public int priceInCents { get; set; }
        public string product { get; set; }
        public string travelClass { get; set; }
        public string discountType { get; set; }
        public int priceInCentsExcludingSupplement { get; set; }
        public int supplementInCents { get; set; }
        public int buyableTicketSupplementPriceInCents { get; set; }
    }

    public class FareLeg
    {
        public Origin origin { get; set; }
        public Destination destination { get; set; }
        public string @operator { get; set; }
        public List<string> productTypes { get; set; }
        public List<Fare> fares { get; set; }
    }

    public class FareOptions
    {
        public bool isInternationalBookable { get; set; }
        public bool isInternational { get; set; }
        public bool isEticketBuyable { get; set; }
        public bool isPossibleWithOvChipkaart { get; set; }
        public bool isTotalPriceUnknown { get; set; }
    }

    public class FareRoute
    {
        public string routeId { get; set; }
        public Origin origin { get; set; }
        public Destination destination { get; set; }
    }

    public class JourneyDetail
    {
        public string type { get; set; }
        public Link link { get; set; }
    }

    public class Leg
    {
        public string idx { get; set; }
        public string name { get; set; }
        public string travelType { get; set; }
        public string direction { get; set; }
        public bool cancelled { get; set; }
        public bool changePossible { get; set; }
        public bool alternativeTransport { get; set; }
        public string journeyDetailRef { get; set; }
        public Origin origin { get; set; }
        public Destination destination { get; set; }
        public Product product { get; set; }
        public List<Note> notes { get; set; }
        public List<object> messages { get; set; }
        public List<Stop> stops { get; set; }
        public string crowdForecast { get; set; }
        public double punctuality { get; set; }
        public bool crossPlatformTransfer { get; set; }
        public bool shorterStock { get; set; }
        public List<JourneyDetail> journeyDetail { get; set; }
        public bool reachable { get; set; }
        public int plannedDurationInMinutes { get; set; }
    }

    public class Link
    {
        public string uri { get; set; }
    }

    public class Note
    {
        public string value { get; set; }
        public string key { get; set; }
        public string noteType { get; set; }
        public bool isPresentationRequired { get; set; }
    }

    public class Origin
    {
        public string name { get; set; }
        public double lng { get; set; }
        public double lat { get; set; }
        public string countryCode { get; set; }
        public string uicCode { get; set; }
        public string type { get; set; }
        public int plannedTimeZoneOffset { get; set; }
        public DateTime plannedDateTime { get; set; }
        public int actualTimeZoneOffset { get; set; }
        public DateTime actualDateTime { get; set; }
        public string plannedTrack { get; set; }
        public string actualTrack { get; set; }
        public string checkinStatus { get; set; }
        public List<object> notes { get; set; }
        public int varCode { get; set; }
    }

    public class Product
    {
        public string number { get; set; }
        public string categoryCode { get; set; }
        public string shortCategoryName { get; set; }
        public string longCategoryName { get; set; }
        public string operatorCode { get; set; }
        public string operatorName { get; set; }
        public int operatorAdministrativeCode { get; set; }
        public string type { get; set; }
        public string displayName { get; set; }
    }

    public class ProductFare
    {
        public int priceInCents { get; set; }
        public int priceInCentsExcludingSupplement { get; set; }
        public int buyableTicketPriceInCents { get; set; }
        public int buyableTicketPriceInCentsExcludingSupplement { get; set; }
        public string product { get; set; }
        public string travelClass { get; set; }
        public string discountType { get; set; }
    }

    public class RegisterJourney
    {
        public string url { get; set; }
        public string searchUrl { get; set; }
        public string status { get; set; }
        public bool bicycleReservationRequired { get; set; }
        public Availability availability { get; set; }
    }

    public class TripAdvices
    {
        public string source { get; set; }
        public List<Trip> trips { get; set; }
        public string scrollRequestBackwardContext { get; set; }
        public string scrollRequestForwardContext { get; set; }
    }

    public class ShareUrl
    {
        public string uri { get; set; }
    }

    public class Stop
    {
        public string uicCode { get; set; }
        public string name { get; set; }
        public double lat { get; set; }
        public double lng { get; set; }
        public string countryCode { get; set; }
        public List<object> notes { get; set; }
        public int routeIdx { get; set; }
        public DateTime plannedDepartureDateTime { get; set; }
        public int plannedDepartureTimeZoneOffset { get; set; }
        public DateTime actualDepartureDateTime { get; set; }
        public int actualDepartureTimeZoneOffset { get; set; }
        public string actualDepartureTrack { get; set; }
        public string plannedDepartureTrack { get; set; }
        public string plannedArrivalTrack { get; set; }
        public string actualArrivalTrack { get; set; }
        public int departureDelayInSeconds { get; set; }
        public bool cancelled { get; set; }
        public bool borderStop { get; set; }
        public bool passing { get; set; }
        public DateTime? plannedArrivalDateTime { get; set; }
        public int? plannedArrivalTimeZoneOffset { get; set; }
        public DateTime? actualArrivalDateTime { get; set; }
        public int? actualArrivalTimeZoneOffset { get; set; }
        public int? arrivalDelayInSeconds { get; set; }
    }

    public class Trip
    {
        public int idx { get; set; }
        public string uid { get; set; }
        public string ctxRecon { get; set; }
        public int plannedDurationInMinutes { get; set; }
        public int actualDurationInMinutes { get; set; }
        public int transfers { get; set; }
        public string status { get; set; }
        public List<object> messages { get; set; }
        public List<Leg> legs { get; set; }
        public string crowdForecast { get; set; }
        public double punctuality { get; set; }
        public bool optimal { get; set; }
        public FareRoute fareRoute { get; set; }
        public List<Fare> fares { get; set; }
        public List<FareLeg> fareLegs { get; set; }
        public ProductFare productFare { get; set; }
        public FareOptions fareOptions { get; set; }
        public string type { get; set; }
        public ShareUrl shareUrl { get; set; }
        public bool realtime { get; set; }
        public string routeId { get; set; }
        public RegisterJourney registerJourney { get; set; }
    }


}