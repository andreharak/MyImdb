using System;
using SQLite;

namespace MyImdbLibrary
{
    [Table("Movie")]
    public class Movie
    {
        [PrimaryKey, AutoIncrement, Column("Id")]
        public int Id { get; set; }
        [MaxLength(8)]
        public string Title { get; set; }
        public int Year { get; set; }
        public string Poster { get; set; }
        public string imdbID { get; set; }
        public string Plot { get; set; }

        public Movie() { }
        public Movie(string title, int year, string poster, string imdbId, string plot)
        {
            Title = title;
            Year = year;
            Poster = poster;
            imdbID = imdbId;
            Plot = plot;
        }
    }
}