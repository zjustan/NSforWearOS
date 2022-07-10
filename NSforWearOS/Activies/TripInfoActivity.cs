using Android.App;
using Android.OS;
using Android.Support.Wearable.Activity;
using Android.Widget;
using NSforWearOS.Services;
using static Android.Views.ViewGroup;
namespace NSforWearOS.Activies
{
    [Activity(Label = "TipInfoActivity")]
    public class TripInfoActivity : WearableActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            SetContentView(Resource.Layout.activity_tripInfo);
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

            layout.RemoveAllViews();
            foreach (var stop in trip.stops)
                if(stop.status != "PASSING")
                new NSforWearOS.controls.StopControl(this, layout, stop);
            
        }
    }
}