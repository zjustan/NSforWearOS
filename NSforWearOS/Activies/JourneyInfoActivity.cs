using Android.App;
using Android.OS;
using Android.Support.Wearable.Activity;
using Android.Widget;
using NSforWearOS.Services;
using NSforWearOS.controls;
using static Android.Views.ViewGroup;
using System.Collections.Generic;
using static Android.InputMethodServices.Keyboard;
using System;
using System.Timers;

namespace NSforWearOS.Activies
{
    [Activity(Label = "JourneyInfoActivity")]
    public class JourneyInfoActivity : WearableActivity
    {

        List<StopControl> stops = new List<StopControl>();
        private Timer timer;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            SetContentView(Resource.Layout.activity_JourneyInfo);
            base.OnCreate(savedInstanceState);
            // Create your application here
            var bundle = Intent.GetBundleExtra("RouteData");


            TextView MainText = FindViewById<TextView>(Resource.Id.MainText);
            MainText.Text = bundle.GetString("direction");

            TextView subText = FindViewById<TextView>(Resource.Id.SubText);
            subText.Text = string.Join(", ", bundle.GetStringArray("stations"));
            subText.Selected = true;

            GetTrip(bundle.GetInt("TrainId"));
        }

        public async void GetTrip(int train)
        {
            LinearLayout layout = FindViewById<LinearLayout>(Resource.Id.TripDetailLayout);
            var trip = await NSservice.GetJourney(train);

            stops = new List<StopControl>();
            layout.RemoveAllViews();
            foreach (var stop in trip.stops)
                if(stop.status != "PASSING")
                    stops.Add( new StopControl(this, layout, stop));

            UpdateStopUi();

            timer = new System.Timers.Timer(1000);
            timer.Elapsed += (e, x) => UpdateStopUi();
            timer.AutoReset = true;
            timer.Enabled = true;

        }

        public void UpdateStopUi()
        {

            DateTime now = DateTime.Now;
            DateTime CompareNow = new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, 0, 0);
            for (int i = 0; i < stops.Count; i++)
            {
                StopControl prev = null;
                StopControl Current;
                StopControl Next = null;
                if (i != 0)
                    prev = stops[i - 1];

                Current = stops[i];

                if (i + 1 < stops.Count)
                    Next = stops[i + 1];

                if (prev == null)
                {
                    Current.Progress = GetProgress(now, Current.DepartTimeStamp, Next.ArriveTimeStamp) + .5f;
                }
                else if (Next == null)
                {
                    Current.Progress = GetProgress(now, prev.DepartTimeStamp, Current.ArriveTimeStamp) - .5f;
                }
                else
                {

                    if (Current.ArriveTimeStamp > CompareNow)
                        Current.Progress = GetProgress(now,prev.DepartTimeStamp, Current.ArriveTimeStamp) - .5f;
                    else if (Current.DepartTimeStamp < CompareNow)
                        Current.Progress = GetProgress(now, Current.DepartTimeStamp, Next.ArriveTimeStamp) + .5f;
                    else
                        Current.Progress = .5f;
                }
            }
        }

        private float GetProgress(DateTime now, DateTime Start, DateTime end)
        {
            var CurrentDiff = now - Start;
            var TotalDiff = end - Start;
            return (float)(CurrentDiff / TotalDiff);
        }
    }

}