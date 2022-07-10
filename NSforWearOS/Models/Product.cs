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
using Newtonsoft.Json;

namespace NSforWearOS.Models.product
{
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

    public class Stock
    {
        public string trainType { get; set; }
        public int numberOfSeats { get; set; }
        public int numberOfParts { get; set; }
        public List<TrainPart> trainParts { get; set; }
        public bool hasSignificantChange { get; set; }
    }

    public class TrainPart
    {
        public string stockIdentifier { get; set; }
        public List<string> facilities { get; set; }
        public Image image { get; set; }
    }

    public class Image
    {
        public string uri { get; set; }
    }

}