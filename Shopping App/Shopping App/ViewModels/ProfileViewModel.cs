using Shopping_App.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Shopping_App.ViewModels
{
    [QueryProperty(nameof(ProfileId), nameof(ProfileId))]
    public class ProfileViewModel: BaseViewModel
    {
        public Command LoadProfileCommand { get; }
        public ObservableCollection<User> Users { get; }

        public int profileId;
        private string name;
        private string email;
        private string password;
        private string image;

        public int Id { get; set; }
        public string Name { get => name; set => SetProperty(ref name, value); }
        public string Email { get => email; set => SetProperty(ref email, value); }
        public string Password { get => password; set => SetProperty(ref password, value); }
        public string Image { get => image; set => SetProperty(ref image, value); }
        public int ProfileId { get { return ProfileId; } set { profileId = value; LoadUserId(value); } }

        public ProfileViewModel()
        {
            Users = new ObservableCollection<User>();
            LoadProfileCommand = new Command(async () => await ExecuteLoadProfileCommand());
        }
        async Task ExecuteLoadProfileCommand()
        {
            IsBusy = true;
            try
            {
                Users.Clear();
                var users = await App.Database.GetUserAsync(profileId);
                Users.Add(users);
                //foreach (var user in users)
                //{
                //    Users.Add(user);
                //}
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
        
        public async void LoadUserId(int UserId)
        {
            try
            {
                var user = await App.Database.GetUserAsync(UserId);
                //Id = user.Id;
                Name = user.Username;
                Email = user.Email;
                Password = user.Password;
                image = user.image;
                //Description = item.Description;
                //Specifikation = item.Specifikation;
                //Image = item.Image;
                //Price = item.Price;
                //Stock = item.Stock;
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }
    }
}
