using System;
using System.ComponentModel.DataAnnotations;

namespace YourProjectToolRental.ViewModels
{
	public class CustomerRentalsViewModel
	{
		[Key]
		public int RentalId { get; set; }
		public DateTime CheckedOutDate { get; set; }
		public string Fname { get; set; }
		public string Lname { get; set; }
	}
}