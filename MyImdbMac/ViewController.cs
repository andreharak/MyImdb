using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppKit;
using Foundation;
using MyImdbLibrary;

namespace ImdbMacApp
{
    public partial class ViewController : NSViewController
    {
        public DatabaseManager DbManager { get; set; }
        public User MainUser { get; set; }
        public ViewSignIn SignInView { get; set; }
        private Movie CurrentMovieInDisplay { get; set; }
        public bool ConnectedToInternet { get; set; }

        private List<Movie> MainUserMovies { get; set; }
        private List<string> MainUserMoviesNames { get; set; }
        private List<int> MainUserMoviesIds { get; set; }

        private bool UpdateAllFeatureExecuted { get; set; }

        public ViewController(IntPtr handle) : base(handle) { }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
        }

        public override void ViewDidAppear()
        {
            base.ViewDidAppear();

            UserInfo.StringValue = string.Format("User: {0} \nLast seen on: {1:ddd dd-MMM-yyyy}\nat: {1:T}", new object[] { MainUser.FirstName, MainUser.LastSeen.ToLocalTime() });
            DbManager.UpdateUserLastSeen(MainUser);
            DbManager.LoadOmdbApiKey(MainUser.Id);

            ConnectedToInternet = false;
            CheckInternetConnection();

            ClearAndStartForm();
            ClearAndStartDisplay();
        }

        public override NSObject RepresentedObject
        {
            get
            {
                return base.RepresentedObject;
            }
            set
            {
                base.RepresentedObject = value;
                // Update the view, if already loaded.

                UpdateAllFeatureExecuted = false;
            }
        }

        private async void CheckInternetConnection()
        {
            ConnectedToInternet = await InternetManager.CheckForInternetConnection();
        }

        public void ClearAndStartForm()
        {
            FormMovieName.StringValue = "";
            FormMovieYear.StringValue = "";

            WelcomeTextField.StringValue = "Fill the information to search for a Movie...";
        }

        private void GetAndUpdateMainUserMovies()
        {
            MainUserMovies = DbManager.GetUserMovies(MainUser.Id);
            MainUserMovies = MainUserMovies.OrderBy(m => m.Title).ThenBy(m => m.Id).ToList();

            MainUserMoviesNames = MainUserMovies.Select(m => m.Title).ToList();
            MainUserMoviesIds = MainUserMovies.Select(m => m.Id).ToList();

            if ((MainUserMovies.Count > 0) && !UpdateAllFeatureExecuted) UpdateAllButton.Enabled = true;
            else UpdateAllButton.Enabled = false;
        }

        public void ClearAndStartDisplay()
        {
            CurrentMovieInDisplay = null;

            GetAndUpdateMainUserMovies();

            DisplayTitle.Enabled = true;
            DisplayTitle.UsesDataSource = true;
            DisplayTitle.DataSource = new DataSource(MainUserMoviesNames);
            DisplayTitle.StringValue = "";

            MoviePoster.Image = NSImage.ImageNamed("happy");
            CkeckOnlineMoviePage.Enabled = false;

            DisplayYear.StringValue = "";
            DisplayTypes.StringValue = "";
            DisplayImdbRating.StringValue = "";
            DisplayUserRating.StringValue = "";
            DisplayCountry.StringValue = "";
            NumberImdbRatings.StringValue = "";

            LevelIndicatorUser.DoubleValue = 0;
            LevelIndicatorImdb.DoubleValue = 0;

            RateMovieScale.SelectedSegment = -1;
            RateMovieMessage.StringValue = "";
            RateMovieMessage.StringValue = "Select the movie you want to rate...";

            DeleteMovieButton.Enabled = false;
            UpdateMovieButton.Enabled = false;

            if (DbManager.GetUserMoviesCount(MainUser.Id) > 0) MostRatedImdbMovieButton.Enabled = true;
            else MostRatedImdbMovieButton.Enabled = false;
        }

