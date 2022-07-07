﻿// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
using System.Collections.Generic;
using System;

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
    public List<RouteStation> routeStations { get; set; }
    public List<object> messages { get; set; }
    public string departureStatus { get; set; }
}

public class Payload
{
    public string source { get; set; }
    public List<Departure> departures { get; set; }
}

public class Product
{
    public string number { get; set; }
    public string categoryCode { get; set; }
    public string shortCategoryName { get; set; }
    public string longCategoryName { get; set; }
    public string operatorCode { get; set; }
    public string operatorName { get; set; }
    public string type { get; set; }
}

public class Root
{
    public Payload payload { get; set; }
}

public class RouteStation
{
    public string uicCode { get; set; }
    public string mediumName { get; set; }
}