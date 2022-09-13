using Shopping_App.Models;
using Shopping_App.ViewModels;
using System;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Shopping_App.Views
{
    //[QueryProperty(nameof(IsLoggedOut), nameof(IsLoggedOut))]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public bool isLoggedIn;
        public bool isLoggedOut { get; set; }

        public AppShell AppShell { get; }
        LoginViewModel _ViewModel;

        public LoginPage()
        {
            InitializeComponent();
            AppShell = new AppShell();
            BindingContext = _ViewModel = new LoginViewModel();
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            User admin = new User()
            {
                Username = "Admin",
                Password = "Admin",
                Date = System.DateTime.Now,
                image = "AdminImage.png",
                Email = "Natthaphong@hotmail.com",
                Usertype = "Admin",
            };
            if (!App.Database.GetUsersAsync().Result.Any(c => c.Email == admin.Email))
            {
                await App.Database.SaveUserAsync(admin);
            }

            try
            {
                isLoggedIn = _ViewModel.LoadApplicationProperty<bool>("isLoggedIn");
            }
            catch (Exception)
            {
                await _ViewModel.SaveApplicationProperty("isLoggedIn", false);
            }

            if (isLoggedIn)
            {
                await Shell.Current.GoToAsync($"//{nameof(ItemsPage)}");
            }
        }
        async void SignInClicked(Object sender, EventArgs e)
        {
            var users = await App.Database.GetUsersAsync();

            var admin = users.Find(u => u.Username == E_Username.Text && u.Password == E_Password.Text && u.Usertype == "Admin");
            var user = users.Find(u => u.Username == E_Username.Text && u.Password == E_Password.Text && u.Usertype == "User");

            if (admin != null)
            {
                //await DisplayAlert("Login as Admin", "Login Success", "Ok");              
                await Shell.Current.GoToAsync($"//{nameof(ItemsPage)}");
                await _ViewModel.SaveApplicationProperty("isLoggedIn", true);
            }
            else
            {
                if (user != null)
                {
                    if (user.Usertype == "User")
                    {
                        //await DisplayAlert("Login", "Login Success", "Ok");               
                        await Shell.Current.GoToAsync($"//{nameof(ItemsPage)}");
                        await _ViewModel.SaveApplicationProperty("isLoggedIn", true);
                        Application.Current.Properties["LogInId"] = user.Id;

                        //loggedin = true;
                        //await Shell.Current.GoToAsync($"{nameof(ProfilePage)}?{nameof(ProfilePage.ProfileId)}={user.Id}");
                    }
                    else
                    {
                        await DisplayAlert("Login", "Your Username or Password is incorrect", "Ok");
                    }
                }
                else
                {
                    await DisplayAlert("Login", "Account is invalid", "Ok");
                }

            }
        }

        async void RegisterClickedAsync(Object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync($"{nameof(RegisterPage)}");

        }
        //void OnRegisterClicked(object sender, EventArgs e)
        //{
        //    Navigation.PushAsync(new RegisterPage());
        //}

    }
}