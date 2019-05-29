using System;

namespace ToolRentalWebAPI.ViewModels
{
	public class CustomerRentalsViewModel
	{
		public int RentalId { get; set; }
		public DateTime CheckedOutDate { get; set; }
		public string Fname { get; set; }
		public string Lname { get; set; }
	}
}