//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ToolRentalWebAPI.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class RentalItem
    {
        public int RentalItemId { get; set; }
        public int RentalId { get; set; }
        public int AssetId { get; set; }
    
        public virtual Inventory Inventory { get; set; }
        public virtual Rental Rental { get; set; }
    }
}
