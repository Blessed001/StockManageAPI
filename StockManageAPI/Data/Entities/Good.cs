using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace StockApp.Data.Entities
{
	public class Good : IEntity
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public string Article { get; set; }

		[DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = false)]
		public decimal Price { get; set; }

		[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = false)]
		public double Weight { get; set; }

		[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = false)]
		public double Volume { get; set; }

        public DateTime DateAdded { get; set; }

        //[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = false)]

        //public double Quantity { get { return Inventories == null ? 0 : Inventories.Sum(i => i.Quantity); } }

        //public virtual List<Inventory> Inventories { get; set; }

    }
}
