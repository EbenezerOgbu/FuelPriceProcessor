using DomainModel;
using System.Collections.Generic;

namespace Repository
{
    public interface IFuelPriceRepository
    {
        FuelPriceModel SaveFuelPrice(FuelPriceModel fuelPrice);

        IEnumerable<FuelPriceModel> FindAllFuelPrices();
    }
}
