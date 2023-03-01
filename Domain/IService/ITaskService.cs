using Domain.Models;
using Domain.Models.ServiceRespone;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domain.IService
{
    public interface ITaskService:IService<Tasks, TasksRepone,Tasks>
    {
        public Task<ServiceRespone<IEnumerable<TasksRepone>>> GetAll(int UserId,Expression<Func<Tasks, bool>> predicate=null);
    }
}
