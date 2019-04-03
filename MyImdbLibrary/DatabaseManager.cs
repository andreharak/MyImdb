using System;
using System.Collections.Generic;
using System.Linq;
using SQLite;

namespace MyImdbLibrary
{
    public class DatabaseManager
    {
        private SQLiteConnection db;

        public List<string> countriesNames;
        public List<CountryEnum> countriesNamesAndCodes;
        public List<MovieTypeEnum> moviesTypes;
        public string OmdbApiKey;

        public DatabaseManager() => db = new SQLiteConnection("TheImdbDatabase.db");

        #region Enumerations
        public void LoadEnumerations()
        {
            countriesNamesAndCodes = db.Table<CountryEnum>().ToList();
            countriesNames = countriesNamesAndCodes.Select(c => c.Name).ToList();
            moviesTypes = db.Table<MovieTypeEnum>().ToList();
        }
        public void LoadOmdbApiKey(int MainUserId)
        {
            OmdbApiKey = db.Query<User>("SELECT * FROM User WHERE Id = ?", MainUserId).First().OmdbApiKey;
        }
        #endregion

        #region Add/Insert
        public void AddNewMovieTransaction(int user_id, Movie movie, List<int> countries, List<int> types, double ImdbRating, int ImdbVotes)
        {
            db.RunInTransaction(action: () => {

                db.Insert(movie);
                AddExistingMovieForUser(user_id, movie.Id);
                if (countries.Count > 0) AddMovieCountries(movie.Id, countries);
                if (types.Count > 0) AddMovieTypes(movie.Id, types);
                if (ImdbRating * ImdbVotes > 0) AddImdbRating(movie.Id, ImdbRating, ImdbVotes);
            });
        }
        public void AddExistingMovieForUser(int user_id, int movie_id) => db.Insert(new UserMovies(user_id, movie_id));
        public void AddMovieCountries(int movie_id, List<int> countries)
        {
            foreach (int country_id in countries) { db.Insert(new MovieCountry(movie_id, country_id)); }
        }
        public void AddMovieTypes(int movie_id, List<int> types)
        {
            foreach (int type_id in types) { db.Insert(new MovieType(movie_id, type_id)); }
        }
        public void AddUser(User user) => db.Insert(user);
        public void AddRating(int movie_id, int user_id, double value, int coef) => db.Insert(new Rating(movie_id, user_id, value, coef));
        public void AddImdbRating(int movie_id, double value, int coef) => db.Insert(new Rating(movie_id, ConstAndParams.ImdbOnlineUserId, value, coef));
        #endregion

        #region Get
        public User GetUserById(int user_id) => db.Get<User>(user_id);
        public void UpdateUserLastSeen(User user)
        {
            user.LastSeen = DateTime.Now;
            db.Update(user);
        }

        public Movie GetMovieById(int movie_id) => db.Query<Movie>("SELECT * FROM Movie WHERE Id = ?", movie_id).First();
        public Movie GetMovieByImdbId(string imdb_id) => db.Query<Movie>("SELECT * FROM Movie WHERE imdbID = ?", imdb_id).First();

        public Rating GetImdbMovieRating(int movie_id) => db.Query<Rating>("SELECT * FROM Rating WHERE User = ? AND Movie = ?", ConstAndParams.ImdbOnlineUserId, movie_id).FirstOrDefault();
        public Rating GetUserMovieRating(int user_id, int movie_id) => db.Query<Rating>("SELECT * FROM Rating WHERE User = ? AND Movie = ?", user_id, movie_id).FirstOrDefault();

        public double GetMovieImdbRating(int movie_id) => db.ExecuteScalar<double>("SELECT coalesce(ROUND(Value,1),0) FROM Rating WHERE User = ? AND Movie = ?", ConstAndParams.ImdbOnlineUserId, movie_id);
        public double GetMovieUserRating(int user_id, int movie_id) => db.ExecuteScalar<double>("SELECT coalesce(ROUND(SUM(Value*Coef)/SUM(Coef),1),0) FROM Rating WHERE User = ? AND Movie = ?", user_id, movie_id);
       
