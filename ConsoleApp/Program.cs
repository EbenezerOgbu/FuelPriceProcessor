using FluentScheduler;
using System.Threading;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //register our dependency injection container
            Bootstrapper.Startup();

            JobManager.Initialize(new FuelPriceScheduler());

            Sleep();
        }

        private static void Sleep()
        {
            Thread.Sleep(Timeout.Infinite);
        }
    }
}
