using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            string id = Application.Current.Properties["LogInId"].ToString();
            var item = await App.Database.GetUserAsync(int.Parse(id));
            ProfileImage.Source = item.image;


        }
    }
}