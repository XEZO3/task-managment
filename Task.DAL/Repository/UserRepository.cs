using CV.DAL.Data;
using Domain.IRepository;
using Domain.Models;
using Domain.Models.Dto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EC.DAL.Repository
{
    public class UserRepository : Repository<Users>, IUserRepository
    {
        private readonly dbContext _context;
        private readonly DbSet<Users> _dbSet;
        public UserRepository(dbContext context):base(context)
        {
            _context = context;
            _dbSet= context.Set<Users>();
        }

        public Users GetByEmail(string email)
        {
           
           var user = _dbSet.FirstOrDefault(x => x.Email == email);
            return user;
        }

        public Users GetByIdNotAsync(int Id)
        {
            var user = _dbSet.Find(Id);
            return user;
        }
    }
}
