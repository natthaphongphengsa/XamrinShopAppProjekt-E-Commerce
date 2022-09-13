using Shopping_App.Models;
using Shopping_App.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Shopping_App.ViewModels
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public class AddItemViewModel : BaseViewModel
    {
        //private int id;
        private string title;
        private string description;
        private string specifikation;
        private string image;
        private string quality;
        private int quantity;
        private decimal price;


        public int Id { get; set; }
        public new string Title { get => title; set => SetProperty(ref title, value); }
        public string Description { get => description; set => SetProperty(ref description, value); }
        public string Specifikation { get => specifikation; set => SetProperty(ref specifikation, value); }
        public string Image { get => image; set => SetProperty(ref image, value); }
        public string Quality { get => quality; set => SetProperty(ref quality, value); }
        public int Quantity { get => quantity; set => SetProperty(ref quantity, value); }
        public decimal Price { get => price; set => SetProperty(ref price, value); }

        public int ItemId { get { return ItemId; } set { Id = value; } }

        public IList<QualityType> qualitytype { get; set; }
        public IList<int> quantitynumber { get; set; }
        public Picker Selected { get; }


        //public string ItemId { set { LoadItem(value); } }

        public Command SaveCommand { get; }
        public Command CancelCommand { get; }
        public Command DeleteCommand { get; }

        public AddItemViewModel()
        {
            Title = "Add product";
            SaveCommand = new Command(OnSave, ValidateSave);
            CancelCommand = new Command(OnCancel);
            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
            SetPickerValue();
        }
        public void SetPickerValue()
        {
            quantitynumber = new ObservableCollection<int>();
            for (int i = 1; i < 100; i++)
            {
                quantitynumber.Add(i);
            }

            try
            {
                qualitytype = new ObservableCollection<QualityType>();
                qualitytype.Add(new QualityType() { Id = 1, Name = "Brand new" });
                qualitytype.Add(new QualityType() { Id = 2, Name = "SecoundHand" });
            }
            catch (Exception)
            {
                Debug.WriteLine("Error to load filtrer");
            }
        }
        private bool ValidateSave()
        {
            return !String.IsNullOrWhiteSpace(title)
                && !String.IsNullOrWhiteSpace(description)
                && !String.IsNullOrWhiteSpace(specifikation)
                && Price != 0
                && Quality != null;
        }
        private async void OnCancel()
        {
            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync($"//D_FAULT_FlyoutItem13/D_FAULT_Tab10/MyProductsPage/");
        }
        private async void OnSave()
        {
            Item newitem = new Item();
            {
                newitem.Id = Id;
                newitem.Title = Title;
                newitem.Description = Description;
                newitem.Specifikation = Specifikation;
                newitem.Image = Image;
                newitem.Quality = Quality;
                newitem.Quantity = Quantity;
                newitem.Price = Price;
            }
            try
            {
                var check = await App.Database.GetItemsAsync();
                foreach (var item in check)
                {
                    if (item.Title == newitem.Title)
                    {
                        await Shell.Current.Navigation.PushModalAsync(new ItemDetailPage());
                    }
                    await App.Database.SaveItemAsync(newitem);
                }
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to add to database");
            }
            await Shell.Current.GoToAsync($"//D_FAULT_FlyoutItem13/D_FAULT_Tab10/MyProductsPage/");
        }
    }
}
