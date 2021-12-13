using Interview.Data;
using Interview.Data.Entities.Abstract;
using InterviewService.Interfaces;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace InterviewService.Concrete
{
    public class EFRepository<T> : IRepository<T> where T : class, IBaseEntity
    {
        private readonly InterviewDbContext _context;
        public EFRepository(InterviewDbContext context)
        {
            _context = context;
        }

        public bool Add(T entity)
        {
            entity.IsActive = true;
            entity.CreatedDate = DateTime.Now;

            _context.Set<T>().Add(entity);

            return _context.SaveChanges() > 0;
        }

        public T Get(Expression<Func<T, bool>> expression = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null)
        {
            var query = _context.Set<T>().AsQueryable();

            if (expression != null)
            {
                query = query.Where(expression);
            }

            if (include != null)
            {
                query = include(query);
            }

            return query.FirstOrDefault();
        }

        public List<T> GetAll(Expression<Func<T, bool>> expression = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null)
        {
            var query = _context.Set<T>().AsQueryable();

            if (expression != null)
            {
                query = query.Where(expression);
            }

            if (include != null)
            {
                query = include(query);
            }

            return query.ToList();
        }
    }
}
