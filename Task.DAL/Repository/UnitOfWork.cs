using CV.DAL.Data;
using Domain.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EC.DAL.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly dbContext _context;
        public UnitOfWork(dbContext context)
        {
            _context = context;
        }
        public void Dispose()
        {
            _context.Dispose();
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
