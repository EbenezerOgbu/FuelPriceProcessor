using DomainModel;
using System.Data.Entity;

namespace Repository
{
    
    public class FuelPriceDbContext : DbContext
    {
        // Your context has been configured to use a 'FuelPriceDbContext' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'Repository.FuelPriceDbContext' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'FuelPriceDbContext' 
        // connection string in the application configuration file.
        public FuelPriceDbContext(): base("name=FuelPriceDbContext")
        {
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        public virtual DbSet<FuelPriceModel> FuelPrices { get; set; }
    }

}