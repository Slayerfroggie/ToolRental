using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using YourProjectToolRental.Models;

namespace YourProjectToolRental.DAL
{
	public class ToolContext : DbContext
	{
		public DbSet<Inventory> Assets { get; set; }

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
		}
	}
}