using Shopping_App.Models;
using Shopping_App.Views;

namespace Shopping_App.ViewModels
{
    public class CartViewModel : BaseViewModel
    {
        Item item;
        public int productsId { get; set; }
        //public List<Item> Item { get; set; }
        CartPage cartPage;
        public CartViewModel()
        {
            Title = "Cart";
        }

        public CartViewModel(int itemId)
        {
            Title = "Cart";
            item = new Item();
            addtocart(itemId);
        }
        public async void addtocart(int itemid)
        {
            Item getitem = await App.Database.GetItemAsync(itemid);
            cartPage = new CartPage(getitem);
        }
    }
}
