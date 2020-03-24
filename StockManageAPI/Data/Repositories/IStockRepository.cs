using System.Linq;
using System.Threading.Tasks;
using StockManageAPI.Data.Entities;

namespace StockManageAPI.Data
{
    public interface IStockRepository:IGenericRepository<Stock>
    {
        public IQueryable GetStockssWithGoodsIn();
        Task<Stock> GetStocksWithGoodsInById(int id);
    }
}
