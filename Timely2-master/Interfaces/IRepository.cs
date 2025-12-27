using Microsoft.Azure.Documents;
using System.Linq.Expressions;

namespace Timely.Interfaces
{
    public interface IRepository<T> where T : class
    {
         IEnumerable<T> GetAll();
        IEnumerable<T> GetAll(params Expression<Func<T, object>>[] includes);

        T GetById(int id);
        T GetByUid(string Uid);

        void Add(T entity);
        void Update(T entity);
        void Delete(int id);
    }
}
