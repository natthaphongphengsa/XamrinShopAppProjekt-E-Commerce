using Shopping_App.Models;
using Shopping_App.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace Shopping_App.Views
{
    public partial class SearchItemPage : ContentPage
    {
        ItemsViewModel _viewModel;
        List<Item> products = new List<Item>();
        public Item Item { get; set; }

        public SearchItemPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new ItemsViewModel();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            // Retrieve all the notes from the database, and set them as the
            // data source for the CollectionView.
            products = await App.Database.GetItemsAsync();
            SearchResultListView.ItemsSource = products.Take(5);
        }

        private void Searchable_SearchButtonPressed(object sender, EventArgs e)
        {
            var keyword = Searchable.Text;

            var suggestion = products.Where(c => c.Title.Contains(keyword));
            //var s = from c in products where c.Contains(keyword) select c;
            SearchResultListView.ItemsSource = suggestion.ToList();
        }

        public void Searchable_TextChanged(object sender, TextChangedEventArgs e)
        {
            var keyword = Searchable.Text;

            var suggestion = products.Where(c => c.Title.Contains(keyword));
            //var s = from c in products where c.Contains(keyword) select c;
            SearchResultListView.ItemsSource = suggestion.ToList();
        }
        private async void OnItemClicked(object sender, SelectionChangedEventArgs e)
        {
            //Shell.Current.FlyoutIsPresented = false;
            Item item = (Item)e.CurrentSelection.FirstOrDefault();
            var Itempage = new ItemDetailPage();
            Itempage.BindingContext = item;
            await Shell.Current.GoToAsync($"{nameof(ItemDetailPage)}");
            //await Navigation.PushAsync(Itempage);
        }
        //async void SuggstionsListView_ItemTappedAsync(object sender, SelectionChangedEventArgs e)
        //{
        //    if (e.CurrentSelection != null)
        //    {
        //        // Navigate to the NoteEntryPage, passing the ID as a query parameter.
        //        Item item = (Item)e.CurrentSelection.FirstOrDefault();
        //        await Shell.Current.GoToAsync($"{nameof(AddItemsPage)}?{nameof(AddItemsPage.ItemId)}={item.Id.ToString()}");
        //    }
        //}
    }
}