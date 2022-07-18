using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.Wearable.Activity;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NSforWearOS.Models.trips;
using NSforWearOS.Services;

namespace NSforWearOS.Activies
{
    [Activity(Label = "TripInfoActivity")]
    public class TripInfoActivity : WearableActivity
    {
        TextView MainText;
        TextView DateText;

        TextView TotalTimeText;
        TextView TimeStampText;
        Trip Trip;
        int IdX;

        private static readonly Dictionary<DayOfWeek, string> ShortDayOfweek = new Dictionary<DayOfWeek, string>

        {
            { DayOfWeek.Monday,"mo" },
             { DayOfWeek.Tuesday,"tue" },
             { DayOfWeek.Wednesday,"wed" },
             { DayOfWeek.Thursday,"thu" },
             { DayOfWeek.Friday,"fri" },
             { DayOfWeek.Saturday,"sat" },
             { DayOfWeek.Sunday,"sun" }
        };

        private static readonly List<string> ShortMonth = new List<string>
        {
            "Jan",
            "Feb",
            "Mar",
            "Apr",
            "May",
            "Jun",
            "Jul",
            "Aug",
            "Sept",
            "Oct",
            "Nov",
            "Dec",
        };
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_TripInfo);
            var bundle = Intent.GetBundleExtra("TripData");
            IdX = bundle.GetInt("IdX");
            Trip = NSservice.GetChachedTrip(IdX);
            // Create your application here


            MainText = FindViewById<TextView>(Resource.Id.UpText);
            MainText.Selected = true;

            DateText = FindViewById<TextView>(Resource.Id.Date);

            TotalTimeText = FindViewById<TextView>(Resource.Id.TotalTime);
            TimeStampText = FindViewById<TextView>(Resource.Id.timeSpanTime);


            FIllInfo(Trip);

        }
        public void FIllInfo(Trip trip)
        {
            MainText.Text = $"{Trip.legs.First().origin.name}→{Trip.legs.Last().destination.name}";

            DateTime DepartTime = trip.legs.First().origin.plannedDateTime;
            DateTime ArriveTime = trip.legs.Last().destination.plannedDateTime;

            DateText.Text = $"{ShortDayOfweek[DepartTime.DayOfWeek]} {DepartTime.Day} {ShortMonth[ DepartTime.Month - 1]}";

            TimeSpan TotalTime = ArriveTime - DepartTime;
            TotalTimeText.Text = ((Math.Floor(TotalTime.TotalHours) > 0) ? $"{Math.Floor(TotalTime.TotalHours)}h " : string.Empty) + ((TotalTime.Minutes != 0) ? $"{TotalTime.Minutes}m. " : ".");

            TimeStampText.Text = $"{DepartTime.ToString("HH:mm")}→{ArriveTime.ToString("HH:mm")}";

        }


    }
}