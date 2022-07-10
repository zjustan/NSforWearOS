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
        public StopControl(Context context, LinearLayout Parent, NSforWearOS.Models.stations.Stop stop )
        {
            view = LayoutInflater.From(context).Inflate(Resource.Layout.control_stop, Parent);
            view = (view as ViewGroup).GetChildAt((view as ViewGroup).ChildCount - 1);
            ArriveTime = view.FindViewById<TextView>(Resource.Id.ArriveTime);
            DepartTime = view.FindViewById<TextView>(Resource.Id.DepartTime);
            Station = view.FindViewById<TextView>(Resource.Id.StationText);

           
            if (stop.arrivals.Count > 0)
            {
                
                ArriveTime.Text = stop.arrivals[0].actualTime.ToString("HH:mm");
            }
            else
            {
                ArriveTime.Visibility = ViewStates.Gone;
            }
            if (stop.departures.Count > 0)
            {

                DepartTime.Text = stop.departures[0].actualTime.ToString("HH:mm");
            }
            else
            {
                DepartTime.Visibility = ViewStates.Gone;
            }

            Station.Text = stop.stop.name;
        }
    }
}