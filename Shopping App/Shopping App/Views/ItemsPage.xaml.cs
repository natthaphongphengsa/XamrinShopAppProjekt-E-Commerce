using Shopping_App.Models;
using Shopping_App.ViewModels;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Forms;

namespace Shopping_App.Views
{
    public partial class ItemsPage : ContentPage
    {
        ItemsViewModel _viewModel;
        //List<Item> items = new List<Item>();
        public ObservableCollection<Item> Items { get; set; }

        public ItemsPage()
        {
            InitializeComponent();
            Items = new ObservableCollection<Item>();
            BindingContext = _viewModel = new ItemsViewModel();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            var products = App.Database.GetItemsAsync().Result.ToList();
            foreach (var item in products)
            {
                Items.Add(item);
            }
            ItemsListView.ItemsSource = Items;
        }

        internal async void Refresh()
        {
            try
            {
                ItemsListView.ItemsSource = Items;
            }
            catch (Exception)
            {
                await DisplayAlert("Update", "Failed to Refreshing", "Return");
            }
            //finally
            //{
            //    //await DisplayAlert("Update", "Refreshing successfully", "Return");
            //    IsBusy = false;
            //}
        }

        async void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection != null)
            {
                // Navigate to the NoteEntryPage, passing the ID as a query parameter.
                Item item = (Item)e.CurrentSelection.FirstOrDefault();
                await Shell.Current.GoToAsync($"{nameof(AddItemsPage)}?{nameof(AddItemViewModel.ItemId)}={item.Id}");
            }
        }

        public async void FilterEntry_SelectedIndexChangedAsync(object sender, EventArgs e)
        {
            var products = await App.Database.GetItemsAsync();
            string type = FilterEntry.Items[FilterEntry.SelectedIndex];
            Items.Clear();
            switch (type)
            {
                case "Popular":
                    var p = products.OrderBy(c => c.Id);
                    foreach (var P in p)
                    {
                        Items.Add(P);
                    }
                    break;
                case "High price first":
                    var h = products.OrderByDescending(c => c.Price);
                    foreach (var H in h)
                    {
                        Items.Add(H);
                    }
                    break;
                case "Low price first":
                    var l = products.OrderBy(c => c.Price);
                    foreach (var L in l)
                    {
                        Items.Add(L);
                    }
                    break;
                case "Alphabetical":
                    var a = products.OrderBy(c => c.Title);
                    foreach (var A in a)
                    {
                        Items.Add(A);
                    }
                    break;
                case "SecoundHand":
                    break;
                case "Brand new":
                    break;
                case "None":
                    foreach (var A in products)
                    {
                        Items.Add(A);
                    }
                    break;
            }
            ItemsListView.ItemsSource = Items;
        }
    }
}