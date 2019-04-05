using DataDrivenSamples.Data.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace DataDrivenSamples.Data.SQL
{
    public class SqlDbContext : DbContext
    {
        public SqlDbContext(DbContextOptions<SqlDbContext> options) : base(options)
        {
            
        }

        public virtual DbSet<Item> Items { get; set; }
    }
}
