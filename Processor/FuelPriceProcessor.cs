using Provider;
using Service;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using DomainModel;
using System.Globalization;
using FluentScheduler;
using StructureMap;

namespace Processor
{
    public class FuelPriceProcessor : IJob
    {
        private readonly IFuelPriceProvider _fuelPriceProvider;
        private readonly IFuelPriceService _fuelPriceService;
        private readonly int _daysCount;

        public FuelPriceProcessor()
        {
            _daysCount = GetDaysCount();
            _fuelPriceProvider = ObjectFactory.GetInstance<FuelPriceProvider>(); 
            _fuelPriceService = ObjectFactory.GetInstance<FuelPriceService>(); 
        }

        public void Execute()
        {
            try
            {
                Console.WriteLine("Fuel Price Processor Job Started");

                //fetch all the prices from the api
                var freshPrices = _fuelPriceProvider.FetchAllWeeklyPrices().Result;

                //select the range of prices based on the days count
                var filteredPrices = GetFilteredFuelPriceDataFrom(freshPrices, _daysCount);

                //save the filtered prices to the database
                foreach (var fuelPrice in filteredPrices)
                {
                    _fuelPriceService.SaveFuelPrice(fuelPrice);
                }

                Console.WriteLine("Fuel Price Processor Job Finished");
            }
            catch (Exception)
            {
                //better way is to log the exception
                Console.WriteLine("Something Has Gone Wrong. The Task Could Not Be Completed");
            }
        }

        private int GetDaysCount()
        {
            var retval = 0;

            var daysCount = ConfigurationManager.AppSettings["DaysCount"];

            if (!string.IsNullOrWhiteSpace(daysCount))
            {
                retval = Convert.ToInt32(daysCount);
            }
            return retval;
        }

        private List<FuelPriceModel> GetFilteredFuelPriceDataFrom(IEnumerable<FuelPriceModel> fuelPriceData, int daysCount)
        {
            List<FuelPriceModel> filteredFuelPrice = new List<FuelPriceModel>();

            //gets the most recent price.
            var getMostRecentDate = fuelPriceData.FirstOrDefault();

            var dateTime = DateTime.MinValue;

            //converts to date instance
            if (getMostRecentDate != null && !string.IsNullOrWhiteSpace(getMostRecentDate.Date))
            {
               dateTime = DateTime.ParseExact(getMostRecentDate.Date, "yyyyMMdd", CultureInfo.InvariantCulture);
            }

            if (_daysCount > 0)
            {
                //subtract the days count from the most recent date to get a new date that is in past
                var backDated = dateTime.AddDays(-_daysCount);

                //convert back the new date to the require string format
                var backDatedString = backDated.ToString("yyyyMMdd");

                //select prices that fall into the required range
                filteredFuelPrice = fuelPriceData.TakeWhile(f => Convert.ToInt32(f.Date) > Convert.ToInt32(backDatedString)).ToList();
            }

           return filteredFuelPrice;
        }
    }
}
