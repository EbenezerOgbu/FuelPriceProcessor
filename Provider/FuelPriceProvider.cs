using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DomainModel;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using System.Configuration;

namespace Provider
{
    public class FuelPriceProvider : IFuelPriceProvider
    {
        private readonly HttpClient _httpClient;

        public FuelPriceProvider()
        {
            _httpClient = new HttpClient();
        }

        public async Task<IEnumerable<FuelPriceModel>> FetchAllWeeklyPrices()
        {
            IList<FuelPriceModel> retval = new List<FuelPriceModel>();

            //get the endpoint
            var endpoint = GetWeeklyPriceEndPoint();

            if (!string.IsNullOrWhiteSpace(endpoint))
            {
                //call the API
                var responseTask = _httpClient.GetAsync(endpoint);
                responseTask.Wait();

                var response  = responseTask.Result;

                //make sure the response is good
                if (response.IsSuccessStatusCode && response.Content != null)
                {
                    //read the response
                    var tmpResult = await response.Content.ReadAsStringAsync();

                    CreateFuelPriceListFrom(tmpResult, retval);
                }
            }

            return retval;
        }

        private static void CreateFuelPriceListFrom(string tmpResult, IList<FuelPriceModel> retval)
        {
            //parse the temporary result through a json tokenizer to allow us to pick the section we need
            var jsonData = JToken.Parse(tmpResult) as dynamic;

            //pick the series section
            var seriesData = jsonData.series;

            //pick the only item in the series array
            var seriesItem = seriesData[0];

            //pick the data array in the series object
            var dataArray = seriesItem.data;

            //builds our model for each price-date array item and add that to our model list
            foreach (var item in dataArray)
            {
                var date = item[0].ToString();
                var price = item[1].ToString();

                var priceDecimal = Convert.ToDecimal(price);

                var fuelPrice = new FuelPriceModel
                {
                    Date = date,
                    Price = priceDecimal
                };

                retval.Add(fuelPrice);
            }
        }

        private string GetWeeklyPriceEndPoint()
        {
            string retval = null;
            var endPoint = ConfigurationManager.AppSettings["WeeklyPriceEndPoint"];

            if (!string.IsNullOrWhiteSpace(endPoint))
            {
                retval = endPoint;
            }
            return retval;
        }
    }
}
