using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YourProjectToolRental.Models
{
	public class Inventory
	{
		public int AssetId { get; set; }
		public string Brand { get; set; }
		public string Description { get; set; }
		public bool Active { get; set; }
		public string comment { get; set; }
	}
}