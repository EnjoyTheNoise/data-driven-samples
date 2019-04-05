using System.Threading.Tasks;
using DataDrivenSamples.Data.Shared.Models;
using DataDrivenSamples.Data.SQL.Repository;

namespace DataDrivenSamples.Data.SQL.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SqlDbContext _context;
        private Repository<Item> _itemRepository;
        public IRepository<Item> ItemRepository => _itemRepository ?? (_itemRepository = new Repository<Item>(_context));

        public UnitOfWork(SqlDbContext context)
        {
            _context = context;
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
