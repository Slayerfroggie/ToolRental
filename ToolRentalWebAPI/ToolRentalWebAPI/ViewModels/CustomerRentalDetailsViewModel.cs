using System.Collections.Generic;
using ToolRentalWebAPI.Models;

namespace ToolRentalWebAPI.ViewModels
{
	public class CustomerRentalDetailsViewModel
	{
		public Rental Rental { get; set; }
		public string Fname { get; set; }
		public string Lname { get; set; }
		public List<CustomerToolViewModel> RentedTools { get; set; }
	}
}
