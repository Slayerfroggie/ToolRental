using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using YourProjectToolRental.Models;

namespace YourProjectToolRental.DAL
{
	public class ToolContext : DbContext
	{
		public DbSet<Inventory> Inventories { get; set; }
		public DbSet<Customer> Customers { get; set; }
		public DbSet<Rental> Rentals { get; set; }
		public DbSet<RentalItem> RentalItems { get; set; }


		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
		}

		public System.Data.Entity.DbSet<YourProjectToolRental.ViewModels.CustomerRentalsViewModel> CustomerRentalsViewModels { get; set; }
	}
}