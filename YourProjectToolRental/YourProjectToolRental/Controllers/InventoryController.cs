using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YourProjectToolRental.Models;

namespace YourProjectToolRental.Controllers
{
    public class InventoryController : Controller
    {
		//temporary database
		public static List<Inventory> inventoryList = new List<Inventory>
		{
			new Inventory{AssetId = 1, Brand = "Generic", Description = "Hammer that hammers", Active = true, Comment = "Works fine" },
			new Inventory{AssetId = 2, Brand = "Makita", Description = "1000w Drill", Active = false, Comment = "Front won't spin" },
			new Inventory{AssetId = 3, Brand = "Generic", Description = "Screwdriver", Active = true, Comment = "Works fine" }
		};

        // GET: Inventory List
        public ActionResult Index()
        {
			var inventory = from a in inventoryList orderby a.AssetId select a;
            return View(inventory);
        }

		#region Edit
		public ActionResult Edit(int Id)
		{
			var inventory = inventoryList.Single(a => a.AssetId == Id);

			return View(inventory);
		}

		//this is the edit post to edit the tool details
		[HttpPost]
		public ActionResult Edit(int Id, FormCollection collection)
		{
			try
			{
				var inventory = inventoryList.Single(a => a.AssetId == Id);
				if (TryUpdateModel(inventory))
				return RedirectToAction("Index");

				return View(inventory);
			}
			catch
			{
				return View();
			}
		}
		#endregion

		#region Create
		// GET - Create
		public ActionResult Create()
		{
			return View();
		}

		// POST - Create
		[HttpPost]
		public ActionResult Create(FormCollection collection)
		{
			try
			{
				Inventory inventory = new Inventory();

				bool active = Convert.ToBoolean(collection["Active"].Split(',')[0]);

				inventory.AssetId = (inventoryList.Count == 0) ? 1 : inventoryList.Max(a => a.AssetId) + 1;
				inventory.Brand = collection["brand"];
				inventory.Description = collection["description"];
				inventory.Active = active;
				inventory.Comment = collection["comment"];
				inventoryList.Add(inventory);
				return RedirectToAction("Index");
			}
			catch
			{
				return View();
			}
		}
		#endregion

		#region Details
		public ActionResult Details(int Id)
		{
			var inventory = inventoryList.Single(m => m.AssetId == Id);

			return View(inventory);
		}
		#endregion

		#region Delete
		public ActionResult Delete(int Id)
		{
			var inventory = inventoryList.Single(m => m.AssetId == Id);

			return View(inventory);
		}

		//this is the delete post to get the tool to be deleted
		[HttpPost]
		public ActionResult Delete(int Id, FormCollection collection)
		{
			try
			{
				var inventory = inventoryList.Single(m => m.AssetId == Id);
				inventoryList.Remove(inventory);
				return RedirectToAction("index");
			}
			catch
			{
				return View();
			}
		}
		#endregion
	}
}