using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;

namespace MyImdbLibrary
{
    public static class ApiProcessorTaste
    {
        public static async Task<List<ApiModelTaste>> LoadTasteRecommendations(string UserFiveMostRatedMoviesByImdbNames)
        {
            string url = $"https://tastedive.com/api/similar?info=1&k={ ConstAndParams.DefaultTasteDiveApiKey }&limit=20&q={ UserFiveMostRatedMoviesByImdbNames }&type=movie";

            var client = new RestClient(url);
            var request = new RestRequest(Method.GET);

            try
            {
                IRestResponse response = await client.ExecuteTaskAsync(request);

                if (response.IsSuccessful)
                {
                    string JsonContent = response.Content;

                    dynamic NewObject = JsonConvert.DeserializeObject<dynamic>(JsonContent);

                    if (NewObject.ContainsKey("Similar"))
                    {
                        dynamic SimilarContent = NewObject.Similar;

                        if (SimilarContent.ContainsKey("Results"))
                        {
                            dynamic ResultsList = SimilarContent.Results;

                            List<ApiModelTaste> movies = new List<ApiModelTaste>();

                            foreach (var i in ResultsList.Children())
                            {
                                ApiModelTaste ModelTaste = new ApiModelTaste(i.Name.Value, i.wTeaser.Value, i.yUrl.Value);
                                movies.Add(ModelTaste);
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

    }
}