        public void FillDisplay(Movie movie)
        {
            CurrentMovieInDisplay = movie;
            DisplayTitle.StringValue = movie.Title;

            if (ConnectedToInternet)
            {
                CkeckOnlineMoviePage.Enabled = true;
                try
                {
                    if ((movie.Poster.Any()) && (!movie.Poster.Contains("N/A")))
                    {
                        Uri uriSource = new Uri(movie.Poster, UriKind.Absolute);
                        MoviePoster.Image = new NSImage(uriSource);
                    }
                    else MoviePoster.Image = NSImage.ImageNamed("unavailable");
                }
                catch (Exception)
                {
                    MoviePoster.Image = NSImage.ImageNamed("unavailable");
                }
            }
            else
            {
                MoviePoster.Image = NSImage.ImageNamed("no_connection");
            }

            DisplayYear.StringValue = movie.Year.ToString();
            DisplayTypes.StringValue = DbManager.GetMovieTypesNames(movie.Id);

            // IMDB rating
            LevelIndicatorImdb.DoubleValue = DbManager.GetMovieImdbRating(movie.Id);
            DisplayImdbRating.StringValue = string.Format("{0:F1}", LevelIndicatorImdb.DoubleValue);

            // User rating
            LevelIndicatorUser.DoubleValue = DbManager.GetMovieUserRating(MainUser.Id, movie.Id);
            DisplayUserRating.StringValue = string.Format("{0:F1}", LevelIndicatorUser.DoubleValue);

            DisplayCountry.StringValue = DbManager.GetMovieCountriesNames(movie.Id);

            int nbOfImdbRatings = DbManager.GetMovieNumberOfImdbRatings(movie.Id);
            if (nbOfImdbRatings == 1) NumberImdbRatings.StringValue = "1 rating";
            else NumberImdbRatings.StringValue = string.Format("{0:n0} ratings", nbOfImdbRatings);

            RateMovieMessage.StringValue = "Click to rate this movie...";
            DeleteMovieButton.Enabled = true;
            UpdateMovieButton.Enabled = true;
        }

        public void ReloadDisplay(Movie movie)
        {
            if (movie != null)
            {
                ClearAndStartDisplay();
                FillDisplay(movie);
            }
        }

        public void AddMovieToDbAndReload(bool newToDb, Movie NewMovie, List<int> Countries, List<int> Types, int user_id, double rating=0, int coef=0)
        {
            if (newToDb)
            {
                DbManager.AddNewMovieTransaction(MainUser.Id, NewMovie, Countries, Types, rating, coef);
            }
            else
            {
                NewMovie = DbManager.GetMovieByImdbId(NewMovie.imdbID);
                DbManager.UpdateMovie(NewMovie);
                DbManager.AddExistingMovieForUser(MainUser.Id, NewMovie.Id);
            }

            WelcomeTextField.StringValue = NewMovie.Title + " has been added...";
            ClearAndStartForm();
            ReloadDisplay(NewMovie);
        }

        partial void FormAddMovieButton_Clicked(NSObject sender)
        {
            if (ConnectedToInternet)
            {
                if (FormMovieName.StringValue.Any()) PerformSegue("LaunchViewOmdbApi", this);
                else AlertMessage("Please fill the movie name.", "Unsufficient!");
            }
            else AlertMessage("Unable to connect to the online platform!", "No Internet Connection");
        }

        partial void DisplayTitle_Changed(NSObject sender)
        {
            RateMovieScale.SelectedSegment = -1;
            string movie_title = DisplayTitle.StringValue;

            if (movie_title.Any())
            {
                int index_in_movies_names = MainUserMoviesNames.IndexOf(movie_title);
                int movie_id = MainUserMoviesIds[index_in_movies_names];

                CurrentMovieInDisplay = DbManager.GetMovieById(movie_id);
                ReloadDisplay(CurrentMovieInDisplay);
            }
        }

        partial void RateMovieScale_Changed(NSSegmentedControl sender)
        {
            if (CurrentMovieInDisplay != null)
            {
                int new_rating = (int)(RateMovieScale.SelectedSegment + 1);

                Rating CurrentUserRating = DbManager.GetUserMovieRating(MainUser.Id, CurrentMovieInDisplay.Id);
                if (CurrentUserRating != null)
                {
                    CurrentUserRating.Value = new_rating;
                    DbManager.UpdateRating(CurrentUserRating);
                }
                else DbManager.AddRating(CurrentMovieInDisplay.Id, MainUser.Id, new_rating, coef: 1);

                ReloadDisplay(CurrentMovieInDisplay);
                RateMovieMessage.StringValue = string.Format("Rated {0}/10", new_rating);
            }
        }

