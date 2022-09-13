using Shopping_App.Models;
using Shopping_App.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Shopping_App.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        private Item _selectedItem;

        public IList<FilterType> filtertype { get; set; }
        public Picker SelectedFilterType { get; }

        public ObservableCollection<Item> Items { get; }
        public List<Item> Products { get; set; }
        public Command LoadItemsCommand { get; }
        public Command SearchItemCommand { get; }
        public Command<Item> ItemTapped { get; }

        public ItemsViewModel()
        {
            Title = "Browse";
            Items = new ObservableCollection<Item>();
#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
            LoadItemsCommand = new Command(() => ExecuteLoadItemsCommandAsync());
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
            ItemTapped = new Command<Item>(OnItemSelected);
            SearchItemCommand = new Command(OnSearchButtom);

            try
            {
                filtertype = new ObservableCollection<FilterType>();
                filtertype.Add(new FilterType() { Id = 1, Name = "None" });
                filtertype.Add(new FilterType() { Id = 2, Name = "Popular" });
                filtertype.Add(new FilterType() { Id = 3, Name = "High price first" });
                filtertype.Add(new FilterType() { Id = 4, Name = "Low price first" });
                filtertype.Add(new FilterType() { Id = 5, Name = "Alphabetical" });
                filtertype.Add(new FilterType() { Id = 6, Name = "SecoundHand" });
                filtertype.Add(new FilterType() { Id = 7, Name = "Brand new" });

            }
            catch (Exception)
            {
                Debug.WriteLine("Error to load filtrer");
            }
        }
        async Task ExecuteLoadItemsCommandAsync()
        {
            IsBusy = true;
            try
            {
                new ItemsPage().Refresh();
                var product = await App.Database.GetItemsAsync();

                foreach (var item in product)
                {
                    Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }

            //await shell.current.gotoasync("//itemspage");
        }
        public new void OnAppearing()
        {
            IsBusy = true;
            SelectedItem = null;
        }
        public Item SelectedItem
        {
            get => _selectedItem;
            set
            {
                SetProperty(ref _selectedItem, value);
                OnItemSelected(value);
            }
        }
        private async void OnAddItem(object obj)
        {
            await Shell.Current.GoToAsync(nameof(AddItemsPage));
        }
        private async void OnSearchButtom(object obj)
        {
            //await Shell.Current.Navigation.PushModalAsync(new SearchItemPage());
            await Shell.Current.GoToAsync($"{nameof(SearchItemPage)}");
        }
        private async void OnItemSelected(Item item)
        {
            if (item == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(ItemDetailPage)}?{nameof(ItemDetailViewModel.ItemId)}={item.Id}");
        }
    }
}