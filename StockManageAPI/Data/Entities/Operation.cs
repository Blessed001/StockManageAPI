using System;
namespace StockManageAPI.Data.Entities
{
    public class Operation : IEntity
    {
        public int Id { get; set; }
        public int OperationTypeId { get; set; }
        public DateTime DateAdded { get; set; }
    }
}