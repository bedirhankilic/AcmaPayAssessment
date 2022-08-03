using AcmePayAssessment.DataAccessLayer.Abstract;
using AcmePayAssessment.Entity.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace AcmePayAssessment.DataAccessLayer.Concretes
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly IUnitOfWork _unitOfWork;

        public Repository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public T Add(T entity)
        {
            return _unitOfWork.Context.Set<T>().Add(entity).Entity;
        }


        public IQueryable<T> Get()
        {
            return _unitOfWork.Context
                .Set<T>()
                .AsQueryable();
        }

        public IQueryable<T> Get(Expression<Func<T, bool>> predicate)
        {
            return _unitOfWork.Context
                    .Set<T>()
                    .Where(predicate)
                    .AsQueryable();
        }

        public void Update(T entity)
        {
            _unitOfWork.Context.Entry(entity).State = EntityState.Modified;
        }
    }
}
