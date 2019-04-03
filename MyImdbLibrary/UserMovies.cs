using System;
using SQLite;

namespace MyImdbLibrary
{
    [Table("UserMovies")]
    public class UserMovies
    {
        [PrimaryKey, AutoIncrement, Column("Id")]
        public int Id { get; set; }
        [MaxLength(8)]
        public int User { get; set; }
        public int Movie { get; set; }

        public UserMovies() { }
        public UserMovies(int user_id, int movie_id)
        {
            User = user_id;
            Movie = movie_id;
        }
    }
}
