using System;
using System.Collections.Generic;

namespace StockManageAPI.Data.Entities
{
     public class Stock:IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateAdded { get; set; }
        public virtual ICollection<GoodInStock> GoodInStocks { get; set; }
    }
}