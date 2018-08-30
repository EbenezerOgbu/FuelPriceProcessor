using System;

namespace DomainModel
{
    public class FuelPriceModel
    {
        public Guid Id { get; set; }

        public string Date { get; set; }

        public decimal Price { get; set; }
    }
}
