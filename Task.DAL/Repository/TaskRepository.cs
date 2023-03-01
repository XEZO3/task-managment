using CV.DAL.Data;
using Domain.IRepository;
using Domain.Models;
using EC.DAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task.DAL.Repository
{
    public class TaskRepository : Repository<Tasks>, ITaskRepository
    {
        private readonly dbContext _context;
        public TaskRepository(dbContext context) : base(context)
        {
            _context = context;
        }
    }
}
