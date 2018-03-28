using Android.App;
using Android.Widget;
using Android.OS;
using Android.Gms.Common;
using Firebase.Iid;
using Square.OkHttp3;
using Android.Util;
using GoogleGson;
using Newtonsoft.Json;

namespace FCMFirstTest
{
    [Activity(Label = "FCMFirstTest", MainLauncher = true)]
    public class MainActivity : Activity
    {
        TextView msgText;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
            msgText = FindViewById<TextView>(Resource.Id.msgText);
            IsPlayServicesAvailable();
            Button bt = FindViewById<Button>(Resource.Id.bt);
            bt.Click += Bt_Click;
            
        }

        private void Bt_Click(object sender, System.EventArgs e)
        {
            Mes mes = new Mes(MyFirebaseIIDService.token,
                new Noti("great","yes"));
            string json = JsonConvert.SerializeObject(mes);
            Log.Error("json",json);
            OkHttpClient client = new OkHttpClient();
            RequestBody body = RequestBody.Create(
            MediaType.Parse("application/json; charset=utf-8"),json);
            Request request = new Request.Builder()
                .Url("https://fcm.googleapis.com/fcm/send")
                .Post(body)
                .AddHeader("Authorization", "key=AAAAjPn9-TY:APA91bE-g4774KmFI72V1gWATmK8uta7N7NgcufoEgGgdidU9wyWBQ5YagCjP0WPBKrgILHZSVeb1I9vegYC-YfFHE2umWWcTzjo-t7W8ynDkwbB6qHY7JZExaxxvlI3VIg3d66sFZ40")
                .Build();

            // Synchronous blocking call
           
            client.NewCall(request).Enqueue(
                (call, response) => {
                // Response came back
                string body1 = response.Body().String();
                Log.Error("lv",body1);
                }, (call, exception) => {
                    // There was an error
                    Log.Error("lv", exception.Message);
                });
        }

        public bool IsPlayServicesAvailable()
        {
            int resultCode = GoogleApiAvailability.Instance.IsGooglePlayServicesAvailable(this);
            if (resultCode != ConnectionResult.Success)
            {
                if (GoogleApiAvailability.Instance.IsUserResolvableError(resultCode))
                    msgText.Text = GoogleApiAvailability.Instance.GetErrorString(resultCode);
                else
                {
                    msgText.Text = "This device is not supported";
                    Finish();
                }
                return false;
            }
            else
            {
                msgText.Text = "Google Play Services is available.";
                return true;
            }
        }

    }
}

