using Shopping_App.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Shopping_App.Views
{
    public partial class MyProductsPage : ContentPage
    {
        List<Item> products = new List<Item>();
        public int itemId { get; set; }
        string title { get; set; }
        //List<User> users = new List<User>();
        public Command GoBackCommand { get; }
        public Command UpdateCommand { get; }

        public MyProductsPage()
        {
            InitializeComponent();
            UpdateCommand = new Command(async () => await RefreshView_RefreshingAsync());
            Shell.SetBackButtonBehavior(this, new BackButtonBehavior
            {
                Command = new Command(async () =>
                {
                    await Shell.Current.GoToAsync("..");
                }),
                IconOverride = "back.png"
            });
            BindingContext = this;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            // Retrieve all the notes from the database, and set them as the
            // data source for the CollectionView.            
            Myproducts.ItemsSource = await App.Database.GetItemsAsync();
        }
        async void OnAddClicked(object sender, EventArgs e)
        {
            await Shell.Current.Navigation.PushAsync(new AddItemsPage());
        }
        async void OnItemDeleteAsync(int id)
        {
            var itemDelete = await App.Database.GetItemsAsync();
            //if (item == null)
            //    return;
            //// This will push the ItemDetailPage onto the navigation stack
            ////await Shell.Current.GoToAsync($"{nameof(ItemDetailEditPage)}?{nameof(ItemDetailEditViewModel.ItemId)}={item.Id}");
        }

        async void OnDeleteSwipe(object sender, EventArgs e)
        {
            SwipeItem itemselected = sender as SwipeItem;
            products.Add(itemselected.BindingContext as Item);
            foreach (var names in products)
            {
                title = names.Title;
                itemId = names.Id;
            }
            string action = await DisplayActionSheet("Delete", "Cancel", "Ok", "Are you sure?");
            if (action == "Ok")
            {
                try
                {
                    var item = await App.Database.GetItemAsync(itemId);
                    await App.Database.DeleteItemAsync(item);
                    products.Clear();
                }
                catch (Exception)
                {
                    await DisplayAlert("Delete", "Failed to delete this product", "Cancel");
                    products.Clear();
                }
                finally
                {
                    await RefreshView_RefreshingAsync();
                }
            }
            else if (action == "Cancel")
            {
                return;
            }

            //var items = await App.Database.GetItemsAsync();
            //Item item = (Item)e.CurrentSelection.FirstOrDefault();
            //items.Remove(item);
        }
        async void OnEditSwipe(object sender, EventArgs e)
        {
            SwipeItem itemselected = sender as SwipeItem;
            products.Add(itemselected.BindingContext as Item);
            foreach (var names in products)
            {
                title = names.Title;
                itemId = names.Id;
            }

            try
            {
                await Shell.Current.GoToAsync($"{nameof(EditPage)}?{nameof(EditPage.ItemId)}={itemId}");
                products.Clear();
            }
            catch (Exception)
            {
                await DisplayAlert("Edit", "Failed to edit this product", "Cancel");
                products.Clear();
            }
            finally
            {
                await RefreshView_RefreshingAsync();
            }
        }

        async Task RefreshView_RefreshingAsync()
        {
            IsBusy = true;
            try
            {
                Myproducts.ItemsSource = await App.Database.GetItemsAsync();
            }
            catch (Exception)
            {
                await DisplayAlert("Update", "Failed to Refreshing", "Return");
            }
            finally
            {
                //await DisplayAlert("Update", "Refreshing successfully", "Return");
                IsBusy = false;
            }

        }
    }
}