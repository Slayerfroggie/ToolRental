using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Mvc;
using YourProjectToolRental.DAL;
using YourProjectToolRental.Models;
using YourProjectToolRental.ViewModels;

namespace YourProjectToolRental.Controllers
{
    public class RentalsController : Controller
    {
		private ToolContext db = new ToolContext();

		// GET: Rentals
		public ActionResult Index()
		{
			HttpResponseMessage response = WebClient.ApiClient.GetAsync("Rentals").Result;
			IEnumerable<Rental> rentals = response.Content.ReadAsAsync<IEnumerable<Rental>>().Result;
			response = WebClient.ApiClient.GetAsync("Customer").Result;
			IList<Customer> customers = response.Content.ReadAsAsync<IList<Customer>>().Result;

			var customerRentalsViewModel = rentals.Select(
				r => new CustomerRentalsViewModel
				{
					RentalId = r.RentalId,
					CheckedOutDate = r.CheckedOutDate,
					Fname = customers.Where(c => c.CustomerId == r.CustomerId).Select(u => u.FName).FirstOrDefault(),
					Lname = customers.Where(c => c.CustomerId == r.CustomerId).Select(u => u.LName).FirstOrDefault(),
					CheckedInDate = r.CheckedInDate
				}).OrderByDescending(o => o.CheckedOutDate).ToList();

			return View(customerRentalsViewModel);
		}

		public ActionResult Edit(int Id)
		{
			try
			{
				HttpResponseMessage response = WebClient.ApiClient.GetAsync($"Rentals/{Id}").Result;
				var rental = response.Content.ReadAsAsync<Rental>().Result;
				response = WebClient.ApiClient.GetAsync($"RentalItemsById/{Id}").Result;
				IList<RentalItem> rentalItems = response.Content.ReadAsAsync<IList<RentalItem>>().Result;
				response = WebClient.ApiClient.GetAsync("Inventory").Result;
				IList<Inventory> dbInventories = response.Content.ReadAsAsync<IList<Inventory>>().Result;

				var customers = GetCustomers();
				var rentedTools = rentalItems.Select(
						m => new CustomerToolsViewModel
						{
							RentalItemId = m.RentalItemId,
							RentalId = m.RentalId,
							ToolType = dbInventories.Where(c => c.AssetId == m.AssetId).Select(f => f.ToolType).FirstOrDefault()
						}).ToList();

				rental.Customers = customers;
				rental.RentedTools = rentedTools;

				return View(rental);
			}
			catch
			{
				return RedirectToAction("Index");
			}
		}

		[HttpPost]
		public ActionResult Edit(int Id, Rental rental)
		{
			try
			{
				HttpResponseMessage response = WebClient.ApiClient.PutAsJsonAsync($"Rentals/{Id}", rental).Result;

				if (response.IsSuccessStatusCode)
					return RedirectToAction("Index");

				return View(rental);
			}
			catch
			{
				return View();
			}
		}

		public ActionResult Details(int Id)
		{
			HttpResponseMessage response = WebClient.ApiClient.GetAsync($"Rentals/{Id}").Result;
			var rental = response.Content.ReadAsAsync<Rental>().Result;
			response = WebClient.ApiClient.GetAsync("Customer").Result;
			IList<Customer> customers = response.Content.ReadAsAsync<IList<Customer>>().Result;
			response = WebClient.ApiClient.GetAsync("Inventory").Result;
			IList<Inventory> dbInventories = response.Content.ReadAsAsync<IList<Inventory>>().Result;

			var customerRentalDetails = new CustomerRentalDetailsViewModel
			{
				Rental = rental,
				FName = customers.Select(cu => cu.FName).FirstOrDefault(),
				LName = customers.Select(cu => cu.LName).FirstOrDefault(),
				RentedTools = rental.RentalItems.Select(
						ri => new CustomerToolsViewModel
						{
							RentalId = ri.RentalId,
							ToolType = dbInventories.Where(c2 => c2.AssetId == ri.AssetId).Select(m => m.ToolType).FirstOrDefault()
						}).ToList()
			};

			return View(customerRentalDetails);
		}

		public ActionResult Create()
		{
			var rental = new Rental();
			HttpResponseMessage response = WebClient.ApiClient.GetAsync("GetRentalMaxId").Result;
			// Setting the primary key value to a negative value will make SQL server to find the next available PKID when you save it.
			rental.RentalId = -999;
			rental.CheckedOutDate = DateTime.Now;
			rental.CheckedInDate = DateTime.Parse("01/01/2000");
			var customers = GetCustomers();
			rental.Customers = customers;
			rental.RentedTools = new List<CustomerToolsViewModel>();

			return View(rental);
		}

