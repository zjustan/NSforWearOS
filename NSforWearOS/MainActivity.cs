﻿using Android.App;
using Android.OS;
using Android.Support.Wearable.Activity;
using Android.Widget;
using NSforWearOS.Models.Departures;
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
        Button btn;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.activity_main);

            imageView = FindViewById<ImageView>(Resource.Id.image);
            EditText = FindViewById<EditText>(Resource.Id.editText1);
             btn = FindViewById<Button>(Resource.Id.button1);
            layout = FindViewById<LinearLayout>(Resource.Id.linearLayout2);
            btn.Click += SendWebRequest;

            new controls.DepartureLayout(BaseContext,layout);

            SetAmbientEnabled();
        }


        async void SendWebRequest(object sender, EventArgs args)
        {

            btn.Text = "searching";
            Departures departures = null;
            try
            {

                departures = await NSservice.GetDepartures(EditText.Text);
            }
            catch
            {
                btn.Text = "station not found";
                return;
            }


            layout.RemoveAllViews();
            foreach(var dep in  departures.departures)
            {

                Button button = new Button(this);
                button.Text = dep.direction + "\n " + dep.actualDateTime.ToString("HH:mm") + ", spoor: "+ dep.plannedTrack;
                LayoutParams lp = new LayoutParams(LayoutParams.MatchParent, LayoutParams.WrapContent);
                layout.AddView(button, lp);
            }
            btn.Text = "showing results: " + EditText.Text;

        }
    }
}


