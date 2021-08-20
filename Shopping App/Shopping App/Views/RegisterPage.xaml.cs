using Android.Graphics;
using Plugin.Media;
using Shopping_App.Models;
using System;
using System.IO;
using System.Text.RegularExpressions;
using Xamarin.Forms;

namespace Shopping_App.Views
{
    public partial class RegisterPage : ContentPage
    {
        private string ImageFilePath;
        public RegisterPage()
        {
            InitializeComponent();
            BindingContext = new User();            
        }
        //void CreateUserAccount(object sender, EventArgs e)
        //{
        //    //User user = new User(E_Username.Text, E_Password.Text, E_Email.Text);
        //}
        async void CreateUserAccount(object sender, EventArgs e)
        {
            var user = (User)BindingContext;
            user.Date = DateTime.UtcNow;
            user.Usertype = "User";
            user.image = ImageFilePath;
            var email = EmailEntry.Text;
            var emailPattern = @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
            @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$";

            if (Regex.IsMatch(email, emailPattern))
            {
                if (!string.IsNullOrWhiteSpace(user.Username))
                {
                    await App.Database.SaveUserAsync(user);
                    Application.Current.Properties["LogInId"] = user.Id;
                    await DisplayAlert("Register", "Register Success", "Ok");
                    await Shell.Current.GoToAsync($"//{nameof(ItemsPage)}");
                }
                else
                {
                    // Navigate backwards
                    await Shell.Current.GoToAsync("..");
                }
            }
            else
            {
                ErrorEmail.Text = "Email is invalid";
            }
        }
        async void OnCancelButtonClicked(object sender, EventArgs e)
        {
            var user = (User)BindingContext;
            await App.Database.DeleteUserAsync(user);

            // Navigate backwards
            await Shell.Current.GoToAsync("..");
        }
        async void OnAddImageButtonClicked(object sender, EventArgs e)
        {
            await CrossMedia.Current.Initialize();

            var file = await CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
            {
                PhotoSize = Plugin.Media.Abstractions.PhotoSize.Full                
            });

            byte[] imageArray = File.ReadAllBytes(file.Path);
            Bitmap bitmap = BitmapFactory.DecodeByteArray(imageArray, 0, imageArray.Length);
            ImageFilePath = file.Path;
            Userimage.Source = ImageFilePath;


            //var Imageresult = await MediaPicker.PickPhotoAsync(new MediaPickerOptions
            //{
            //    Title = "Please select a photo!"
            //});
            //var stream = await Imageresult.OpenReadAsync();
            //Userimage.Source = ImageSource.FromStream(() => stream);
        }
    }
}