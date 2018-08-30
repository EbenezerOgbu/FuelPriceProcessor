using DomainModel;
using System.Collections.Generic;

namespace Service
{
    public interface IFuelPriceService
    {
        FuelPriceModel SaveFuelPrice(FuelPriceModel fuelPrice);

        IEnumerable<FuelPriceModel> FetchAllFuelPrices();
    }
}
