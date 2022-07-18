using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using NSforWearOS.Models.trips;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NSforWearOS.Activies.controls
{
    public class TripPreviewControl
    {
        Trip Trip;
        View view;
        Context CurrentContext;
        TextView TimeSpan;
        TextView TrainTypeInfo;
        TextView AdditionalInfo;
        public TripPreviewControl(Context context, LinearLayout Parent, NSforWearOS.Models.trips.Trip trip)
        {
            view = LayoutInflater.From(context).Inflate(Resource.Layout.control_tripPreview, Parent);
            view = (view as ViewGroup).GetChildAt((view as ViewGroup).ChildCount - 1);

            Trip = trip;
            view.Clickable = true;
            view.Click += OpenTripInfo;

            CurrentContext = context;

            TimeSpan = view.FindViewById<TextView>(Resource.Id.TimeSpans);

            string StartTime = trip.legs.First().origin.plannedDateTime.ToString("HH:mm");
            string EndTime = trip.legs.Last().destination.plannedDateTime.ToString("HH:mm");
            
            TimeSpan.Text = $"{StartTime}→{EndTime}";

            TrainTypeInfo = view.FindViewById<TextView>(Resource.Id.TrainTypeInfo);

            string Trains = string.Join(" + ", trip.legs.Select(x => x.product.operatorCode.ToUpper() +" "+ x.product.shortCategoryName));

            TrainTypeInfo.Text = Trains;


            AdditionalInfo = view.FindViewById<TextView>(Resource.Id.Notes);

            AdditionalInfo.Text = $"€{(trip.fares.First().priceInCents / 100).ToString("0.00")}";


        }

        private void OpenTripInfo(object sender, EventArgs e)
        {
            var intent = new Android.Content.Intent(CurrentContext, typeof(NSforWearOS.Activies.TripInfoActivity));
            var bundle = new Bundle();
            bundle.PutInt("IdX", Trip.idx);
            intent.PutExtra("TripData", bundle);
            CurrentContext.StartActivity(intent);
        }
    }
}
