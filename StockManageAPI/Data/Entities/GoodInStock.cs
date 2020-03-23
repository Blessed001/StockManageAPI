using System;
namespace StockManageAPI.Data.Entities
{
    public class GoodInStock:IEntity
    {
        public int Id { get; set; }
        public double Quantity { get; set; }
        public int GoodId { get; set; }
        public int StockId { get; set; }
        public int StockIdTo { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime DateEddited { get; set; }


    }
}