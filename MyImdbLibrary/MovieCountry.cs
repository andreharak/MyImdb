using System;
using SQLite;

namespace MyImdbLibrary
{
    [Table("MovieCountry")]
    public class MovieCountry
    {
        [PrimaryKey, AutoIncrement, Column("Id")]
        public int Id { get; set; }
        [MaxLength(8)]
        public int Movie { get; set; }
        public int Country { get; set; }

        public MovieCountry() { }
        public MovieCountry(int movie_id, int country_id)
        {
            Movie = movie_id;
            Country = country_id;
        }
    }
}
