using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NSforWearOS;
namespace controls
{
    public class DepartureLayout : LinearLayout
    {
        public DepartureLayout(Context context, LinearLayout Parent) : base(context)
        {
            LayoutInflater.From(context).Inflate(Resource.Layout.control_departure, Parent);
        }
    }
}