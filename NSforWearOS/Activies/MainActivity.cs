using Android.App;
using Android.Gms.Common;
using Android.Gms.Extensions;
using Android.Gms.Location;
using Android.OS;
using Android.Support.Wearable.Activity;
using Android.Util;
using Android.Widget;
using NSforWearOS.Models.Departures;
using NSforWearOS.Models.stations;
using NSforWearOS.Services;
using System;
using System.Linq;
using System.Threading;
using Xamarin.Essentials;
using static Android.Views.ViewGroup;
using static Android.Views.View;
using Android.Views;
using Android.Content;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace NSforWearOS
{
    [Activity(Label = "@string/app_name", MainLauncher = true)]
    public class MainActivity : WearableActivity, IOnTouchListener
    {
        TextView textView;
        ImageView imageView;
        EditText EditText;
        LinearLayout layout;
        Button btn;
        private CancellationTokenSource cts;

        FusedLocationProviderClient fusedLocationProviderClient;

        FusedLocationProviderCallback callback;

        HorizontalScrollView scrollView;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.activity_main);
            Xamarin.Essentials.Platform.Init(this, bundle);


            LinearLayout lin = FindViewById<LinearLayout>(Resource.Id.HorizontalLinearLayout);

            for (int i = 0; i < lin.ChildCount; i++)
            {
                var view = lin.GetChildAt(i);

                view.LayoutParameters.Width = Resources.DisplayMetrics.WidthPixels;
                view.SetMinimumWidth(Resources.DisplayMetrics.WidthPixels);
            }

            scrollView = FindViewById<HorizontalScrollView>(Resource.Id.MainHorizontal);
            scrollView.SetOnTouchListener(this);

            new NSforWearOS.Activies.Partials.StationInfo(this, FindViewById<ScrollView>(Resource.Id.StationInfo));
            new NSforWearOS.Activies.Partials.TripSearchControl(this, FindViewById<ScrollView>(Resource.Id.TripSearch));

            imageView = FindViewById<ImageView>(Resource.Id.image);
            EditText = FindViewById<EditText>(Resource.Id.editText1);
            btn = FindViewById<Button>(Resource.Id.button1);
            layout = FindViewById<LinearLayout>(Resource.Id.linearLayout2);
            EditText.AfterTextChanged += SendWebRequest;

            fusedLocationProviderClient = LocationServices.GetFusedLocationProviderClient(this);
            callback = new FusedLocationProviderCallback(OnLocationResult);
            IsGooglePlayServicesInstalled();
            SetAmbientEnabled();
        }


        public bool OnTouch(View v, MotionEvent e)
        {
            if (e.Action == MotionEventActions.Up)
            {

                int Width = Resources.DisplayMetrics.WidthPixels;
                float Divided = (float)scrollView.ScrollX / Resources.DisplayMetrics.WidthPixels;

                scrollView.SmoothScrollTo((int)Math.Round(Divided) * Width, 0);

                return true;
            }
            return false;
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
        protected override void OnStart()
        {
            FindByWebRequest();
            base.OnStart();


        }


        async void FindByWebRequest()
        {

            btn.Text = "searching";
            Models.Location.Location Station;

            Android.Locations.Location location = null;
            int marker = 0;
            try
            {


                location = await fusedLocationProviderClient.GetLastLocationAsync();
                LocationRequest locationRequest = new LocationRequest()
                                                  .SetPriority(LocationRequest.PriorityHighAccuracy)
                                                  .SetInterval(60 * 1000)
                                                  .SetFastestInterval(60 * 1000);

                await fusedLocationProviderClient.RequestLocationUpdatesAsync(locationRequest, callback);

            }
            catch (FeatureNotSupportedException fnsEx)
            {
                btn.Text = "feature not supported";
                // Handle not supported on device exception
            }
            catch (FeatureNotEnabledException fneEx)
            {
                btn.Text = "feature not enabled";
                // Handle not enabled on device exception
            }
            catch (PermissionException pEx)
            {
                btn.Text = "permission missing";
                // Handle permission exception
            }
            catch (Exception ex)
            {
                btn.Text = "location failed\n" + marker;
                // Unable to get location
                return;
            }



        }

        public async void OnLocationResult(LocationResult location)
        {
            Models.Location.Location Station;

            try
            {
                var result = await NSservice.GetClosestStation(location.Locations[0].Latitude.ToString(), location.Locations[0].Longitude.ToString());
                Station = result[0].locations[0];
            }
            catch (Exception exe)
            {
                btn.Text = "station not found";
                return;
            }
            Departures departures = null;
            try
            {

                departures = await NSservice.GetDepartures(Station.stationCode);
            }
            catch (Exception exe)
            {
                btn.Text = "error getting departures";
                return;
            }

            ShowDepartures(departures, Station.name);
        }

        async void SendWebRequest(object sender, EventArgs args)
        {

            btn.Text = "searching";
            Models.Location.Location Station;
            try
            {
                var result = await NSservice.GetPlaces(EditText.Text);
                Station = result[0].locations[0];
            }
            catch
            {
                btn.Text = "station not found";
                return;
            }
            Departures departures = null;
            try
            {

                departures = await NSservice.GetDepartures(Station.stationCode);
            }
            catch
            {
                btn.Text = "error getting departures";
                return;
            }

            ShowDepartures(departures, Station.name);

        }
        System.Timers.Timer timer;
        private void ShowDepartures(Departures departures, string stationname)
        {

            layout.RemoveAllViews();

            (Button, Departure)[] buttons = new(Button, Departure)[departures.departures.Count];
            for (int i = 0; i < departures.departures.Count; i++)
            {
                Departure dep = departures.departures[i];
                Button button = new Button(this);
                button.Click += (s, e) => Button_Click(dep);
                LayoutParams lp = new LayoutParams(LayoutParams.MatchParent, LayoutParams.WrapContent);
                layout.AddView(button, lp);
                buttons[i] = (button, dep);
            }

            Updatetext(buttons);
            timer = new System.Timers.Timer(1000);
            timer.Elapsed += (e, x) => Updatetext(buttons);
            timer.AutoReset = true;
            timer.Enabled = true;
            btn.Text = "showing results: " + stationname;
        }


        private void Updatetext((Button button, Departure departure)[] values)
        {
            foreach (var value in values)
            {
                string topText = value.departure.direction;

                int timeLeft = (int)(value.departure.actualDateTime - DateTime.Now).TotalMinutes;
                string Time = (timeLeft < 20) ? (timeLeft < 1)  ? ">1 min"  : timeLeft + " min" : value.departure.actualDateTime.ToString("HH:mm");
                value.button.Text = topText + "\n " + Time  + ", spoor: " + value.departure.plannedTrack;
            }
        }
        private void Button_Click(Departure departure)
        {
            var intent = new Android.Content.Intent(this, typeof(NSforWearOS.Activies.JourneyInfoActivity));
            var bundle = new Bundle();
            bundle.PutStringArray("stations", departure.routeStations.Select(x => x.mediumName).ToArray());
            bundle.PutString("direction", departure.direction);
            bundle.PutInt("TrainId", int.Parse(departure.product.number));
            intent.PutExtra("RouteData", bundle);
            StartActivity(intent);

        }

        bool IsGooglePlayServicesInstalled()
        {
            var queryResult = GoogleApiAvailability.Instance.IsGooglePlayServicesAvailable(this);
            if (queryResult == ConnectionResult.Success)
            {
                Log.Info("MainActivity", "Google Play Services is installed on this device.");
                return true;
            }

            if (GoogleApiAvailability.Instance.IsUserResolvableError(queryResult))
            {
                // Check if there is a way the user can resolve the issue
                var errorString = GoogleApiAvailability.Instance.GetErrorString(queryResult);
                Log.Error("MainActivity", "There is a problem with Google Play Services on this device: {0} - {1}",
                          queryResult, errorString);

                // Alternately, display the error to the user.
            }

            return false;
        }



    }
    public class FusedLocationProviderCallback : LocationCallback
    {
        readonly Action<LocationResult> activity;

        public FusedLocationProviderCallback(Action<LocationResult> activity)
        {
            this.activity = activity;
        }

        public override void OnLocationAvailability(LocationAvailability locationAvailability)
        {
            Log.Debug("FusedLocationProviderSample", "IsLocationAvailable: {0}", locationAvailability.IsLocationAvailable);
        }

        public override void OnLocationResult(LocationResult result)
        {
            if (result.Locations.Any())
                activity(result);

        }
    }
}


