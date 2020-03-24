using System;
using System.Linq;
using System.Threading.Tasks;
using StockManageAPI.Data.Entities;

namespace StockManageAPI.Data
{
    public interface IGoodInStockRepository:IGenericRepository<GoodInStock>
    {
        Task DoOperation(int id,GoodInStock goodInStock);
        protected bool ValidateGood(GoodInStock goodInStock);
    }

}
