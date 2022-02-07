using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Widget;
using AndroidX.AppCompat.App;
using System;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace ClipboardEx
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        EditText editText;
        TextView mytext;
        
        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
        
            SetContentView(Resource.Layout.activity_main);


            editText = FindViewById<EditText>(Resource.Id.editText1);
            mytext = FindViewById<TextView>(Resource.Id.textView1);
            
            var hasText = Clipboard.HasText;
            await Clipboard.SetTextAsync("Hello World");
            _ = GetTextAsync();
            
           

            Clipboard.ClipboardContentChanged += Clipboard_ClipboardContentChanged;
            await Clipboard.SetTextAsync("Welcome");
            _ = GetTextAsync();



            var blueHex = ColorConverters.FromHex("#3498db");

            var bluewithAlpha = blueHex.MultiplyAlpha(0.5f);

            var system = System.Drawing.Color.FromArgb(204, 70, 53);
            var platform = system.ToPlatformColor();

            mytext.SetBackgroundColor(platform);
            

       

        }

        private async Task GetTextAsync()
        {
            var text = await Clipboard.GetTextAsync();
            mytext.Text = text;
        }

        private void Clipboard_ClipboardContentChanged(object sender, System.EventArgs e)
        {
            

            Log.Debug(tag: "Info", $"last clipboard change at {DateTime.UtcNow:t}");
            Toast.MakeText(Application.Context, $"last clipboard change at {DateTime.UtcNow:t}", ToastLength.Short).Show();
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}