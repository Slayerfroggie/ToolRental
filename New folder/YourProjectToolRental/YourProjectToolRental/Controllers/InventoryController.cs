using System.Collections.Generic;
using System.Net.Http;
using System.Web.Mvc;
using YourProjectToolRental.DAL;
using YourProjectToolRental.Models;

namespace YourProjectToolRental.Controllers
{
    public class InventoryController : Controller
    {
		private ToolContext db = new ToolContext();

		public ActionResult Index()
		{
			HttpResponseMessage response = WebClient.ApiClient.GetAsync("Inventories").Result;

			IEnumerable<Inventory> inventories = response.Content.ReadAsAsync<IEnumerable<Inventory>>().Result;

			return View(inventories);
		}
	}
}