using System;
using System.Linq;
using System.Threading.Tasks;
using StockManageAPI.Data.Entities;

namespace StockManageAPI.Data
{
    public class SeedDb 
    {
        private readonly DataContext context;
        private readonly Random _random;

        public SeedDb(DataContext context)
        {
            this.context = context;
            _random = new Random();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>Send data to db on stat application</returns>
         public async Task SeedAsync()
        {
            await this.context.Database.EnsureCreatedAsync();

            if (!this.context.Stocks.Any() && !this.context.Goods.Any())
            {
                await this.AddGoodsStocksAndGoodInStockAsync();
            }

            if (!this.context.OperationTypes.Any())
            {
                await this.AddOperationTypeAsync();
            }

        }


        private async Task AddGoodsStocksAndGoodInStockAsync()
        {
            this.AddGoodAndStock("Good 1","Stock 1", new int[] { _random.Next(100)});
            this.AddGoodAndStock("Good 2","Stock 2", new int[] { _random.Next(100)});
            this.AddGoodAndStock("Good 3","Stock 3", new int[] { _random.Next(100)});
            this.AddGoodAndStock("Good 4","Stock 4", new int[] { _random.Next(100)});
            this.AddGoodAndStock("Good 5","Stock 5", new int[] { _random.Next(100)});
            this.AddGoodAndStock("Good 6","Stock 6", new int[] { _random.Next(100)});
            this.AddGoodAndStock("Good 7","Stock 7", new int[] { _random.Next(100)});
            this.AddGoodAndStock("Good 8","Stock 8", new int[] { _random.Next(100)});
            await this.context.SaveChangesAsync();
        }

        /// <summary>
        /// Add good, stock and goodsInStock to db
        /// </summary>
        /// <param name="good"></param>
        /// <param name="stock"></param>
        /// <param name="goodInStocks"></param>
        private void AddGoodAndStock(string good, string stock, int[] goodInStocks)
        {
            var theGoodInStocks = goodInStocks.Select(i => new GoodInStock { Quantity = i, DateAdded = DateTime.Now }).ToList();
            this.context.Goods.Add(new Good
            {
                GoodInStocks = theGoodInStocks,
                Name = good,
                DateAdded = DateTime.Now
            });

            this.context.Stocks.Add(new Stock
            {
                GoodInStocks = theGoodInStocks,
                Name = stock,
                DateAdded = DateTime.Now
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>AddOperationType to db</returns>
        private async Task AddOperationTypeAsync()
        {
            this.context.OperationTypes.Add(new OperationType
            {
                Name = "Приход"
            });

            this.context.OperationTypes.Add(new OperationType
            {
                Name = "Расход"
            });

            this.context.OperationTypes.Add(new OperationType
            {
                Name = "Внутреннее перемещение"
            });

            await this.context.SaveChangesAsync();
        }
    }

}