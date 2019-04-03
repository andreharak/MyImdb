using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using RestSharp;

namespace MyImdbLibrary
{
    public static class InternetManager
    {
        public static async Task<bool> CheckForInternetConnection()
        {
            string url = $"http://clients3.google.com/generate_204";

            var client = new RestClient(url);
            var request = new RestRequest(Method.GET);

            try
            {
                IRestResponse response = await client.ExecuteTaskAsync(request);

                if (response.IsSuccessful) return true;
                return false;
            }
            catch
            {
                return false;
            }
        }

        public static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("No network adapters with an IPv4 address in the system!");
        }

        public static async Task<string> GetCountryOfThisMachine(List<CountryEnum> countriesNamesAndCode)
        {
            string current_country = null;

            ApiModelipData ipDataResult = await ApiProcessorIpData.GetCountryOfThisMachine();

            if (ipDataResult != null)
            {
                foreach (CountryEnum country in countriesNamesAndCode)
                {
                    if (string.Compare(ipDataResult.country_name, country.Name, StringComparison.OrdinalIgnoreCase) == 0)
                    {
                        current_country = country.Name;
                        break;
                    }
                    if (string.Compare(ipDataResult.country_code, country.Code, StringComparison.OrdinalIgnoreCase) == 0)
                    {
                        current_country = country.Name;
                        break;
                    }
                    if (string.Compare(ipDataResult.country_code, country.Code_Bis, StringComparison.OrdinalIgnoreCase) == 0)
                    {
                        current_country = country.Name;
                        break;
                    }
                }
            }
            return current_country;
        }
    }
}
