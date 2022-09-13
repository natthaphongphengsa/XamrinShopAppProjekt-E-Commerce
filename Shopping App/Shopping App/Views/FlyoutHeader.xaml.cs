using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Shopping_App.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FlyoutHeader : ContentView
    {
        public FlyoutHeader()
        {
            InitializeComponent();
            OnAppearing();
        }
        async void OnAppearing()
        {
            //string isLoggedIn = Application.Current.Properties["isLoggedIn"].ToString();
            //if (isLoggedIn == "True")
            //{
            //    string id = Application.Current.Properties["isLoggedIn"].ToString();
            //    var item = await App.Database.GetUserAsync(int.Parse(id));
            //    ProfileImage.Source = item.image;
            //}
        }
    }
}