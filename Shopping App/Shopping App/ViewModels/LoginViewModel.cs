using Shopping_App.Views;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Shopping_App.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        public Command GoToProfile { get; }
        public Command GoBack { get; }

        public LoginViewModel()
        {
            Title = "Sign in";
            //if (App.Authenticator != null && authenticated == false)
            //{
            //    authenticated = await App.Authenticator.Authenticate();
            //}

            GoToProfile = new Command(OnLoginClicked);
            GoBack = new Command(OnCancelClicked);
        }

        private async void OnLoginClicked(object obj)
        {
            // Prefixing with `//` switches to a different navigation stack instead of pushing to the active one
            await Shell.Current.GoToAsync($"//{nameof(ItemsPage)}");
        }
        private async void OnCancelClicked(object obj)
        {
            await Shell.Current.GoToAsync("//..");
        }
        public async Task SaveApplicationProperty<T>(string key, T value)
        {
            Application.Current.Properties[key] = value;
            await Application.Current.SavePropertiesAsync();
        }
        public T LoadApplicationProperty<T>(string key)
        {
            return (T)Application.Current.Properties[key];
        }
    }
}
