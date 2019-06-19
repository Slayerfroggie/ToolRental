using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace YourProjectToolRental.Controllers
{
    public class HelpController : Controller
    {
        // GET: Help
        public ActionResult Index()
        {
            return View();
        }

		public ActionResult Inventory()
		{
			return View();
		}

		public ActionResult Rentals()
		{
			return View();
		}

		public ActionResult Customers()
		{
			return View();
		}
	}
}