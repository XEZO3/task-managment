using Domain.Models.ServiceRespone;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domain.IService
{
    public interface IService<T,TResp,TDto>  where TResp : class 
    {
        Task<ServiceRespone<TResp>> Add(TDto entity);
        ServiceRespone<TResp> Delete(T entity);
        ServiceRespone<TResp> Update(TDto entity);

        Task<ServiceRespone<IEnumerable<TResp>>> GetAll(Expression<Func<T, bool>> predicate = null);
        Task<ServiceRespone<TResp>> DeleteById(int Id);
        Task<ServiceRespone<TResp>> FirstOrDefult(Expression<Func<T, bool>> predicate = null);

        Task<ServiceRespone<TResp>> GetById(int Id);
    }
}
