using System.Collections.Generic;
using DomainModel;
using Repository;

namespace Service
{
    public class FuelPriceService : IFuelPriceService
    {
        private readonly IFuelPriceRepository _fuelPriceRepository;

        public FuelPriceService(IFuelPriceRepository fuelPriceRepository)
        {
            _fuelPriceRepository = fuelPriceRepository;
        }

        public FuelPriceModel SaveFuelPrice(FuelPriceModel fuelPrice)
        {
            var fuelPriceModel = _fuelPriceRepository.SaveFuelPrice(fuelPrice);

            return fuelPriceModel;
        }

        public IEnumerable<FuelPriceModel> FetchAllFuelPrices()
        {
            var fuelPrices = _fuelPriceRepository.FindAllFuelPrices();

            return fuelPrices;
        }
    }
}
