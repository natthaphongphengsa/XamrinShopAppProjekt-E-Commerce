using Plugin.Media;
using Shopping_App.Models;
using Shopping_App.ViewModels;
using System;
using Xamarin.Forms;

namespace Shopping_App.Views
{
    //[XamlCompilation(XamlCompilationOptions.Compile)]
    //[QueryProperty(nameof(ItemId), nameof(ItemId))]
    public partial class AddItemsPage : ContentPage
    {
        //public ICommand GoBackCommand { get; }
        //public string ItemId { set { LoadItem(value); } }

        AddItemViewModel _Viewmodel;
        public AddItemsPage()
        {
            InitializeComponent();
            // Set the BindingContext of the page to a new Note.            
            BindingContext = new Item();
            BindingContext = _Viewmodel = new AddItemViewModel();
            Shell.SetBackButtonBehavior(this, new BackButtonBehavior
            {
                Command = new Command(async () =>
                {
                    await Shell.Current.GoToAsync($"//D_FAULT_FlyoutItem13/D_FAULT_Tab10/MyProductsPage/");
                }),
                IconOverride = "back.png"
            });
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();

            // Retrieve all the notes from the database, and set them as the
            // data source for the CollectionView.

        }
        async void OnAddImageButtonClicked(object sender, EventArgs e)
        {
            //var Imageresult = await MediaPicker.PickPhotoAsync(new MediaPickerOptions
            //{
            //    Title = "Please select a photo!"
            //});
            //var stream = await Imageresult.OpenReadAsync();
            ////GetImage.Source = ImageSource.FromStream(() => stream);

            await CrossMedia.Current.Initialize();

            var file = await CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
            {
                PhotoSize = Plugin.Media.Abstractions.PhotoSize.Full
            });
            GetImage.Source = file.Path;
            _Viewmodel.Image = file.Path;

        }
        private void QualityEntry_SelectedIndexChangedAsync(object sender, EventArgs e)
        {
            string type = QualityEntry.Items[QualityEntry.SelectedIndex];
            switch (type)
            {
                case "SecoundHand":
                    _Viewmodel.Quality = "SecoundHand";
                    break;
                case "Brand new":
                    _Viewmodel.Quality = "Brand new";
                    break;
            }
        }

        private void QuantityEntry_SelectedIndexChanged(object sender, EventArgs e)
        {
            object type = QuantityEntry.SelectedItem;
            _Viewmodel.Quantity = int.Parse(type.ToString());
        }
    }
}