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

namespace NSforWearOS.Activies.Partials
{
    public class StationInfo
    {
        public View view;
        public StationInfo(Context context, ViewGroup parent)
        {
            view = LayoutInflater.From(context).Inflate(Resource.Layout.control_stationInfo, parent);
            view = (view as ViewGroup).GetChildAt((view as ViewGroup).ChildCount - 1);
            


        }
    }
}