        partial void UpdateMovieButton_Clicked(NSObject sender)
        {
            if (ConnectedToInternet) UpdateMovieInfoFromApi(CurrentMovieInDisplay);
            else
            {
                MoviePoster.Image = NSImage.ImageNamed("no_connection");
                AlertMessage("Unable to connect and synchronise with the online platform!", "No Internet Connection");
            }
        }

        partial void UpdateAllButton_Clicked(NSObject sender)
        {
            if (ConnectedToInternet)
            {
                EnablePreLoader();

                UpdateAllFeatureExecuted = true;
                UpdateAllButton.Enabled = false;
                UpdateAllMoviesInfoFromApi();
                UpdateAllButton.ToolTip = "Only allowed once!";
            }
            else
            {
                MoviePoster.Image = NSImage.ImageNamed("no_connection");
                AlertMessage("Unable to connect and synchronise with the online platform!", "No Internet Connection");
            }
        }

        private async void UpdateMovieInfoFromApi(Movie movieToUpdate)
        {
            string imdb_id = movieToUpdate.imdbID;
            if (imdb_id.Any())
            {
                EnablePreLoader();
                List<bool> CurrentStates = DetermineStateOfDisplayButtonsAndComboBox();
                DisableDisplayButtonsAndComboBox();

                ApiModelOmdb OmdbMovie = await ApiProcessorOmdb.LoadOmdbFromId(imdb_id, MainUser.OmdbApiKey);

                if (OmdbMovie != null)
                {
                    double new_rating = 0;
                    int new_coef = 0;

                    if (OmdbMovie.imdbRating.Any()) double.TryParse(OmdbMovie.imdbRating, out new_rating);
                    if (OmdbMovie.imdbVotes.Any()) int.TryParse(OmdbMovie.imdbVotes.Replace(",", ""), out new_coef);

                    bool ApiDidReturnRating = true;
                    bool ApiDidReturnPoster = true;
                    bool PlotInfoAlreadyUpToDate = false;
                    bool RatingInfoAlreadyUpToDate = false;
                    bool PosterInfoAlreadyUpToDate = false;

                    // Update Movie Plot
                    if (!string.IsNullOrEmpty(OmdbMovie.Plot))
                    {
                        if (OmdbMovie.Plot != movieToUpdate.Plot)
                        {
                            movieToUpdate.Plot = OmdbMovie.Plot;
                            DbManager.UpdateMovie(movieToUpdate);
                        }
                        else PlotInfoAlreadyUpToDate = true;
                    }

                    // Update Movie IMDB Rating
                    Rating ImdbRating = DbManager.GetImdbMovieRating(movieToUpdate.Id);

                    if ((new_rating * new_coef > 0))
                    {
                        if (ImdbRating != null)
                        {
                            if ((Math.Abs(ImdbRating.Value - new_rating) > double.Epsilon) || (ImdbRating.Coef != new_coef))
                            {
                                ImdbRating.Value = new_rating;
                                ImdbRating.Coef = new_coef;
                                DbManager.UpdateRating(ImdbRating);
                                ReloadDisplay(movieToUpdate);
                            }
                            else RatingInfoAlreadyUpToDate = true;
                        }
                        else
                        {
                            DbManager.AddImdbRating(movieToUpdate.Id, new_rating, new_coef);
                            ReloadDisplay(movieToUpdate);
                        }
                    }
                    else ApiDidReturnRating = false;

                    // Update Movie Poster
                    if (OmdbMovie.Poster != null)
                    {
                        if (OmdbMovie.Poster.Any())
                        {
                            if (movieToUpdate.Poster != OmdbMovie.Poster)
                            {
                                if ((movieToUpdate.Poster != OmdbMovie.Poster) && (!OmdbMovie.Poster.Contains("N/A")))
                                {
                                    movieToUpdate.Poster = OmdbMovie.Poster;
                                    DbManager.UpdateMovie(movieToUpdate);
                                    ReloadDisplay(movieToUpdate);
                                }
                            }
                            else PosterInfoAlreadyUpToDate = true;
                        }
                        else ApiDidReturnPoster = false;
                    }
                    else ApiDidReturnPoster = false;

                    if (!ApiDidReturnRating && !ApiDidReturnPoster)
                    {
                        AlertMessage("The attempt to update this movie via API was not successful.", "Could not update!");
                    }
                    if (RatingInfoAlreadyUpToDate && PosterInfoAlreadyUpToDate && PlotInfoAlreadyUpToDate)
                    {
                        AlertMessage("The information on your local database is already up to date for this movie.", "Already up to update!");
                    }
                }
                else AlertMessage("Failed to update this movie! Please check your API key.", "Could not update!");

                ResetStateOfDisplayButtonsAndComboBox(CurrentStates);
                DisablePreLoader();
            }
            else AlertMessage("This movie could not be linked to the IMDB online platform.", "Could not update!");
        }