        public int GetMovieNumberOfImdbRatings(int movie_id) => db.ExecuteScalar<int>("SELECT coalesce(SUM(Coef),0) FROM Rating WHERE User = ? AND Movie = ?", ConstAndParams.ImdbOnlineUserId, movie_id);
        public List<int> GetMovieTypesIndexes(int movie_id)
        {
            List<MovieType> Types = db.Query<MovieType>("SELECT Type FROM MovieType WHERE Movie = ?", movie_id);
            List<int> TypesIndexes = new List<int>();
            foreach (MovieType t in Types) { TypesIndexes.Add(t.Type); }
            return TypesIndexes;
        }
        public string GetMovieTypesNames(int movie_id)
        {
            List<string> types_names = new List<string>();
            List<MovieType> movie_types = db.Query<MovieType>("SELECT * FROM MovieType WHERE Movie = ?", movie_id);
            foreach (MovieType t in movie_types) { types_names.Add(db.ExecuteScalar<string>("SELECT Name FROM MovieTypeEnum WHERE Id = ?", t.Type)); }
            types_names.Sort();
            if (types_names.Any()) return types_names.Aggregate((i, j) => i + ", " + j);
            return "";
        }
        public string GetMovieCountriesNames(int movie_id)
        {
            List<string> countries_names = new List<string>();
            List<MovieCountry> countries = db.Query<MovieCountry>("SELECT * FROM MovieCountry WHERE Movie = ?", movie_id);
            foreach (MovieCountry n in countries) { countries_names.Add(db.ExecuteScalar<string>("SELECT Name FROM CountryEnum WHERE Id = ?", n.Country)); }
            countries_names.Sort();

            // Replace USA and UK in countries_names
            if (countries_names.Contains("United States")) countries_names[countries_names.IndexOf("United States")] = "US";
            if (countries_names.Contains("United Kingdom")) countries_names[countries_names.IndexOf("United Kingdom")] = "UK";

            string result = string.Join(", ", countries_names.ToArray());
            return result;
        }
        public int GetMovieUsersCount(int movie_id) => db.ExecuteScalar<int>("SELECT Count(User) FROM UserMovies WHERE Movie = ?", movie_id);
        public int GetUserMoviesCount(int user_id) => db.ExecuteScalar<int>("SELECT Count(Movie) FROM UserMovies WHERE User = ?", user_id);
        public List<Movie> GetUserMovies(int user_id) => db.Query<Movie>("SELECT * FROM Movie m INNER JOIN UserMovies um ON um.Movie = m.Id WHERE um.User = ?", user_id);
        #endregion

        #region Update
        public void UpdateMovie(Movie movie) => db.Update(movie);
        public void UpdateUser(User user) => db.Update(user);
        public void UpdateRating(Rating rating) => db.Update(rating);
        #endregion

        #region Verification
        public bool IsThereAnExistingMovie(string imdb_id) => db.ExecuteScalar<int>("SELECT Count(*) FROM Movie WHERE imdbID LIKE ?", imdb_id) > 0;
        public bool IsThereAnExistingMovieForUser(string imdb_id, int user_id) => db.ExecuteScalar<int>("SELECT Count(*) FROM Movie m INNER JOIN UserMovies um ON um.Movie = m.Id WHERE m.imdbID LIKE ? AND um.User", imdb_id, user_id) > 0;

        public bool CheckIfEmailAddressIsFree(string email) => db.ExecuteScalar<int>("SELECT Count(*) FROM User WHERE Email LIKE ?", email) == 0;
        public (bool, bool, User) CheckIfUserIsValid(string email, string password)
        {
            bool user_exists = false;
            bool password_correct = false;
            User user = null;

            List<User> result1 = db.Query<User>("SELECT * FROM User WHERE Email = ?", email);
            List<User> result2 = db.Query<User>("SELECT * FROM User WHERE Email = ? AND Password = ?", email, password);

            if (result1.Any()) user_exists = true;
            if (result2.Any())
            {
                password_correct = true;
                user = result2.First();
            }
            return (user_exists, password_correct, user);
        }
        #endregion

        #region Remove
        public void RemoveAllMovieTypes(int id) => db.Execute("DELETE FROM MovieType WHERE Movie = ?", id);
        public void RemoveAllMovieCountries(int id) => db.Execute("DELETE FROM MovieCountry WHERE Movie = ?", id);

