using DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Repository
{
    public class FuelPriceRepository : IFuelPriceRepository
    {

        private readonly FuelPriceDbContext _fuelPriceDbContext;

        public FuelPriceRepository(FuelPriceDbContext fuelPriceDbContext)
        {
            _fuelPriceDbContext = fuelPriceDbContext;
        }

		public FuelPriceModel SaveFuelPrice(FuelPriceModel fuelPrice)
        {
            FuelPriceModel savedFuelPrice = null;

            if (fuelPrice != null)
            {
                if (fuelPrice.Id == Guid.Empty)
                {
                    fuelPrice.Id = Guid.NewGuid();
                }

                //this checks if the data already exists
                var existingFuelPrice = _fuelPriceDbContext.FuelPrices.Any(f => f.Date == fuelPrice.Date);

                if (!existingFuelPrice)
                {
                    _fuelPriceDbContext.FuelPrices.Add(fuelPrice);

                    var results = _fuelPriceDbContext.SaveChanges();

                    if (results > 0)
                    {
                        savedFuelPrice = fuelPrice;
                    }
                }
            }

            return savedFuelPrice;
        }

        public IEnumerable<FuelPriceModel> FindAllFuelPrices()
        {
            IList<FuelPriceModel> retval = new List<FuelPriceModel>();

            retval = _fuelPriceDbContext.FuelPrices.ToList();

            return retval;
        }

    }
}
