using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace MyImdbLibrary
{
    public class ApiModelOmdb
    {
        public string Title { get; set; }
        public string Year { get; set; }
        public string imdbRating { get; set; }  // ?
        public string imdbVotes { get; set; }   // ?
        public string imdbID { get; set; }
        public string Country { get; set; }     // ?
        public string Genre { get; set; }       // ?
        public string Poster { get; set; }
        public string Plot { get; set; }

        public ApiModelOmdb() { }

        public ApiModelOmdb(string title, string year, string imdbId, string poster)
        {
            Title = title;
            Year = year;
            imdbID = imdbId;
            Poster = poster;
        }

        public ApiModelOmdb(string title, string year, string ImdbRating, string ImdbVotes, string imdbId, string country, string genre, string poster, string plot)
        {
            Title = title;
            Year = year;
            imdbRating = ImdbRating;
            imdbVotes = ImdbVotes;
            imdbID = imdbId;
            Country = country;
            Genre = genre;
            Poster = poster;
            Plot = plot;
        }
    }
}
