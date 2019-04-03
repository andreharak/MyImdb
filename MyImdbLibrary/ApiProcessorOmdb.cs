using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;

namespace MyImdbLibrary
{
    public static class ApiProcessorOmdb
    {
        public static ApiModelOmdb LoadOmdbObject(string PresumedTitle, string PresumedYear, string OmdbApiKey)
        {
            PresumedTitle = PresumedTitle.Replace(' ', '+');
            string url = $"http://www.omdbapi.com/?t={ PresumedTitle }&y={ PresumedYear }&apikey={ OmdbApiKey }";

            var client = new RestClient(url);
            var request = new RestRequest(Method.GET);

            try
            {
                IRestResponse response = client.Execute(request);

                if (response.IsSuccessful)
                {
                    string JsonContent = response.Content;

                    var NewObject = JsonConvert.DeserializeObject<dynamic>(JsonContent.ToString());

                    if (NewObject.ContainsKey("Title"))
                    {
                        // Only consider movies
                        if (string.Compare(NewObject.Type.Value, "movie", StringComparison.OrdinalIgnoreCase) == 0)
                        {
                            ApiModelOmdb OmdbMovie = new ApiModelOmdb
                            {
                                Title = NewObject.Title.Value,
                                Year = NewObject.Year.Value,
                                imdbRating = NewObject.imdbRating.Value,
                                imdbVotes = NewObject.imdbVotes.Value,
                                imdbID = NewObject.imdbID.Value,
                                Country = NewObject.Country.Value,
                                Genre = NewObject.Genre.Value,
                                Poster = NewObject.Poster.Value,
                                Plot = NewObject.Plot
                            };
                            return OmdbMovie;
                        }
                    }
                }
                return null;
            }
            catch
            {
                return null;
            }
        }

        public static List<ApiModelOmdb> LoadOmdbList(string PresumedTitle, string PresumedYear, string OmdbApiKey)
        {
            PresumedTitle = PresumedTitle.Replace(' ', '+');
            string url = $"http://www.omdbapi.com/?s={ PresumedTitle }&y={ PresumedYear }&page=1&apikey={ OmdbApiKey }";

            var client = new RestClient(url);
            var request = new RestRequest(Method.GET);

            try
            {
                IRestResponse response = client.Execute(request);

                if (response.IsSuccessful)
                {
                    string JsonContent = response.Content;

                    dynamic NewObject = JsonConvert.DeserializeObject<dynamic>(JsonContent);

                    if (!JsonContent.Contains("Movie not found"))
                    {
                        if (NewObject.ContainsKey("Search"))
                        {
                            dynamic ResultsList = NewObject.Search;

                            List<ApiModelOmdb> movies = new List<ApiModelOmdb>();

                            foreach (var i in ResultsList.Children())
                            {
                                // Only consider movies
                                if (string.Compare(i.Type.Value, "movie", StringComparison.OrdinalIgnoreCase) == 0)
                                {
                                    ApiModelOmdb ModelOmdb = new ApiModelOmdb(i.Title.Value, i.Year.Value, i.imdbID.Value, i.Poster.Value);
                                    movies.Add(ModelOmdb);
                                }
                            }
                            return movies;
                        }
                    }
                }
                return null;
            }
            catch
            {
                return null;
            }
        }

        public static async Task<ApiModelOmdb> LoadOmdbFromId(string MovieId, string OmdbApiKey)
        {
            string url = $"http://www.omdbapi.com/?i={ MovieId }&apikey={ OmdbApiKey }";

            var client = new RestClient(url);
            var request = new RestRequest(Method.GET);

            try
            {
                IRestResponse response = await client.ExecuteTaskAsync(request);

                if (response.IsSuccessful)
                {
                    string JsonContent = response.Content;

                    dynamic NewObject = JsonConvert.DeserializeObject<dynamic>(JsonContent);

                    if (NewObject.ContainsKey("Title"))
                    {
                        // It can only be a movie
                        ApiModelOmdb OmdbMovie = new ApiModelOmdb
                        {
                            Title = NewObject.Title,
                            Year = NewObject.Year,
                            imdbRating = NewObject.imdbRating,
                            imdbVotes = NewObject.imdbVotes,
                            imdbID = NewObject.imdbID,
                            Country = NewObject.Country,
                            Genre = NewObject.Genre,
                            Poster = NewObject.Poster,
                            Plot = NewObject.Plot
                        };
                        return OmdbMovie;
                    }
                }
                return null;
            }
            catch
            {
                return null;
            }
        }

        public static async Task<ApiModelOmdb> LoadOmdbObjectAsync(string PresumedTitle, string PresumedYear, string OmdbApiKey)
        {
            PresumedTitle = PresumedTitle.Replace(' ', '+');
            string url = $"http://www.omdbapi.com/?t={ PresumedTitle }&y={ PresumedYear }&apikey={ OmdbApiKey }";

            var client = new RestClient(url);
            var request = new RestRequest(Method.GET);

            try
            {
                IRestResponse response = await client.ExecuteTaskAsync(request);

                if (response.IsSuccessful)
                {
                    string JsonContent = response.Content;

                    var NewObject = JsonConvert.DeserializeObject<dynamic>(JsonContent.ToString());

                    if (NewObject.ContainsKey("Title"))
                    {
                        // Only consider movies
                        if (string.Compare(NewObject.Type.Value, "movie", StringComparison.OrdinalIgnoreCase) == 0)
                        {
                            ApiModelOmdb OmdbMovie = new ApiModelOmdb
                            {
                                Title = NewObject.Title.Value,
                                Year = NewObject.Year.Value,
                                imdbRating = NewObject.imdbRating.Value,
                                imdbVotes = NewObject.imdbVotes.Value,
                                imdbID = NewObject.imdbID.Value,
                                Country = NewObject.Country.Value,
                                Genre = NewObject.Genre.Value,
                                Poster = NewObject.Poster.Value,
                                Plot = NewObject.Plot
                            };
                            return OmdbMovie;
                        }
                    }
                }
                return null;
            }
            catch
            {
                return null;
            }
        }
    }
}
