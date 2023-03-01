using Domain.Models;
using Domain.Models.Dto;
using Domain.Models.ServiceRespone;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.IService
{
    public interface IUserService:IService<Users,UsersRespone, Users>
    {
        Task<ServiceRespone<LoginRespone>> Login(LoginDto login);
        Task<ServiceRespone<UsersRespone>> Register(RegisterDto register);
        //Task<ServiceRespone<UsersRespone>> DeleteById(int Id);
        string GeneratePassword(string password, byte[] salt);
    }
}
