
using Plugin.Media;
using Shopping_App.Models;
using Shopping_App.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Shopping_App.Views
{
    public partial class ProfilePage : ContentPage
    {
        public int ProfileId { get; set; }
        public ObservableCollection<User> Users { get; }
        ProfileViewModel _viewModel;

        public ProfilePage()
        {
            InitializeComponent();
            // Set the BindingContext of the page to a new Note.
            BindingContext = new ProfileViewModel();
            BindingContext = _viewModel = new ProfileViewModel();
            Users = new ObservableCollection<User>();
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            // Retrieve all the notes from the database, and set them as the
            // data source for the CollectionView.
            //var user = await App.Database.GetUsersAsync();
            //ProfileId = await App.Database.GetUserAsync(ProfileId);
            string id = Application.Current.Properties["LogInId"].ToString();
            var user = await App.Database.GetUserAsync(int.Parse(id));
            Users.Add(user);
            Userview.ItemsSource = Users;

            ProfileImage.Source = user.image;

            if (ProfileImage.Source == null || ProfileBackground.Source == null)
            {
                if (ProfileImage.Source == null)
                {
                    ProfileImage.Source = "EmptyProfileImage.png";
                }
                
                ProfileBackground.Source = "BackgroundImage.png";
            }
            return;
        }
        async void AddImageClicked(object sender, EventArgs e)
        {
            //var Imageresult = await MediaPicker.PickPhotoAsync(new MediaPickerOptions
            //{
            //    Title = "Please select a photo!"
            //});
            //var stream = await Imageresult.OpenReadAsync();
            //ProfileImage.Source = ImageSource.FromStream(() => stream);

            await CrossMedia.Current.Initialize();

            var file = await CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
            {
                PhotoSize = Plugin.Media.Abstractions.PhotoSize.Full
            });

            string id = Application.Current.Properties["LogInId"].ToString();
            var user = await App.Database.GetUserAsync(int.Parse(id));
            user.image = file.Path;
            await App.Database.SaveUserAsync(user);


        }
        async void ProductsClicked(object sender, EventArgs e)
        {
            await Shell.Current.Navigation.PushAsync(new MyProductsPage());
        }
        void OrdersClicked(object sender, EventArgs e)
        {

        }
        void FavoriteClicked(object sender, EventArgs e)
        {

        }
        void CustomerClicked(object sender, EventArgs e)
        {

        }
        void PaymentClicked(object sender, EventArgs e)
        {

        }
        void RecieptClicked(object sender, EventArgs e)
        {

        }
    }
}