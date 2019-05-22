﻿using System.Collections.Generic;
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
			HttpResponseMessage response = WebClient.ApiClient.GetAsync("Inventory").Result;

			IEnumerable<Inventory> inventories = response.Content.ReadAsAsync<IEnumerable<Inventory>>().Result;

			return View(inventories);
		}

		public ActionResult Edit(int Id)
		{
			HttpResponseMessage response = WebClient.ApiClient.GetAsync($"Inventory/{Id}").Result;
			var inventory = response.Content.ReadAsAsync<Inventory>().Result;
			return View(inventory);
		}

		[HttpPost]
		public ActionResult Edit(int Id, Inventory inventory)
		{
			try
			{
				HttpResponseMessage response = WebClient.ApiClient.PutAsJsonAsync($"Inventory/{Id}", inventory).Result;
				//we will refer to this in the Index.cshtml of the Movie so alertify can display the message.
				TempData["SuccessMessage"] = "Saved successfully.";

				if (response.IsSuccessStatusCode)
					return RedirectToAction("Index");

				return View(inventory);
			}
			catch
			{
				return View();
			}
		}

		public ActionResult Details(int Id)
		{
			HttpResponseMessage response = WebClient.ApiClient.GetAsync($"Inventory/{Id}").Result;
			var inventory = response.Content.ReadAsAsync<Inventory>().Result;
			return View(inventory);
		}

		public ActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public ActionResult Create(Inventory inventory)
		{
			try
			{
				HttpResponseMessage response = WebClient.ApiClient.PostAsJsonAsync("Inventory", inventory).Result;
				//we will refer to this in the Index.cshtml of the Movie so alertify can display the message.
				TempData["SuccessMessage"] = "Tool added successfully.";

				return RedirectToAction("Index");
			}
			catch
			{
				return View();
			}
		}

		public ActionResult Delete(int Id)
		{
			HttpResponseMessage response = WebClient.ApiClient.GetAsync($"Inventory/{Id}").Result;
			var inventory = response.Content.ReadAsAsync<Inventory>().Result;
			return View(inventory);
		}

		[HttpPost]
		public ActionResult Delete(int Id, FormCollection collection)
		{
			try
			{
				HttpResponseMessage response = WebClient.ApiClient.DeleteAsync($"Inventory/{Id}").Result;
				//we will refer to this in the Index.cshtml of the Movie so alertify can display the message.
				TempData["SuccessMessage"] = "Tool deleted successfully.";
				return RedirectToAction("Index");
			}
			catch
			{
				return View();
			}
		}
	}
}