using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V7.App;
using Shopping_App.Droid;

namespace com.xamarin.sample.splashscreen
{
    [Activity(
        Label = "ShopNow",
        Icon = "@mipmap/AppIcon",
        Theme = "@style/MyTheme.Splash",
        MainLauncher = true,
        NoHistory = true)]
    public class SplashActivity : AppCompatActivity
    {
        static readonly string TAG = "X:" + typeof(SplashActivity).Name;

        public override void OnCreate(Bundle savedInstanceState, PersistableBundle persistentState)
        {
            base.OnCreate(savedInstanceState, persistentState);
        }
        // Launches the startup task
        protected override void OnResume()
        {
            base.OnResume();
            StartActivity(new Intent(Application.Context, typeof(MainActivity)));
        }
    }
}