using Newtonsoft.Json;
using Shopping_App.Models;
using Shopping_App.ViewModels;
using Shopping_App.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Shopping_App
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        LoginViewModel _ViewModel;
        public AppShell()
        {
            InitializeComponent();
            OnAppearing();
            _ViewModel = new LoginViewModel();
            Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
            Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            Routing.RegisterRoute(nameof(SearchItemPage), typeof(SearchItemPage));
            Routing.RegisterRoute(nameof(RegisterPage), typeof(RegisterPage));
            Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
            Routing.RegisterRoute(nameof(AddItemsPage), typeof(AddItemsPage));
            Routing.RegisterRoute(nameof(HomePage), typeof(HomePage));
            Routing.RegisterRoute(nameof(ItemsPage), typeof(ItemsPage));
            Routing.RegisterRoute(nameof(ProfilePage), typeof(ProfilePage));
            Routing.RegisterRoute(nameof(MyProductsPage), typeof(MyProductsPage));
            Routing.RegisterRoute(nameof(EditPage), typeof(EditPage));
            Routing.RegisterRoute(nameof(ChatPage), typeof(ChatPage));
            Routing.RegisterRoute(nameof(CartPage), typeof(CartPage));
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            var getItems = await App.Database.GetItemsAsync();

            if (getItems.Count == 0)
            {
                //Get Fake products
                var webClient = new WebClient();
                webClient.Encoding = Encoding.UTF8;
                var json = webClient.DownloadString(@"https://fakestoreapi.com/products/");
                var products = JsonConvert.DeserializeObject<List<Item>>(json);
                foreach (var item in products)
                {
                    Item newitem = new Item();
                    {
                        newitem.Title = item.Title;
                        newitem.Description = item.Description;
                        newitem.Specifikation = item.Specifikation;
                        newitem.Image = item.Image;
                        newitem.Price = item.Price;
                        newitem.Quantity = 2;
                    }
                    if (!App.Database.GetItemsAsync().Result.Any(p => p.Title == newitem.Title))
                    {
                        await App.Database.SaveItemAsync(newitem);
                    }
                }
            }
        }

        private async void LogOutClicked(object sender, EventArgs e)
        {
            Preferences.Clear();
            await _ViewModel.SaveApplicationProperty("isLoggedIn", false);
            await Current.GoToAsync($"//{nameof(LoginPage)}");
            //await Current.GoToAsync($"//{nameof(LoginPage)}");
        }
    }
}
