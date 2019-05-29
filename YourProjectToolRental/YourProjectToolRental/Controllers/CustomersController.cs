using System.Collections.Generic;
using System.Net.Http;
using System.Web.Mvc;
using YourProjectToolRental.DAL;
using YourProjectToolRental.Models;

namespace YourProjectToolRental.Controllers
{
    public class CustomersController : Controller
    {
		private ToolContext db = new ToolContext();

		public static List<Customer> customerList = new List<Customer>
		{
			new Customer{CustomerId = 1, FName = "John", LName = "Smith", Phone_="3390 0675", Email="JohnSmith@email.com"},
			new Customer{CustomerId = 2, FName = "Mary", LName = "Parks", Phone_="3855 1515", Email="MaryParks@email.com"},
			new Customer{CustomerId = 3, FName = "Robert", LName = "Boyd", Phone_="3290 9090", Email="RobertBoyd@email.com"},

		};

		// GET: Customers
		public ActionResult Index()
		{
			HttpResponseMessage response = WebClient.ApiClient.GetAsync("Customer").Result;
			IEnumerable<Customer> customers = response.Content.ReadAsAsync<IEnumerable<Customer>>().Result;
			return View(customers);
		}

		// GET: Customers/Details/5
		public ActionResult Details(int Id)
		{
			HttpResponseMessage response = WebClient.ApiClient.GetAsync($"Customer/{Id}").Result;
			var customer = response.Content.ReadAsAsync<Customer>().Result;
			return View(customer);
		}

		// GET: Customers/Create
		public ActionResult Create()
		{
			return View();
		}

		// POST: Customers/Create
		[HttpPost]
		public ActionResult Create(Customer customer)
		{
			try
			{
				HttpResponseMessage response = WebClient.ApiClient.PostAsJsonAsync("Customer", customer).Result;
				return RedirectToAction("Index");
			}
			catch
			{
				return View();
			}
		}

		// GET: Customers/Edit/5
		public ActionResult Edit(int Id)
		{
			HttpResponseMessage response = WebClient.ApiClient.GetAsync($"Customer/{Id}").Result;
			var customer = response.Content.ReadAsAsync<Customer>().Result;
			return View(customer);
		}

		// POST: Customers/Edit/5
		[HttpPost]
		public ActionResult Edit(int Id, Customer customer)
		{
			try
			{
				HttpResponseMessage response = WebClient.ApiClient.PutAsJsonAsync($"Customer/{Id}", customer).Result;
				if (response.IsSuccessStatusCode)
					return RedirectToAction("Index");

				return View(customer);
			}
			catch
			{
				return View();
			}
		}

		// GET: Customers/Delete/5
		public ActionResult Delete(int Id)
		{
			HttpResponseMessage response = WebClient.ApiClient.GetAsync($"Customer/{Id}").Result;
			var customer = response.Content.ReadAsAsync<Customer>().Result;

			return View(customer);
		}

		// POST: Customers/Delete/5
		[HttpPost]
		public ActionResult Delete(int Id, Customer customer)
		{
			try
			{
				HttpResponseMessage response = WebClient.ApiClient.DeleteAsync($"Customer/{Id}").Result;

				return RedirectToAction("Index");
			}
			catch
			{
				return View();
			}
		}
	}
}