        public void RemoveMovieForUser(int movie_id, int user_id)
        {
            int nb_users_of_this_movie = GetMovieUsersCount(movie_id);
            db.RunInTransaction(action: () => {
                db.Execute("DELETE FROM Rating WHERE Movie = ?", movie_id);
                db.Execute("DELETE FROM UserMovies WHERE Movie = ?", movie_id);
                if (nb_users_of_this_movie == 1) RemoveMovieFromDb(movie_id);
            });
        }
        public void RemoveMovieFromDb(int movie_id)
        {
            db.RunInTransaction(action: () => {
                RemoveAllMovieTypes(movie_id);
                RemoveAllMovieCountries(movie_id);
                db.Delete<Movie>(movie_id);
            });
        }
        #endregion

        #region Statistics
        public Movie GetUserMostRatedMovie(int user_id)
        {
            int movie_id = db.ExecuteScalar<int>("SELECT coalesce(m.Id,0) FROM Movie m INNER JOIN Rating r ON r.Movie = m.Id WHERE r.User = ? GROUP BY m.Id ORDER BY r.Value desc LIMIT 1", user_id);
            if (movie_id > 0) return db.Get<Movie>(movie_id);
            return null;
        }
        public double GetUserAverageGenreRating(int user_id, int genre_id) => db.ExecuteScalar<double>("SELECT coalesce(ROUND(avg(r.Value),1),0) FROM Rating r INNER JOIN MovieType mt ON mt.Movie = r.Movie INNER JOIN UserMovies um ON um.Movie = mt.Movie WHERE r.User = ? AND mt.Type = ?", user_id, genre_id);
        public Dictionary<int, double> GetUserTopRatedGenres(int user_id)
        {
            Dictionary<int, double> results = new Dictionary<int, double>();

            foreach (int genre_id in moviesTypes.Select(mt => mt.Id).ToList())
            {
                double r = GetUserAverageGenreRating(user_id, genre_id);
                results.Add(genre_id, r);
            }
            return results;
        }
        public double GetImdbAverageGenreRatingForUserMovies(int user_id, int genre_id) => db.ExecuteScalar<double>("SELECT coalesce(ROUND(avg(r.Value),1),0) FROM Rating r INNER JOIN MovieType mt ON mt.Movie = r.Movie INNER JOIN UserMovies um ON um.Movie = mt.Movie WHERE r.User = ? AND mt.Type = ? AND um.User = ?", ConstAndParams.ImdbOnlineUserId, genre_id, user_id);
        public Dictionary<int, double> GetImdbTopRatedGenresForUserMovies(int user_id)
        {
            Dictionary<int, double> results = new Dictionary<int, double>();

            foreach (int genre_id in moviesTypes.Select(mt => mt.Id).ToList())
            {
                double r = GetImdbAverageGenreRatingForUserMovies(user_id, genre_id);
                results.Add(genre_id, r);
            }
            return results;
        }
        public List<int> GetUserFiveMostRatedMoviesByImdbIds(int user_id)
        {
            string movie_ids = db.ExecuteScalar<string>("SELECT group_concat(Movie) FROM (SELECT r.Movie FROM UserMovies um INNER JOIN Rating r ON r.Movie = um.Movie INNER JOIN Movie m ON m.Id = r.Movie WHERE r.User = ? AND um.User = ? ORDER BY r.Value desc, r.Coef desc LIMIT 5) t", ConstAndParams.ImdbOnlineUserId, user_id);

            List<int> result = movie_ids.Split(',').Select(Int32.Parse).ToList();
            return result;
        }
        public string GetUserTenMostRatedMoviesByImdbNames(int user_id)
        {
            return db.ExecuteScalar<string>("SELECT group_concat(Title) FROM (SELECT m.Title FROM UserMovies um INNER JOIN Rating r ON r.Movie = um.Movie INNER JOIN Movie m ON m.Id = r.Movie WHERE r.User = ? AND um.User = ? ORDER BY r.Value desc, r.Coef desc LIMIT 10) t", ConstAndParams.ImdbOnlineUserId, user_id);
        }
        #endregion

        #region Other-Functions
        public static bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
        #endregion
    }
}