using System;
using SQLite;

namespace MyImdbLibrary
{
    [Table("MovieType")]
    public class MovieType
    {
        [PrimaryKey, AutoIncrement, Column("Id")]
        public int Id { get; set; }
        [MaxLength(8)]
        public int Movie { get; set; }
        public int Type { get; set; }

        public MovieType() { }
        public MovieType(int movie_id, int type_id)
        {
            Movie = movie_id;
            Type = type_id;
        }
    }
}