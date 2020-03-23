using System;
using System.Linq;
using System.Threading.Tasks;
using StockManageAPI.Data.Entities;

namespace StockManageAPI.Data
{
    public interface IGoodRepository:IGenericRepository<Good>
    {
        public IQueryable GetGoodsWithInventories();
        Task<Good> GetGoodsWithInventoriesById(int id);
    }
}
