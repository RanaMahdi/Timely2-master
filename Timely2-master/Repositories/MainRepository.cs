using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Timely.Data;
using Timely.Interfaces;

namespace Timely.Repositories
{
    public class MainRepository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<T> _dbSet;

        public MainRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();

        }
        public IEnumerable<T> GetAll(params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _dbSet;
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
            return query.ToList();
        }

        public IEnumerable<T> GetAll()
        {
            //return _context.Set<T>().ToList();
            return _dbSet.ToList();

        }

        public T GetById(int id)
        {
            return _context.Set<T>().Find(id);

        }
        public T GetByUid(string Uid)
        {
            return _context.Set<T>().SingleOrDefault(e => EF.Property<string>(e, "Uid") == Uid);

        }
        public void Add(T entity)
        {
            _context.Set<T>().Add(entity);
            _context.SaveChanges();
        }
        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
            _context.SaveChanges();

        }
        public void Delete(int id)
        {
            var entity = _context.Set<T>().Find(id);
            if (entity != null)
            {
                _context.Set<T>().Remove(entity);
                _context.SaveChanges();
            }

        }

    }

}