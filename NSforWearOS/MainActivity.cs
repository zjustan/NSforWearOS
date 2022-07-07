using Android.App;
using Android.Graphics;
using Android.OS;
using Android.Support.Wearable.Activity;
using Android.Widget;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using static Android.Views.ViewGroup;

namespace NSforWearOS
{
    [Activity(Label = "@string/app_name", MainLauncher = true)]
    public class MainActivity : WearableActivity
    {
        TextView textView;
        ImageView imageView;
        LinearLayout layout;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.activity_main);

            imageView = FindViewById<ImageView>(Resource.Id.image);
            Button btn = FindViewById<Button>(Resource.Id.button1);
            layout = FindViewById<LinearLayout>(Resource.Id.linearLayout2);
            btn.Click += SendWebRequest;
            

            SetAmbientEnabled();
        }


        async void SendWebRequest(object sender, EventArgs args)
        {
            HttpResponseMessage result = null;
            HttpClient client = new HttpClient();
            HttpRequestMessage msg = new HttpRequestMessage();
            msg.RequestUri = new Uri("https://gateway.apiportal.ns.nl/reisinformatie-api/api/v2/departures?station=pt");
            msg.Headers.Add("Ocp-Apim-Subscription-Key", "38aecdd9f0664f64aa5b96a79790cff9");
            try
            {
                 result = await client.SendAsync(msg);
            }
            catch (HttpRequestException exe)
            {
                textView.Text = "request failed";
                return;
            }
            catch (Exception exe)
            {
                textView.Text = exe.Message;
                return;
            }

            Stream stream = await result.Content.ReadAsStreamAsync();

            var serializer = new JsonSerializer();
            using (var sr = new StreamReader(stream))
            using (var jsonTextReader = new JsonTextReader(sr))
            {
                var obj = serializer.Deserialize<Root>(jsonTextReader);

                layout.RemoveAllViews();
                obj.payload.departures.ForEach(x =>
                {

                    Button button = new Button(this);

                    button.Text = x.direction + " " + x.actualDateTime.ToString("HH:mm");
                    LayoutParams lp = new LayoutParams(LayoutParams.MatchParent, LayoutParams.WrapContent);
                    layout.AddView(button,lp);


                });
            }
        }
    }
}


