using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StockManageAPI.Data.Entities;

namespace StockManageAPI.Data
{
    public class StockRepository:GenericRepository<Stock>, IStockRepository
    {
        private readonly DataContext _context;

        public StockRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>Stocks with goods in stock</returns>
        public IQueryable GetStockssWithGoodsIn()
        {
            return _context.Stocks.Include(s => s.GoodInStocks);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Stock with goods in stock by id</returns>
        public Task<Stock> GetStocksWithGoodsInById(int id)
        {
            return _context.Stocks.Include(g => g.GoodInStocks).Where(s => s.Id == id).FirstAsync();
        }
    }
}
