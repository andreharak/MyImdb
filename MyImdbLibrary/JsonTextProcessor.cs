using System;
using System.Collections.Generic;
using System.Linq;

namespace MyImdbLibrary
{
    public static class JsonTextProcessor
    {
        public static List<int> ExtractCountriesIds(string movieCountries, List<CountryEnum> countriesNamesAndCodes)
        {
            movieCountries = movieCountries.Replace(" ", string.Empty);
            List<string> movie_countries = movieCountries.Split(',').ToList();

            List<int> Countries_Ids = new List<int> { };

            foreach (string movie_country in movie_countries)
            {
                foreach (CountryEnum c in countriesNamesAndCodes)
                {
                    if (!string.IsNullOrEmpty(c.Code))
                    {
                        if (string.Compare(movie_country, c.Code, StringComparison.OrdinalIgnoreCase) == 0)
                        {
                            Countries_Ids.Add(c.Id);
                            continue;
                        }
                    }
                    if (!string.IsNullOrEmpty(c.Code_Bis))
                    {
                        if (string.Compare(movie_country, c.Code_Bis, StringComparison.OrdinalIgnoreCase) == 0)
                        {
                            Countries_Ids.Add(c.Id);
                            continue;
                        }
                    }
                    if (!string.IsNullOrEmpty(c.Name))
                    {
                        if (string.Compare(movie_country, c.Name, StringComparison.OrdinalIgnoreCase) == 0)
                        {
                            Countries_Ids.Add(c.Id);
                            continue;
                        }
                    }
                }
            }

            return Countries_Ids;
        }
    }
}
