using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DataDrivenSamples.Data.SQL.Repository
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> GetAll();

        IQueryable<T> Get(Expression<Func<T, bool>> predicate);

        T GetById(object id);

        void Add(T entity);

        Task AddAsync(T entity);

        void Delete(T entity);

        void Update(T entity);
    }
}
