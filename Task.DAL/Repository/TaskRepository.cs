using CV.DAL.Data;
using Domain.IRepository;
using Domain.Models;
using EC.DAL.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using task_managment.Common;

namespace Task.DAL.Repository
{
    public class TaskRepository : Repository<Tasks>, ITaskRepository
    {
        private readonly dbContext _context;
        private readonly DbSet<Tasks> _dbSet;
        public TaskRepository(dbContext context) : base(context)
        {
            _context = context;
            _dbSet = context.Set<Tasks>();
        }

        public bool CanUserDo(int TaskId)
        {
           var task = _dbSet.FirstOrDefault(x=>x.Id == TaskId && x.UsersId == UserId.Id);
            if (task == null)
            {
                return false;
            }
            else {
                return true;
            }
            
        }
    }
}
