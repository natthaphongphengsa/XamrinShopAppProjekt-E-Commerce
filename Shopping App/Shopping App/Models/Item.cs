using SQLite;
using System;

namespace Shopping_App.Models
{
    public class Item
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [Indexed]
        public int UserId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Specifikation { get; set; }
        public string Image { get; set; }
        public string Quality { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }

        public DateTime Date { get; set; }
        public string Filename { get; internal set; }

        //public int Id { get; set; }
        //public string Name { get; set; }
        //public string Description { get; set; }
        //public string image { get; set; }
        //public decimal Price { get; set; }
        //public int Stock { get; set; }


    }
}