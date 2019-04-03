using System;
using System.Text;
using System.Collections.Generic;
using SQLite;

namespace MyImdbLibrary
{
    [Table("User")]
    public class User
    {
        private string _email;

        [PrimaryKey, AutoIncrement, Column("Id")]
        public int Id { get; set; }
        [MaxLength(8)]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Country { get; set; }
        public string Email
        {
            get { return _email; }
            set
            {
                if (DatabaseManager.IsValidEmail(value)) { _email = value; }
                else { throw new Exception("The email address entered is not valid."); }
            }
        }
        public DateTime LastSeen { get; set; }
        public string Password { get; set; }
        public string OmdbApiKey { get; set; }

        public User() { }
        public User(string firstName, string lastName, int country, string email, string password, string omdbApiKey)
        {
            FirstName = firstName;
            LastName = lastName;
            Country = country;
            Email = email;
            Password = password;
            LastSeen = DateTime.Now;
            OmdbApiKey = omdbApiKey;
        }
    }
}