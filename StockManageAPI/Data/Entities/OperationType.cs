using System;

namespace StockManageAPI.Data.Entities
{
    public class OperationType:IEntity
    {
        public int Id { get; set; }
         public string Name { get; set; }
    }
}