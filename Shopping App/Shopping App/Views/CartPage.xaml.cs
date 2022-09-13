
using Shopping_App.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Input;
using Xamarin.Forms;

namespace Shopping_App.Views
{
    //[XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CartPage : ContentPage
    {
        public ICommand LoadCartProducts { get; }
        public List<Item> item = new List<Item>();
        Item items;

        public CartPage()
        {
            InitializeComponent();
            LoadCartProducts = new Command(() => Loadproducts());
            //BindingContext = new CartPage();
        }

        public CartPage(Item getitem)
        {
            item = new List<Item>();

            Application.Current.Properties["title"] = getitem.Title;
            Application.Current.Properties["image"] = getitem.Image;
            Application.Current.Properties["des"] = getitem.Description;
            Application.Current.Properties["spec"] = getitem.Specifikation;
            Application.Current.Properties["price"] = getitem.Price;
            Application.Current.Properties["quality"] = getitem.Quality;
            Application.Current.Properties["quantity"] = getitem.Quantity;

        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (Application.Current.Properties.ContainsKey("title"))
            {
                string title = Application.Current.Properties["title"].ToString();
                string image = Application.Current.Properties["image"].ToString();
                string des = Application.Current.Properties["des"].ToString();
                //string spec = Application.Current.Properties["spec"].ToString();
                //string quality = Application.Current.Properties["quality"].ToString();
                string quantity = Application.Current.Properties["quantity"].ToString();
                string price = Application.Current.Properties["price"].ToString();

                items = new Item()
                {
                    Title = title,
                    Image = image,
                    Description = des,
                    Quality = "Brand new",
                    Quantity = int.Parse(quantity),
                    Price = decimal.Parse(price),
                };
                var i = item.Find(t => t.Title == items.Title);
                if (i == null)
                {
                    item.Add(items);
                }
                Myproducts.ItemsSource = item;
            }
        }

        public void Loadproducts()
        {
            IsBusy = true;
            try
            {
                Myproducts.ItemsSource = item;
            }
            catch (Exception)
            {
                Debug.WriteLine("Error could'n load");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}