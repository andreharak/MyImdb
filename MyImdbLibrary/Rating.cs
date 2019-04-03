using System;
using SQLite;

namespace MyImdbLibrary
{
    [Table("Rating")]
    public class Rating
    {
        [PrimaryKey, AutoIncrement, Column("Id")]
        public int Id { get; set; }
        [MaxLength(8)]
        public int Movie { get; set; }
        public int User { get; set; }
        public double Value { get; set; }
        public int Coef { get; set; }

        public Rating() { }
        public Rating(int movie, int user, double value, int coef)
        {
            Movie = movie;
            User = user;
            Value = value;
            Coef = coef;
        }
    }
}
