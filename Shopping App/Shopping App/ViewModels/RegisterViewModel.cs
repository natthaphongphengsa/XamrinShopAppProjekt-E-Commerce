using Shopping_App.Models;
using Shopping_App.Views;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace Shopping_App.ViewModels
{
    public class RegisterViewModel : BaseViewModel
    {
        public ObservableCollection<User> Users { get; }
        public Command GoBackCommand { get; }
        public RegisterViewModel()
        {
            Title = "Register";
            Users = new ObservableCollection<User>();
            GoBackCommand = new Command(CancelCreateAccount);
        }
        private async void CancelCreateAccount(object obj)
        {
            //await Navigation.PushAsync(new ItemsPage());
            await Shell.Current.GoToAsync($"//{nameof(ItemsPage)}");
        }
    }
}
