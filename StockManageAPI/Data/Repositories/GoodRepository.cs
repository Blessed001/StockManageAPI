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

        public IQueryable GetGoodsWithInventories()
        {
           return _context.Goods.Include(g => g.GoodInStocks);
        }

        public Task<Good> GetGoodsWithInventoriesById(int id)
        {
            return _context.Goods.Include(g => g.GoodInStocks).Where(g => g.Id==id).FirstAsync();
        }
    }

}