        private async void UpdateAllMoviesInfoFromApi()
        {
            if (MainUserMovies.Count > 0)
            {
                bool success = true;
                List<bool> CurrentStates = DetermineStateOfDisplayButtonsAndComboBox();
                DisableDisplayButtonsAndComboBox();

                foreach (Movie movieToUpdate in MainUserMovies)
                {
                    string imdb_id = movieToUpdate.imdbID;
                    if (!string.IsNullOrEmpty(imdb_id))
                    {
                        ApiModelOmdb OmdbMovie = await ApiProcessorOmdb.LoadOmdbFromId(imdb_id, MainUser.OmdbApiKey);

                        if (OmdbMovie != null)
                        {
                            // Update Movie Plot
                            if (!string.IsNullOrEmpty(OmdbMovie.Plot))
                            {
                                movieToUpdate.Plot = OmdbMovie.Plot;
                                DbManager.UpdateMovie(movieToUpdate);
                            }

                            double new_rating = 0;
                            int new_coef = 0;

                            if (OmdbMovie.imdbRating.Any()) double.TryParse(OmdbMovie.imdbRating, out new_rating);
                            if (OmdbMovie.imdbVotes.Any()) int.TryParse(OmdbMovie.imdbVotes.Replace(",", ""), out new_coef);

                            // Update Movie IMDB Rating
                            Rating ImdbRating = DbManager.GetImdbMovieRating(movieToUpdate.Id);

                            if ((new_rating * new_coef > 0))
                            {
                                if (ImdbRating != null)
                                {
                                    if ((Math.Abs(ImdbRating.Value - new_rating) > double.Epsilon) || (ImdbRating.Coef != new_coef))
                                    {
                                        ImdbRating.Value = new_rating;
                                        ImdbRating.Coef = new_coef;
                                        DbManager.UpdateRating(ImdbRating);
                                    }
                                }
                                else
                                {
                                    DbManager.AddImdbRating(movieToUpdate.Id, new_rating, new_coef);
                                }
                            }
                        }
                        else
                        {
                            success = false;
                            break;
                        }
                    }
                }
                ClearAndStartDisplay();
                ResetStateOfDisplayButtonsAndComboBox(CurrentStates, DisplayReset: true);
                DisablePreLoader();

                if (success) AlertMessage("All your movies have been successfully updated.", "Success!");
                else AlertMessage("Failed to update your movies! Please check your API key.", "Could not update!");
            }
            else AlertMessage("You have no movies in your list!", "Could not update!");
        }

        private List<bool> DetermineStateOfDisplayButtonsAndComboBox()
        {
            List<bool> CurrentStates = new List<bool>()
            {
                UpdateMovieButton.Enabled,
                DeleteMovieButton.Enabled,
                MostRatedImdbMovieButton.Enabled,
                DisplayTitle.Enabled,
                FormAddMovieButton.Enabled,
                StatisticsButton.Enabled,
                RateMovieScale.Enabled,
                SignOutButton.Enabled,
                ProfileButton.Enabled
            };
            return CurrentStates;
        }

        private void DisableDisplayButtonsAndComboBox()
        {
            UpdateMovieButton.Enabled = false;
            DeleteMovieButton.Enabled = false;
            MostRatedImdbMovieButton.Enabled = false;
            DisplayTitle.Enabled = false;
            FormAddMovieButton.Enabled = false;
            StatisticsButton.Enabled = false;
            RateMovieScale.Enabled = false;
            SignOutButton.Enabled = false;
            ProfileButton.Enabled = false;
        }