		[HttpPost]
		public ActionResult Create(Rental rental)
		{
			try
			{
				HttpResponseMessage response = WebClient.ApiClient.PostAsJsonAsync("Rentals", rental).Result;
				rental = response.Content.ReadAsAsync<Rental>().Result;
				response = WebClient.ApiClient.GetAsync($"RentalItemsById/{rental.RentalId}").Result;
				IList<RentalItem> rentalItems = response.Content.ReadAsAsync<IList<RentalItem>>().Result;

				if (rentalItems.Count == 0)
					return RedirectToAction("Edit", new { Id = rental.RentalId });
				else
					return RedirectToAction("Index");
			}
			catch
			{
				return View();
			}
		}

		public ActionResult Delete(int Id)
		{
			HttpResponseMessage response = WebClient.ApiClient.GetAsync($"Rentals/{Id}").Result;
			var rental = response.Content.ReadAsAsync<Rental>().Result;

			return View(rental);
		}

		[HttpPost]
		public ActionResult Delete(int Id, Rental rental)
		{
			try
			{
				HttpResponseMessage response = WebClient.ApiClient.DeleteAsync($"Rentals/{Id}").Result;

				return RedirectToAction("Index");
			}
			catch
			{
				return View();
			}
		}

		public ActionResult AddTools(int RentalId)
		{
			var rentalItem = new RentalItem();
			var tools = GetTools();
			rentalItem.RentalItemId = RentalId + 1;
			rentalItem.RentalId = RentalId;
			rentalItem.Tools = tools;

			return View(rentalItem);
		}

		[HttpPost]
		public ActionResult AddTools(RentalItem rentalItem)
		{
			int Id = 0;
			try
			{
				Id = rentalItem.RentalId;
				HttpResponseMessage response = WebClient.ApiClient.PostAsJsonAsync("RentalItems", rentalItem).Result;

				return RedirectToAction("Edit", new { Id });
			}
			catch (Exception)
			{
				return View("No record of the associated rental can be found.  Make sure to submit the rental details before adding tools.");
			}
		}

		public ActionResult EditRentedTool(int Id)
		{
			HttpResponseMessage response = WebClient.ApiClient.GetAsync($"RentalItems/{Id}").Result;
			var rentalItem = response.Content.ReadAsAsync<RentalItem>().Result;
			var tools = GetTools();
			rentalItem.Tools = tools;

			return View(rentalItem);
		}

		[HttpPost]
		public ActionResult EditRentedTool(int Id, RentalItem rentalItem)
		{
			try
			{
				HttpResponseMessage response = WebClient.ApiClient.PutAsJsonAsync($"RentalItems/{Id}", rentalItem).Result;
				Id = rentalItem.RentalId;
				if (response.IsSuccessStatusCode)
					return RedirectToAction("Edit", new { Id });

				return View(rentalItem);
			}
			catch
			{
				return View();
			}
		}

		public ActionResult DeleteRentedTool(int Id)
		{
			HttpResponseMessage response = WebClient.ApiClient.GetAsync($"RentalItem/{Id}").Result;
			var rentalItem = response.Content.ReadAsAsync<RentalItem>().Result;
			var tools = GetTools();
			rentalItem.Tools = tools;

			return View(rentalItem);
		}

		[HttpPost]
		public ActionResult DeleteRentedTool(int Id, FormCollection collection)
		{
			try
			{
				HttpResponseMessage response = WebClient.ApiClient.DeleteAsync($"RentalItem/{Id}").Result;
				var rentalItem = response.Content.ReadAsAsync<RentalItem>().Result;
				Id = rentalItem.RentalId;
				return RedirectToAction("Edit", new { Id });
			}
			catch
			{
				return View();
			}
		}

		public IEnumerable<SelectListItem> GetTools()
		{
			HttpResponseMessage response = WebClient.ApiClient.GetAsync("Inventory").Result;
			IList<Inventory> dbInventories = response.Content.ReadAsAsync<IList<Inventory>>().Result;

			List<SelectListItem> tools = dbInventories
											.OrderBy(o => o.ToolType)
											.Select(m => new SelectListItem
											{
												Value = m.AssetId.ToString(),
												Text = m.ToolType
											}).ToList();

			return new SelectList(tools, "Value", "Text");
		}

		public IEnumerable<SelectListItem> GetCustomers()
		{
			HttpResponseMessage response = WebClient.ApiClient.GetAsync("Customer").Result;
			IList<Customer> dbCustomers = response.Content.ReadAsAsync<IList<Customer>>().Result;
			List<SelectListItem> customers = dbCustomers
				.OrderBy(o => o.FName)
				.Select(c => new SelectListItem
				{
					Value = c.CustomerId.ToString(),
					Text = c.FName + " " + c.LName
				}).ToList();

			return new SelectList(customers, "Value", "Text");
		}
	}
}