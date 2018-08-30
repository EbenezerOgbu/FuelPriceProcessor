using StructureMap;
using Provider;
using Repository;
using Service;

namespace ConsoleApp
{
    public static class Bootstrapper
    {
        public static void Startup()
        {
            ObjectFactory.Initialize(config =>
            {
                config.For<IFuelPriceService>().Use<FuelPriceService>();
                config.For<IFuelPriceProvider>().Use<FuelPriceProvider>();
                config.For<IFuelPriceRepository>().Use<FuelPriceRepository>();
            });
        }
    }
}
