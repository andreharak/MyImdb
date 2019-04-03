using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;

namespace MyImdbLibrary
{
    public static class ApiProcessorIpData
    {
        public static async Task<ApiModelipData> GetCountryOfThisMachine()
        {
            string url = $"https://api.ipdata.co/?api-key={ ConstAndParams.IpDataApiKey }";

            var client = new RestClient(url);
            var request = new RestRequest(Method.GET);

            try
            {
                IRestResponse response = await client.ExecuteTaskAsync(request);

                if (response.IsSuccessful)
                {
                    string JsonContent = response.Content;

                    dynamic NewObject = JsonConvert.DeserializeObject<dynamic>(JsonContent);

                    if (NewObject.ContainsKey("country_name"))
                    {
                        ApiModelipData ModelIpData = new ApiModelipData
                        {
                            country_name = NewObject.country_name,
                            country_code = NewObject.country_code
                        };
                        return ModelIpData;
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
