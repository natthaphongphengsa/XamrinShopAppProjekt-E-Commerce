using SQLite;
using System;

namespace Shopping_App.Models
{
    public class User
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string image { get; set; }
        public string Email { get; set; }
        public string Usertype { get; set; }

        public DateTime Date { get; set; }
        //public string Filename { get; internal set; }

        //public User(string Username, string Password, string Email)
        //{
        //    this.Username = Username;
        //    this.Password = Password;
        //    this.Email = Email;
        //}
        //public bool CheckInformation()
        //{
        //    if (!this.Username.Equals("") && !this.Password.Equals(""))
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}
    }
}
