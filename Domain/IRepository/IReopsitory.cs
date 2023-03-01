using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domain.IRepository
{
    public interface IReopsitory<T> where T : class
    {
         Task<T> Add(T entity);
         void Delete(T entity);
         T Update(T entity);

         //Task<IEnumerable<T>> GetAll(Expression<Func<T,bool>> predicate = null);
        Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>> predicate = null, List<Expression<Func<T, object>>> children = null);
        Task<T> FirstOrDefult(Expression<Func<T, bool>> predicate = null, List<Expression<Func<T, object>>> children = null);

         Task<T> GetById(int Id, List<Expression<Func<T, object>>> children = null);


    }
}
