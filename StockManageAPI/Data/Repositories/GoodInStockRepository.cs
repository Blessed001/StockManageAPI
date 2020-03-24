using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StockManageAPI.Data.Entities;

namespace StockManageAPI.Data
{
	public class GoodInStockRepository : GenericRepository<GoodInStock>, IGoodInStockRepository
	{
        private readonly IOperationTypeRepository _operationTypeRepository;
        private readonly DataContext _context;

        public GoodInStockRepository(DataContext context,
                                   IOperationTypeRepository operationTypeRepository
                                   ) : base(context)
		{
            _operationTypeRepository = operationTypeRepository;
            _context = context;
        }

        public async Task DoOperation(int idOperation, GoodInStock goodInStock)
        {
            var operation = await _operationTypeRepository.GetByIdAsync(idOperation);
            var duplicate = await _context.GoodInStocks.Where(i => i.GoodId == goodInStock.GoodId && i.StockId == goodInStock.StockId).FirstOrDefaultAsync();
            var duplicateTo = await _context.GoodInStocks.Where(i => i.GoodId == goodInStock.GoodId && i.StockId == goodInStock.StockIdTo).FirstOrDefaultAsync();

            if(operation.Name == "Приход")
            {
                if(duplicate == null)
                {
                    goodInStock.DateAdded = DateTime.Now;
                    await CreateAsync(goodInStock);
                    OperationLog(idOperation);
                }
                else
                {
                    duplicate.DateEdited = DateTime.Now;
                    duplicate.Quantity += goodInStock.Quantity;
                    await UpdateAsync(duplicate);
                    OperationLog(idOperation);

                }

            }
            else if (operation.Name == "Расход" && duplicate != null)
            {
                duplicate.DateEdited = DateTime.Now;
                duplicate.Quantity -= goodInStock.Quantity;
                await UpdateAsync(duplicate);
                OperationLog(idOperation);

            }
            else
            {
                if (duplicateTo == null)
                {
                    var goodInStockTo = new GoodInStock
                    {
                        Quantity = goodInStock.Quantity,
                        GoodId = goodInStock.GoodId,
                        StockId = goodInStock.StockIdTo,
                        DateAdded = DateTime.Now
                    };
                    await CreateAsync(goodInStockTo);
                    OperationLog(idOperation);

                }
                else
                {
                    duplicateTo.DateEdited = DateTime.Now;
                    duplicateTo.Quantity += goodInStock.Quantity;
                    await UpdateAsync(duplicateTo);
                    OperationLog(idOperation);
                }

                duplicate.Quantity -= goodInStock.Quantity;
                duplicate.DateEdited = DateTime.Now;
                await UpdateAsync(duplicate);

            }
        }

        public bool ValidateGood(GoodInStock goodInStock)
        {
            var duplicate = _context.GoodInStocks.Where(i => i.GoodId == goodInStock.GoodId && i.StockId == goodInStock.StockId).FirstOrDefault();

            if(duplicate == null)
            {
                return false;
            }
            else if (goodInStock.Quantity > duplicate.Quantity)
            {
                return true;
            }
            else if (goodInStock.Quantity <= 0)
            {
                return true;
            }
            else 
            {
                return false;
            }
        }

        private void OperationLog(int idOperation)
        {
            var operationLog = new Operation
            {
                OperationTypeId = idOperation,
                DateAdded = DateTime.Now
            };
            _context.Operations.Add(operationLog);
            _context.SaveChanges();
        }
    }

}
