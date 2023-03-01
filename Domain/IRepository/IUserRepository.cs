using Domain.Models;
using Domain.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.IRepository
{
    public interface IUserRepository : IReopsitory<Users>
    {
        
        Users GetByEmail (string email);
        Users GetByIdNotAsync(int Id);
    }
}
