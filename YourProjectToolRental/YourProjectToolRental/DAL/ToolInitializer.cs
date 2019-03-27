using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using YourProjectToolRental.Models;

namespace YourProjectToolRental.DAL
{
	public class ToolInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<ToolContext>
	{
		protected override void Seed(ToolContext context)
		{
			var Assets = new List<Inventory>
			{
				new Inventory{AssetId = 1, Brand = "Generic", Description = "Hammer that hammers", Active = true, Comment = "Works fine" },
				new Inventory{AssetId = 2, Brand = "Makita", Description = "1000w Drill", Active = false, Comment = "Front won't spin" },
				new Inventory{AssetId = 3, Brand = "Generic", Description = "Screwdriver", Active = true, Comment = "Works fine" }
			};

			Assets.ForEach(a => context.Assets.Add(a));
			context.SaveChanges();
		}
	}
}