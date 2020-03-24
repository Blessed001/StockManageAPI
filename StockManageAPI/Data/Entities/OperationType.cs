using System;
using System.Collections.Generic;

namespace StockManageAPI.Data.Entities
{
    public class OperationType:IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual List<Operation> Operations { get; set; }

    }
}