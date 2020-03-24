using System.Linq;
using System.Threading.Tasks;
using StockManageAPI.Data.Entities;

namespace StockManageAPI.Data
{
    public interface IGoodRepository:IGenericRepository<Good>
    {
        public IQueryable GetGoodsWithGoodsIn();
        Task<Good> GetGoodsWithGoodsInById(int id);
    }
}