        private void ResetStateOfDisplayButtonsAndComboBox(List<bool> CurrentStates, bool DisplayReset=false)
        {
            if (!DisplayReset)
            {
                UpdateMovieButton.Enabled = CurrentStates[0];
                DeleteMovieButton.Enabled = CurrentStates[1];
            }
            MostRatedImdbMovieButton.Enabled = CurrentStates[2];
            DisplayTitle.Enabled = CurrentStates[3];
            FormAddMovieButton.Enabled = CurrentStates[4];
            StatisticsButton.Enabled = CurrentStates[5];
            RateMovieScale.Enabled = CurrentStates[6];
            SignOutButton.Enabled = CurrentStates[7];
            ProfileButton.Enabled = CurrentStates[8];
        }

        partial void MostRatedImdbMovieButton_Clicked(NSObject sender)
        {
            ReloadDisplay(DbManager.GetUserMostRatedMovie(MainUser.Id));
        }

        public void AlertMessage(string message, string title)
        {
            var alert = new NSAlert()
            {
                AlertStyle = NSAlertStyle.Informational,
                InformativeText = message,
                MessageText = title,
            };
            alert.AddButton("Ok");
            alert.BeginSheet(View.Window);
        }

        partial void SignOutButton_Clicked(NSObject sender)
        {
            PerformSegue("LaunchSignInView", sender);
        }

        partial void StatisticsButton_Clicked(NSObject sender)
        {
            if (DbManager.GetUserMoviesCount(MainUser.Id) >= 10) PerformSegue("LaunchViewStatistics", sender);
            else AlertMessage("You need to have at least 10 movies to access the statistics report!", "Not enough movies!");
        }

        partial void RecommendationButton_Clicked(NSObject sender)
        {
            if (ConnectedToInternet)
            {
                if (DbManager.GetUserMoviesCount(MainUser.Id) >= 10) PerformSegue("LaunchViewRecommendations", sender);
                else AlertMessage("You need to have at least 10 movies to access the recommendations!", "Not enough movies!");
            }
            else AlertMessage("Unable to connect to the recommendations platform!", "No Internet Connection");
        }

        private void EnablePreLoader()
        {
            LoadingGif.Image = NSImage.ImageNamed("loading2");
            LoadingGif.Enabled = true;
        }

        private void DisablePreLoader()
        {
            LoadingGif.Image = null;
            LoadingGif.Enabled = false;
        }

        [Export("prepareForSegue:sender:")]
        public override void PrepareForSegue(NSStoryboardSegue segue, NSObject sender)
        {
            base.PrepareForSegue(segue, sender);

            switch (segue.Identifier)
            {
                case "AskAreYouSureToDeleteMovie":
                    {
                        ViewAreYouSureController target = segue.DestinationController as ViewAreYouSureController;
                        target.MainUser_id = MainUser.Id;
                        target.Id_movieToDelete = CurrentMovieInDisplay.Id;
                        target.DbManager = DbManager;
                        target.MainView = this;
                    }
                    break;
                case "LaunchViewOmdbApi":
                    {
                        ViewOmdbApi target = segue.DestinationController as ViewOmdbApi;
                        target.DbManager = DbManager;
                        target.MainUser = MainUser;
                        target.PresumedTitle = FormMovieName.StringValue;
                        target.PresumedYear = FormMovieYear.StringValue;
                        target.MainView = this;
                    }
                    break;
                case "LaunchViewStatistics":
                    {
                        ViewStatistics target = segue.DestinationController as ViewStatistics;
                        target.DbManager = DbManager;
                        target.MainUser = MainUser;
                        target.ConnectedToInternet = ConnectedToInternet;
                    }
                    break;
                case "LaunchViewRecommendations":
                    {
                        ViewRecommendations target = segue.DestinationController as ViewRecommendations;
                        target.DbManager = DbManager;
                        target.MainUser = MainUser;
                        target.MainView = this;
                    }
                    break;
                case "LaunchSignInView":
                    {
                        ViewSignIn target = segue.DestinationController as ViewSignIn;
                        DismissViewController(this);
                        SignInView.ClearFieldsAfterSignOut();
                    }
                    break;
                case "LaunchViewImdbMoviePage":
                    {
                        ViewImdbMoviePage target = segue.DestinationController as ViewImdbMoviePage;
                        target.MovieImdbId = CurrentMovieInDisplay.imdbID;
                    }
                    break;
                case "LaunchProfileWindow":
                    {
                        ViewProfile target = segue.DestinationController as ViewProfile;
                        target.MainView = this;
                        target.DbManager = DbManager;
                        target.MainUser = MainUser;
                    }
                    break;
            }
        }
    }
}
