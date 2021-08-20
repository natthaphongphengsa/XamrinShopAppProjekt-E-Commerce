using Shopping_App.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Shopping_App.ViewModels
{
    //[QueryProperty(nameof(ItemId), nameof(ItemId))]
    public class MyProductsViewModel : BaseViewModel
    {
        public MyProductsViewModel()
        {
            Title = "Myproducts";
        }
    }
}
