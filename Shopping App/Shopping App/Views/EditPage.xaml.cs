using Plugin.Media;
using Shopping_App.Models;
using Shopping_App.ViewModels;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Shopping_App.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public partial class EditPage : ContentPage
    {
        EditItemViewModel _ViewModel;

        public new int Id { get; set; }
        public int ItemId { get { return ItemId; } set { Id = value; } }

        public EditPage()
        {
            InitializeComponent();
            BindingContext = new Item();
            BindingContext = _ViewModel = new EditItemViewModel();
            Shell.SetBackButtonBehavior(this, new BackButtonBehavior
            {
                Command = new Command(async () =>
                {
                    await Shell.Current.GoToAsync($"//D_FAULT_FlyoutItem13/D_FAULT_Tab10/MyProductsPage/");
                }),
                IconOverride = "back.png"
            });
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            _ViewModel.ItemId = Id;
            var GetItem = await App.Database.GetItemAsync(Id);
            name.Text = GetItem.Title;
            GetImage.Source = GetItem.Image;
            description.Text = GetItem.Description;
            specifikation.Text = GetItem.Specifikation;
            QualityEntry.SelectedItem = GetItem.Quality;
            QuantityEntry.SelectedItem = GetItem.Quantity;
            //quantity.Text = GetItem.Quantity.ToString();
            price.Text = GetItem.Price.ToString();
            //image.Text = GetItem.Image.ToString();
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
            _ViewModel.Image = file.Path;
            GetImage.Source = file.Path;
        }
        private void QualityEntry_SelectedIndexChangedAsync(object sender, EventArgs e)
        {
            string type = QualityEntry.Items[QualityEntry.SelectedIndex];
            switch (type)
            {
                case "SecoundHand":
                    _ViewModel.Quality = "SecoundHand";
                    break;
                case "Brand new":
                    _ViewModel.Quality = "Brand new";
                    break;
            }
        }

        private void QuantityEntry_SelectedIndexChanged(object sender, EventArgs e)
        {
            object type = QuantityEntry.SelectedItem;
            _ViewModel.Quantity = int.Parse(type.ToString());
        }
    }
}