using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using StockManageAPI.Data.Entities;

namespace StockManageAPI.Data
{
	public class GoodRepository : GenericRepository<Good>, IGoodRepository
	{
        private readonly DataContext _context;

        public GoodRepository(DataContext context) : base(context)
		{
            _context = context;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>Goods with goods in stock</returns>
        public IQueryable GetGoodsWithGoodsIn()
        {
           return _context.Goods.Include(g => g.GoodInStocks);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Good with goods in stock by id</returns>
        public Task<Good> GetGoodsWithGoodsInById(int id)
        {
            return _context.Goods.Include(g => g.GoodInStocks).Where(g => g.Id==id).FirstAsync();
        }
    }

}
