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
using NSforWearOS.Models.trips;
using NSforWearOS.Services;
using NSforWearOS.Models.Departures;
using static Android.App.ActionBar;
using NSforWearOS.Activies.controls;
using System.Threading.Tasks;

namespace NSforWearOS.Activies.Partials
{
    public class TripSearchControl
    {
        public View view;
        public Context context;

        public EditText FromStation;
        public EditText ToStation;

        Button btn;
        public TripSearchControl(Context _context, ViewGroup parent)
        {
            context = _context;
            view = LayoutInflater.From(context).Inflate(Resource.Layout.control_TripSearch, parent);
            view = (view as ViewGroup).GetChildAt((view as ViewGroup).ChildCount - 1);

            FromStation = view.FindViewById<EditText>(Resource.Id.FromStation);
            ToStation = view.FindViewById<EditText>(Resource.Id.ToStation);

            btn = view.FindViewById<Button>(Resource.Id.searchbutton);

            btn.Click += Search;
        }

        private async void Search(object sender, EventArgs e)
        {


            string FromStationCode;
            try
            {
                FromStationCode = await FindStation(FromStation);

            }
            catch
            {
                btn.Text = $"station {FromStation.Text} not found";
                return;
            }

            string ToStationCode;
            try
            {
                ToStationCode = await FindStation(ToStation);

            }
            catch
            {
                btn.Text = $"station {ToStation.Text} not found";
                return;
            }

            btn.Text = "searching";



            try
            {

                TripAdvices advices = await NSservice.GetTripAdvices(FromStationCode, ToStationCode);
                ShowResults(advices);
            }
            catch (Exception exe)
            {
                btn.Text = "getting advices failed";
            }
        }

        public async Task<string> FindStation(EditText text)
        {
            var result = await NSservice.GetPlaces(text.Text);
            var Station = result[0].locations[0];
            text.Text = Station.names.middel;
            return Station.stationCode;


        }

        private void ShowResults(TripAdvices advices)
        {
            LinearLayout parent = view.FindViewById<LinearLayout>(Resource.Id.TipResultsLayout);
            parent.RemoveAllViews();
            foreach (var trip in advices.trips)
            {
                new TripPreviewControl(context, parent, trip);
            }
            btn.Text = "search";
        }
    }
}