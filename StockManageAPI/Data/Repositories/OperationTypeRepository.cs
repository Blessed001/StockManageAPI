using StockManageAPI.Data.Entities;

namespace StockManageAPI.Data
{
    public class OperationTypeRepository:GenericRepository<OperationType>, IOperationTypeRepository
    {
        public OperationTypeRepository(DataContext context) : base(context)
        {

        }
    }
}
