using System;
using System.Collections.Generic;
using YourProjectToolRental.Models;

namespace YourProjectToolRental.DAL
{
	public class ToolInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<ToolContext> // DropCreateDatabaseIfModelChanges is normal
	{
		protected override void Seed(ToolContext context)
		{
			var assets = new List<Inventory>
			{
				new Inventory{AssetId = 1, Brand = "Generic", Description = "Hammer that hammers", Active = true, Comment = "Works fine" },
				new Inventory{AssetId = 2, Brand = "Makita", Description = "1000w Drill", Active = false, Comment = "Front won't spin" },
				new Inventory{AssetId = 3, Brand = "Generic", Description = "Screwdriver", Active = true, Comment = "Works fine" }
			};

			assets.ForEach(a => context.Inventories.Add(a));
			context.SaveChanges();

			var customers = new List<Customer>
			{
				new Customer{CustomerId = 1, FName = "John", LName = "Smith", Phone_="3390 0675", Email="JohnSmith@email.com"},
				new Customer{CustomerId = 2, FName = "Mary", LName = "Parks", Phone_="3855 1515", Email="MaryParks@email.com"},
				new Customer{CustomerId = 3, FName = "Robert", LName = "Boyd", Phone_="3290 9090", Email="RobertBoyd@email.com"}

			};

			customers.ForEach(c => context.Customers.Add(c));
			context.SaveChanges();

			var rentals = new List<Rental>
			{
				new Rental{RentalId = 1, CustomerId = 1, CheckedOutDate = DateTime.Parse("01/01/2017"), CheckedInDate = DateTime.Parse("01/01/2000")},
				new Rental{RentalId = 2, CustomerId = 2, CheckedOutDate = DateTime.Parse("01/01/2018"), CheckedInDate = DateTime.Parse("01/01/2000")},
				new Rental{RentalId = 3, CustomerId = 3, CheckedOutDate = DateTime.Parse("01/05/2017"), CheckedInDate = DateTime.Parse("01/01/2000")},
			};

			rentals.ForEach(r => context.Rentals.Add(r));
			context.SaveChanges();

			var rentalItems = new List<RentalItem>
			{
				new RentalItem{RentalItemId = 1, RentalId = 1, AssetId = 1},
				new RentalItem{RentalItemId = 2, RentalId = 1, AssetId = 2},
				new RentalItem{RentalItemId = 3, RentalId = 2, AssetId = 3},
				new RentalItem{RentalItemId = 4, RentalId = 3, AssetId = 1},
				new RentalItem{RentalItemId = 5, RentalId = 3, AssetId = 2},
				new RentalItem{RentalItemId = 6, RentalId = 3, AssetId = 3},
			};
			rentalItems.ForEach(ri => context.RentalItems.Add(ri));
			context.SaveChanges();
		}
	}
}