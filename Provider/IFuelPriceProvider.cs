using DomainModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Provider
{
    public interface IFuelPriceProvider
    {
        Task<IEnumerable<FuelPriceModel>> FetchAllWeeklyPrices();
    }
}
