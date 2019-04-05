using System.Threading.Tasks;
using DataDrivenSamples.Data.Shared.Models;
using DataDrivenSamples.Data.SQL.Repository;

namespace DataDrivenSamples.Data.SQL.UnitOfWork
{
    public interface IUnitOfWork
    {
        IRepository<Item> ItemRepository { get; }

        void Save();

        Task SaveAsync();
    }
}
