using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.IRepository
{
    public interface ITaskRepository:IReopsitory<Tasks>
    {
        public bool CanUserDo(int TaskId);
    }
}
