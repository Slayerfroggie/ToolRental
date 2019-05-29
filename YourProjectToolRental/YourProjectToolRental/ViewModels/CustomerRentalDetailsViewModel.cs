using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using YourProjectToolRental.Models;

namespace YourProjectToolRental.ViewModels
{
	public class CustomerRentalDetailsViewModel
	{
		[Key]
		public Rental Rental { get; set; }
		public string FName { get; set; }
		public string LName { get; set; }
		public List<CustomerToolsViewModel> RentedTools { get; set; }
	}
}