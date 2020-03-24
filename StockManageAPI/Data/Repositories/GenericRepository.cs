using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StockManageAPI.Data.Entities;

namespace StockManageAPI.Data
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class, IEntity
	{
		private readonly DataContext _context;

		public GenericRepository(DataContext context)
		{
			_context = context;
		}

        /// <summary>
        /// 
        /// </summary>
        /// <returns> Get all </returns>
		public IQueryable<T> GetAll()
		{
			return _context.Set<T>().AsNoTracking();
		}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Get by id</returns>
		public async Task<T> GetByIdAsync(int id)
		{
			return await _context.Set<T>()
				.AsNoTracking()
				.FirstOrDefaultAsync(e => e.Id == id);
		}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>Create new object</returns>
		public async Task CreateAsync(T entity)
		{
			await _context.Set<T>().AddAsync(entity);
			await SaveAllAsync();
		}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>Update object</returns>
		public async Task UpdateAsync(T entity)
		{
			_context.Set<T>().Update(entity);
			await SaveAllAsync();
		}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>Delete object</returns>
		public async Task DeleteAsync(T entity)
		{
			_context.Set<T>().Remove(entity);
			await SaveAllAsync();
		}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>true if exist</returns>
		public async Task<bool> ExistAsync(int id)
		{
			return await _context.Set<T>().AnyAsync(e => e.Id == id);

		}

        /// <summary>
        /// 
        /// </summary>
        /// <returns>Save changes to db</returns>
		public async Task<bool> SaveAllAsync()
		{
			return await _context.SaveChangesAsync() > 0;
		}
	}

}
