using System;
using Microsoft.EntityFrameworkCore;
using StockManageAPI.Data.Entities;

namespace StockManageAPI.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
        : base(options)
        {
        }
        public DbSet<Good> Goods { get; set; }
        public DbSet<GoodInStock> GoodInStocks { get; set; }

    }
}
