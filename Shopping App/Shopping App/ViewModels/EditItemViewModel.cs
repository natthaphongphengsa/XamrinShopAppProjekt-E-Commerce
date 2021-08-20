using Shopping_App.Models;
using Shopping_App.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using Xamarin.Forms;

namespace Shopping_App.ViewModels
{
    public class EditItemViewModel : BaseViewModel
    {
        private int id;
        private string title;
        private string description;
        private string specifikation;
        private string image;
        public string quality;
        public int quantity;
        public decimal price;

        public int Id { get => id; set => SetProperty(ref id, value); }
        public new string Title { get => title; set => SetProperty(ref title, value); }
        public string Description { get => description; set => SetProperty(ref description, value); }
        public string Specifikation { get => specifikation; set => SetProperty(ref specifikation, value); }
        public string Image { get => image; set => SetProperty(ref image, value); }
        public string Quality { get => quality; set => SetProperty(ref quality, value); }
        public int Quantity { get => quantity; set => SetProperty(ref quantity, value); }
        public decimal Price { get => price; set => SetProperty(ref price, value); }

        public int ItemId { get { return ItemId; } set { Id = value; } }

        public Command UpdateCommand { get; }
        public Command CancelCommand { get; }

        public IList<QualityType> Qualitytype { get; set; }
        public IList<int> Quantitynumber { get; set; }
        public Picker Selected { get; }

        AddItemViewModel _additemviewmodel;

        public EditItemViewModel()
        {
            _additemviewmodel = new AddItemViewModel();
            _additemviewmodel.SetPickerValue();
            Qualitytype = _additemviewmodel.qualitytype;
            Quantitynumber = _additemviewmodel.quantitynumber;

            UpdateCommand = new Command(OnSave);
            CancelCommand = new Command(OnCancel);

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
                newitem.Quantity = Quantity;
                newitem.Quality = Quality;
                newitem.Price = Price;
            }

            try
            {
                await App.Database.SaveItemAsync(newitem);
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to add to database");
            }

            await Shell.Current.GoToAsync($"//D_FAULT_FlyoutItem13/D_FAULT_Tab10/MyProductsPage/");
        }
    }
}
