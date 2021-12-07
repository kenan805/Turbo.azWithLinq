using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Turbo.azWithLinq.DataAccess.Abstract;
using Turbo.azWithLinq.DataAccess.Context;

namespace Turbo.azWithLinq.DataAccess.Concrete
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected DataClassesCarsDataContext _context;
        public GenericRepository()
        {
            _context = new DataClassesCarsDataContext();
        }
        public IQueryable<T> GetAll()
        {
            return from entity in _context.GetTable<T>()
                   select entity;
        }

        public IQueryable<T> Query(Expression<Func<T, bool>> predicate)
        {
            return _context.GetTable<T>().Where(predicate);
        }

        public void Insert(T entity)
        {
            _context.GetTable<T>().InsertOnSubmit(entity);
            SubmitChanges();
        }

        public void Update(T entity)
        {
            _context.GetTable<T>().Attach(entity);
            SubmitChanges();
        }

        public void Delete(T entity)
        {
            _context.GetTable<T>().DeleteOnSubmit(entity);
            SubmitChanges();
        }

        public void SubmitChanges()
        {
            _context.SubmitChanges();
        }
    }
}
