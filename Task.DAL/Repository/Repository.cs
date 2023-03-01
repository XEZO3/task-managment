using CV.DAL.Data;
using Domain.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EC.DAL.Repository
{ 
    public class Repository<T> : IReopsitory<T> where T : class
    {
        private readonly dbContext _context;
        private readonly DbSet<T> dbSet;
        public Repository(dbContext context)
        {
            _context = context;
            dbSet = context.Set<T>();
            
        }

        public async Task<T> Add(T entity)
        {
            await dbSet.AddAsync(entity);
            return entity;
        }

        public void Delete(T entity)
        {
            dbSet.Remove(entity);
        }

        public async Task<T> FirstOrDefult(Expression<Func<T, bool>> predicate = null, List<Expression<Func<T, object>>> children = null)
        {
            if (predicate != null)
            {
                if (children == null)
                {
                    return await dbSet.FirstOrDefaultAsync(predicate);
                }
                else
                {

                    children.ToList().ForEach(x => dbSet.Include(x).Load());
                    return await dbSet.FirstOrDefaultAsync(predicate);
                }
            }
            else
            {
                if (children == null)
                {
                    return await dbSet.FirstOrDefaultAsync();
                }
                else
                {
                    children.ToList().ForEach(x => dbSet.Include(x).Load());
                    return await dbSet.FirstOrDefaultAsync();
                }
            }
        }


        public async Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>> predicate = null, List<Expression<Func<T, object>>> children = null)
        {
            if (predicate != null)
            {
                if (children == null)
                {
                    return await dbSet.Where(predicate).ToListAsync();
                }
                else
                {
                   
                        children.ToList().ForEach(x => dbSet.Include(x).Load());
                        return await dbSet.Where(predicate).ToListAsync();          
                }          
            }
            else {
                if (children == null)
                {
                    return await dbSet.ToListAsync();
                }
                else
                {
                    children.ToList().ForEach(x => dbSet.Include(x).Load());
                    return await dbSet.ToListAsync();
                }
            }
           
        }
        public async Task<T> GetById(int Id, List<Expression<Func<T, object>>> children = null)
        {
            if (children != null) {

                children.ToList().ForEach(x => dbSet.Include(x).Load());
            }
            return await dbSet.FindAsync(Id);
        }

        public T Update(T entity)
        {
            dbSet.Update(entity);
            return entity;
        }
    }
}
