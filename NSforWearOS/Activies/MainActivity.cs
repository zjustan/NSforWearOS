using Android.App;
using Android.Gms.Common;
using Android.Gms.Extensions;
using Android.Gms.Location;
using Android.OS;
using Android.Support.Wearable.Activity;
using Android.Util;
using Android.Widget;
using NSforWearOS.Models.Departures;
using NSforWearOS.Services;
using System;
using System.Linq;
using System.Threading;
using Xamarin.Essentials;
using static Android.Views.ViewGroup;

namespace NSforWearOS
{
    [Activity(Label = "@string/app_name", MainLauncher = true)]
    public class MainActivity : WearableActivity
    {
        TextView textView;
        ImageView imageView;
        EditText EditText;
        LinearLayout layout;
        Button btn;
        private CancellationTokenSource cts;

        FusedLocationProviderClient fusedLocationProviderClient;

        LocationCallback callback;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.activity_main);
            Xamarin.Essentials.Platform.Init(this, bundle);



            imageView = FindViewById<ImageView>(Resource.Id.image);
            EditText = FindViewById<EditText>(Resource.Id.editText1);
             btn = FindViewById<Button>(Resource.Id.button1);
            layout = FindViewById<LinearLayout>(Resource.Id.linearLayout2);
            EditText.AfterTextChanged += SendWebRequest;

            fusedLocationProviderClient = LocationServices.GetFusedLocationProviderClient(this);
            callback = new LocationCallback();
            IsGooglePlayServicesInstalled();
            SetAmbientEnabled();
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

               var task =  fusedLocationProviderClient.RequestLocationUpdatesAsync(locationRequest, callback);
                task.Wait();
                if (task.IsFaulted)
                {
                    throw task.Exception;
                }
             
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

            try
            {
                var result = await NSservice.GetClosestStation(location.Latitude.ToString(), location.Longitude.ToString());
                Station = result[0].locations[0];
            }
            catch(Exception exe)
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

        private void ShowDepartures(Departures departures, string stationname)
        {

            layout.RemoveAllViews();
            foreach (var dep in departures.departures)
            {

                Button button = new Button(this);
                button.Text = dep.direction + "\n " + dep.actualDateTime.ToString("HH:mm") + ", spoor: " + dep.plannedTrack;
                button.Click += (s, e) => Button_Click(dep);
                LayoutParams lp = new LayoutParams(LayoutParams.MatchParent, LayoutParams.WrapContent);
                layout.AddView(button, lp);
            }
            btn.Text = "showing results: " + stationname;
        }

        private void Button_Click(Departure departure)
        {
            var intent = new Android.Content.Intent(this, typeof(NSforWearOS.Activies.TripInfoActivity));
            var bundle = new Bundle();
            bundle.PutStringArray("stations", departure.routeStations.Select(x => x.mediumName).ToArray());
            bundle.PutString("direction", departure.direction);
            bundle.PutInt("TrainId", int.Parse( departure.product.number));
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
        readonly MainActivity activity;

        public FusedLocationProviderCallback(MainActivity activity)
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
            {
                var location = result.Locations.First();
                Log.Debug("Sample", "The latitude is :" + location.Latitude);
            }
            else
            {
                // No locations to work with.
            }
        }
    }
}


