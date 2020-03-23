using System;
using Microsoft.EntityFrameworkCore;
using StockApp.Data.Entities;

namespace StockManageAPI.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
        : base(options)
        {
        }
        public DbSet<Good> Goods { get; set; }

    }
}
