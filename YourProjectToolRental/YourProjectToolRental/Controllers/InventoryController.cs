using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YourProjectToolRental.DAL;
using YourProjectToolRental.Models;

namespace YourProjectToolRental.Controllers
{
    public class InventoryController : Controller
    {
		private ToolContext db = new ToolContext();

		// GET: Inventory List
		public ActionResult Index()                            
        {
			var inventories = from a in db.Inventories orderby a.AssetId select a;
            return View(db.Inventories.ToList());
        }

		#region Edit
		public ActionResult Edit(int Id)
		{
			var inventory = db.Inventories.Single(a => a.AssetId == Id);

			return View(inventory);
		}

		//this is the edit post to edit the tool details
		[HttpPost]
		public ActionResult Edit(int Id, FormCollection collection)
		{
			try
			{
				var inventory = db.Inventories.Single(a => a.AssetId == Id);
				if (TryUpdateModel(inventory))
				{
					db.SaveChanges();

					return RedirectToAction("Index");
				}
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

				inventory.AssetId = (db.Inventories.Count<Inventory>() == 0) ? 1 : db.Inventories.Max(a => a.AssetId) + 1;
				inventory.Brand = collection["brand"];
				inventory.Description = collection["description"];
				inventory.Active = active;
				inventory.Comment = collection["comment"];
				db.Inventories.Add(inventory);

				db.SaveChanges();

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
			var inventory = db.Inventories.Single(m => m.AssetId == Id);

			return View(inventory);
		}
		#endregion

		#region Delete
		public ActionResult Delete(int Id)
		{
			var inventory = db.Inventories.Single(m => m.AssetId == Id);

			return View(inventory);
		}

		//this is the delete post to get the tool to be deleted
		[HttpPost]
		public ActionResult Delete(int Id, FormCollection collection)
		{
			try
			{
				var inventory = db.Inventories.Single(m => m.AssetId == Id);
				db.Inventories.Remove(inventory);
				db.SaveChanges();

				return RedirectToAction("index");
			}
			catch
			{
				return View();
			}
		}
		#endregion

		#region Route
		[Route("Inventory/DisplayPageSort/{pageIndex}/{sortBy}")]
		public ActionResult DisplayPageSort(int? pageIndex, string sortBy)
		{
			if (!pageIndex.HasValue) pageIndex = 1;
			if (string.IsNullOrWhiteSpace(sortBy)) sortBy = "Brand";

			return Content(string.Format("PageIndex={0} and SortBy={1}",
						   pageIndex, sortBy));
		}
		#endregion
	}
}