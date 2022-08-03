using System;
using System.Linq;
using System.Linq.Expressions;

namespace AcmePayAssessment.DataAccessLayer.Abstract
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> Get();
        IQueryable<T> Get(Expression<Func<T, bool>> predicate);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
