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

namespace NSforWearOS.controls
{
    public class StopControl
    {
        View view;
        TextView ArriveTime;
        TextView DepartTime;
        TextView Station;

        public DateTime ArriveTimeStamp;
        public DateTime DepartTimeStamp;
        public string StationName;

        public float Progress { set => bar.Progress = 
                (int)(value * 100); get => bar.Progress / 100f; }

        private ProgressBar bar;
        public StopControl(Context context, LinearLayout Parent, NSforWearOS.Models.stations.Stop stop )
        {
            view = LayoutInflater.From(context).Inflate(Resource.Layout.control_stop, Parent);
            view = (view as ViewGroup).GetChildAt((view as ViewGroup).ChildCount - 1);
            ArriveTime = view.FindViewById<TextView>(Resource.Id.ArriveTime);
            DepartTime = view.FindViewById<TextView>(Resource.Id.DepartTime);
            Station = view.FindViewById<TextView>(Resource.Id.StationText);
            bar = view.FindViewById<ProgressBar>(Resource.Id.progressBar1);


            if (stop.arrivals.Count > 0)
            {

                ArriveTimeStamp = stop.arrivals[0].actualTime;
                ArriveTime.Text = ArriveTimeStamp.ToString("HH:mm");
            }
            else
            {
                ArriveTime.Text = "";
            }
            if (stop.departures.Count > 0)
            {
                DepartTimeStamp = stop.departures[0].actualTime;
                DepartTime.Text =DepartTimeStamp.ToString("HH:mm");
            }
            else
            {
                DepartTime.Text = "";
            }

            Station.Text = stop.stop.name;
            StationName = stop.stop.name;
        }
    }
}