﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YourProjectToolRental.ViewModels;

namespace YourProjectToolRental.Models
{
	public class Rental
	{
		public int RentalId { get; set; }
		public int CustomerId { get; set; }
		public DateTime CheckedInDate { get; set; }
		public DateTime CheckedOutDate { get; set; }
		public virtual ICollection<RentalItem> RentalItems { get; set; }
		public IEnumerable<SelectListItem> Customers { get; set; }
		public IEnumerable<CustomerToolsViewModel> RentedTools { get; set; }
	}
}