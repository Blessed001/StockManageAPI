using System;
using System.Linq;
using System.Threading.Tasks;

namespace StockManageAPI.Data.Entities
{
    public interface IStockRepository:IGenericRepository<Stock>
    {
        public IQueryable GetStockssWithGoodsIn();
        Task<Stock> GetStocksWithGoodsInById(int id);
    }
}
