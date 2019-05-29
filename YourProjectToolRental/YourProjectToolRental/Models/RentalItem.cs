using System.Collections.Generic;
using System.Web.Mvc;

namespace YourProjectToolRental.Models
{
	public class RentalItem
	{
		public int RentalItemId { get; set; }
		public int RentalId { get; set; }
		public int AssetId { get; set; }
		public IEnumerable<SelectListItem> Tools { get; set; }
	}
}