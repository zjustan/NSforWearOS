using Android.App;
using Android.OS;
using Android.Support.Wearable.Activity;
using Android.Widget;
using NSforWearOS.Services;
using System;
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

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.activity_main);

            imageView = FindViewById<ImageView>(Resource.Id.image);
            EditText = FindViewById<EditText>(Resource.Id.editText1);
            Button btn = FindViewById<Button>(Resource.Id.button1);
            layout = FindViewById<LinearLayout>(Resource.Id.linearLayout2);
            btn.Click += SendWebRequest;

            new controls.DepartureLayout(BaseContext,layout);

            SetAmbientEnabled();
        }


        async void SendWebRequest(object sender, EventArgs args)
        {
            var departures = await NSservice.GetDepartures(EditText.Text);


            layout.RemoveAllViews();
            foreach(var dep in  departures.departures)
            {

                Button button = new Button(this);
                button.Text = dep.direction + " " + dep.actualDateTime.ToString("HH:mm");
                LayoutParams lp = new LayoutParams(LayoutParams.MatchParent, LayoutParams.WrapContent);
                layout.AddView(button, lp);
            }

        }
    }
}


