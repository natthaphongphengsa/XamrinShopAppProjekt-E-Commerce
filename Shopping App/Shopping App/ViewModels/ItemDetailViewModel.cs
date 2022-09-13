using System;
using System.Diagnostics;
using System.Windows.Input;
using Xamarin.Forms;

namespace Shopping_App.ViewModels
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public class ItemDetailViewModel : BaseViewModel
    {
        private int itemId;
        private string title;
        private string description;
        private string specifikation;
        private string image;
        private string quality;
        public decimal price;
        public int quantity;

        public string Filter;

        public int Id { get; set; }
        public new string Title { get => title; set => SetProperty(ref title, value); }
        public string Description { get => description; set => SetProperty(ref description, value); }
        public string Specifikation { get => specifikation; set => SetProperty(ref specifikation, value); }
        public string Image { get => image; set => SetProperty(ref image, value); }
        public string Quality { get => quality; set => SetProperty(ref quality, value); }
        public int Quantity { get => quantity; set => SetProperty(ref quantity, value); }
        public decimal Price { get => price; set => SetProperty(ref price, value); }


        public int ItemId { get { return ItemId; } set { itemId = value; LoadItemId(value); } }

        public ICommand MinusCommand { get; }
        public ICommand PlusCommand { get; }
        public Command AddToCartCommand { get; }

        private int number = 0;
        private string icon;

        CartViewModel cartviewmodel;

        public ItemDetailViewModel()
        {
            AddToCartCommand = new Command(AddToCartAsync);
            PlusCommand = new Command(
            execute: () =>
            {
                number++;
                OnPropertyChanged("Number");
                RefreshCanExecutes();
            });

            MinusCommand = new Command(
            execute: () =>
            {
                if (number <= 0)
                {

                }
                else
                {
                    number--;
                    OnPropertyChanged("Number");
                    RefreshCanExecutes();
                }
            });
        }

        private void AddToCartAsync()
        {
            cartviewmodel = new CartViewModel(itemId);
        }

        private void AvailableStatus()
        {
            if (Quantity == 0)
            {
                icon = "NotAvailable.png";
                OnPropertyChanged("Icon");
            }
            else
            {
                icon = "AvailableIcon.png";
                OnPropertyChanged("Icon");
            }

        }

        public string Icon
        {
            set
            {
                if (Quantity == 0)
                {
                    icon = "NotAvailable.png";
                    OnPropertyChanged("Icon");
                }
                else
                {
                    icon = "AvailableIcon.png";
                    OnPropertyChanged("Icon");
                }
            }
            get
            {
                return icon;
            }
        }

        public int Number
        {
            set
            {
                if (number != value)
                {
                    number = value;
                    OnPropertyChanged("Number");
                }
            }
            get
            {
                return number;
            }
        }

        private void RefreshCanExecutes()
        {
            (PlusCommand as Command).ChangeCanExecute();
            (MinusCommand as Command).ChangeCanExecute();
        }

        public async void LoadItemId(int itemId)
        {
            try
            {
                var item = await App.Database.GetItemAsync(itemId);
                Id = item.Id;
                Title = item.Title;
                Description = item.Description;
                Specifikation = item.Specifikation;
                Image = item.Image;
                Quality = item.Quality;
                Quantity = item.Quantity;
                Price = item.Price;

            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
            finally
            {
                AvailableStatus();
            }
        }

    }
}
